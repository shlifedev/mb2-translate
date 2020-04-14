using System;
using System.Collections.Generic;

namespace GlobalConfig
{
    public class Config
    { 
        /// <summary>
        /// Module Name.
        /// </summary>
        public string ModuleName = "MBKoreanFont";
        /// <summary>
        /// 번역 시트 ID
        /// </summary>
        public string TRANSLATE_SHEET_ID = "1oY5F5P-tMBj1-kryB5gR4gS4T5KrlqmDc-tHQBrQBDo";
        /// <summary>
        /// 다운로드 파일 저장 경로
        /// </summary>
        public string OutputPath
        {
            get
            {
                return "Downloaded/";
            }
        }
        /// <summary>
        /// CSV 저장경로
        /// </summary>
        public string CSVSavePath
        {
            get
            {
                return OutputPath + "sheet_downloaded.csv";
            }
        }

        public List<string> BannerlordModuleLangPathList = new List<string>(){
            @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Native\ModuleData\Languages",
            @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBox\ModuleData\Languages", 
            @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBoxCore\ModuleData\Languages",
            @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\StoryMode\ModuleData\Languages"
        };
        /// <summary>
        /// xml 저장이름
        /// </summary>
        public string XmlSavePath
        {
            get
            {
                return OutputPath + "LatestTranslate.xml";
            }
        } 
        public string PatchSaveXmlPath = "PatchLang/LatestSheet.xml";
        public string PatchSaveCsvPath = "PatchLang/LatestSheet.csv";
        public static void Save(Config target, string path)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(target);
            System.IO.File.WriteAllText(path, json);
        }
        public static void Load(Config target, string path)
        {
        
            if (System.IO.File.Exists(path))
            {
                Console.WriteLine("Custom Config used");
                var json = System.IO.File.ReadAllText(path);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(json);
                target = obj; 
            }
            else
            {
                Console.WriteLine("Default Config used");
                target = new Config();
            }
            
        }
    }

}
