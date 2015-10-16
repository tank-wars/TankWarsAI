using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class PlayerDetails
    {
        public Direction Direction
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Coordinate Position
        {
            get; set;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Player: " + Name);
            builder.AppendLine("-Position: " + Position.ToString());
            builder.AppendLine("-Direction: " + Direction.ToString());
            return builder.ToString();
        }
    }
}
