using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    class Program
    { 
        static void StartPatch()
        {

            GameFilePatcher.Patch();
            PrefabPatcher.PrefabUnPatch(); 
        }
        static void Main(string[] args)
        {
            StartPatch();
        }
    }
}
