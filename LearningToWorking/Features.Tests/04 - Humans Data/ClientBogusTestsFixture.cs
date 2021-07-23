using Bogus;
using Bogus.DataSets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Features.Tests.HumansDate
{
    [CollectionDefinition(nameof(ClientBogusCollection))]
    public class ClientBogusCollection : ICollectionFixture<ClientBogusTestsFixture>
    { }

    public class ClientBogusTestsFixture : IDisposable
    {
        public void Dispose()
        {
            
        }

        public Client.Client GenerationClientValidBogus() {
            return GenerationClientsBogus(1, true).FirstOrDefault();
        }

        public Client.Client GenerationClientInvalidBogus()
        {
            var Gender = new Faker().PickRandom<Name.Gender>();

            var client = new Faker<Client.Client>("pt_BR")
                .CustomInstantiator(f => new Client.Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(Gender),
                    f.Name.LastName(Gender),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    DateTime.Now,
                    "",
                    false
                 ));


            return client;
        }

        public IEnumerable<Client.Client> GetClientsSort() {
            var clients = new List<Client.Client>();

            clients.AddRange(GenerationClientsBogus(50, true).ToList());
            clients.AddRange(GenerationClientsBogus(50, false).ToList());

            return clients;
        }

        public IEnumerable<Client.Client> GenerationClientsBogus(int quantidade, bool ativo)
        {

            var Gender = new Faker().PickRandom<Name.Gender>();

            var clients = new Faker<Client.Client>("pt_BR")
                .CustomInstantiator(f => new Client.Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(Gender),
                    f.Name.LastName(Gender),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    DateTime.Now,
                    "",
                    ativo
                 ))
                .RuleFor(c => c.Mail, (f, c) => f.Internet.Email(c.Name.ToLower(), c.LastName.ToLower()));

            return clients.Generate(quantidade);
        }

    }
}
