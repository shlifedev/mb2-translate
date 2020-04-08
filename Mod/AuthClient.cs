using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace MBKoreanFont
{
    public class AuthClient
    {
        public static string key
        {
            get
            {
                if(!System.IO.File.Exists(MBKoreanFontSubModule.ModulePath + "licence.txt"))
                {
                    return "";
                }
               return System.IO.File.ReadAllText(MBKoreanFontSubModule.ModulePath+"licence.txt");
            }
        }
        public static bool valid = false;

        public static string SendMsg(string msg)
        {
            try
            {
                TcpClient sockClient = new TcpClient("121.140.154.113", 9090); //소켓생성,커넥트
                NetworkStream ns = sockClient.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(msg);
                sw.Flush();
                var read = sr.ReadLine(); 
                ns.Close();
                sockClient.Close();
                return read;
            }
            catch(Exception e)
            {
                 
            }
            return "InValid";
        }
        public static bool Connect()
        {  
            try
            { 
                string v =  SendMsg(key); 
                if (v == "Valid")
                {
                     return true;
                }
                else
                { 
                    return false;
                } 
            }
            catch (SocketException e)
            { 
                return false;
            }
        }
    }

}

