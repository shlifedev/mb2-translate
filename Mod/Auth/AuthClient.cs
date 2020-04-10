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
        public static string TargetIP = "127.0.0.1";
        public static string key
        {
            get
            {
                if (!System.IO.File.Exists(MBKoreanFontSubModule.ModulePath + "licence.txt"))
                {
                    return "";
                }
                return System.IO.File.ReadAllText(MBKoreanFontSubModule.ModulePath + "licence.txt");
            }
        }

        public enum STATE
        {
            NONE, CONNETING, CONNECTED
        }
        public static STATE valid = STATE.NONE;

        public static string SendMsg(string msg)
        {
            try
            {
                TcpClient sockClient = new TcpClient(TargetIP, 9090); //소켓생성,커넥트
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
            catch (Exception e)
            {

            }
            return "InValid";
        }
        public static bool Connect()
        {
#if PRIVATE
            if(valid == STATE.CONNECTED) return true;
            if(valid == STATE.CONNETING) return false;
            valid = STATE.CONNETING;
            try
            {
                string v =  SendMsg(key);
                if (v == "Valid")
                {
                    valid =  STATE.CONNECTED;
                    return true;
                }
                else
                {
                    valid = STATE.NONE;
                    return false;
                }
            }
            catch (SocketException e)
            {
                valid = STATE.NONE;
                return false;
            }
        }
#else 
        return true;
#endif
    }

}

