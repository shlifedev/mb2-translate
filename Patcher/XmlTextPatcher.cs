using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class XmlTextPatcher
    {

        public static void Patch(string baseModulePath)
        {
            ReplaceXmlWrite(baseModulePath, "EditableTextWidget", "MBKoreanFontTextWidget");
        }
        public static void UnPatch(string baseModulePath)
        {
            ReplaceXmlWrite(baseModulePath, "MBKoreanFontTextWidget", "EditableTextWidget");
        }
        public static void ReplaceXmlWrite(string baseModulePath, string oldValue, string newValue)
        {
            var path = baseModulePath;
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
