using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    public class GameInitiationMessage : ServerMessage
    {
        public MapDetails mapDetails { get; set; }

        public override void Execute()
        {
            GameWorld.Instance.Map = mapDetails;
            GameWorld.Instance.State = GameWorld.GameWorldState.Running;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" ");
            builder.AppendLine("Game Initiation Message");
            builder.AppendLine(mapDetails.ToString());
            builder.AppendLine(" ");
            return builder.ToString();
        }

        public class GameInitiationMessageParser : ServerMessage.ServerMessageParser
        {
            private static GameInitiationMessageParser instance = null;


            public static GameInitiationMessageParser Instance
            {
                get
                {
                    if(instance == null)
                        instance = new GameInitiationMessageParser();
                    return instance;
                }
            }

            public override ServerMessage TryParse(string[] sections)
            {
                if (sections[0].ToLower() == "i")
                {
                    //I:P<num>: <Brick x>,<Brick y>;<Brick x>,<Stone x>.<Stone y>;<Stone x>,<Stone y>:<Water x>.<Water y>;<Water x>,<Water y>#
                    GameClient.MapDetails mapDetails = new GameClient.MapDetails();
                    
                    string section = sections[2];
                    string[] parameters = Tokenizer.TokernizeParameters(section);
                    Coordinate[] coordinates = new Coordinate[parameters.Length];
                    for (int j = 0; j < parameters.Length; j++ )
                    {
                        coordinates[j] = Tokenizer.TokernizeCoordinates(parameters[j]);
                    }
                    mapDetails.Brick = coordinates;

                    section = sections[3];
                    parameters = Tokenizer.TokernizeParameters(section);
                    coordinates = new Coordinate[parameters.Length];
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        coordinates[j] = Tokenizer.TokernizeCoordinates(parameters[j]);
                    }
                    mapDetails.Stone = coordinates;

                    section = sections[4];
                    parameters = Tokenizer.TokernizeParameters(section);
                    coordinates = new Coordinate[parameters.Length];
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        coordinates[j] = Tokenizer.TokernizeCoordinates(parameters[j]);
                    }
                    mapDetails.Water = coordinates;
                    
                    GameInitiationMessage result = new GameInitiationMessage();
                    result.mapDetails = mapDetails;
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
