using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    public class ClientTests
    {
        [Fact]
        [Trait("Category", "Client Trait Tests")]
        public void Client_newClient_IsValid() {

            //Arrange
            var client = new Client.Client(
                Guid.NewGuid(),
                "Luiz",
                "Ruiz",
                DateTime.Now.AddYears(-23),
                DateTime.Now,
                "lu@lu.com",
                true
               
                );

            //Act
            var result = client.IsValid();

            //Assert
            Assert.True(result);
            Assert.Empty(client.ValidationResult.Errors);
        }

        [Fact]
        [Trait("Category", "Client Trait Tests")]
        public void Client_newClient_NotIsValid()
        {

            //Arrange
            var client = new Client.Client(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                DateTime.Now,
                "lu@lu.com",
                true

                );

            //Act
            var result = client.IsValid();

            //Assert
            Assert.False(result);
            Assert.NotEmpty(client.ValidationResult.Errors);
        }
    }
}
