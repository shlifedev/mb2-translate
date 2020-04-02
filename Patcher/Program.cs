using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    class Program
    { 
        static void Main(string[] args)
        {  
            PrefabPatcher.PrefabPatch(Config.MB_SANDBOX_PATH);
        }
    }
}
