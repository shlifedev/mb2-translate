using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace MBKoreanFontInstallerConsole
{
    public class DownloadFile
    {
        public string fileID;
        public string savePath;
    }
    public class XMLDownloader
    {
        public List<DownloadFile> list = new List<DownloadFile>();
        public void Init()
        {

            Add("14aalpyNKgwJZnOMRvl4OHT1-7ijINF6lETJKjTfbtc8", "KR");
            Add("1CwNZax7IeK-HO-vrKyV-BPXHbmtfX59SQ9pQr9V_D9A", "KR");
            Add("1gK9eBdHPaGqRNhE4QOWqAWKT3CN8fRjMn_rtbkraG9M", "KR");
            Add("1gEN3I27_L6hIgi3RHN6_8cB-0JyO5cduaaWnW_VYwv0", "KR");
            Add("1_gJY4sWqtiKAR6iFqM69FGb1G3nT3cCjeks664Ih7ms", "KR");
            Add("1hFK6HUSTl8J5cQpwHxXymr4kPhXkM7brZ95rOn2GJxA", "KR");
            Add("1xdz2UzXf5dgV0CQHTRPdb0DiBCg51Ux1mthIkjf35gk", "KR");
            Add("18Gq5_ndaaPpASUCYtcb12VWD7kRZDLL6Z5qXX0u-H1U", "KR");
            Add("1bUhjyRFyVsbJVriDxiHbfTTrBmBWN-GEcesRdA0QpMo", "KR");
            Add("1dG0p249xgtyvgUAwzf9lWNuUGIpitw7wJzFUkUrWnvU", "KR");
            Add("1rdZuv-_bO4Ec--2xggHm6TQzBprVkomjpqO8RSMpEkU", "KR");
            Add("1VUnV1YbyjaKFir19CbdB2K93a7ak73z0h2tKBuuGvBo", "KR");
            //sandbox//

            Add("1QRNtSyoZwwj7WOGgeGc0kobH9s0NaZy36caaQad3nRU", "KR");
            Add("1MTMCMXH9nqlC3wbHC1mWrb_PNyFMoUj_Bdl5wHWkFvI", "KR");
            Add("1tQqm67i_GFXJAn6sKLyN-nafVQ_bjjIiDjcpRtSwKLw", "KR");
            Add("1Z2LpvFkYA_038Fiiv7jI6ByraGf796o-xGBgfWPw94M", "KR");
            Add("1VeKlueEPaVgi46z9L2sil10U_PBJUzFBiGPgsGi1d2o", "KR");
            Add("1JEteyU3qpuYvhXZepxrz5nhNke4FEMVsVQym8NW3KOY", "KR");
            Add("1ouqemdvldBj7xLSjGi6-byieaAbIm598gfiryEAXLT0", "KR");
            Add("1jY9-VYVhoucD6RJYXYmcjj_C23n3IE_ipGJGvApk9Ks", "KR"); 
            Add("1ms1uVQP34Vmywub8nFzXaLfKgXSIPNHTPCJLLUM39g4", "KR");
            Add("1BRoSAZYNqJAe1C-NGPmWcmcO8jct5zd0Y3qhmqo2BdI", "KR");
            Add("1nJ19laVwvwhsDO4yw5HdAx0f1q79Lp1q4R16AAvv2g4", "KR");
            Add("1Uv7c5RS3bd-aN0waURxDBJdaUeNFBdWZJgISgnHOtI8", "KR");
            Add("1z5vWrCaZZ4Jb6lSqVGAlS22Cp5RRK1jlT6jZ9-cNvy8", "KR");
            Add("1KgcK4Ct5gPN6FLwJEbLgnAPWFImAA8TqmW9AhRAxY2s", "KR");
            Add("1O_aUt0ta6Oz_MTQGNlwe6J8cGzR_p2UbxXtZyqj6cKU", "KR");
            Add("1lug3FhXuIYytL1Xqbl66vKAzZy3Ize6lRu9ybudcCVs", "KR");
            Add("1BDHokh332cMEIgu5cI8D-OiVExwATPRJCI28FrJTpNU", "KR");
            Add("1xF8pXphKmPfAUrIS759c2ny25VjMbXBejyWqZomS1sc", "KR");
            Add("1Q3PCFhyBbm4vVA9ImNzkaYeY5D9maSvVlFw_vD7-bNQ", "KR");
            Add("1YM99Wbc7O_xubs2ra1FVH0uXBR5S7L10eACAPF8Tce4", "KR");
            Add("1anH8VYqzwW_Xom7PdnF31dVWha2EytCAYAueqGSDERQ", "KR"); 

        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }


        public void DownloadAll()
        {
            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

            foreach (var data in list)
            { 
                var fileName = DriveManager.GetFileName(data.fileID);
                var xmlData = DriveManager.DownloadXML(data.fileID);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{fileName} Downloaded!");
                Console.ForegroundColor = ConsoleColor.White;
                System.IO.Directory.CreateDirectory(data.savePath);
                System.IO.File.WriteAllText(data.savePath+"/"+fileName+".xml", xmlData); 
                try
                {
                 
                    XmlDocument document = new XmlDocument();
                    document.Load(data.savePath + "/" + fileName + ".xml");
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(fileName);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" File Has Error!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
           
            }
        }
        public void Add(string id, string savePath)
        {
            list.Add(new DownloadFile()
            {
                fileID = id,
                savePath = savePath
            });
        }
    }
}
