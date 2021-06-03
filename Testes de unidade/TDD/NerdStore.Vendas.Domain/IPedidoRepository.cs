using NerdStore.Core.Data;
using System;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        void AdicionarItem(PedidoItem item);
        void AtualizarItem(PedidoItem item);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
    }
}
