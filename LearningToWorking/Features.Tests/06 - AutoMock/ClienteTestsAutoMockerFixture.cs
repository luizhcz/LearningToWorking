using Bogus;
using Bogus.DataSets;
using Features.Client;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteAutoMockerCollection))]
    public class ClienteAutoMockerCollection : ICollectionFixture<ClienteTestsAutoMockerFixture>
    {
    }

    public class ClienteTestsAutoMockerFixture : IDisposable
    {
        public ClientService ClienteService;
        public AutoMocker Mocker;

        public Client.Client GerarClienteValido()
        {
            return GerarClientes(1, true).FirstOrDefault();
        }

        public IEnumerable<Client.Client> ObterClientesVariados()
        {
            var clientes = new List<Client.Client>();

            clientes.AddRange(GerarClientes(50, true).ToList());
            clientes.AddRange(GerarClientes(50, false).ToList());

            return clientes;
        }

        public IEnumerable<Client.Client> GerarClientes(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            //var email = new Faker().Internet.Email("eduardo","pires","gmail");
            //var clientefaker = new Faker<Cliente>();
            //clientefaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var clientes = new Faker<Client.Client>("pt_BR")
                .CustomInstantiator(f => new Client.Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    DateTime.Now,
                    "",
                    ativo
                    ))
                .RuleFor(c => c.Mail, (f, c) =>
                      f.Internet.Email(c.Name.ToLower(), c.LastName.ToLower()));

            return clientes.Generate(quantidade);
        }

        public Client.Client GerarClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Client.Client>("pt_BR")
                .CustomInstantiator(f => new Client.Client(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    DateTime.Now,
                    "",
                    false));

            return cliente;
        }

        public ClientService ObterClienteService()
        {
            Mocker = new AutoMocker();
            ClienteService = Mocker.CreateInstance<ClientService>();

            return ClienteService;
        }

        public void Dispose()
        {
        }
    }
}
