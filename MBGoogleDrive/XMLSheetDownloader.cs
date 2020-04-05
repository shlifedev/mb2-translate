using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using GoogleFile = Google.Apis.Drive.v3.Data.File;
using Newtonsoft.Json;
using CsvHelper;
using System.IO;
using System.Globalization;

public class XMLSheetDownloader
{
    static List<TranslateData> data = new List<TranslateData>(); 
    static string OutputPath
    {
        get
        {
            return "Downloaded/";
        }
    }

    static string CSVSavePath
    {
        get
        {
            return OutputPath + "sheet_downloaded.csv";
        } 
    }

    static string XmlSavePath
    {
        get
        {
            return OutputPath + "MBKoreanFont.xml";
        }
    }
    public void DownloadFromSheet(string xmlPath = null)
    { 
        System.IO.Directory.CreateDirectory(OutputPath);

        string xmlSavePath = "";
        if(xmlPath == null)
            xmlSavePath = XmlSavePath;


        // 생성할 XML 파일 경로와 이름, 인코딩 방식을 설정합니다. 
        XmlTextWriter textWriter = new XmlTextWriter(xmlSavePath, Encoding.UTF8);
        // 들여쓰기 설정 
        textWriter.Formatting = System.Xml.Formatting.Indented;
        // 문서에 쓰기를 시작합니다. 
        textWriter.WriteStartDocument(); 
        textWriter.WriteStartElement("base");
        textWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        textWriter.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        textWriter.WriteAttributeString("type", "string");
        textWriter.WriteStartElement("tags");
        textWriter.WriteStartElement("tag");
        textWriter.WriteAttributeString("language", "한국어");
        textWriter.WriteEndElement();
        textWriter.WriteEndElement();
        textWriter.WriteStartElement("strings");
        string strings ="";
        var csv = DriveManager.DownloadCSV("1oY5F5P-tMBj1-kryB5gR4gS4T5KrlqmDc-tHQBrQBDo");
        System.IO.File.WriteAllText(CSVSavePath, csv);
        var splitnl = csv.Split('\n');
        using (var reader = new StreamReader(CSVSavePath))
        {
            using (var td = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = td.GetRecords<TranslateData>();
               
                foreach (var record in records)
                {
                    textWriter.WriteStartElement("string");
                    textWriter.WriteAttributeString("id", record.Id);
                    textWriter.WriteAttributeString("text", record.Translate); 
                    textWriter.WriteEndElement();
                }   
            }

        }  
        textWriter.WriteEndElement(); 
        textWriter.WriteEndElement(); 
        textWriter.WriteEndDocument(); 
        textWriter.Close(); 
    }
}