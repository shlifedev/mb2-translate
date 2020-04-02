using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlSyncer
{
    public class Syncer
    {
        /// <param name="prevFilePath"> 마앤블 이전 버전 xml의 path </param>
        /// <param name="updatedFilePath"> 마앤블 신버전 xml의 path </param>
        /// <param name="outputPath"> 마앤블 새 버전 xml의 출력 path </param>
        public static void Validate(String prevFilePath, String updatedFilePath, String outputPath)
        {
            var prevXmlFileList = GetXmlFilesInDirectory(prevFilePath);
            var updatedXmlFileList = GetXmlFilesInDirectory(updatedFilePath);

            foreach(var test in prevXmlFileList)
            {
                Console.WriteLine(test.FullName);
            }
        }
        
        private static void Processing(string leftCompareFile, string rightCampareFile)
        {
           
        } 
        
         
        private static IEnumerable<XmlFileData> GetXmlFilesInDirectory(String path)
        {
            List<XmlFileData> filePathList = new List<XmlFileData>();
            if (System.IO.Directory.Exists(path))
            {
                System.IO.DirectoryInfo xmlDirectory = new System.IO.DirectoryInfo(path); 
                foreach(var v in xmlDirectory.GetFiles())
                {
                    filePathList.Add(new XmlFileData() {
                        FileName = v.Name,
                        FullName = v.FullName
                    });
                }
            } 
            return filePathList;
        }
    }
}
