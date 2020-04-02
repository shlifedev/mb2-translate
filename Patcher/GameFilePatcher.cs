using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class GameFilePatcher
    {

        static void PatchLangXML()
        {
            try
            {
                Console.WriteLine("Start Patch SandBox Language XML..");
                var installPath = Config.MB_INSTALL_PATH;
                var fullPath = installPath + "GUI/GauntletUI/Fonts/Languages.xml";
                System.IO.File.WriteAllText(fullPath, PatchText.LANGUAGE_XML);
            }
            catch (Exception e)
            {

            }
        }
         
        static void PatchSandBox()
        {
            try
            {
                Console.WriteLine("Start Patch SandBox SubModule..");
                var sandboxPath = Config.MB_SANDBOX_PATH;
                var fullPath = sandboxPath + "SubModule.xml";
                System.IO.File.WriteAllText(fullPath, PatchText.SANDBOX_SUB_XML);
            }
            catch (Exception e)
            {

            }
        }
        public static void Patch()
        {
            PatchSandBox();
            PatchLangXML(); 
        }
    }
}
