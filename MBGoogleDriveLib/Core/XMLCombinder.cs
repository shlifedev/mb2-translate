using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
public class XMLCombinder
{
    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, TranslateData> dataMap = new Dictionary<string, TranslateData>();

    public void ExportReadDataToCSV(string savePath )
    {
        System.IO.FileInfo fi = new System.IO.FileInfo(savePath);
        System.IO.Directory.CreateDirectory(fi.Directory.FullName); 
        string v = "Id\tOriginal\tTranslate\tFilename\tModule\n";
        foreach (var data in dataMap)
        {
            v += $"{data.Key}\t'{data.Value.Original}\t'{data.Value.Translate}\t{data.Value.Filename}\t{data.Value.Module}\n"; 
        }
        System.IO.File.WriteAllText(savePath, v);
        dataMap.Clear();
    }
     

    private string GetMoudleNameByPath(string diFullname)
    { 
        var idx = diFullname.LastIndexOf(@"Modules\");
        var modulePath = diFullname.Substring(idx, (diFullname.Length-idx));
        var mod = modulePath.Split('\\')[1]; 
        string modName = mod;
        return modName; 
        //old code
        if (diFullname.Contains(@"Modules\SandBox"))
            modName = "SandBox";
        if (diFullname.Contains(@"Modules\SandBoxCore"))
            modName = "SandBoxCore";
        else if (diFullname.Contains(@"Modules\Native")) 
            modName = "Native"; 
        else modName ="Unknown";
        return modName;
    }
    /// <summary>
    /// 경로에서 xml을 읽어서 dataMap에 저장.
    /// </summary>
    /// <param name="xmlPath"></param>
    /// <param name="langCode"></param>
    public void ReadXMLDatas(string xmlPath)
    { 
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(xmlPath);
        var files = di.GetFiles("*.xml");
        foreach (var data in files)
        {
            string lang = "";
            XmlReader reader = XmlReader.Create(data.FullName);
            TranslateData targetTranData = null;
            string fileName = data.Name;
            string modName = data.Name;
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name == "tag")
                    {
                        lang = reader["language"];
                    }
                    if (reader.Name == "string")
                    {
                        var id = reader["id"]; 
                        //기존 번역이 불러와진 상태.
                        if (dataMap.ContainsKey(id))
                            targetTranData = dataMap[id];
                        else //기존 번역이 불러와지지 않은 상태
                        { 
                            //기존 번역에 대한 번역 정보 작성.
                            targetTranData = new TranslateData();
                            targetTranData.Id = id;
                            targetTranData.Filename = data.Name;
                            if(lang != "한국어")
                            targetTranData.Module = GetMoudleNameByPath(di.FullName);
                            dataMap.Add(id, targetTranData);
                        }
                        //언어 파악
                        if (lang == "한국어") 
                            targetTranData.Translate = reader["text"]; 
                        if (lang == "English") 
                            targetTranData.Original = reader["text"]; 
                    }
                }
            }
        }
    }
}