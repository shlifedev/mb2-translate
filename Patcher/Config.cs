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
                return @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\";
            }
        }
        public static string MB_SANDBOX_PATH
        {
            get
            {
                return MB_INSTALL_PATH + @"Modules\SandBox\";
            }
        }
        public static string MB_NATIVE_PATH
        {
            get
            {
                return MB_INSTALL_PATH + @"Modules\Native\";
            }
        }
        public static readonly string[] PatcherTarget = new string[]{

                "Modules/MBKoreanFont",
                "Modules/Native/ModuleData/Languages/KR",
                "GUI/GauntletUI/Fonts/Languages.xml"
        };
        /// <summary>
        /// replace class name. (a to b)
        /// </summary>
        public static readonly (string, string, string)[] ReplaceType = new (string, string, string)[]{
            (MB_SANDBOX_PATH, "EditableTextWidget","MBKoreanFontTextWidget"),
            (MB_SANDBOX_PATH, "TextWidget","CustomTextWidget"),
            (MB_SANDBOX_PATH, "RichTextWidget","CustomRichTextWidget"),
            (MB_NATIVE_PATH, "EditableTextWidget","MBKoreanFontTextWidget"),
            (MB_NATIVE_PATH, "TextWidget","CustomTextWidget"),
            (MB_NATIVE_PATH, "RichTextWidget","CustomRichTextWidget"),

        };
    }
}
