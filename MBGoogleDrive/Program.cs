using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MBKoreanFontInstallerConsole;
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
    class Program
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";

        static void Credential2()
        { 
            Console.WriteLine("\t MB Korean Downloader :: 사용자 인증 중 입니다.");
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
            DriveManager.Init(service);
        }
        static void Credential()
        {
            Google.Apis.Services.BaseClientService.Initializer bcs = new Google.Apis.Services.BaseClientService.Initializer();
            bcs.ApiKey = "AIzaSyA71-yjK1IVUWEEgy5X76uNONpLbe02rDs";
            bcs.ApplicationName = "MBTranslate"; 
            Google.Apis.Drive.v3.DriveService service = new Google.Apis.Drive.v3.DriveService(bcs);
            DriveManager.Init(service);
        }
         
        static void Main(string[] args)
        { 
            Credential2();
            Download();
        }

        static void Download()
        {
            XMLDownloader dl = new XMLDownloader();
            dl.Init();
            dl.DownloadAll();
        }

    }
}