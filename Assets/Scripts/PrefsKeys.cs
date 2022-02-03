using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PrefsKeys
{
    public const string masterVolKey = "master_volume";
    public const string effectsVolKey = "effects_volume";
    public const string musicVolKey = "music_volume";
    public const string saveFileFormat = "/MirrorCollapseSaveFile";
    public static bool sceneChanged = false;
    public static bool newGame = true;

    public static ItemList inventory = new ItemList(); //Inventory between levels
    public static float interLevelHealth = 0;//Health between levels
    
}
