using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Client
{
    public interface IClientService : IDisposable
    {
        IEnumerable<Client> GetAllActive();

        void Add(Client client );

        void Update(Client client);

        void Inative(Client client);

        void Delete(Client client);
    }
}