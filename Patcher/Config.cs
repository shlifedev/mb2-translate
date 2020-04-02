using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public static class Config
    {
        public static readonly string[] PatcherTarget = new string[]{

                "Modules/MBKoreanFontz",
                "Modules/Native/ModuleData/Languages/KR",
                "GUI/GauntletUI/Fonts/Languages.xml" 
        };
    }
}
