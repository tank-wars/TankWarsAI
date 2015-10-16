using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    /*
    A message to be sent from client to server
    */
    abstract class ClientMessage : AbstractMessage
    {
        public abstract override string ToString();
        public abstract String GenerateStringMessage();
    }
}
