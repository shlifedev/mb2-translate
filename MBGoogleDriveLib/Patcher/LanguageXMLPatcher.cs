using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
    public static class LanguageXMLPatcher
    {
        public static void RunPatch()
        {
            Console.Clear();

            Logger.Log("새로운 패치파일을 만듭니다.");
            CredentialManager.CredentialDriveService();
            XMLCombinder combinder = new XMLCombinder();

            Logger.Log("배너로드 경로에서 XML 데이터 파일을 읽어옵니다.");
            combinder.ReadXMLDatas(@"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Native\ModuleData\Languages");
        combinder.ReadXMLDatas(@"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBox\ModuleData\Languages");
        combinder.ReadXMLDatas(@"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBoxCore\ModuleData\Languages");
        XMLSheetDownloader dl = new XMLSheetDownloader();

            Logger.Log("구글드라이브에서 번역본 XML 파일을 읽어옵니다.");
            var xmlSavePath = "PatchLang/LatestSheet.xml";
            FileInfo fi = new FileInfo(xmlSavePath);
            var dirName = fi.Directory.FullName;
            dl.DownloadFromSheet(xmlSavePath);
            combinder.ReadXMLDatas(dirName);

            Logger.Log("번역본 데이터와 로컬 스트링을 취합중입니다.");
            combinder.ExportReadDataToCSV("PatchLang/PatchedLatestSheet.csv");
        }

    } 