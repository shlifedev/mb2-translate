using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace MBModAuthServer
{
    class Program
    {

        
        public class UserAuth
        {
            public string value; 
            public string privateKey;
            public string registeredIP;

            public int auth_failed_count;
            public int auth_successfully_count;

            /// <summary>
            /// 이 값이 TRUE면 요청시 registeredIP을 초기화
            /// </summary>
            public bool clear = false; 
        }

        static public List<UserAuth> validKeys = new List<UserAuth>();
        static (bool,string) Auth(string pkey, string ip)
        {
       
            var data = validKeys.Find(x=>x.privateKey==pkey);

            if(data == null)
            {
                Logger.Log($"Auth Failed {pkey}\t{ip}");
                return (false, "NoExist");
            }
            else
            {
                if (data.clear) {
                    Logger.Log($"Clear AuthData {pkey}\t{ip}\t{data.value}");
                    data.registeredIP = null;
                    data.clear = false;
                    SaveAuth();
                }
                if (data.registeredIP == ip)
                {
                    Logger.Log($"Auth {pkey}\t{ip}\t{data.value}");
                    data.auth_successfully_count++;
                    SaveAuth();
                    return (true, "Auth!");
                }
                else if(string.IsNullOrEmpty(data.registeredIP))
                {
                    data.registeredIP = ip;
                    data.auth_successfully_count++; 
                    SaveAuth();
                    Logger.Log($"Auth {pkey}\t{ip}\t{data.value}");
                    return (true, "Auth!");
                } 
                else
                {
                    data.auth_failed_count++;
                    SaveAuth();
                    Logger.Log($"본인이 아닌 사용자가 로그인 시도 인증시도키:{pkey}\tIP:{ip}\t키주인:{data.value}");
                    return (false, "InValid IP!");
                }
            }
        }
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create(); 
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input + "private")); 
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder(); 
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            } 
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        static void AddKey(string value)
        {
            Logger.Log("Server::Addkey", value);
            UserAuth auth = new UserAuth();
            auth.value = value;
            auth.privateKey = getMd5Hash(value);
            validKeys.Add(auth);
            SaveAuth();
        }
        static void RemoveKey(string value)
        {
            var p = validKeys.Find(x => x.value == value);
            if (p != null)
            {
                Logger.Log("Server::RemoveKey", value); 
                validKeys.Remove(p);
                SaveAuth();
            }
        }

        static void LoadAuth()
        {
            if(System.IO.File.Exists("auth.json"))
            {
                validKeys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserAuth>>(System.IO.File.ReadAllText("auth.json"));
            }
            else
            {
                validKeys = new List<UserAuth>();
            }
        }
        static void SaveAuth()
        {
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(validKeys);
            System.IO.File.WriteAllText("auth.json", js);
        }
        static void Auth()
        {
            LoadAuth();
            TcpListener listener = new TcpListener(IPAddress.Any, 9090);
            listener.Start();
            Logger.Log("Listener", "서버 시작됨");
            while (true)
            {
                TcpClient tc = listener.AcceptTcpClient();
                NetworkStream stream = tc.GetStream();
                StreamReader sr = new StreamReader(stream);
                StreamWriter sw = new StreamWriter(stream); ;
                var data = sr.ReadLine();
                var ipAdress = ((IPEndPoint)tc.Client.RemoteEndPoint).Address.ToString();
                var auth = Auth(data, ipAdress);
                if (auth.Item1 == true)
                {
                    Logger.Log("인증 성공", "User Info : " + ipAdress +"\t"+ data);
                    sw.WriteLine("Valid");
                }
                else
                {
                    Logger.Log("인증 실패", "User Info : " + ipAdress);
                    sw.WriteLine("Invalid");
                }
                sw.Flush();
                stream.Close();
                tc.Close();
            }
        }

        static void Command(string command)
        {
            try
            {
                if (command.Contains("addkey"))
                {
                    var split = command.Split(' ');
                    AddKey(split[1]);
                }
                if (command.Contains("removekey"))
                {
                    var split = command.Split(' ');
                    RemoveKey(split[1]);
                }
                if (command.Contains("clearkey"))
                {
                    validKeys.Clear();
                    SaveAuth();
                }
                if (command.Contains("clear"))
                {
                    var split = command.Split(' ');
                    var user = validKeys.Find(x=>x.value ==split[1]);
                    if (user != null)
                    {
                        Logger.Log(user.value + " 의 private key가 클리어 예약 되었습니다.");
                        user.clear = true;
                    }
                }
                if(command.Contains("show"))
                {
                    var split = command.Split(' ');
                    var user = validKeys.Find(x=>x.value ==split[1]);
                    if (user != null)
                    {
                        var org =   Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\tShow Info");
                        Console.WriteLine("\tName\tip\tLIC\tSuccess\tFailed");
                        Console.WriteLine($"{user.value}\t{user.registeredIP}\t{user.privateKey}\t{user.auth_successfully_count}\t{user.auth_failed_count}"); 
                    }
                }
            }
            catch
            {

            }

        }
        static void Main(string[] args)
        {
        
            System.Threading.Thread authServer = new System.Threading.Thread(new System.Threading.ThreadStart(Auth));
            authServer.Start();

            LoadAuth();
            while (true)
            {
                int t = 1; 
                foreach (var data in validKeys)
                { 
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"activate liecence : {t}.{data.value}\t{data.privateKey}\t{data.registeredIP}");
                    t++;
                }
                var p = Console.ReadLine(); 
                Command(p);
            }
        }
    }
}
