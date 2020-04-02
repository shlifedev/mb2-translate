using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class EditableTextPatcher
    {
        public static void ReplaceXmlWrite(string oldValue = "EditableTextWidget" , string newValue = "MBKoreanFontTextWidget")
        {
            var path = Config.MB_STORY_MODULE_PATH;
            System.IO.DirectoryInfo guiDataPath = new System.IO.DirectoryInfo(path +"GUI/Prefab");
            foreach(var searchedXmlFile in guiDataPath.GetFiles("*.xml", System.IO.SearchOption.AllDirectories))
            {
                var readedXml = System.IO.File.ReadAllText(searchedXmlFile.FullName);
                var patchdXml = readedXml.Replace("EditableTextWidget", "MBKoreanFontTextWidget");
                System.IO.File.WriteAllText(searchedXmlFile.FullName, patchdXml);
            }
        }
    }
}
