
using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
/// <summary>
/// 모드파일/소스코드 무단수정 배포 금지합니다.
/// Writer : shlifedev@gmail.com 
/// </summary>
namespace MBKoreanFont
{
    [HarmonyPatch]
    public static class EditableTextWidgetModule
    {
        public static System.Collections.Generic.HashSet<Object> ApplyMap = new System.Collections.Generic.HashSet<Object>();
        static bool FirstCall = false;
        [HarmonyPatch(typeof(UIResourceManager), "OnLanguageChange")]
        static void Postfix()
        {
            if (MBKoreanFontSubModule.FontLoaded)
            {
                try
                {
                    if (UIResourceManager.FontFactory.CurrentLangageID == "한국어" || UIResourceManager.FontFactory.CurrentLangageID == "English")
                    {
                        InformationManager.ClearAllMessages();
                        InformationManager.DisplayMessage(new InformationMessage("[KOR Mod] 폰트가 깨지면 게임을 재시작하세요."));
                        UIResourceManager.FontFactory.GetType().GetProperty("CurrentLangageID").SetValue(UIResourceManager.FontFactory, "한국어");
                        MBKoreanFontSubModule.LoadFontFromModule();
                    }
                    else
                    {
                        InformationManager.ClearAllMessages();
                        InformationManager.DisplayMessage(new InformationMessage("[KOR Mod] Sorry, Korean mod support only 'english or korean' font. ", new TaleWorlds.Library.Color(1, 0, 0, 1)));
                    }
                }
                catch (Exception e)
                {
                    InformationManager.DisplayMessage(new InformationMessage(e.Message));
                }
            }
        } 
         
        [HarmonyPatch(typeof(EditableTextWidget), "UpdateFontData")] 
        static void Prefix(Object __instance)
        {
            try
            {
                Type instType = AccessTools.TypeByName("TaleWorlds.GauntletUI.EditableTextWidget");
                Traverse t = Traverse.Create(__instance);
                Brush _brush = t.Field("_brush").GetValue<Brush>();
                if (ApplyMap.Contains(__instance) == false)
                {
                    if (_brush == null)
                    {

                    }
                    else
                    {
                        // InformationManager.DisplayMessage(new InformationMessage("Loaded EditableTextWidgetModule."));
                        _brush.Font = MBKoreanFont.MBKoreanFontSubModule.font;
                        ApplyMap.Add(__instance);
                    }
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}