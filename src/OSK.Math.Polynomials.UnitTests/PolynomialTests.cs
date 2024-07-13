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

        #region ToString

        [Fact]
        public void ToString_NoCoeffecients_ReturnsZeroString()
        {
            // Arrange
            var polynomial = new Polynomial(Array.Empty<int>());

            // Act
            var str = polynomial.ToString();

            // Assert
            Assert.Equal("0", str);
        }

        [Fact]
        public void ToString_AllZeroCoeffecients_ReturnsZeroString()
        {
            // Arrange
            var polynomial = new Polynomial(0, 0, 0, 0); // 0 + 0x + 0x^2 + 0x^3

            // Act
            var str = polynomial.ToString();

            // Assert
            Assert.Equal("0", str);
        }

        [Fact]
        public void ToString_ManyZeroAndNonZeroCoeffecients_ReturnsNonZeroCoeffecientString()
        {
            // Arrange
            var polynomial = new Polynomial(1, 2, 0, 0, 0, -5, 0, 6); // 1 + 2x + 0x^2 + 0x^3 0x^4 - 5x^5 + 0x^6 + 6x^7

            // Act
            var str = polynomial.ToString();

            // Assert
            Assert.Equal("1 + 2x - 5x^5 + 6x^7", str);
        }

        #endregion
    }
}
