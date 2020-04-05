
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Translate.Console.Commands
{
    public static class Commands
    {


        [CommandLineFunctionality.CommandLineArgumentFunction("Reload", "KoreanMod")]
        public static string ReloadKoreaMod(List<string> strings)
        {
            MBKoreanFont.Translate.TranslateUtility.DownloadLatestTranslate();
            MBKoreanFont.Translate.TranslateUtility.ReloadTranslate(); 
            InformationManager.ShowInquiry(new  InquiryData("주의!", "일부 UI는 메인화면 나갔다와야 갱신 됩니다.", true, false, "네", "아니오", ()=> { 
            
            }, ()=> {
                Debug.Print("test");
            }));
            return "Succesfully Reload Korean!";
        }
    }
}