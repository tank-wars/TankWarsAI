using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameClient.GameDomain
{
    /*
    The class combining all the components of the Game. Consists of Map, Players. Handles dynamics related to time-elapse.
    */
    class GameWorld
    {
        /*
        Has the GameWorld been started?
        */
        public GameWorldState State { get { return state; } set { state = value; } }
        
        /*
        Contains locations of non-movable objects of map
        */
        public MapDetails Map { get; set; }

        /*
        The players in the GameWorlds
        */
        public PlayerDetails[] Players { get; set; }
        /*
        The updated states of bricks. 
        */
        public Brick[] BrickState { get; set; }

        /*
        The coins that are added to the world
        */
        private List<Coin> coins = new List<Coin>();

        /*
        The life packs that are added to the world
        */
        private List<LifePack> lifePacks = new List<LifePack>();

        //Singleton Instance
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

        /*
        The coins that are added to the world
        */
        public List<Coin> Coins
        {
            get
            {
                return coins;
            }
            set { coins = value; }
        }

        /*
        The life packs that are added to the world
        */
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

        /*
         A textual description of entire GameWorld
        */
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
