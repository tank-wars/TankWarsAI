using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameClient.GameDomain
{
    class GameWorld
    {
        
        public GameWorldState State { get { return state; } set { state = value; } }
        
        public MapDetails Map { get; set; }

        public PlayerDetails[] Players { get; set; }

        public Brick[] BrickState { get; set; }


        private List<Coin> coins = new List<Coin>();

        private List<LifePack> lifePacks = new List<LifePack>();

        private static GameWorld instance = null;

        private GameWorldState state = GameWorldState.NotStarted;

        /*
            Advance the gameworld to next frame
        */
        public void AdvanceFrame()
        {
            foreach(LifePack lifePack in lifePacks)
            {
                lifePack.AdvanceFrame();
            }
            foreach (Coin coin in coins)
            {
                coin.AdvanceFrame();
            }
        }

        public List<Coin> Coins
        {
            get
            {
                return coins;
            }
            set { coins = value; }
        }
        public List<LifePack> LifePacks
        {
            get { return lifePacks;  }
            set { lifePacks = value; }
        }

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
            builder.AppendLine("State: " + State.ToString());
            builder.AppendLine(" ");

            if (Map != null)
                builder.AppendLine("Map: " + Map.ToString());
            if (Players != null)
            {
                builder.AppendLine("Players: ");
                foreach (PlayerDetails player in Players)
                    builder.AppendLine(player.ToString());
            }
            if (BrickState != null && BrickState.Length>0)
            {
                builder.AppendLine("Bricks:");
                foreach (Brick brick in BrickState)
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
            if (lifePacks != null && lifePacks.Count>0)
            {
                builder.AppendLine("Life pack:");
                foreach (LifePack lifePack in lifePacks)
                    builder.Append(lifePack.ToString());
            }
            builder.AppendLine(" ");
 
            return builder.ToString();
        }

        public enum GameWorldState
        {
            NotStarted, Running, Finished
        }

    }
}
