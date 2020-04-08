
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.TwoDimension;
using ITexture = TaleWorlds.TwoDimension.ITexture;
using IResourceContext = TaleWorlds.TwoDimension.ITwoDimensionResourceContext;
using EngineTex = TaleWorlds.Engine.Texture;
using GameTex = TaleWorlds.TwoDimension.Texture;
using TaleWorlds.Core;
using System;
using System.Linq;
using HarmonyLib;
using Module = TaleWorlds.MountAndBlade.Module;
using System.Xml;
using System.Runtime.InteropServices;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;

/// <summary>
/// 모드파일/소스코드 무단수정 배포 금지합니다.
/// Writer : shlifedev@gmail.com 
/// </summary>
namespace MBKoreanFont
{
    /// <summary>
    /// Special Thanks : Akintos  
    /// </summary>
    public class MBKoreanFontSubModule : MBSubModuleBase 
    {
        [DllImport("Rgl.dll", EntryPoint = "?toggle_imgui_console_visibility@rglCommand_line_manager@@QEAAXXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern void toggle_imgui_console_visibility(UIntPtr x);

        public static ModuleConfig config = new ModuleConfig();
        public static string ModulePath
        { 
            get { return $"../../Modules/{ModuleName}/"; } 
        }
        private static Dictionary<string, Dictionary<string, Font>> LocalizationMap;
        private static Dictionary<string, Font> DefaultFontMap;
        private static bool legit = false;
        /// <summary>
        /// Your Font File Name. (xxx.png, xxx.fnt)
        /// </summary>
        public static string FontName = "kor";
        /// <summary>
        /// Xml Key Value.
        /// </summary>
        public static string XMLKey = "한국어";
        /// <summary>
        /// Cover Target.
        /// </summary>
        public static string CoverFontName = "simkai";
        /// <summary>
        /// Module Name.
        /// </summary>
        public static string ModuleName = "MBKoreanFont";
        /// <summary>
        /// SinceUp Time.
        /// </summary>
        private float _gameUpTime = 0;

        public static bool FontLoaded { get; set; }
        public static Font font;

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("[KoreanModule] Korean Mod Loaded! by.https://cafe.naver.com/warband", Color.FromUint(4282569842U)));
            InformationManager.DisplayMessage(new InformationMessage("[KoreanModule] Develop Console :  CTRL + ` ", Color.FromUint(4282569842U)));
        } 
        /* Load For Late Loaded FontMap Datas. */
        protected override void OnApplicationTick(float dt)
        {
            _gameUpTime += dt; 
            ScreenBase topScreen = ScreenManager.TopScreen;
            if (topScreen == null || !topScreen.DebugInput.IsControlDown() || !topScreen.DebugInput.IsKeyPressed(InputKey.Tilde))
                return;
             
            toggle_imgui_console_visibility(new UIntPtr(1U));
        }
        public static void LoadFontFromModule()
        {
            if (IsLegitPlayer())
            {
                //load texture
                GameTex texture = new GameTex((ITexture) new EngineTexture(EngineTex.CreateTextureFromPath($"../../Modules/{ModuleName}/Font", $"{FontName}.png")));

                //set temporary sprite data
                SpriteData spriteData = new SpriteData("Font Atlas");
                SpriteCategory category = new SpriteCategory($"{FontName}", spriteData, 1);
                SpritePart spritePart = new SpritePart($"{FontName}", category, texture.Width, texture.Height);
                SpriteGeneric spriteGeneric = new SpriteGeneric($"{FontName}", spritePart);

                // set sprite part data. (sheetid = 1 is chinise font.)
                category.Load((IResourceContext)new TextureLoader(texture), (ResourceDepot)null);
                spritePart.SheetID = 1;
                spritePart.SheetX = 0;
                spritePart.SheetY = 0;
                spriteData.SpriteNames.Add($"{FontName}", (Sprite)spriteGeneric);

                Font font = new Font($"{FontName}", $"../../Modules/{ModuleName}/Font/{FontName}.fnt", spriteData);
                /* reflection font factory */
                Dictionary<string, Font> _bitmapFont =
                typeof (FontFactory).GetField("_bitmapFonts", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue((object) UIResourceManager.FontFactory)
                as Dictionary<string, Font>;


                Dictionary<string, Dictionary<string, Font>> _localizationMap =
                typeof (FontFactory).GetField("_fontLocalizationMap", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue((object) UIResourceManager.FontFactory)
                as Dictionary<string, Dictionary<string, Font>>;
                _bitmapFont[CoverFontName] = font;



                Dictionary<string, Font> _defaultFontMap =
                typeof (FontFactory).GetField("_fontLocalizationMap", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue((object) UIResourceManager.FontFactory) as Dictionary<string, Font>;



                LocalizationMap = _localizationMap;
                /* cover font data. */
                foreach (string index in new List<string>((IEnumerable<string>)_localizationMap[XMLKey].Keys))
                    _localizationMap[XMLKey][index] = font;

                /* apply */
                UIResourceManager.FontFactory.DefaultFont = font;
                FontLoaded = true;
            }
            else
            {
                InformationManager.ShowInquiry(new  InquiryData("Module Load Faile!", "failed load module.", true, false, ":(", null, null, null));
            }
        }
        /// <summary>
        /// You Bokdol..?
        /// </summary>
        /// <returns></returns>
        public static bool IsLegitPlayer()
        {
            return true;
            try
            {
                if (legit == true) return true;
                var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", null);
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return false;
                }
                var ret =  value + @"\steamapps\";
                var di = new System.IO.DirectoryInfo(ret);
                var bannerlordACF = di.GetFiles("*.acf").Where(x=>x.Name.Contains("261550")).First();
                if (bannerlordACF != null)
                {
                    legit = true;
                    return legit;
                }
            }
            catch (Exception e)
            {
                return legit;
            }
            return legit;
        }
        public void AddStartMenu(string name, System.Action callback)
        {
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption(name,
    new TextObject(name, null),
    9990,
    () =>
    {
        callback();
    },
    false));
        }

  

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            LoadFontFromModule();
            Harmony harmony = new Harmony("de.schplorg.bannerfix");
            harmony.PatchAll(); 
            if (FontLoaded)
            {
                AddStartMenu("최신번역다운", () =>
                {
                    if(UIResourceManager.FontFactory.CurrentLangageID != "한국어")
                    {
                        InformationManager.DisplayMessage(new InformationMessage("옵션에서 한국어로 바꾼 후 시도하세요1"));
                        return;
                    }
                    try
                        {
                            InformationManager.ShowInquiry(new InquiryData("번역 업데이트", "최신 한국어 번역 파일을 다운로드 받겠습니까?\n구글 앱 로그인이 필요합니다.", true, true, "Yes", "No", () =>
                        {
                            MBKoreanFont.Translate.TranslateUtility.DownloadLatestTranslate();
                            MBKoreanFont.Translate.TranslateUtility.ReloadTranslate();
                        }, () =>
                        {

                        }, ""));
                        InformationManager.ShowInquiry(new InquiryData("번역 파일 다운로드 완료", "게임을 재접속 할 필요는 없습니다. 즐기세요!", true, false, "Thank", null, null, null, ""));
                    }
                    catch (Exception e)
                    {
                        InformationManager.ShowInquiry(new InquiryData("실패 사유는 아래와 같습니다.", "1. 개발자가 막아둔경우.\n2.시트에오류가 있는경우.\n3.구글 트래픽 제한 (잠시후 다시시도)", true, false, "sorry..", null, null, null, ""));
                        InformationManager.ShowInquiry(new InquiryData("실패! 개발자에게 메세지를 제보하세요", "실패 => " + e.Message, true, false, "sorry..", null, null, null, ""));
                    }

                });
            }
             
        }

    }

}



