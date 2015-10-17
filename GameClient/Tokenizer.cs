using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class Tokenizer
    {
        private Tokenizer()
        {

        }
        /*
            Sections are seperated by colon
        */
        public static String[] TokenizeSections(string message)
        {
            string trimmedMessage = message.Trim();
            string messageSections = trimmedMessage;
            if(MessageParser.ValidateMessageFooter(message))
            {
                messageSections = trimmedMessage.Substring(0, trimmedMessage.Length - 1);
            }
            string[] paras = messageSections.Split(new char[] { ":"[0] },StringSplitOptions.RemoveEmptyEntries);
            return paras;
        }
        /*
            Parameters are separated by semi colon. A section may have many parameters
        */
        public static String[] TokernizeParameters(String section)
        {
            string trimmedSection = section.Trim();
         
            string[] paras = trimmedSection.Split(new char[] { ";"[0] }, StringSplitOptions.RemoveEmptyEntries);
            return paras;
        }
        /*
            Coordinates are separated by ,. A parameter may have 1 or 2 coordinates
        */

/*        public static String[] TokernizeCoordinates(String parameter)
        {
            string trimmedParameter = parameter.Trim();

            string[] paras = trimmedParameter.Split(new char[] { ";"[0] }, StringSplitOptions.RemoveEmptyEntries);
            return paras;
        }*/
        public static Coordinate TokernizeCoordinates(String parameter)
        {
            string trimmedParameter = parameter.Trim();

            string[] paras = trimmedParameter.Split(new char[] { ","[0] }, StringSplitOptions.RemoveEmptyEntries);
            Coordinate c = new Coordinate(Convert.ToInt32(paras[0]), Convert.ToInt32(paras[1]));
            return c;
        }
    }
}
