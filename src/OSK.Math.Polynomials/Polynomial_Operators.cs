using Fractions;
using System.Linq;
using System.Text;

namespace OSK.Math.Polynomials
{
    public partial class Polynomial
    {
        #region Object Overrides

        public override string ToString()
        {
            if (TotalCoeffecients == 0)
            {
                return "Empty";
            }

            // Avoid prematurely freezing the polynomial due to ToString on text tooltips that call this method.
            Pack(removeZeroCoeffecients: false);

            var stringBuilder = new StringBuilder();
            var first = true;
            foreach (var term in _polynomialTerms.Where(term => term.Coeffecient != 0))
            {
                var isPositive = term.Coeffecient > Fraction.Zero;
                if (!first)
                {
                    stringBuilder.Append(isPositive ? " + " : " - ");
                }

                var value = first
                    ? term.Coeffecient
                    : isPositive ? term.Coeffecient : -term.Coeffecient;
                if (value != Fraction.One || term.Exponent == 0)
                {
                    stringBuilder.Append(value);
                }
                if (term.Exponent != 0)
                {
                    stringBuilder.Append($"x");
                    if (term.Exponent > 1)
                    {
                        stringBuilder.Append("^");
                        stringBuilder.Append(term.Exponent);
                    }
                }

                first = false;
            }

            var str = stringBuilder.ToString();
            return str == string.Empty
                ? "0"
                : str;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                return true;
            }

            return obj is Polynomial polynomial
                ? polynomial == this
                : false;
        }

        #endregion

        #region Equivalence

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            a.Pack();
            b.Pack();

            if (a.Equals(b))
            {
                return false;
            }
            if (a.Length != b.Length || a.Degree != b.Degree)
            {
                return true;
            }

            for (var i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            a.Pack();
            b.Pack();

            if (a.Length != b.Length || a.Degree != b.Degree)
            {
                return false;
            }

            for (var i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Addition

        public static Polynomial operator +(Fraction fraction, Polynomial a)
            => PolynomialMath.Add(a, fraction);

        public static Polynomial operator +(Polynomial a, Fraction fraction)
            => PolynomialMath.Add(a, fraction);

        public static Polynomial operator +(PolynomialTerm polynomialTerm, Polynomial a)
            => PolynomialMath.Add(a, polynomialTerm);

        public static Polynomial operator +(Polynomial a, PolynomialTerm polynomialTerm)
            => PolynomialMath.Add(a, polynomialTerm);

        public static Polynomial operator +(Polynomial a, Polynomial b)
            => PolynomialMath.Add(a, b);

        #endregion

        #region Subtraction

        public static Polynomial operator -(Fraction fraction, Polynomial a)
            => PolynomialMath.Subtract(a, -1 * fraction);

        public static Polynomial operator -(Polynomial a, Fraction fraction)
            => PolynomialMath.Subtract(a, fraction);

        public static Polynomial operator -(PolynomialTerm polynomialTerm, Polynomial a)
            => PolynomialMath.Subtract(a, -1 * polynomialTerm);

        public static Polynomial operator -(Polynomial a, PolynomialTerm polynomialTerm)
            => PolynomialMath.Subtract(a, polynomialTerm);

        public static Polynomial operator -(Polynomial a, Polynomial b)
            => PolynomialMath.Subtract(a, b);

        #endregion

        #region Multiplication

        public static Polynomial operator *(Fraction fraction, Polynomial polynomial)
            => PolynomialMath.Multiply(polynomial, fraction);

        public static Polynomial operator *(Polynomial polynomial, Fraction fraction)
            => PolynomialMath.Multiply(polynomial, fraction);

        public static Polynomial operator *(PolynomialTerm polynomialTerm, Polynomial polynomial)
            => PolynomialMath.Multiply(polynomial, polynomialTerm);

        public static Polynomial operator *(Polynomial polynomial, PolynomialTerm polynomialTerm)
            => PolynomialMath.Multiply(polynomial, polynomialTerm);

        public static Polynomial operator *(Polynomial a, Polynomial b)
            => PolynomialMath.Multiply(a, b);

        #endregion

        #region Mod

        public static Polynomial operator %(Polynomial dividend, int modulo) 
            => PolynomialMath.Mod(dividend, modulo);

        public static Polynomial operator %(Polynomial dividend, Polynomial divisor)
            => PolynomialMath.Divide(dividend, divisor).Remainder;

        #endregion

        #region Division
        
        public static Polynomial operator /(Polynomial dividend, PolynomialTerm divisor)
            => PolynomialMath.Divide(dividend, divisor);

        public static Polynomial operator /(Polynomial polynomial, Fraction fraction)
            => PolynomialMath.Divide(polynomial, fraction);

        public static Polynomial operator /(Polynomial dividend, Polynomial divisor)
            => PolynomialMath.Divide(dividend, divisor).Quotient;

        #endregion
    }
}
