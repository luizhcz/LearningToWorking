using Features.Tests.HumansDate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientBogusTests
    {
        private readonly ClientBogusTestsFixture _clientBogusTestsFixture;

        public ClientBogusTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
        }

        [Fact(DisplayName = "New Client Faker")]
        [Trait("Category", "Client Fixture Fake Tests")]
        public void Client_NewFakerClient_IsValid()
        {
            // Arrange
            var cliente = _clientBogusTestsFixture.GenerationClientValidBogus();

            // Act
            var result = cliente.IsValid();

            // Assert 
            Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);
        }
    }
}
