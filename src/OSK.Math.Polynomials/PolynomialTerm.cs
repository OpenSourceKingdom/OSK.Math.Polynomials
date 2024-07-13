using Fractions;

namespace OSK.Math.Polynomials
{
    public partial struct PolynomialTerm
    {
        #region Static

        public static PolynomialTerm Zero = new PolynomialTerm(0);
        public static PolynomialTerm One = new PolynomialTerm(1);
        public static PolynomialTerm NegativeOne = new PolynomialTerm(-1);

        #endregion

        #region Variables

        public int Exponent { get; }

        public Fraction Coeffecient { get; }

        #endregion

        #region Constructors

        public PolynomialTerm(int value)
        {
            Exponent = 0;
            Coeffecient = new Fraction(value);
        }

        public PolynomialTerm(int numerator, int denominator)
        {
            Exponent = 0;
            Coeffecient = new Fraction(numerator, denominator);
        }

        public PolynomialTerm(int numerator, int denominator, int exponent)
        {
            Exponent = exponent;
            Coeffecient = new Fraction(numerator, denominator);
        }

        public PolynomialTerm(Fraction value)
        {
            Exponent = 0;
            Coeffecient = value;
        }

        public PolynomialTerm(Fraction value, int exponent)
        {
            Exponent = exponent;
            Coeffecient = value;
        }

        #endregion

        #region Helpers

        public double Evaluate(double x) 
            => System.Math.Pow(x, Exponent) * Coeffecient.ToDouble();

        public PolynomialTerm GetDerivative()
            => Exponent == 0
            ? Zero
            : new PolynomialTerm(Coeffecient * Exponent, Exponent - 1);

        #endregion
    }
}
