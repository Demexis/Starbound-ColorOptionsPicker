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
        public static string ApplicationVersion = "1.0";
        //public static int NumberOfCores = Environment.ProcessorCount;

        public static byte TransparencyCut = 255;

        public static Color SpriteBackColor = SystemColors.ButtonShadow;

        public static bool IgnoreHeadFiles = false, IgnoreChestFiles = false, IgnoreLegsFiles = false, IgnoreBackFiles = false;
        public static bool IgnoreMasks = false;

        //public static bool GenerateComments = false;
    }
}
