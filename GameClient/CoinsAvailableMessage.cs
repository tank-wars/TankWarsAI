using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Messages
{
    class CoinsAvailableMessage : ServerMessage
    {
        public Coin coin { get; set; }

        public override void Execute()
        {
            GameWorld.Instance.Coins.Add(coin);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" ");
            builder.AppendLine("Coin Available Message");
            builder.AppendLine(coin.ToString());
            builder.AppendLine(" ");
            return builder.ToString();
        }

        public class CoinAvailbleMessageParser : ServerMessage.ServerMessageParser
        {
            private static CoinAvailbleMessageParser instance = null;

            public static CoinAvailbleMessageParser Instance
            {
                get
                {
                    if (instance == null)
                        instance = new CoinAvailbleMessageParser();
                    return instance;
                }
            }

            public override ServerMessage TryParse(string[] sections)
            {
                if (sections[0].ToLower() == "c")
                {
                    //C:<x>,<y>:<LT>:<Val>#
                    Coin coin = new Coin();
                    coin.Position = Tokenizer.TokernizeCoordinates(sections[1]);
                    coin.TimeLimit = Convert.ToInt32(sections[2]);
                    coin.Value = Convert.ToInt32(sections[3]);

                    CoinsAvailableMessage result = new CoinsAvailableMessage();
                    result.coin = coin;
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
