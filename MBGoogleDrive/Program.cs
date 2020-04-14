using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store; 
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
        static void Main(string[] args)
        {
            while (true)
            { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t 최신 XML 다운로더입니다. 제작:shlifedev@gmail.com | 마공카 햄스터 에비츄");
                Console.ForegroundColor = ConsoleColor.White; 
                Console.WriteLine("\n\n1 입력시 최신번역을 다운로드 합니다.\n2 입력시 언어데이터를 취합합니다.");
                var v = Console.ReadLine();  
                Console.Clear();
                if (v == "1") 
                    DownloadFromSheet();  
                else if (v == "2") 
                    PatchLanguageData();  
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
        static void PatchLanguageData()
        {
            LanguageXMLPatcher.RunPatch();
        }
        static void DownloadFromSheet()
        {
            CredentialManager.CredentialDriveServiceByToken();
            XMLSheetDownloader dl = new XMLSheetDownloader(); 
            dl.DownloadFromSheet("Downloaded/LastTranslate.xml");
        }
        static void Download()
        {
            CredentialManager.CredentialDriveServiceByToken();
            XMLDownloader dl = new XMLDownloader();
            dl.Init();
            dl.DownloadAll();
        }

    }
}