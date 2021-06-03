using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain;
using System;
using System.Linq;
using Xunit;
namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar item command válido")]
        [Trait("Categoria", "Vendas - Pedido commands")]
        public void AdicionarItemPedidoCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.NewGuid(), Guid.NewGuid(), "Produto", 2, 100);

            // Act
            var result = pedidoCommand.EstaValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar item command inválido")]
        [Trait("Categoria", "Vendas - Pedido commands")]
        public void AdicionarItemPedidoCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.Empty, Guid.Empty, "", 0, 0);

            // Act
            var result = pedidoCommand.EstaValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar item unidades acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido commands")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesSuperiorAoPermitido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.NewGuid(), Guid.NewGuid(), "Produto", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act
            var result = pedidoCommand.EstaValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg, pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }
    }
}
