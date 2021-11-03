using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public class ColorProcessing
    {
        public static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static Color GetColorFromHue(int h)
        {
            h = MissingMath.Clamp(h, 0, 360);

            int r, g, b;

            int diff = (int)Math.Floor(255 * (((double)h / 60) - h / 60));

            if (h < 60)
            {
                r = 255;
                g = diff;
                b = 0;
            }
            else if (h < 120)
            {
                r = 255 - diff;
                g = 255;
                b = 0;
            }
            else if (h < 180)
            {
                r = 0;
                g = 255;
                b = diff;
            }
            else if (h < 240)
            {
                r = 0;
                g = 255 - diff;
                b = 255;
            }
            else if (h < 300)
            {
                r = diff;
                g = 0;
                b = 255;
            }
            else if (h < 360)
            {
                r = 255;
                g = 0;
                b = 255 - diff;
            }
            else
            {
                r = 255;
                g = 0;
                b = 0;
            }

            return Color.FromArgb(r, g, b);
        }
    }
}
