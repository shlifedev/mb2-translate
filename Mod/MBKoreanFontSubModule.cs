
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

namespace MBKoreanFont
{
    /// <summary>
    /// Special Thanks : Akintos  
    /// </summary>
    public class MBKoreanFontSubModule : MBSubModuleBase
    {
        private static Harmony harmony = new Harmony("de.schplorg.bannerfix");

        /// <summary>
        /// Your Font File Name. (xxx.png, xxx.fnt)
        /// </summary>
        public readonly string FontName = "kor";
        /// <summary>
        /// Xml Key Value.
        /// </summary>
        public readonly string XMLKey = "한국어";
        /// <summary>
        /// Cover Target.
        /// </summary>
        public readonly string CoverFontName = "simkai";
        /// <summary>
        /// Module Name.
        /// </summary>
        public readonly string ModuleName = "MBKoreanFont";
        /// <summary>
        /// SinceUp Time.
        /// </summary>
        private float _gameUpTime = 0;

        public Font font;

        /* Load For Late Loaded FontMap Datas. */
        protected override void OnApplicationTick(float dt)
        {
            _gameUpTime += dt;
            if (_gameUpTime >= 5)
            {
                LoadFontFromModule();
                _gameUpTime = 0;
            }
        }
        public override void OnCampaignStart(Game game, object starterObject)
        {
            base.OnCampaignStart(game, starterObject);

        }

        private void PostLoad()
        {

        }
        private void PrefixLoad()
        {


        }

        public void LoadFontFromModule()
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

                /* cover font data. */
                foreach (string index in new List<string>((IEnumerable<string>)_localizationMap[XMLKey].Keys))
                    _localizationMap[XMLKey][index] = font;

                /* apply */
                UIResourceManager.FontFactory.DefaultFont = font;
            }
            else
            {
                InformationManager.DisplayMessage(new InformationMessage("what the bok dol bok d  o  l    d    . . .           ."));
            }
        }


        private static bool legit = false;
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
                var value = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath", "i hate bokdol. i will kill you..");
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

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            LoadFontFromModule(); 
            DetailFontChanger();  
        }

        private void PatchAssembly()
        {
            var mOriginal = AccessTools.Method(typeof(EditableTextWidget), "OnMousePressed");
            var mPrefix = AccessTools.Method(typeof(MBKoreanFontSubModule), "Prefix");

            if (mOriginal != null)
            {
                InformationManager.DisplayMessage(new InformationMessage("original n null"));
            }

            if (mPrefix != null)
            {
                InformationManager.DisplayMessage(new InformationMessage("finalize n null"));
            }
             harmony.Patch(mOriginal, null, null, null, new HarmonyMethod(mPrefix));
        }
        public void DetailFontChanger()
        {
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Message",
            new TextObject("하모니 적용", null),
            9990,
            () =>
            {
                PatchAssembly();
            },
false));
        }

        public void Prefix(Object __instance)
        {
            //Type instType = AccessTools.TypeByName("TaleWorlds.GauntletUI.EditableTextWidget");
            //Traverse t = Traverse.Create(__instance);
            //Brush brush = t.Field("_brush").GetValue<Brush>();
            //if (brush == null)
            //{
            //    Bannersample.Log("_poachersParty NULL!");

            //}
            //else
            //{
            //    brush.Font = font;
            //}
        }

    }
}