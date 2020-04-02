using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class PrefabPatcher
    {

        /// <summary>
        /// 프리팹 패치
        /// </summary>
        /// <param name="baseModulePath"></param>
        public static void PrefabPatch(string baseModulePath)
        {
            foreach (var data in Config.ReplaceType)
            {
                ReplaceXmlWrite(data.Item1, data.Item2, data.Item3);
            }
        }
        /// <summary>
        /// 프리팹 패치
        /// </summary>
        /// <param name="baseModulePath"></param>
        public static void PrefabUnPatch(string baseModulePath)
        {
            foreach (var data in Config.ReplaceType)
            {
                ReplaceXmlWrite(data.Item1, data.Item3, data.Item2);
            }
        }

        public static void ReplaceXmlWrite(string baseModulePath, string oldValue, string newValue)
        {
            var path = baseModulePath;
            System.IO.DirectoryInfo guiDataPath = new System.IO.DirectoryInfo(path +"GUI/Prefabs");
            foreach (var searchedXmlFile in guiDataPath.GetFiles("*.xml", System.IO.SearchOption.AllDirectories))
            {
                var readedXml = System.IO.File.ReadAllText(searchedXmlFile.FullName);
                var patchdXml = readedXml.Replace(oldValue, newValue);
                System.IO.File.WriteAllText(searchedXmlFile.FullName, patchdXml);
            }
        }

    }
}
