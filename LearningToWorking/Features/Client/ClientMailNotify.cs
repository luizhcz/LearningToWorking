using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Client
{
    public class ClientMailNotify : INotification
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Context { get; set; }
        public string Message { get; set; }

        public ClientMailNotify(string origin, string destination, string context, string message)
        {
            Origin = origin;
            Destination = destination;
            Context = context;
            Message = message;
        }
    }
}
