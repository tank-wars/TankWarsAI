using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Foundation;

namespace GameClient.GameDomain
{
    public class MapDetails
    {
        public Coordinate[] Brick { get; set; }

        public Coordinate[] Stone { get; set; }

        public Coordinate[] Water { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Bricks: " +Coordinate.coordinateArrayToString(Brick));
            builder.AppendLine("Stones " + Coordinate.coordinateArrayToString(Stone));
            builder.AppendLine("Water: " + Coordinate.coordinateArrayToString(Water));
            return builder.ToString();
        }
                
    }
    
}
