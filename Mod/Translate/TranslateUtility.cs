using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace MBKoreanFont.Translate
{
    public class TranslateUtility
    {

        public static void DownloadLatestTranslate()
        {
            CredentialManager.CredentialDriveService();
            XMLSheetDownloader dl = new XMLSheetDownloader();
            dl.DownloadFromSheet($"../../Modules/{MBKoreanFontSubModule.ModuleName}/ModuleData/Languages/KR/LatestTranslate.xml");
        }
        public static void ReloadTranslate()
        {
            try
            {
                var _defaultFontMap = typeof (LocalizedTextManager).GetField("_gameTextDictionary", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                //call clear();
                _defaultFontMap.GetType().GetMethod("Clear").Invoke(_defaultFontMap, null);
                TaleWorlds.Localization.LocalizedTextManager.LoadLocalizationXmls();
            }
            catch (Exception e)
            {
                InformationManager.ShowInquiry(new InquiryData("Except!!", e.Message, true, false, "ok", null, null, null, ""));
            }
        }
    }
}
