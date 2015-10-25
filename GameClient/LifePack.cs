using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Foundation;

namespace GameClient.GameDomain
{
    public class LifePack
    {
        public Coordinate Position { get; set; }

        private bool grabbed = false;
        private int elapsedTime = 0;
        public int RemainingTime {
            get
            {
                return TimeLimit - elapsedTime;
            }

        }

        /*
        is the medi pack still alive
        */
        public bool IsAlive
        {
            get
            {
                return RemainingTime > 0 & (!grabbed);
            }
        }

        /*
        Mark the lifepack as grabbed
        */
        public void Grab()
        {
            this.grabbed = true;
        }


        public int TimeLimit { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("LifePack position: " + Position.ToString() + "\tTimeLimit: " + TimeLimit);
            return builder.ToString();
        }

        public void AdvanceFrame()
        {
            if (elapsedTime < TimeLimit)
                elapsedTime+=1000;

            if(IsAlive)
            {
                //check whether the lifepack is grabbed by any players
                foreach(PlayerDetails p in GameWorld.Instance.Players)
                {
                    if (p.Position.X==Position.X && p.Position.Y == Position.Y)
                    {
                        Grab();
                    }
                }
            }
        }
    }
}
