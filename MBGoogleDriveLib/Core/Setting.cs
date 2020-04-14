using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class Setting
{
    static GlobalConfig.Config config = null;
    public static GlobalConfig.Config Config
    {
        get
        {
            if (config == null)
            { 
                GlobalConfig.Config.Load(config, "config.txt");
            }
            if( config == null)
            {
                config = new GlobalConfig.Config();
            }
            return config;
        }
    }
}
