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

    }
}
