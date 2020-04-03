using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public static class Utility
    {
        static bool legit = false;
        /// <summary>
        /// 복돌 유저 검출
        /// </summary>
        /// <returns></returns>
        public static bool IsLegitPlayer()
        {
            try
            { 
                if(legit) return true;
                var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", "i hate bokdol. i will kill you..");
                var ret =  value + @"\steamapps\";
                var di = new System.IO.DirectoryInfo(ret);
                var bannerlordACF = di.GetFiles("*.acf").Where(x=>x.Name.Contains("261550")).First();
                if(bannerlordACF != null)
                {
                    legit = true;
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return false;
        }
    }
}
