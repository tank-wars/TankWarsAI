using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
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
