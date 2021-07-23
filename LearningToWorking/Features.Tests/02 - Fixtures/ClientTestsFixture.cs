using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<ClientTestsFixture>
    { }

    public class ClientTestsFixture : IDisposable
    {

        public Client.Client GenerationClientInvalid() {

            var client = new Client.Client(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                DateTime.Now,
                "lu@lu.com",
                true

                );

            return client;
        }

        public Client.Client GenerationClientValid() {
            var client = new Client.Client(
                Guid.NewGuid(),
                "Luiz",
                "Ruiz",
                DateTime.Now.AddYears(-23),
                DateTime.Now,
                "lu@lu.com",
                true

                );

            return client;
        }


        public void Dispose()
        {
        }
    }
}
