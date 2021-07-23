using Features.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientValidTests
    {
        private readonly ClientTestsFixture _clienteTestsFixture;

        public ClientValidTests(ClientTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }


        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GenerationClientValid();

            // Act
            var result = cliente.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);
        }
    }
}
