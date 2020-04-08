using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace MBModAuthServer
{
    class Program
    {

        public List<string> validKeys = new List<string>(){
         "f5d024111e0b61ed1173b28c3b9bc67c",
         "f5d024111e0b61ed1173b28c3b9bc67c"
        };
        
       
        static void Auth()
        {
          
            TcpListener listener = new TcpListener(IPAddress.Any, 9090);
            listener.Start();
            while (true)
            {
                TcpClient tc = listener.AcceptTcpClient();
                NetworkStream stream = tc.GetStream();
                StreamReader sr = new StreamReader(stream);
                StreamWriter sw = new StreamWriter(stream); ;
                var data = sr.ReadLine();
                var ipAdress = ((IPEndPoint)tc.Client.RemoteEndPoint).Address.ToString();
                if (data == "f5d024111e0b61ed1173b28c3b9bc67c")
                    sw.WriteLine("Valid");
                else
                {
                    Logger.Log("인증 실패", "invalid" + ipAdress);
                    sw.WriteLine("Invalid");
                }
                sw.Flush();
                stream.Close();
                tc.Close();
            }
        }
        static void Main(string[] args)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Auth));
            thread.Start();  
        }
    }
}
