using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public interface IPedidoRepository :IRepository<Pedido>
    {
        void Adicionar(Pedido pedido);

        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clientId);

        void Atualizar(Pedido pedido);

        void AdicionarItem(PedidoItem pedidoItem);

        void AtualizarItem(PedidoItem pedidoItem);
    }
}