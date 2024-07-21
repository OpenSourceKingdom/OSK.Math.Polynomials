using Fractions;
using OSK.Math.Polynomials.Exceptions;
using OSK.Math.Polynomials.Models;
using System;
using System.Collections.Generic;

namespace OSK.Math.Polynomials
{
    public static class PolynomialMath
    {
        #region Addition

        public static Polynomial Add(Polynomial a, Fraction fraction)
        {
            a.Pack();

            var result = new Polynomial(a.TotalCoeffecients);
            result[0] = (result[0] + fraction)[0];

            for (int i = 0; i < a.Length; i++)
            {
                var aDegree = a[i].Exponent;
                result[aDegree] = (result[aDegree] + a[i])[0];
            }

            result.Pack();
            return result;
        }

        public static Polynomial Add(Polynomial a, PolynomialTerm polynomialTerm)
        {
            a.Pack();

            var result = new Polynomial(a.TotalCoeffecients);
            result[polynomialTerm.Exponent] = polynomialTerm;

            for (int i = 0; i < a.Length; i++)
            {
                var aDegree = a[i].Exponent;
                result[aDegree] = (result[aDegree] + a[i])[0];
            }

            result.Pack();
            return result;
        }

        public static Polynomial Add(Polynomial a, Polynomial b)
        {
            a.Pack();
            b.Pack();

            var maxSize = System.Math.Max(a.Degree, b.Degree) + 1;
            var result = new Polynomial(maxSize);
            for (var i = 0; i < a.Length; i++)
            {
                var degree = a[i].Exponent;
                result[degree] = a[i];
            }
            for (var i = 0; i < b.Length; i++)
            {
                var degree = b[i].Exponent;
                result[degree] = (result[degree] + b[i])[0];
            }

            result.Pack();
            return result;
        }

        #endregion

        #region Subtraction

        public static Polynomial Subtract(Polynomial a, Fraction fraction)
        {
            a.Pack();

            var result = new Polynomial(a.TotalCoeffecients);
            result[0] = (result[0] - fraction)[0];

            for (int i = 0; i < a.Length; i++)
            {
                var aDegree = a[i].Exponent;
                result[aDegree] = (result[aDegree] + a[i])[0];
            }

            result.Pack();
            return result;
        }

        public static Polynomial Subtract(Polynomial a, PolynomialTerm polynomialTerm)
        {
            a.Pack();

            var result = new Polynomial(a.TotalCoeffecients);
            result[polynomialTerm.Exponent] = (result[polynomialTerm.Exponent] - polynomialTerm)[0];

            for (int i = 0; i < a.Length; i++)
            {
                var aDegree = a[i].Exponent;
                result[aDegree] = (result[aDegree] + a[i])[0];
            }

            result.Pack();
            return result;
        }

        public static Polynomial Subtract(Polynomial a, Polynomial b)
        {
            a.Pack();
            b.Pack();

            var maxSize = System.Math.Max(a.Degree, b.Degree) + 1;
            var result = new Polynomial(maxSize);
            for (var i = 0; i < a.Length; i++)
            {
                var degree = a[i].Exponent;
                result[degree] = a[i];
            }
            for (var i = 0; i < b.Length; i++)
            {
                var degree = b[i].Exponent;
                result[degree] = (result[degree] - b[i])[0];
            }

            result.Pack();
            return result;
        }

        #endregion

        #region Multiplication

        public static Polynomial Multiply(Polynomial polynomial, Fraction fraction)
        {
            polynomial.Pack();

            var result = new Polynomial(polynomial.TotalCoeffecients);
            for (var i = 0; i < polynomial.Length; i++)
            {
                result[i] = fraction * polynomial[i];
            }

            result.Pack();
            return result;
        }

        public static Polynomial Multiply(Polynomial polynomial, PolynomialTerm polynomialTerm)
        {
            polynomial.Pack();

            var result = new Polynomial(polynomial.Degree + polynomialTerm.Exponent + 1);
            for (var i = 0; i < polynomial.Length; i++)
            {
                result[i + polynomialTerm.Exponent] = polynomialTerm * polynomial[i];
            }

            result.Pack();
            return result;
        }

        public static Polynomial Multiply(Polynomial a, Polynomial b)
        {
            a.Pack();
            b.Pack();

            var result = new Polynomial(a.Degree + b.Degree + 1);
            for (var i = 0; i < a.Length; i++)
            {
                for (var j = 0; j < b.Length; j++)
                {
                    var resultDegree = a[i].Exponent + b[j].Exponent;
                    result[resultDegree] = new PolynomialTerm(result[resultDegree].Coeffecient + a[i].Coeffecient * b[j].Coeffecient,
                        a[i].Exponent + b[j].Exponent);
                }
            }

            result.Pack();
            return result;
        }

        #endregion

        #region Division

        public static Polynomial Divide(Polynomial dividend, Fraction fraction)
        {
            dividend.Pack();
            var result = new Polynomial(dividend.TotalCoeffecients);
            for (var i = 0; i < dividend.Length; i++)
            {
                result[i] = dividend[i] / fraction;
            }

            result.Pack();
            return result;
        }

        public static Polynomial Divide(Polynomial dividend, PolynomialTerm divisor)
        {
            dividend.Pack();

            var result = new Polynomial(dividend.Degree - divisor.Exponent + 1);
            for (var i = dividend.Length - 1; i - divisor.Exponent >= 0; i--)
            {
                result[i - divisor.Exponent] = dividend[i] / divisor;
            }

            result.Pack();
            return result;
        }

