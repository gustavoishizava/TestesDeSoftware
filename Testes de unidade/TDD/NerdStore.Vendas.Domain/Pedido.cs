using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : EntityBase, IAggregateRoot
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;

        private readonly List<PedidoItem> _pedidoItens;
        protected Pedido()
        {
            _pedidoItens = new List<PedidoItem>();
        }
        public Guid ClienteId { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public Voucher Voucher { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public ReadOnlyCollection<PedidoItem> PedidoItens => _pedidoItens.AsReadOnly();

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var result = voucher.ValidarSeAplicavel();

            if (!result.IsValid) return result;

            Voucher = voucher;
            VoucherUtilizado = true;

            CalcularValorTotalDesconto();

            return result;
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            decimal valor = ValorTotal;

            if (Voucher.TipoDesconto == TipoDescontoVoucher.Valor && Voucher.ValorDesconto.HasValue)
            {
                desconto = Voucher.ValorDesconto.Value;
                valor -= desconto;
            }
            else if (Voucher.PercentualDesconto.HasValue)
            {
                desconto = (ValorTotal * Voucher.PercentualDesconto.Value) / 100;
                valor -= desconto;
            }

            Desconto = desconto;
            ValorTotal = valor < 0 ? 0 : valor;
        }

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItens.Sum(x => x.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItens.Any(x => x.ProdutoId == item.ProdutoId);
        }

        private void ValidarItemInexistente(PedidoItem item)
        {
            if (!PedidoItemExistente(item))
                throw new DomainException($"O item não existe no pedido");
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem item)
        {
            var quantidadeItens = item.Quantidade;
            if (PedidoItemExistente(item))
            {
                var existente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == item.ProdutoId);
                quantidadeItens += existente.Quantidade;
            }

            if (quantidadeItens > MAX_UNIDADES_ITEM)
                throw new DomainException($"Máximo de {MAX_UNIDADES_ITEM} unidades por produto.");
        }

        public void AdicionarItem(PedidoItem item)
        {
            ValidarQuantidadeItemPermitida(item);

            if (PedidoItemExistente(item))
            {
                var existente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == item.ProdutoId);
                existente.AdicionarUnidades(item.Quantidade);
                item = existente;

                _pedidoItens.Remove(existente);
            }

            _pedidoItens.Add(item);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            ValidarItemInexistente(item);
            ValidarQuantidadeItemPermitida(item);

            var existente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == item.ProdutoId);

            _pedidoItens.Remove(existente);
            _pedidoItens.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            ValidarItemInexistente(item);

            _pedidoItens.Remove(item);

            CalcularValorPedido();
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
