
using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;

namespace MBKoreanFont
{
    [HarmonyPatch]
    public static class EditableTextWidgetModule
    {
        public static System.Collections.Generic.HashSet<Object> ApplyMap = new System.Collections.Generic.HashSet<Object>();
        [HarmonyPatch(typeof(EditableTextWidget), "OnUpdate")] 
        //OnUpdate에서 호출합니다.
        static void Prefix(Object __instance)
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
    }
}