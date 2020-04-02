using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class XmlTextPatcher
    { 
        public static void ReplaceEditableTextToMBFontText()
        {
            ReplaceXmlWrite("EditableTextWidget", "MBKoreanFontTextWidget");
        }
        public static void ReplaceMBFontTextToEditableText()
        {
            ReplaceXmlWrite("MBKoreanFontTextWidget", "EditableTextWidget");
        }
        public static void ReplaceXmlWrite(string oldValue = "EditableTextWidget", string newValue = "MBKoreanFontTextWidget")
        {
            var path = Config.MB_SANDBOX_PATH
;
            System.IO.DirectoryInfo guiDataPath = new System.IO.DirectoryInfo(path +"GUI/Prefabs");
            foreach (var searchedXmlFile in guiDataPath.GetFiles("*.xml", System.IO.SearchOption.AllDirectories))
            {
                var readedXml = System.IO.File.ReadAllText(searchedXmlFile.FullName);
                var patchdXml = readedXml.Replace("EditableTextWidget", "MBKoreanFontTextWidget");
                System.IO.File.WriteAllText(searchedXmlFile.FullName, patchdXml); 
            }
        }
    }
}
