using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using Newtonsoft.Json; 
[Obsolete]
public class CachedXML
{
    public string id;
    public string originalFileName;
}
[Obsolete]
public class XMLDownloader
{
    public List<CachedXML> cachedXML = new List<CachedXML>();
    public List<(string,string)> DownloadFolders = new List<(string,string)>();
    static string AppCachePath
    {
        get
        {
            var path = "AppCache/Cache.json";
            System.IO.Directory.CreateDirectory(path);
            return path;
        }
    }
    public void Init()
    {
        DownloadFolders.Add(("1BlL7Nk8btu4s05vIsPNlUJn_iF8aiu3R", "Native"));
        DownloadFolders.Add(("1LWi0qud9RU_pfIDwTTNEWD9wvjAhfwzO", "Sandbox"));
    }
    private static Random random = new Random();
    public void SaveCacheData()
    {
        var json = JsonConvert.SerializeObject(cachedXML);
        System.IO.File.WriteAllText(AppCachePath, json);
    }
    public void LoadCacheData()
    {
        if (System.IO.File.Exists(AppCachePath))
        {
            var data = System.IO.File.ReadAllText(AppCachePath);
            var json = JsonConvert.DeserializeObject<List<CachedXML>>(data);
            this.cachedXML = json;
        }
        else
        {
            cachedXML.Clear();
        }
    }
    public List<GoogleFile> GetFolderFiles()
    {
        List<GoogleFile> files = new List<GoogleFile>();
        foreach (var data in DownloadFolders)
        {
            var downloadRequireList = DriveManager.GetFolderFiles(data.Item1).ToList();
            files.AddRange(downloadRequireList);
        }
        return files;
    }
    public void DownloadAll(string savePath = "DownloadedKrDocuments")
    {
        LoadCacheData();
        var files = GetFolderFiles();
        foreach (var data in files)
        {
            string fileName = data.Id;
            var cacheData = cachedXML.Find(x=>x.id == data.Id);
            if (cacheData == null)
            {
                cacheData = new CachedXML()
                {
                    id = data.Id
                };
                cacheData.originalFileName = DriveManager.GetFileName(cacheData.id);
                cachedXML.Add(cacheData);
                fileName = cacheData.originalFileName + "_" + data.Id.Substring(0, 6);
            }
            else
            {
                fileName = cacheData.originalFileName + "_" + data.Id.Substring(0, 6);
            }
            var xmlData = DriveManager.DownloadXML(data.Id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{fileName} Downloaded!");
            Console.ForegroundColor = ConsoleColor.White;

            //create di
            System.IO.Directory.CreateDirectory(savePath);
            System.IO.File.WriteAllText(savePath + "/" + fileName + ".xml", xmlData);
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(savePath + "/" + fileName + ".xml");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(fileName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" File Has Error!\n" + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        SaveCacheData();
    }
}