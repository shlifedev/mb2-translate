
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

            InformationManager.ShowInquiry(new InquiryData("최신 번역 다운로드 Google 인증필요", "이 기능을 사용하려면 구글로그인으로 권한을 요청해야합니다.", true, true, "동의", "동의 안함", () =>
            {
                MBKoreanFont.Translate.TranslateUtility.DownloadLatestTranslate();
                MBKoreanFont.Translate.TranslateUtility.ReloadTranslate();
                InformationManager.DisplayMessage(new InformationMessage("[한글모드 적용완료]일부 메세지는 재접속 후 적용됩니다.", new Color(0,1,0,1))); 
            }, null));


            return "Succesfully Reload Korean!";

        }
    }
}