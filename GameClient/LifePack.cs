using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class LifePack
    {
        public Coordinate position { get; set; }

        public int TimeLimit { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("LifePack position: " + position.ToString() + "\tTimeLimit: " + TimeLimit);
            return builder.ToString();
        }
    }
}
