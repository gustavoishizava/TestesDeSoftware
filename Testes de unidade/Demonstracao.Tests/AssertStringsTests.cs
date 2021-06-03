using Xunit;

namespace Demonstracao.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var st = new StringTools();

            // Act
            var resultado = st.Unir("Gustavo", "Cabral");

            // Assert
            Assert.Equal("Gustavo Cabral", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var st = new StringTools();

            // Act
            var resultado = st.Unir("Gustavo", "Cabral");

            // Assert
            Assert.Equal("GUSTAVO CABRAL", resultado, true);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var st = new StringTools();

            // Act
            var resultado = st.Unir("Gustavo", "Cabral");

            // Assert
            Assert.Contains("avo", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var st = new StringTools();

            // Act
            var resultado = st.Unir("Gustavo", "Cabral");

            // Assert
            Assert.StartsWith("Gus", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var st = new StringTools();

            // Act
            var resultado = st.Unir("Gustavo", "Cabral");

            // Assert
            Assert.EndsWith("bral", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var st = new StringTools();

            // Act
            var resultado = st.Unir("Gustavo", "Cabral");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", resultado);
        }
    }
}
