
using HarmonyLib;
using System;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;

namespace MBKoreanFont
{
    [HarmonyPatch]
    public static class EditableTextWidgetModule
    {
        [HarmonyPatch(typeof(EditableTextWidget), "OnMousePressed")]
        static void Prefix(Object __instance)
        {
           
            Type instType = AccessTools.TypeByName("TaleWorlds.GauntletUI.EditableTextWidget");
            Traverse t = Traverse.Create(__instance);
            Brush brush = t.Field("_brush").GetValue<Brush>();
            if (brush == null)
            {
                Bannersample.Log("_poachersParty NULL!"); 
            }
            else
            {
                InformationManager.DisplayMessage(new InformationMessage("Mouse Focused Editable Text."));
                brush.Font = MBKoreanFont.MBKoreanFontSubModule.font;
            }


        }
    }
}