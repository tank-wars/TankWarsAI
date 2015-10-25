using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Foundation;

namespace GameClient.GameDomain
{
    public class Brick
    {
        public Coordinate Postition { get; set; }

        public int DamageLevel { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Brick position: " + Postition.ToString() + "\tDamage Level: " + DamageLevel);
            return builder.ToString();
        }

    }
}
