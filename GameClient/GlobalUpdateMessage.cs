using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    class GlobalUpdateMessage : ServerMessage
    {
        public GlobalUpdate globalUpdate { get; set; }

        public override void Execute()
        {
            GameWorld.Instance.BrickState = globalUpdate.brickUpdate;
            GameWorld.Instance.Players = globalUpdate.PlayerUpdates;
            GameWorld.Instance.AdvanceFrame();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" ");
            builder.AppendLine("Global update Message");
            builder.AppendLine(globalUpdate.ToString());
            builder.AppendLine("------------------------\n");
            return builder.ToString();
        }

        public class GlobalUpdateMessageParser : ServerMessage.ServerMessageParser
        {
            private static GlobalUpdateMessageParser instance = null;


            public static GlobalUpdateMessageParser Instance
            {
                get
                {
                    if (instance == null)
                        instance = new GlobalUpdateMessageParser();
                    return instance;
                }
            }

            public override ServerMessage TryParse(string[] sections)
            {
                if (sections[0].ToLower() == "g")
                {
                    /*
                    G:P1;< player location  x>,< player location  y>;<Direction>;< whether shot>;<health>;< coins>;< points>: …. :
                    P5;< player location  x>,< player location  y>;<Direction>;< whether shot>;<health>;< coins>;< points>:
                    < x>,<y>,<damage-level>;< x>,<y>,<damage-level>…..< x>,<y>,<damage-level># 
                    */

                    GlobalUpdate globalUpdate = new GlobalUpdate();
                    PlayerDetails[] playerUpdate = new PlayerDetails[sections.Length-2];
                    for(int i = 1; i < sections.Length - 1; i++)
                    {
                        string[] parameters = Tokenizer.TokernizeParameters(sections[i]);
                        PlayerDetails player = new PlayerDetails();
                        player.Name = parameters[0];
                        player.Position = Tokenizer.TokernizeCoordinates(parameters[1]);
                        player.Direction = (Direction)Convert.ToInt32(parameters[2]);
                        int shot = Convert.ToInt32(parameters[3]);
                        player.IsShooting = shot == 1;
                        player.Health = Convert.ToInt32(parameters[4]);
                        player.Coins = Convert.ToInt32(parameters[5]);
                        player.Points = Convert.ToInt32(parameters[6]);
                        playerUpdate[i-1] = player;
                    }

                    string[] paras = Tokenizer.TokernizeParameters(sections[sections.Length-1]);
                    Brick[] brickUpdate = new Brick[paras.Length];
                    for(int i = 0; i < paras.Length; i++)
                    {
                        Brick brick = new Brick();
                        int[] brickDamage= Tokenizer.TokernizeBrickDamage(paras[i]);
                        brick.Postition = new Coordinate(brickDamage[0], brickDamage[1]);
                        brick.DamageLevel = brickDamage[2];
                        brickUpdate[i] = brick;
                    }

                    globalUpdate.PlayerUpdates = playerUpdate;
                    globalUpdate.brickUpdate = brickUpdate;
                    GlobalUpdateMessage result = new GlobalUpdateMessage();
                    result.globalUpdate = globalUpdate;
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
