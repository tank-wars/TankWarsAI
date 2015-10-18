using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class GlobalUpdate
    {
        public PlayerDetails[] PlayerUpdates { get; set; }

        public Brick[] brickUpdate { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach(PlayerDetails player in PlayerUpdates)
            {
                builder.AppendLine(" ");
                builder.AppendLine(player.ToString());
            }

            foreach(Brick brick in brickUpdate)
            {
                builder.AppendLine(brick.postition.ToString() + "\tDamage level: " + brick.damageLevel);
            }
            return builder.ToString();
        }
    }
}
