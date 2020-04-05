using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class DriveManager
{
    static DriveService Service = null; 
    public static void Init(DriveService service)
    {
        Service = service;
    }
    public static string GetFileName(string fileID)
    {
        return Service.Files.Get(fileID).Execute().Name;
    }
     

    public static string DownloadCSV(string fileID)
    {
        String fileId = fileID;
        var request = Service.Files.Export(fileId, "text/csv");
        var stream = new System.IO.MemoryStream();
        request.MediaDownloader.ProgressChanged +=
                (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine($"Download failed. =>{progress.Exception}");
                                break;
                            }
                    }
                };

        request.Download(stream);
        var v = System.Text.Encoding.UTF8.GetString(stream.ToArray());
        return v;
    }

    public static string DownloadXML(string fileID)
    {
        String fileId = fileID;
        var request = Service.Files.Export(fileId, "text/plain");
        var stream = new System.IO.MemoryStream();
        request.MediaDownloader.ProgressChanged +=
                (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                    }
                };

        request.Download(stream);
        var v = System.Text.Encoding.UTF8.GetString(stream.ToArray());
        return v;
    }


    private static void SaveStream(System.IO.MemoryStream stream, string saveTo)
    {
        using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
        {
            stream.WriteTo(file);
        }
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