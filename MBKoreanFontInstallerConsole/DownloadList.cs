using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKoreanFontInstallerConsole
{
    public class DownloadFile
    {
        public string fileID;
        public string savePath;
    }
    public class XMLDownloader
    {
        public List<DownloadFile> list = new List<DownloadFile>();
        public void Init()
        {
            Add("1YM99Wbc7O_xubs2ra1FVH0uXBR5S7L10eACAPF8Tce4", "Native/KR");
        }

        public void DownloadAll()
        {
            foreach(var data in list)
            { 
                var fileName = DriveManager.GetFileName(data.fileID);
                var xmlData = DriveManager.DownloadXML(data.fileID);
                System.IO.Directory.CreateDirectory(data.savePath);
                System.IO.File.WriteAllText(data.savePath+"/"+fileName+".xml", xmlData);
            }
        }
        public void Add(string id, string savePath)
        {
            list.Add(new DownloadFile()
            {
                fileID = id,
                savePath = savePath
            });
        }
    }
}
