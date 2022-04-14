using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public static class AppPreferences
    {
        public static string ApplicationName = "SB Color Options Picker";
        public static string ApplicationVersion = "1.1";
        //public static int NumberOfCores = Environment.ProcessorCount;

        public static byte TransparencyCut = 255;

        public static Color SpriteBackColor = SystemColors.ButtonShadow;

        public static bool IgnoreHeadFiles = false, IgnoreChestFiles = false, IgnoreLegsFiles = false, IgnoreBackFiles = false;
        public static bool IgnoreMasks = false;

        //public static bool GenerateComments = false;

        // Importing Color Options Preferences

        public static Dictionary<Rules.ColorOptions, bool> ImportColorOptions = new Dictionary<Rules.ColorOptions, bool>()
        {
            [Rules.ColorOptions.Default] = true,
            [Rules.ColorOptions.Black] = true,
            [Rules.ColorOptions.Grey] = true,
            [Rules.ColorOptions.White] = true,
            [Rules.ColorOptions.Red] = true,
            [Rules.ColorOptions.Orange] = true,
            [Rules.ColorOptions.Yellow] = true,
            [Rules.ColorOptions.Green] = true,
            [Rules.ColorOptions.Blue] = true,
            [Rules.ColorOptions.Purple] = true,
            [Rules.ColorOptions.Pink] = true,
            [Rules.ColorOptions.Brown] = true
        };

        public static Dictionary<Rules.ColorOptions, bool> ExportColorOptions = new Dictionary<Rules.ColorOptions, bool>()
        {
            [Rules.ColorOptions.Default] = true,
            [Rules.ColorOptions.Black] = true,
            [Rules.ColorOptions.Grey] = true,
            [Rules.ColorOptions.White] = true,
            [Rules.ColorOptions.Red] = true,
            [Rules.ColorOptions.Orange] = true,
            [Rules.ColorOptions.Yellow] = true,
            [Rules.ColorOptions.Green] = true,
            [Rules.ColorOptions.Blue] = true,
            [Rules.ColorOptions.Purple] = true,
            [Rules.ColorOptions.Pink] = true,
            [Rules.ColorOptions.Brown] = true
        };
    }
}
