using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
public class XMLCombinder
{
    Dictionary<string, TranslateData> dataMap = new Dictionary<string, TranslateData>();

    public void ExportReadDataToCSV(string savePath )
    {
        System.IO.FileInfo fi = new System.IO.FileInfo(savePath);
        System.IO.Directory.CreateDirectory(fi.Directory.FullName); 
        string v = "Id\tOriginal\tTranslate\tFilename\tModule\n";
        foreach (var data in dataMap)
        {
            v += $"{data.Key}\t{data.Value.Original}\t{data.Value.Translate}\t{data.Value.Filename}\t{data.Value.Module}\n";

        }
        System.IO.File.WriteAllText(savePath, v);
        dataMap.Clear();
    }
    /// <summary>
    /// 경로에서 xml을 읽어서 dataMap에 저장.
    /// </summary>
    /// <param name="xmlPath"></param>
    /// <param name="langCode"></param>
    public void ReadXMLDatas(string xmlPath)
    { 
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(xmlPath);
        var files = di.GetFiles();
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
                        if (dataMap.ContainsKey(id))
                            targetTranData = dataMap[id];
                        else
                        {
                            targetTranData = new TranslateData();
                            targetTranData.Id = id;
                            targetTranData.Filename = data.Name;
                            if (di.FullName.Contains(@"Modules\SandBox"))
                            {
                                modName = "SandBox";
                            }
                            else if (di.FullName.Contains(@"Modules\Native"))
                            {
                                modName = "Native";
                            }
                            targetTranData.Module = modName;
                            dataMap.Add(id, targetTranData);
                        }
                        if (lang == "한국어")
                        {
                            targetTranData.Translate = reader["text"];
                        }
                        if (lang == "English")
                        {
                            targetTranData.Original = reader["text"];
                        }
                    }
                }
            }
        }
    }
}