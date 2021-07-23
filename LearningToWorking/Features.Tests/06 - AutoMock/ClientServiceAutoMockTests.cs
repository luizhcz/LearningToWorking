using Features.Client;
using Features.Tests.HumansDate;
using MediatR;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Features.Tests
{

    /// <summary>
    /// Pacote Moq.AutoMock
    /// </summary>
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceAutoMockTests
    {
        readonly ClientBogusTestsFixture _clientBogusTestsFixture;

        public ClientServiceAutoMockTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
        }

        [Fact(DisplayName = "Add Client With Success")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_add_ExecuteWithSuccess()
        {
            //Arrange
            var client = _clientBogusTestsFixture.GenerationClientValidBogus();
            var mocker = new AutoMocker();

            var clientService = mocker.CreateInstance<ClientService>();

            //Act
            clientService.Add(client);

            //Assert
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Once);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client with Fail")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_add_ExecuteWithClientInvalid()
        {
            //Arrange
            var client = _clientBogusTestsFixture.GenerationClientInvalidBogus();
            var mocker = new AutoMocker();

            var clientService = mocker.CreateInstance<ClientService>();

            //Act
            clientService.Add(client);

            //Assert
            mocker.GetMock<IClientRepository>().Verify(r => r.Add(client), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Get Clients Actives")]
        [Trait("Category", "Client Service AutoMock Tests")]
        public void ClientService_add_GetClientsActives()
        {
            //Arrange
            var mocker = new AutoMocker();
            var clientService = mocker.CreateInstance<ClientService>();

            mocker.GetMock<IClientRepository>().Setup(c => c.GetAll())
                .Returns(_clientBogusTestsFixture.GetClientsSort());

            //Act
            var clients = clientService.GetAllActives();

            //Assert
            mocker.GetMock<IClientRepository>().Verify(r => r.GetAll(), Times.Once);
            Assert.True(clients.Any());
            Assert.False(clients.Count(c => !c.Active) > 0);
        }
    }
}
