using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using System.Numerics;
using Fractions;

namespace OSK.Math.Polynomials
{
    public partial class Polynomial: IEnumerable<PolynomialTerm>
    {
        #region Static

        public static Polynomial NegativeOne = new Polynomial(PolynomialTerm.NegativeOne);
        public static Polynomial One = new Polynomial(PolynomialTerm.One);
        public static Polynomial Zero = new Polynomial(PolynomialTerm.Zero);

        #endregion

        #region Variables

        private bool _isPacked;
        private PolynomialTerm[] _polynomialTerms;
        private Dictionary<int, int> _polynomialIndexLookupByDegree;

        #endregion

        #region Constructors

        internal Polynomial(int size)
            : this(Enumerable.Range(0, size).Select(i => new PolynomialTerm(0, 1, i)))
        {
        }

        public Polynomial(params Fraction[] coefficients)
            : this(coefficients.Select((value, index) => new PolynomialTerm(value, index))
                .Where(term => term.Coeffecient != 0))
        {
        }

        public Polynomial(params PolynomialTerm[] coefficients)
            : this((IEnumerable<PolynomialTerm>)coefficients)
        {
        }

        public Polynomial(IEnumerable<PolynomialTerm> polynomialTerms)
        {
            _polynomialTerms = polynomialTerms?.ToArray() ?? throw new ArgumentNullException(nameof(polynomialTerms));
            _polynomialIndexLookupByDegree = new Dictionary<int, int>();
        }

        #endregion

        #region IEnumerable

        public IEnumerator<PolynomialTerm> GetEnumerator()
        {
            return _polynomialTerms.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _polynomialTerms.GetEnumerator();
        }

        #endregion

        #region Polynomial

        /// <summary>
        /// The total legnth of the underlying collection for this polynomial. This value can differ from <see cref="Polynomial.TotalCoeffecients"/>
        /// due to the nature of potential internal packing. For example, if the polynomial had coeffecients of 0 + 0x^1 + 0x^2 + 4x^3, and
        /// was packed, this value will return 1, if the polynomial was not packed, then it would return 4.
        /// </summary>
        public int Length => _polynomialTerms.Length;

        /// <summary>
        /// The total amount of coeffecients that this polynomial contains, without any packing considerations.
        /// For example, the following polynomial 0 + x + 0x^2 + 0x^3 + 5x^4 will return 5.
        /// </summary>
        public int TotalCoeffecients 
        {
            get => Degree + 1;
        }

        /// <summary>
        /// Represents the highest exponent on this polynomial
        /// </summary>
        public int Degree
        {
            get
            {
                if (_polynomialTerms.Length == 0)
                {
                    return 0;
                }

                return _isPacked
                    ? _polynomialTerms[_polynomialTerms.Length - 1].Exponent
                    : _polynomialTerms.Max(p => p.Exponent);
            }
        }

        /// <summary>
        /// Get or set the polynomial term at the index within the underlying collection. Note: this index must be packing aware! 
        /// </summary>
        /// <param name="index">The index to get or set a value to. Index should be aware of any packing.</param>
        /// <returns>The polynomial term at the index</returns>
        /// <exception cref="InvalidOperationException">Unable to set a value if the polynomial is packed. It is read-only.</exception>
        public PolynomialTerm this[int index]
        {
            get
            {
                return _polynomialTerms[index];
            }
            internal set
            {
                if (_isPacked)
                {
                    throw new InvalidOperationException("Unable to set the value of a polynomial that has been packed.");
                }

                _polynomialTerms[index] = value;
            }
        }

        public Polynomial Clone()
            => new Polynomial(_polynomialTerms.Select(term => term));

        public double Evaluate(double x)
        {
            return _polynomialTerms.Sum(term => term.Evaluate(x));
        }

        public Polynomial GetDerivative()
        {
            if (Length == 0)
            {
                return Zero;
            }

            Pack();
            var result = new Polynomial(Length - 1);
            for (var i = Length - 1; i >= 1; i--)
            {
                result[i - 1] = this[i].GetDerivative();
            }

            result.Pack();
            return result;
        }

        public PolynomialTerm GetPolynomialTermByDegree(int degree)
        {
            if (degree < 0)
            {
                throw new IndexOutOfRangeException("The polynomial degree must be greater than or equal to zero.");
            }
            if (degree > Degree)
            {
                throw new IndexOutOfRangeException($"The provided polynomial term degree, {degree}, is outside the degree of the polynomial.");
            }
            if (_polynomialIndexLookupByDegree.TryGetValue(degree, out var polynomialTermIndex))
            {
                return _polynomialTerms[polynomialTermIndex];
            }

            for (var i = 0; i < degree; i++)
            {
                if (_polynomialTerms[i].Exponent == degree)
                {
                    return _polynomialTerms[i];
                }
            }

            return new PolynomialTerm(0, 1, Degree);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Organizes the polynomial such that polynomial terms will flow in ascending order and are added based on their exponents.
        /// Additionally, the method will remove all zero based coeffecients from the polynomial, if directed to do so.
        /// </summary>
        /// <param name="removeZeroCoeffecients">Whether the zero based coeffecients should be removed. If set to true, this will make the polynomial read-only. Defaults to true.</param>
        internal void Pack(bool removeZeroCoeffecients = true)
        {
            if (_isPacked)
            {
                return;
            }

            var polynomialTerms = _polynomialTerms.GroupBy(term => term.Exponent)
                .Select(termGroup =>
                {
                    var polynomialSum = termGroup.Select(term => term.Coeffecient).Aggregate((frac, total) => frac.Add(total));
                    return new PolynomialTerm(polynomialSum, termGroup.Key);
                });
            if (removeZeroCoeffecients)
            {
                polynomialTerms = polynomialTerms.Where(term => term.Coeffecient != 0);
            }

            _polynomialIndexLookupByDegree = new Dictionary<int, int>();
            _polynomialTerms = polynomialTerms
                .OrderBy(term => term.Exponent)
                .Select((term, index) =>
                {
                    _polynomialIndexLookupByDegree[term.Exponent] = index;
                    return term;
                })
                .ToArray();

            _isPacked = removeZeroCoeffecients;
        }

        #endregion
    }
}
