#if USE_GOOGLE_API
using MBKoreanFont.Translate;
#endif
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
#if USE_GOOGLE_API
            return TranslateUtility.DownloadAndReloadTranslate();
#endif

            return "google api disable";
        }
    }
}