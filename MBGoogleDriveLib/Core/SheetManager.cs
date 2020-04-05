using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class SheetManager
{

    static SheetsService Service = null;
    public static void Init(SheetsService service)
    {
        Service = service;
    }


    public static string GetVersion()
    {
        String spreadsheetId = "1oY5F5P-tMBj1-kryB5gR4gS4T5KrlqmDc-tHQBrQBDo";
        String range = "엑셀정보!B7";

        SpreadsheetsResource.ValuesResource.GetRequest request = Service.Spreadsheets.Values.Get(spreadsheetId, range);
        ValueRange response = request.Execute();
        IList<IList<Object>> values = response.Values;
        if (values != null && values.Count > 0)
        { 
            foreach (var row in values)
            {
                foreach(var col in row)
                {
                    Console.WriteLine(col);
                }
            } 
        }
            return null;
    }
}