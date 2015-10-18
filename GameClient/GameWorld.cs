using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class GameWorld
    {
        public bool isGameRunning { get; set; }
        
        public MapDetails map { get; set; }

        public PlayerDetails[] players { get; set; }

        public Brick[] brickState { get; set; }

        public ArrayList coins = new ArrayList();

        public ArrayList lifePack = new ArrayList();

        private static GameWorld instance = null;

        private GameWorld()
        {
            
        }

        public static GameWorld Instance
        {
            get
            {
                if(instance== null)
                    instance = new GameWorld();
                return instance;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("\nGame World Details ---------------------------------\n");
            if (map != null)
                builder.AppendLine("Map: " + map.ToString());
            if (players != null)
            {
                builder.AppendLine("Players: ");
                foreach (PlayerDetails player in players)
                    builder.AppendLine(player.ToString());
            }
            if (brickState != null && brickState.Length>0)
            {
                builder.AppendLine("Bricks:");
                foreach (Brick brick in brickState)
                    builder.Append(brick.ToString());
            }
            builder.AppendLine(" ");
            if (coins != null && coins.Count>0)
            {
                builder.AppendLine("Coins:");
                foreach (Coin coin in coins)
                    builder.Append(coin.ToString());
            }
            builder.AppendLine(" ");
            if (lifePack != null && lifePack.Count>0)
            {
                builder.AppendLine("Life pack:");
                foreach (LifePack lifePack in lifePack)
                    builder.Append(lifePack.ToString());
            }
            builder.AppendLine(" ");
            return builder.ToString();
        }

    }
}
