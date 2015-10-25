using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Network.Messages
{
    class JoinRequestMessage : ClientMessage
    {
        public override string GenerateStringMessage()
        {
            return "JOIN#";
        }

        public override string ToString()
        {
            return "Join Request Message";
        }
    }
}
