using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Pedido Vazio")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor() {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);


            //Act
            pedido.AdicionarItem(pedidoItem);

            //Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Unidades No Item")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadeDoItem()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var ProdutoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(ProdutoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);

            //Act
            var pedidoIte2 = new PedidoItem(ProdutoId, "Produto Teste", 1, 100);
            pedido.AdicionarItem(pedidoIte2);

            //Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == ProdutoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido acima do permitido")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_UnidadesItemAcimaDoPermitido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var ProdutoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(ProdutoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente acima do permitido")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistenteUnidadesItemAcimaDoPermitido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var ProdutoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(ProdutoId, "Produto Teste", 1, 100);
            var pedidoItem1 = new PedidoItem(ProdutoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);
            pedido.AdicionarItem(pedidoItem);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem1));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            pedido.AdicionarItem(pedidoItem);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            //Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            //Assert
            Assert.Equal(novaQuantidade, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Atualizar Item Validar Total")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferente_DeveAtualizarValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem1);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
            var totalPedido = pedidoItem1.Quantidade * pedidoItem.ValorUnitario +
                              pedidoItemAtualizado.Quantidade * pedidoItemAtualizado.ValorUnitario;

            //Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            //Assert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade acima do permitido")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("TDD", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemNaoExistenteNaLista_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("TDD", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemExistente_DeveAualizarValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xptio", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 3, 15);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var totalPedido = pedidoItem2.Quantidade * pedidoItem2.ValorUnitario;

            //Act
            pedido.RemoverItem(pedidoItem1);

            //Assert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplica voucher valido")]
        [Trait("TDD", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherValido_DeveRetornarSemErros() {

            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            //Act
            var result = pedido.AplicarVoucher(voucher);

            //Assert
            Assert.True(result.IsValid);

        }

        [Fact(DisplayName = "Aplica voucher tipo valor desconto")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoValorDesconto_DeveDescontarDoValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xptio", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 3, 15);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO-15-REAIS", null, 15, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            var valorComDesconto = pedido.ValorTotal - voucher.ValorDesconto;

            //Act
            pedido.AplicarVoucher(voucher);

            //Assert
            Assert.Equal(valorComDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplica voucher tipo percentual desconto")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoPercentualDesconto_DeveDescontarDoValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xptio", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 3, 15);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var voucher = new Voucher("PROMO-15-REAIS", 15, null, 1, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);

            var valorDesconto = (pedido.ValorTotal * voucher.PercentualDesconto) / 100;
            var valorTotalComDesconto = pedido.ValorTotal - valorDesconto;

            //Act
            pedido.AplicarVoucher(voucher);

            //Assert
            Assert.Equal(valorTotalComDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplica voucher desconto excede  valor total")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AplicarVoucher_DescontoExcedeValorTotalPedido_PedidoDeveTerValorZero(){

            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            pedido.AdicionarItem(pedidoItem1);

            var voucher = new Voucher("Promo-15-OFF", null, 300, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(10), true, false);

            //Act
            pedido.AplicarVoucher(voucher);

            //Assert
            Assert.Equal(0, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplica voucher recalcular desconto na modificação do pedido")]
        [Trait("TDD", "Vendas - Pedido")]
        public void AplicarVoucher_ModificiarItensPedido_DeveCalcularDescontoValorTotal() {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            pedido.AdicionarItem(pedidoItem1);

            var voucher = new Voucher("Promo-15-OFF", null, 300, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(10), true, false);
            pedido.AplicarVoucher(voucher);

            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste", 4, 25);

            //Act
            pedido.AdicionarItem(pedidoItem2);

            //Assert
            var totalEsperado = pedido.PedidoItems.Sum(i => i.Quantidade * i.ValorUnitario) - voucher.ValorDesconto;
            Assert.Equal(totalEsperado, pedido.ValorTotal);
        }
    }
}
