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
    class Program
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";
        public static Setting setting = new Setting(); 
        static void Main(string[] args)
        {
            Console.WriteLine("\t 실행 할 기능 선택\n   1.공유 폴더를 다운로드 받습니다.\n   2.CSV를 만듭니다.\n   3.공식 스프레드 시트를 xml로 변환합니다.");
            var v = Console.ReadLine();
            if (v == "1")
            {
                Download();
            }
            else if (v == "2")
            {
                PatchLanguageData();
            }
            else if (v == "3")
            {
                DownloadFromSheet();
            }
        }
        static void PatchLanguageData()
        {
            LanguageXMLPatcher.RunPatch();
        }
        static void DownloadFromSheet()
        {
            CredentialManager.Credential();
            XMLSheetDownloader dl = new XMLSheetDownloader();
            dl.DownloadFromSheet();
        }
        static void Download()
        {
            CredentialManager.Credential();
            XMLDownloader dl = new XMLDownloader();
            dl.Init();
            dl.DownloadAll();
        }

    }
}