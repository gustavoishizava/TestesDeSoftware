using Features.Tests._02___Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Features.Tests._07___FluentAssertions
{
    [Collection(nameof(ClienteCollection))]
    public class ClientFluentAssertionsTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;
        private readonly ITestOutputHelper _outputHelper;

        public ClientFluentAssertionsTests(ClienteTestsFixture clienteTestsFixture, ITestOutputHelper testOutputHelper)
        {
            _clienteTestsFixture = clienteTestsFixture;
            _outputHelper = testOutputHelper;
        }

        [Fact(DisplayName = "Novo cliente válido")]
        [Trait("Categoria", "Cliente Fluent Assertion Tests")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo cliente inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Tests")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1, "Mensagem de erro");

            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
        }
    }
}
