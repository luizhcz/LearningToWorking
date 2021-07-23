using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientInvalidTests
    {
        private readonly ClientTestsFixture _clienteTestsFixture;

        public ClientInvalidTests(ClientTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GenerationClientInvalid();

            // Act
            var result = cliente.IsValid();

            // Assert 
            Assert.False(result);
            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }
    }
}
