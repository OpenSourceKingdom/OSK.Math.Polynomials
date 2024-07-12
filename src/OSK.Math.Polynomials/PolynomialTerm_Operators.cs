using Fractions;

namespace OSK.Math.Polynomials
{
    public partial struct PolynomialTerm
    {
        #region Object Overrides

        public override int GetHashCode()
        {
            return Exponent.GetHashCode() + Coeffecient.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return Exponent switch
            {
                0 => $"{Coeffecient}",
                1 => $"{Coeffecient}x",
                _ => $"{Coeffecient}x^{Exponent}"
            };
        }

        #endregion

        #region Subtraction

        public static Polynomial operator -(Fraction fraction, PolynomialTerm a)
            => new PolynomialTerm(fraction) - a;

        public static Polynomial operator -(PolynomialTerm a, Fraction fraction)
            => a - new PolynomialTerm(fraction);

        public static Polynomial operator -(PolynomialTerm a, PolynomialTerm b)
        {
            return a.Exponent == b.Exponent
                ? new Polynomial(new PolynomialTerm(a.Coeffecient.Subtract(b.Coeffecient), a.Exponent))
                : new Polynomial(a, b);
        }

        #endregion

        #region Addition

        public static Polynomial operator +(PolynomialTerm a, Fraction fraction)
            => a + new PolynomialTerm(fraction);

        public static Polynomial operator +(Fraction fraction, PolynomialTerm a)
            => a + new PolynomialTerm(fraction);

        public static Polynomial operator +(PolynomialTerm a, PolynomialTerm b)
        {
            return a.Exponent == b.Exponent
                ? new Polynomial(new PolynomialTerm(a.Coeffecient + b.Coeffecient, a.Exponent))
                : new Polynomial(a, b);
        }

        #endregion

        #region Multiplication

        public static PolynomialTerm operator *(PolynomialTerm a, PolynomialTerm b)
        {
            return new PolynomialTerm(a.Coeffecient * b.Coeffecient, a.Exponent + b.Exponent);
        }

        public static PolynomialTerm operator *(Fraction fraction, PolynomialTerm a)
            => a * fraction;

        public static PolynomialTerm operator *(PolynomialTerm a, Fraction fraction)
        {
            return new PolynomialTerm(a.Coeffecient * fraction, a.Exponent);
        }

        #endregion

        #region Division
        
        public static PolynomialTerm operator /(PolynomialTerm a, Fraction divisor)
        {
            return new PolynomialTerm(a.Coeffecient / divisor, a.Exponent);
        }

        public static PolynomialTerm operator /(PolynomialTerm a, PolynomialTerm b)
        {
            return new PolynomialTerm(a.Coeffecient / b.Coeffecient, a.Exponent - b.Exponent);
        }

        #endregion

        #region Mod

        public static PolynomialTerm operator %(PolynomialTerm a, int modulo)
        {
            return new PolynomialTerm(a.Coeffecient % modulo, a.Exponent);
        }

        #endregion

        #region Equivalence

        public static bool operator ==(PolynomialTerm a, PolynomialTerm b)
        {
            return a.Exponent == b.Exponent && a.Coeffecient == b.Coeffecient;
        }

        public static bool operator !=(PolynomialTerm a, PolynomialTerm b)
        {
            return a.Exponent != b.Exponent || a.Coeffecient != b.Coeffecient;
        }

        #endregion
    }
}
