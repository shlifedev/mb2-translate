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


            double hour = 6;
            hour = hour%12;
            double minute = 0;
            double second = 0;
            hour = (hour * Math.PI / 6) + (minute * Math.PI / (6 * 60)) + (second * Math.PI / (360 * 60));;

            Console.WriteLine(hour);
            while (true)
            { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t 최신 XML 다운로더입니다. 제작:shlifedev@gmail.com | 마공카 햄스터 에비츄");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("최신 XML 다운로더입니다.");
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
            dl.DownloadFromSheet($"Downloaded/LatestTranslate.xml");
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