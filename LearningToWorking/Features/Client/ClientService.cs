using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Features.Client
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;

        public ClientService(IClientRepository clientRepository, IMediator mediator)
        {
            _clientRepository = clientRepository;
            _mediator = mediator;
        }

        public IEnumerable<Client> GetAllActives() {
            return _clientRepository.GetAll().Where(c => c.Active);
        }

        public void Add(Client client) {
            if (!client.IsValid())
                return;

            _clientRepository.Add(client);
            _mediator.Publish(new ClientMailNotify("admin@me.com", client.Mail, "Add Client", "Add New Client"));
        }

        public void Update(Client client)
        {
            if (!client.IsValid())
                return;

            _clientRepository.Update(client);
            _mediator.Publish(new ClientMailNotify("admin@me.com", client.Mail, "Update Client", "Update a Client"));
        }

        public void Inative(Client client) {
            if (!client.IsValid())
                return;

            client.Inative();
            _clientRepository.Update(client);
            _mediator.Publish(new ClientMailNotify("admin@me.com", client.Mail, "Inative Client", "Inative a Client"));
        }

        public void Delete(Client client) {
            _clientRepository.Delete(client.Id);
            _mediator.Publish(new ClientMailNotify("admin@me.com", client.Mail, "Delete Client", "Delete a Client"));
        }

        public void Dispose() {
            _clientRepository.Dispose();
        }

    }

}