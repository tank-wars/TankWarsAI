using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.GameDomain;

namespace GameClient.Network.Messages
{
    class GameFinishedMessage : ServerMessage
    {
        public override void Execute()
        {
            GameWorld.Instance.State = GameWorld.GameWorldState.Finished;
        }

        public override string ToString()
        {
            return "Game finished!!!";
        }

        public class GameFinishedMessageParser : ServerMessageParser
        {
            private GameFinishedMessageParser()
            {

            }
            private static GameFinishedMessageParser instance = null;
            public static GameFinishedMessageParser Instance
            {
                get
                {
                    if (instance == null)
                        instance = new GameFinishedMessageParser();
                    return instance;
                }
            }
            public override ServerMessage TryParse(string[] sections)
            {
                if(sections[0].Trim().ToUpper() == "GAME_FINISHED")
                {
                    return new GameFinishedMessage();
                }
                return null;
            }
        }
    }
}