        public static PolynomialDivsionResult Divide(Polynomial dividend, Polynomial divisor)
        {
            dividend.Pack();
            divisor.Pack();
            if (dividend.Degree < divisor.Degree)
            {
                return new PolynomialDivsionResult()
                {
                    Quotient = Polynomial.Zero,
                    Remainder = dividend
                };
            }

            var quotient = new Polynomial(dividend.Degree - divisor.Degree + 1);
            do
            {
                var quotientIndex = dividend.Degree - divisor.Degree;
                var quotientValue = dividend[dividend.Length - 1].Coeffecient / divisor[divisor.Length - 1].Coeffecient;
                quotient[quotientIndex] = new PolynomialTerm(quotientValue, quotientIndex);

                dividend = dividend - divisor * quotient[quotientIndex];
            }
            while (dividend.Degree >= divisor.Degree && dividend != Polynomial.Zero);

            quotient.Pack();
            return new PolynomialDivsionResult()
            {
                Quotient = quotient,
                Remainder = dividend
            };
        }

        #endregion

        #region Mod

        public static Polynomial Mod(Polynomial polynomial, int modulo)
        {
            if (modulo == 0)
            {
                throw new InvalidOperationException($"Modulo can not be 0.");
            }

            polynomial.Pack();
            var result = new Polynomial(polynomial.Length);
            for (var i = 0; i < polynomial.Length; i++)
            {
                result[i] = polynomial[i] % modulo;
            }

            result.Pack();
            return result;
        }

        public static Polynomial ModRemainder(Polynomial dividend, Polynomial divisor, int modulo)
        {
            if (modulo == 0)
            {
                throw new InvalidOperationException($"Modulo can not be 0.");
            }

            dividend.Pack();
            divisor.Pack();

            var divisionResult = Divide(dividend, divisor);
            return ModInverse(divisionResult.Remainder, modulo);
        }

        public static Polynomial ModInverse(Polynomial polynomial, int modulo)
        {
            if (modulo == 0)
            {
                throw new ModInverseException($"Modulo can not be 0.");
            }

            polynomial.Pack();

            var result = new Polynomial(polynomial.TotalCoeffecients);
            for (var i = 0; i < result.Length; i++)
            {
                var polynomialTerm = polynomial.GetPolynomialTermByDegree(i);
                result[i] = new PolynomialTerm(MathUtilities.ModInverse(polynomialTerm.Coeffecient, modulo), result[i].Exponent);
            }

            result.Pack();
            return result;
        }

        public static bool TryModInverse(Polynomial polynomial, int modulo, out Polynomial result)
        {
            if (modulo == 0)
            {
                throw new ModInverseException($"Modulo can not be 0.");
            }

            polynomial.Pack();

            result = new Polynomial(polynomial.TotalCoeffecients);
            for (var i = 0; i < result.Length; i++)
            {
                var polynomialTerm = polynomial.GetPolynomialTermByDegree(i);
                if (!MathUtilities.TryModInverse(polynomialTerm.Coeffecient, modulo, out var inverse))
                {
                    result = null;
                    return false;
                }
                result[i] = new PolynomialTerm(inverse, result[i].Exponent);
            }

            result.Pack();
            return true;
        }

        #endregion

        #region Special

        public static EuclideanResult<Polynomial> ExtendedEuclidean(Polynomial dividend, Polynomial divisor)
        {
            dividend.Pack();
            divisor.Pack();
            if (dividend.Degree < divisor.Degree)
            {
                throw new InvalidOperationException($"Unable to divide a polynomial with degree {dividend.Degree} by a polynomial with a higher degree {divisor.Degree}");
            }

            var quotientPolynomials = new List<Polynomial>();
            var remainderPolynomials = new List<Polynomial>();
            while (divisor != Polynomial.Zero)
            {
                var divisionResult = Divide(dividend, divisor);
                quotientPolynomials.Add(divisionResult.Quotient);
                remainderPolynomials.Add(divisionResult.Remainder);

                dividend = divisor;
                divisor = divisionResult.Remainder;
            }

            var uPolynomials = new Polynomial[remainderPolynomials.Count + 2];
            uPolynomials[0] = Polynomial.Zero;
            uPolynomials[1] = Polynomial.One;

            var vPolynomials = new Polynomial[quotientPolynomials.Count + 2];
            vPolynomials[0] = Polynomial.One;
            vPolynomials[1] = Polynomial.Zero;

            for (var i = 2; i < vPolynomials.Length; i++)
            {
                var product = quotientPolynomials[i - 2] * vPolynomials[i - 1];
                vPolynomials[i] = vPolynomials[i - 2] - product;

                product = quotientPolynomials[i - 2] * uPolynomials[i - 1];
                uPolynomials[i] = uPolynomials[i - 2] - product;
            }

            var gcd = remainderPolynomials[remainderPolynomials.Count - 2];
            var u = uPolynomials[uPolynomials.Length - 2];
            var v = vPolynomials[vPolynomials.Length - 2];

            gcd.Pack();
            u.Pack();
            v.Pack();

            // Make lead coeffecient one
            var scaleFactor = gcd[gcd.TotalCoeffecients - 1].Coeffecient;
            gcd /= scaleFactor;
            u /= scaleFactor;
            v /= scaleFactor;

            return new EuclideanResult<Polynomial>()
            {
                Gcd = divisor == Polynomial.Zero
                    ? gcd
                    : divisor,
                U = u,
                V = v
            };
        }

        #endregion
    }
}
