using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
/// <summary>
/// 모드파일/소스코드 무단수정 배포 금지합니다.
/// Writer : shlifedev@gmail.com 
/// </summary>
namespace MBKoreanFont.Translate
{
    public class TranslateUtility
    {
        static System.DateTime NextQueryableTime;

        public static string DownloadAndReloadTranslate()
        {
            InformationManager.ShowInquiry(new InquiryData("최신 번역 다운로드 Google 인증필요", "이 기능을 사용하려면 구글로그인으로 권한을 요청해야합니다.", true, true, "동의", "동의 안함", () =>
            {
                if (NextQueryableTime < System.DateTime.Now)
                {
                    MBKoreanFont.Translate.TranslateUtility.DownloadLatestTranslate();
                    MBKoreanFont.Translate.TranslateUtility.ReloadTranslate();
                    InformationManager.DisplayMessage(new InformationMessage("[한글모드 적용완료]일부 메세지는  메인화면으로 나갔다 온 이후 적용됩니다.", new Color(0, 1, 0, 1)));
                    NextQueryableTime = System.DateTime.Now.AddSeconds(10);
                }
                else
                {
                    InformationManager.DisplayMessage(new InformationMessage("아직은 사용할 수 없습니다. 잠시후에 다시 시도하세요.", new Color(0, 1, 0, 1)));
                }
            }, null));
            return "Succesfully! Latest Korean Translate File Downloaded.";
        }
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

        public static void LoadLocalizationKorean()
        {
            LocalizedTextManager.LanguageIds.Clear();  
            string path = MBKoreanFontSubModule.ModulePath + "ModuleData/Languages";
            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories))
                {
                    typeof(LocalizedTextManager).GetMethod("LoadLocalizedTexts").Invoke(null, new Object[] { file });
                }
            } 
        }


        /// <summary>
        /// 한국어만 로드합니다.
        /// </summary>
        public static void LoadOnlyKorean()
        {
            try
            {
                var _gameTextDictionary = typeof (LocalizedTextManager).GetField("_gameTextDictionary", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                //call clear();
                _gameTextDictionary.GetType().GetMethod("Clear").Invoke(_gameTextDictionary, null);
                LoadLocalizationKorean();
            }
            catch (Exception e)
            {
                InformationManager.ShowInquiry(new InquiryData("Except!!", e.Message, true, false, "ok", null, null, null, ""));
            }
        }
    }
}
