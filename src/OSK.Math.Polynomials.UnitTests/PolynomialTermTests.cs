using Fractions;
using System.Numerics;
using Xunit;

namespace OSK.Math.Polynomials.UnitTests
{
    public class PolynomialTermTests
    {
        #region Multiplication

        [Fact]
        public void Multiplication_PolynomialTermWithPositiveExponents_ReturnsExpectedPolynomialTerm()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 2);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, 2);

            // Assert
            var result = polynomialTerm1 * polynomialTerm2;

            // Assert
            Assert.Equal(fraction1 * fraction2, result.Coeffecient);
            Assert.Equal(4, result.Exponent);
        }

        [Fact]
        public void Multiplication_PolynomialTermWithPositiveAndNegativeExponents_ReturnsExpectedPolynomialTerm()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 2);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, -1);

            // Assert
            var result = polynomialTerm1 * polynomialTerm2;

            // Assert
            Assert.Equal(fraction1 * fraction2, result.Coeffecient);
            Assert.Equal(1, result.Exponent);
        }

        [Fact]
        public void Multiplication_PolynomialTermMultipliedByCoeffecient_ReturnsPolynomialTermWithNewCoeffecient()
        {
            // Arrange
            var fraction = new Fraction(2, 1);
            var polynomialTerm = new PolynomialTerm(fraction, 2);

            // Act
            var result = polynomialTerm * fraction;
            var result2 = fraction * polynomialTerm;

            // Assert
            Assert.Equal(4, result.Coeffecient);
            Assert.Equal(2, result.Exponent);

            Assert.Equal(4, result2.Coeffecient);
            Assert.Equal(2, result2.Exponent);
        }

        #endregion

        #region Division

        [Fact]
        public void Division_PolynomialTermDividedByCoeffecient_ReturnsExpectedPolynomialTerm()
        {
            // Arrange
            var fraction = new Fraction(2, 1);
            var polynomialTerm = new PolynomialTerm(fraction, 3);


            // Assert
            var result = polynomialTerm / fraction;

            // Assert
            Assert.Equal(1, result.Coeffecient);
            Assert.Equal(3, result.Exponent);
        }

        [Fact]
        public void Division_PolynomialTermWithPositiveExponents_ReturnsExpectedPolynomialTerm()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 3);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, 2);

            // Assert
            var result = polynomialTerm1 / polynomialTerm2;

            // Assert
            Assert.Equal(fraction1 / fraction2, result.Coeffecient);
            Assert.Equal(1, result.Exponent);
        }

        [Fact]
        public void Division_PolynomialTermWithPositiveAndNegativeExponents_ReturnsExpectedPolynomialTerm()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 2);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, -1);

            // Assert
            var result = polynomialTerm1 / polynomialTerm2;

            // Assert
            Assert.Equal(fraction1 / fraction2, result.Coeffecient);
            Assert.Equal(3, result.Exponent);
        }

        #endregion

        #region Addition

        [Fact]
        public void Addition_PolynomialWithSameExponent_ReturnsSinglePolynomialWithExpectedValue()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 2);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, 2);

            // Assert
            var result = polynomialTerm1 + polynomialTerm2;

            // Assert
            Assert.Equal(1, result.Length);
            Assert.Equal(5, result[0].Coeffecient);
            Assert.Equal(2, result[0].Exponent);
        }

        [Fact]
        public void Addition_PolynomialWithDifferentExponent_ReturnsMultiPolynomialWithExpectedValue()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 1);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, 2);

            // Assert
            var result = polynomialTerm1 + polynomialTerm2;

            // Assert
            Assert.Equal(2, result.Length);

            var term = result.GetPolynomialTermByDegree(1);
            Assert.Equal(2, term.Coeffecient);
            Assert.Equal(1, term.Exponent);

            term = result.GetPolynomialTermByDegree(2);
            Assert.Equal(3, term.Coeffecient);
            Assert.Equal(2, term.Exponent);
        }

        #endregion

        #region Subtraction

        [Fact]
        public void Subtraction_PolynomialWithSameExponent_ReturnsSinglePolynomialWithExpectedValue()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 2);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, 2);

            // Assert
            var result = polynomialTerm1 - polynomialTerm2;

            // Assert
            Assert.Equal(1, result.Length);
            Assert.Equal(-1, result[0].Coeffecient);
            Assert.Equal(2, result[0].Exponent);
        }

        [Fact]
        public void Subtraction_PolynomialWithDifferentExponent_ReturnsMultiPolynomialWithExpectedValue()
        {
            // Arrange
            var fraction1 = new Fraction(2, 1);
            var polynomialTerm1 = new PolynomialTerm(fraction1, 1);

            var fraction2 = new Fraction(3, 1);
            var polynomialTerm2 = new PolynomialTerm(fraction2, 2);

            // Assert
            var result = polynomialTerm1 - polynomialTerm2;

            // Assert
            Assert.Equal(2, result.Length);

            var term = result.GetPolynomialTermByDegree(1);
            Assert.Equal(2, term.Coeffecient);
            Assert.Equal(1, term.Exponent);

            term = result.GetPolynomialTermByDegree(2);
            Assert.Equal(3, term.Coeffecient);
            Assert.Equal(2, term.Exponent);
        }

        #endregion

        #region Mod

        [Fact]
        public void Mod_PolynomialTermModValue_ReturnsPolynomialTermWithCoeffecientModed()
        {
            // Arrange
            var polynomialTerm = new PolynomialTerm(8, 1, 2);

            // Assert
            var result = polynomialTerm % 3;

            // Act
            Assert.Equal(2, result.Coeffecient);
            Assert.Equal(2, result.Exponent);
        }

        #endregion

        #region Equals

        [Fact]
        public void Equals_PolynomialTermsWithSameExponentAndCoeffecient_ExpectedToEqual()
        {
            // Arrange
            var polynomialTermA = new PolynomialTerm(2, 1, 1);
            var polynomialTermB = new PolynomialTerm(2, 1, 1);

            // Act
            var result = polynomialTermA == polynomialTermB;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_PolynomialTermsWithDifferentExponentAndCoeffecient_ExpectedToEqual()
        {
            // Arrange
            var controlTerm = new PolynomialTerm(2, 1, 1);

            var polynomialTermA = new PolynomialTerm(2, 0, 1);
            var polynomialTermB = new PolynomialTerm(2, 1, 0);
            var polynomialTermC = new PolynomialTerm(1, 1, 1);

            // Act
            var result = controlTerm == polynomialTermA;
            var result2 = controlTerm == polynomialTermB;
            var result3 = controlTerm == polynomialTermC;

            // Assert
            Assert.False(result);
            Assert.False(result2);
            Assert.False(result3);
        }

        #endregion

        #region DoesNotEqual

        [Fact]
        public void DoesNotEqual_PolynomialTermsWithSameExponentAndCoeffecient_ExpectedFalse()
        {
            // Arrange
            var polynomialTermA = new PolynomialTerm(2, 1, 1);
            var polynomialTermB = new PolynomialTerm(2, 1, 1);

            // Act
            var result = polynomialTermA != polynomialTermB;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DoesNotEqual_PolynomialTermsWithDifferentExponentAndCoeffecient_ExpectedTrue()
        {
            // Arrange
            var controlTerm = new PolynomialTerm(2, 1, 1);

            var polynomialTermA = new PolynomialTerm(2, 0, 1);
            var polynomialTermB = new PolynomialTerm(2, 1, 0);
            var polynomialTermC = new PolynomialTerm(1, 1, 1);

            // Act
            var result = controlTerm != polynomialTermA;
            var result2 = controlTerm != polynomialTermB;
            var result3 = controlTerm != polynomialTermC;

            // Assert
            Assert.True(result);
            Assert.True(result2);
            Assert.True(result3);
        }

        #endregion

        #region Evaluate

        [Theory]
        [InlineData(5, 1, 1, 1, 5)]
        [InlineData(10, 1, 0, 2, 10)]
        [InlineData(3, 1, 2, 2, 12)]
        public void Evaluate_VaryingCoeffecientsAndExponents_ReturnsExpectedValues(int numerator, int denominator, int exponent, int value, double expectedValue)
        {
            // Arrange
            var fraction = new Fraction(numerator, denominator);
            var polynomialTerm = new PolynomialTerm(fraction, exponent);

            // Act
            var result = polynomialTerm.Evaluate(value);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        #endregion

        #region GetDerivative

        [Theory]
        [InlineData(1, 1, 2, 2, 1)]
        [InlineData(1, 2, 4, 2, 3)]
        [InlineData(1, 1, 1, 1, 0)]
        [InlineData(5, 2, 0, 0, 0)]
        public void GetDerivative_ReturnsExpectedPolynomialTerm(int numerator, int denominator, int exponent, 
            double expectedCoeffecient, int expectedExponent)
        {
            // Arrange
            var fraction = new Fraction(numerator, denominator);
            var polynomialTerm = new PolynomialTerm(fraction, exponent);

            // Act
            var result = polynomialTerm.GetDerivative();

            // Assert
            Assert.Equal(expectedCoeffecient, result.Coeffecient.ToDouble());
            Assert.Equal(expectedExponent, result.Exponent);
        }

        #endregion

        #region ToString

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void ToString_PolynomialTermWithExponentOf0_ReturnsStringWithOnlyCoeffecient(int coeffecient)
        {
            // Arrange
            var polynomialTerm = new PolynomialTerm(coeffecient, 1, 0);

            // Act
            var str = polynomialTerm.ToString();

            // Assert
            Assert.Equal($"{coeffecient}", str);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void ToString_PolynomialTermWithExponentOf1_ReturnsStringWithCoeffecientAndVariableWithoutExponent(int coeffecient)
        {
            // Arrange
            var polynomialTerm = new PolynomialTerm(coeffecient, 1, 1);

            // Act
            var str = polynomialTerm.ToString();

            // Assert
            Assert.Equal($"{coeffecient}x", str);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(2, 3)]
        [InlineData(1, -1)]
        public void ToString_PolynomialTermWithExponent_ReturnsStringWithCoeffecientAndVariableWithExponent(int coeffecient, int exponent)
        {
            // Arrange
            var polynomialTerm = new PolynomialTerm(coeffecient, 1, exponent);

            // Act
            var str = polynomialTerm.ToString();

            // Assert
            Assert.Equal($"{coeffecient}x^{exponent}", str);
        }

        #endregion
    }
}
