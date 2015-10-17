using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    class ShootMessage : ClientMessage
    {
        public override string GenerateStringMessage()
        {
            return "SHOOT#";
        }

        public override string ToString()
        {
            return "Shoot Message";
        }
    }
}
