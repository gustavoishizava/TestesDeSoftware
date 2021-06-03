using MediatR;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _repository;
        private readonly IMediator _mediator;

        public PedidoCommandHandler(IPedidoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) return false;

            var pedido = await _repository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            var pedidoItem = new PedidoItem(request.ProdutoId, request.Nome, request.Quantidade, request.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(request.ClienteId);
                pedido.AdicionarItem(pedidoItem);

                _repository.Adicionar(pedido);
            }
            else
            {
                var itemExistente = pedido.PedidoItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (itemExistente)
                {
                    _repository.AtualizarItem(pedido.PedidoItens.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId));
                }
                else
                {
                    _repository.AdicionarItem(pedidoItem);
                }


                _repository.Atualizar(pedido);
            }

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(
                request.ClienteId,
                request.ProdutoId,
                pedido.Id,
                request.Nome,
                request.Quantidade,
                request.ValorUnitario));

            return await _repository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command request)
        {
            if (request.EstaValido()) return true;

            foreach (var error in request.ValidationResult.Errors)
            {
                _mediator.Publish(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
