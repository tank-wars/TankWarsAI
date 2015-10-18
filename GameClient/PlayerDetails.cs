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

        public bool IsShooting { get; set; }

        public int Health { get; set; }

        public int Coins { get; set; }

        public int Points { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Player: " + Name);
            builder.AppendLine("-Position: " + Position.ToString());
            builder.AppendLine("-Direction: " + Direction.ToString());
            builder.AppendLine("-isShot: " + IsShooting);
            builder.AppendLine("-health: " + Health);
            builder.AppendLine("-coins: " + Coins);
            builder.AppendLine("-points: " + Points);
            return builder.ToString();
        }
    }
}
