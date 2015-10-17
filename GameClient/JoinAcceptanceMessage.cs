using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    class JoinAcceptanceMessage : ServerMessage
    {

        private PlayerDetails[] playerDetails;
        public PlayerDetails[] PlayerDetails
        {
            get
            {
                return playerDetails;
            }
            set
            {
                playerDetails = value;
            }
        }
        
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Join Acceptance Message");
            foreach(PlayerDetails p in playerDetails)
            {
                builder.AppendLine(p.ToString());
            }
            return builder.ToString();
        }

        public class JoinAcceptanceMessageParser : ServerMessage.ServerMessageParser
        {
            private JoinAcceptanceMessageParser()
            {

            }
            private static JoinAcceptanceMessageParser instance = null;
            public static JoinAcceptanceMessageParser Instance
            {
                get
                {
                    if (instance == null)
                        instance = new JoinAcceptanceMessageParser();
                    return instance;
                }
            }
            public override ServerMessage TryParse(string[] sections)
            {
                if(sections[0].ToLower() == "s")
                {
                    //S:P0;0,0;0#
                    GameClient.PlayerDetails[] players = new GameClient.PlayerDetails[sections.Length - 1];
                    for(int i = 1; i < sections.Length; i++)
                    {
                        string section = sections[i];
                        string[] parameters = Tokenizer.TokernizeParameters(section);
                        GameClient.PlayerDetails player = new GameClient.PlayerDetails();
                        player.Name = parameters[0];
                        player.Position = Tokenizer.TokernizeCoordinates(parameters[1]);
                        int direction = Convert.ToInt32(parameters[2]);
                        player.Direction = (Direction)direction;
                        players[i - 1] = player;
                    }

                    JoinAcceptanceMessage result = new JoinAcceptanceMessage();
                    result.PlayerDetails = players;
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
