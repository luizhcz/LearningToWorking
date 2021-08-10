using NerdStore.Vendas.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Valido")]
        [Trait("TDD", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstadoValido_DevePassarNaValidacao() {

            //Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);

            //Act
            var result = pedidoCommand.EhValido();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Item Command Invalido")]
        [Trait("TDD", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstadoValido_NaoDevePassarNaValidacao()
        {

            //Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            //Act
            var result = pedidoCommand.EhValido();

            //Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
