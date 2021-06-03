using Xunit;

namespace Demonstracao.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(4, resultado);
        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(1, 1, 2)]
        [InlineData(3, 2, 5)]
        [InlineData(2, 5, 7)]
        [InlineData(10, 5, 15)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(v1, v2);

            // Assert
            Assert.Equal(total, resultado);
        }

        [Fact]
        public void Calculadora_Dividir_RetornarValorDivisao()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Dividir(4, 2);

            // Assert
            Assert.Equal(2, resultado);
        }
    }
}
