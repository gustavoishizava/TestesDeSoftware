using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests._01___Traits
{
    public class ClienteTests
    {
        [Fact(DisplayName = "Novo cliente válido")]
        [Trait("Categoria", "Cliente Trait Tests")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Gustavo",
                "Cabral",
                DateTime.Now.AddYears(-23),
                "gu@adm.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo cliente inválido")]
        [Trait("Categoria", "Cliente Trait Tests")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "gu@adm.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
