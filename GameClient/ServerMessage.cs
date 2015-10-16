using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    /*
        The parent type of the object returned by a specialization of ServerMessageParser
        A parsed message sent from the Server
    */
    abstract class ServerMessage
    {
        public abstract void Execute();
        public abstract override string ToString();

        /*
            Parser to be used to parse a message of parent classes type
        */
        public abstract class ServerMessageParser
        {
            /*
                Returned parsed object if success. Otherwise return null
            */
            public abstract ServerMessage TryParse(string[] Message);
        }

    }
}
