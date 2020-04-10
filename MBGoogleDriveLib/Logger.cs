using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Logger
{
    public static void WriteLog(string tag, string msg)
    { 
        if(!System.IO.File.Exists("logs.txt"))
        {
            System.IO.File.WriteAllText("logs.txt", "doy~~");
        }
        System.IO.StreamWriter writer = new System.IO.StreamWriter("logs.txt");
        writer.WriteLine($"[{tag}, {System.DateTime.Now.ToString()}]" + msg);
        writer.Close();
    }
    public static void Log(string msg)
    {
        Log($"Log", msg);
        WriteLog("Log", msg);
    }
    public static void Log(string tag, string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[{tag}][{System.DateTime.Now.ToString()}]");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" {msg} \n");
        WriteLog(tag,msg);
    }
}
