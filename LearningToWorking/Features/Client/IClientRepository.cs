using Features.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Client
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetToMail(string mail);
    }
}