using NerdStore.Vendas.Application.Commands;
using System;
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
    }
}
