using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MBGoogleDrive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace GoogleDrive
{

    public static class CredentialManager
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";
        public static Google.Apis.Drive.v3.DriveService Service = new DriveService();
        public static void Credential()
        {
            Google.Apis.Services.BaseClientService.Initializer bcs = new Google.Apis.Services.BaseClientService.Initializer();
            bcs.ApiKey = "AIzaSyA71-yjK1IVUWEEgy5X76uNONpLbe02rDs";
            bcs.ApplicationName = "MBTranslate";
            Google.Apis.Drive.v3.DriveService service = new Google.Apis.Drive.v3.DriveService(bcs);
            Service = service;
            DriveManager.Init(service);
        }
        public static void CredentialBySecretKey()
        { 
            UserCredential credential;
            using (var stream =
                new FileStream("secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Service = service;
            DriveManager.Init(service);
        }
    }
    class Program
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";
        public static Setting setting = new Setting();
 
 
        static void Main(string[] args)
        { 
            Console.WriteLine("\t 실행 할 기능 선택\n   1.공유 폴더를 다운로드 받습니다.\n   2.CSV를 만듭니다.\n   3.공식 스프레드 시트를 xml로 변환합니다.");
            var v = Console.ReadLine();
            if(v == "1")
            {
                CredentialManager.CredentialBySecretKey();
                Download();
            }
            else if(v == "2")
            {
                XMLCombinder.ReadXMLDatas(@"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\Native\ModuleData\Languages");
                XMLCombinder.ReadXMLDatas(@"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBox\ModuleData\Languages");
                XMLCombinder.ReadXMLDatas(@"C:\Users\shlif\OneDrive\Desktop\Github\MBKoreanFont\MBGoogleDrive\bin\Debug\Downloaded");
                XMLCombinder.SaveToCSV();
            } 
            else if(v == "3")
            {
                CredentialManager.Credential();
                DownloadFromSheet(); 
            }
        }
      
        static void DownloadFromSheet()
        {
            XMLSheetDownloader dl = new XMLSheetDownloader();
            dl.DownloadFromSheet();
        }
        static void Download()
        {
            XMLDownloader dl = new XMLDownloader();
            dl.Init();
            dl.DownloadAll();
        }

    }
}