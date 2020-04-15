using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.PlatformService;
using TaleWorlds.PlatformService.Steam;

namespace MBKoreanFont
{
    public class AuthClient
    {
#if PRIVATE
        public static string TargetIP = "121.140.154.113";
#else
        public static string TargetIP = "none";
#endif
        public static string key
        {
            get
            {
#if PRIVATE
                if (!System.IO.File.Exists(MBKoreanFontSubModule.ModulePath + "licence.txt"))
                {
                    return "";
                }
                return System.IO.File.ReadAllText(MBKoreanFontSubModule.ModulePath + "licence.txt");
#endif
                return "none :D";
            }
        }
        public enum STATE
        {
            NONE, CONNETING, CONNECTED
        }
        public static STATE valid = STATE.NONE;
        public static string SendMsg(string msg)
        {
#if PRIVATE
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
#endif

            return "non-use";
        }
        public static bool Connect()
        {
#if PRIVATE
            if (valid == STATE.CONNECTED) return true;
            if (valid == STATE.CONNETING) return false;
            valid = STATE.CONNETING;
            try
            {
                string v =  SendMsg(key);
                if (v == "Valid")
                {
                    valid = STATE.CONNECTED;
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

#else
            return true;
#endif
        }
        public enum Auth
        {
            None,
            Good,
            FailedByFoundPage,
            FailedByOwnerShip,
            FailedByKnownCrackID,
            Unknown
        }
        public static Action<Auth> onAuthEvent;
        public static Auth currentState;
        public static Auth IsSteamAuth()
        {
            if (currentState == Auth.Good) return currentState;
            try
            {
                PlatformServices.Initialize();
                var providerName = PlatformServices.ProviderName;
                if (providerName == "Epic")
                {
                    onAuthEvent?.Invoke(Auth.Good);
                    currentState = Auth.Good;
                    return Auth.Good;
                }
                if (providerName == "Steam")
                {
                    var userid = PlatformServices.UserId;
                    //first web req
                    WebRequest request = WebRequest.Create($"https://steamcommunity.com/profiles/{userid}");

                    WebResponse response = request.GetResponse();
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.  
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        if (responseFromServer.Contains("The specified profile could not be found."))
                        {
                            onAuthEvent?.Invoke(Auth.FailedByFoundPage);
                            return Auth.FailedByFoundPage;
                        }
                    }
                    if (userid == "76561201195729065") //짱깨 id
                    {
                        onAuthEvent?.Invoke(Auth.FailedByKnownCrackID);
                        return Auth.FailedByKnownCrackID;
                    }
                    else if (userid == "0")
                    {
                        onAuthEvent?.Invoke(Auth.FailedByOwnerShip);
                        return Auth.FailedByOwnerShip;
                    }
                }
                currentState = Auth.Good;
                onAuthEvent?.Invoke(Auth.Good);

                return Auth.Good;
            }
            catch (Exception e)
            {
                onAuthEvent?.Invoke(Auth.Unknown);
                return Auth.Unknown;
            }
        }
    }
}

