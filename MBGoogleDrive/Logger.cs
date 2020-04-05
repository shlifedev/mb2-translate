using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Logger
{
    public static void Log(string msg)
    {
        Log("Log", msg);
    }
    public static void Log(string tag, string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[{tag}]");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"[{msg}]\n"); 
    }
}
