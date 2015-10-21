using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class LifePack
    {
        public Coordinate Position { get; set; }

        public int TimeLimit { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("LifePack position: " + Position.ToString() + "\tTimeLimit: " + TimeLimit);
            return builder.ToString();
        }
    }
}
