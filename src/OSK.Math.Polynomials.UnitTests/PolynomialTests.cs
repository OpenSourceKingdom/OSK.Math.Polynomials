using Xunit;

namespace OSK.Math.Polynomials.UnitTests
{
    public class PolynomialTests
    {
        #region Evaluate

        [Fact]
        public void Polynomial_Evaluate_ReturnsExpectedResult()
        {
            // Arrange
            var polynomial = new Polynomial(2, 4, 3, 2, 1);

            // Act
            var result = polynomial.Evaluate(2);

            // Assert

            // 2 + 4(2)^1 + 3(2)^2 + 2(2)^3 + (2)^4 = 54

            Assert.Equal(54, result);
        }

        #endregion

        #region GetDerivative

        [Fact]
        public void Polynomial_GetDerivative_ReturnsExpectedPolynomial()
        {
            // Arrange
            var polynomial = new Polynomial(2, 3, 4, 2); // 2 + 3x + 4x^2 + 2x^3

            // Act
            var result = polynomial.GetDerivative();

            // Assert

            // 3 + 8x + 6x^2

            Assert.Equal(3, result.Length);
            Assert.Equal(2, result.Degree);

            Assert.Equal(3, result[0].Coeffecient);
            Assert.Equal(0, result[0].Exponent);

            Assert.Equal(8, result[1].Coeffecient);
            Assert.Equal(1, result[1].Exponent);

            Assert.Equal(6, result[2].Coeffecient);
            Assert.Equal(2, result[2].Exponent);
        }

        #endregion
    }
}
