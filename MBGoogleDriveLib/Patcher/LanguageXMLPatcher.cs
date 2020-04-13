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
        CredentialManager.CredentialDriveServiceByToken();
        XMLCombinder combinder = new XMLCombinder(); 
        Logger.Log("배너로드 경로에서 XML 데이터 파일을 읽어옵니다.");

        foreach (var data in Setting.Config.BannerlordModuleLangPathList) 
            combinder.ReadXMLDatas(data);

        XMLSheetDownloader dl = new XMLSheetDownloader(); 
        Logger.Log("구글드라이브에서 번역본 XML 파일을 읽어옵니다.");

        var xmlSavePath = Setting.Config.PatchSaveXmlPath;
        FileInfo fi = new FileInfo(xmlSavePath);

        var dirName = fi.Directory.FullName;
            dl.DownloadFromSheet(xmlSavePath);
        Logger.Log("번역본 데이터와 로컬 스트링을 취합중입니다.");
            combinder.ReadXMLDatas(dirName);
            combinder.ExportReadDataToCSV(Setting.Config.PatchSaveCsvPath);
    }

}