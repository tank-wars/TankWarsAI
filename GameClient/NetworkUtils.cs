using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    //Support tool to safely close connections
    class NetworkUtils
    {
        public static void CloseSafely(Socket socket)
        {
            if(socket != null)
            {
                try
                {                    
                    socket.Close();
                }
                catch(IOException ex)
                {

                }
                
            }
        }
        public static void CloseSafely(Stream stream)
        {
            if (stream != null)
            {
                try
                {
                    stream.Close();
                }
                catch (IOException ex)
                {

                }

            }
        }

        public static void CloseSafely(StreamReader r)
        {
            if (r != null)
            {
                try
                {
                    r.Close();
                }
                catch (IOException ex)
                {

                }

            }
        }
        public static void CloseSafely(StreamWriter w)
        {
            if (w != null)
            {
                try
                {
                    w.Close();
                }
                catch (IOException ex)
                {

                }

            }
        }
    }
}
