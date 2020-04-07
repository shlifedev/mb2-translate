
using MBKoreanFont.Translate;
using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Translate.Console.Commands
{
    public static class Commands
    {


        [CommandLineFunctionality.CommandLineArgumentFunction("update", "koreanmod")]
        public static string UpdateTranslate(List<string> strings)
        {
            return TranslateUtility.DownloadAndReloadTranslate();
        }
    }
}