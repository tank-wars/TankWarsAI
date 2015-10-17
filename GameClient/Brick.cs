using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class Brick
    {
        public Coordinate postition { get; set; }

        public int damageLevel { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Brick position: " + postition.ToString() + "\tDamage Level: " + damageLevel);
            return builder.ToString();
        }

    }
}
