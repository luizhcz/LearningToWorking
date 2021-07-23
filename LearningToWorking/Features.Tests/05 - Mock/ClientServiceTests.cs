using Features.Client;
using Features.Tests.HumansDate;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClientBogusCollection))]
    public class ClientServiceTests
    {
        readonly ClientBogusTestsFixture _clientBogusTestsFixture;

        public ClientServiceTests(ClientBogusTestsFixture clientBogusTestsFixture)
        {
            _clientBogusTestsFixture = clientBogusTestsFixture;
        }

        [Fact(DisplayName = "Add Client With Success")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_add_ExecuteWithSuccess() {
            //Arrange
            var client = _clientBogusTestsFixture.GenerationClientValidBogus();
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            //Act
            clientService.Add(client);

            //Assert
            clientRepo.Verify(r => r.Add(client), Times.Once);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Add Client with Fail")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_add_ExecuteWithClientInvalid()
        {
            //Arrange
            var client = _clientBogusTestsFixture.GenerationClientInvalidBogus();
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            //Act
            clientService.Add(client);

            //Assert
            clientRepo.Verify(r => r.Add(client), Times.Never);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Get Clients Actives")]
        [Trait("Category", "Client Service Mock Tests")]
        public void ClientService_add_GetClientsActives()
        {
            //Arrange
            var clientRepo = new Mock<IClientRepository>();
            var mediator = new Mock<IMediator>();

            clientRepo.Setup(c => c.GetAll())
                .Returns(_clientBogusTestsFixture.GetClientsSort());

            var clientService = new ClientService(clientRepo.Object, mediator.Object);

            //Act
            var clients = clientService.GetAllActives();

            //Assert
            clientRepo.Verify(r => r.GetAll(), Times.Once);
            Assert.True(clients.Any());
            Assert.False(clients.Count( c => !c.Active) > 0);
        }

    }
}
