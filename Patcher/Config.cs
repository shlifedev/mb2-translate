using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Patcher
{
    public static class Config
    {
        public static string MB_INSTALL_PATH
        {
            get
            {
                var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", "i hate bokdol. i will kill you.."); 
                var ret =  value + @"\steamapps\";
                var di = new System.IO.DirectoryInfo(ret);
                var bannerlordACF = di.GetFiles("*.acf").Where(x=>x.Name.Contains("261550")).First();  
                return ret;
            }
        }
        public static string MB_SANDBOX_PATH

        {
            get
            {
                return MB_INSTALL_PATH + @"Modules\SandBox\";
            }
        } 
        public static readonly string[] PatcherTarget = new string[]{

                "Modules/MBKoreanFont",
                "Modules/Native/ModuleData/Languages/KR",
                "GUI/GauntletUI/Fonts/Languages.xml"
        };
    }
}
