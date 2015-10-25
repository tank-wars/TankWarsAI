using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Foundation
{
    public struct Coordinate
    {
        private int x;
        private int y;
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        
        public Coordinate (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }

        public static string coordinateArrayToString(Coordinate[] para)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Coordinate coordinate in para)
            {
                builder.Append(coordinate.ToString() + ",");
            }
            return builder.ToString();
        }
    }
}
