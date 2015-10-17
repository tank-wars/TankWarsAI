using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class PlayerDetails
    {
        public Direction Direction { get; set; }

        public string Name { get; set; }

        public Coordinate Position { get; set; }

        public int isShot { get; set; }

        public int health { get; set; }

        public int coins { get; set; }

        public int points { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Player: " + Name);
            builder.AppendLine("-Position: " + Position.ToString());
            builder.AppendLine("-Direction: " + Direction.ToString());
            builder.AppendLine("-isShot: " + isShot);
            builder.AppendLine("-health: " + health);
            builder.AppendLine("-coins: " + coins);
            builder.AppendLine("-points: " + points);
            return builder.ToString();
        }
    }
}
