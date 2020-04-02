using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public static class Config
    {
        public static string MB_INSTALL_PATH
        {
            get
            {
                return @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\";
            }
        }
        public static string MB_STORY_MODULE_PATH
        {
            get
            {
                return MB_INSTALL_PATH + @"Modules\StoryMod\";
            }
        } 
        public static readonly string[] PatcherTarget = new string[]{

                "Modules/MBKoreanFont",
                "Modules/Native/ModuleData/Languages/KR",
                "GUI/GauntletUI/Fonts/Languages.xml"
        };
    }
}
