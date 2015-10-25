using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class Coin
    {
        public Coordinate Position { get; set; }

        public int TimeLimit { get; set; }

        public int Value { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Coin position: " + Position.ToString() + "\tTimeLimit: " + TimeLimit + "\tValue: " + Value);
            return builder.ToString();
        }

        private bool grabbed = false;
        private int elapsedTime = 0;
        /*
        How much more time the coin will be visible
        */
        public int RemainingTime
        {
            get
            {
                return TimeLimit - elapsedTime;
            }

        }

        /*
        is the coin still alive
        */
        public bool IsAlive
        {
            get
            {
                return RemainingTime > 0 & (!grabbed);
            }
        }

        /*
        Mark the coin as grabbed
        */
        public void Grab()
        {
            this.grabbed = true;
        }

        public void AdvanceFrame()
        {
            if (elapsedTime < TimeLimit)
                elapsedTime+=1000;

            if (IsAlive)
            {
                //check whether the lifepack is grabbed by any players
                foreach (PlayerDetails p in GameWorld.Instance.Players)
                {
                    if (p.Position.X == Position.X && p.Position.Y == Position.Y)
                    {
                        Grab();
                    }
                }
            }
        }

    }
}
