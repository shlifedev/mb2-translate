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


namespace MBKoreanFontInstallerConsole
{
    public static class DriveManager
    {
        static DriveService Service = null;
        public static void Init(DriveService service)
        {
            Service = service;
        }

        public static IEnumerable<Google.Apis.Drive.v3.Data.File> GetFolderFiles(string folderID, string filterExtention = null)
        {
            var request = Service.Files.List();
            request.Q = $"'{folderID}' in parents";

            if (filterExtention != null)
            { 
                var executeResult = request.Execute().Files.Where(x=>x.FullFileExtension.Contains(filterExtention));
                return executeResult;
            }
            else
            {
                var executeResult = request.Execute().Files;
                return executeResult;
            } 
        }
    }
}
