using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
/// <summary>
/// 모드파일/소스코드 무단수정 배포 금지합니다.
/// Writer : shlifedev@gmail.com 
/// </summary>
namespace MBKoreanFont.Translate
{
    public class TranslateUtility
    {

        /// <summary>
        /// 최신 시트를 다운로드 합니다
        /// </summary>
        public static void DownloadLatestTranslate()
        {
            CredentialManager.InitCredentialManager($"../../Modules/{MBKoreanFontSubModule.ModuleName}/secret.json");
            CredentialManager.CredentialDriveServiceByToken();
            XMLSheetDownloader dl = new XMLSheetDownloader();
            dl.DownloadFromSheet($"../../Modules/{MBKoreanFontSubModule.ModuleName}/ModuleData/Languages/KR/LatestTranslate.xml");
        }
        /// <summary>
        /// 언어파일을 리로드 합니다.
        /// </summary>
        public static void ReloadTranslate()
        {
            try
            {
                var _gameTextDictionary = typeof (LocalizedTextManager).GetField("_gameTextDictionary", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                //call clear();
                _gameTextDictionary.GetType().GetMethod("Clear").Invoke(_gameTextDictionary, null);
                TaleWorlds.Localization.LocalizedTextManager.LoadLocalizationXmls();
            }
            catch (Exception e)
            {
                InformationManager.ShowInquiry(new InquiryData("Except!!", e.Message, true, false, "ok", null, null, null, ""));
            }
        }
    }
}
