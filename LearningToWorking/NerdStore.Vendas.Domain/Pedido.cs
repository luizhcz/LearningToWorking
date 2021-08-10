using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, IAggregateRoot
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;


        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public Voucher Voucher { get; private set; }
        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;


        public ValidationResult AplicarVoucher(Voucher voucher) {
            var result =  voucher.ValidarSeAplicavel();
            if (!result.IsValid) return result;

            Voucher = voucher;
            VoucherUtilizado = true;

            CalcularValorTotalDesconto();

            return result;
        }

        public void CalcularValorTotalDesconto() {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            decimal valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Valor)
            {
                if (Voucher.ValorDesconto.HasValue) {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
                    
            }
            else {
                if (Voucher.PercentualDesconto.HasValue) {
                    desconto = (ValorTotal * Voucher.PercentualDesconto.Value) / 100;
                    valor -= desconto;
                } 
            }

            ValorTotal = valor < 0 ? 0: valor;
            Desconto = desconto;
        }

        private void CalcularValorPedido() {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public bool PedidoItemExistente(PedidoItem item) {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        private void ValidarPedidoItemInexistente(PedidoItem item) {
            if (!PedidoItemExistente(item)) throw new DomainException("O item não pertence ao pedido");
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem item) {
            var quantidadeItems = item.Quantidade;
            if (PedidoItemExistente(item)) {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                quantidadeItems += itemExistente.Quantidade;
            }

            if(quantidadeItems > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {MAX_UNIDADES_ITEM} unidades por produto.");
        } 

        public void AdicionarItem(PedidoItem item) {

            ValidarQuantidadeItemPermitida(item);

            if (PedidoItemExistente(item)) 
            {
                var itemExistente =_pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;
                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(item);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item) {
            
            ValidarPedidoItemInexistente(item);
            ValidarQuantidadeItemPermitida(item);

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item) {
            ValidarPedidoItemInexistente(item);

            _pedidoItems.Remove(item);

            CalcularValorPedido();
        }

        public void TornarRascunho() {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory {
            public static Pedido NovoPedidoRascunho(Guid clienteId) 
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId,
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }

    }

    public enum PedidoStatus 
    {
        Rascunho = 0,
        Iniciado = 1,
        Pago = 4,
        Entregue = 5,
        Cancelado = 6
    }

    public class PedidoItem {
        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Pedido.MIN_UNIDADES_ITEM) throw new DomainException($"Mínimo de {Pedido.MIN_UNIDADES_ITEM} unidades por produto.");

            ProdutoId     = produtoId;
            ProdutoNome   = produtoNome;
            Quantidade    = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid ProdutoId { get; private set; }

        public string ProdutoNome { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }


        internal void AdicionarUnidades(int unidades) {
            Quantidade += unidades;
        }

        internal decimal CalcularValor() {
            return Quantidade * ValorUnitario;
        }

    }

    
}
