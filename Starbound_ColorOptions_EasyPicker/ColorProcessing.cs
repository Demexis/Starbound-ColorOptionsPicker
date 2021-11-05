﻿using System;
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

        public static Tuple<int, int, int> HSVToRGB(int h, int s, int v)
        {
            Color c = ColorProcessing.GetColorFromHue(h);

            int r1 = c.R;
            int g1 = c.G;
            int b1 = c.B;

            //Console.WriteLine($"Hue: {r} {g} {b}");

            // Saturation
            r1 += (int)Math.Floor((255 - r1) * (((float)100 - s) / 100));
            g1 += (int)Math.Floor((255 - g1) * (((float)100 - s) / 100));
            b1 += (int)Math.Floor((255 - b1) * (((float)100 - s) / 100));

            //Console.WriteLine($"Saturation: {r} {g} {b}");

            // Value
            r1 = (int)Math.Floor(r1 * (float)v / 100);
            g1 = (int)Math.Floor(g1 * (float)v / 100);
            b1 = (int)Math.Floor(b1 * (float)v / 100);

            //Console.WriteLine($"Value: {r} {g} {b}");

            return Tuple.Create(r1, g1, b1);
        }

        public static Tuple<int, int, int> RGBtoHSV(double r, double g, double b)
        {
            // Source: https://www.geeksforgeeks.org/program-change-rgb-color-model-hsv-color-model/

            // R, G, B values are divided by 255
            // to change the range from 0..255 to 0..1
            r = r / 255.0;
            g = g / 255.0;
            b = b / 255.0;

            // h, s, v = hue, saturation, value
            double cmax = Math.Max(r, Math.Max(g, b)); // maximum of r, g, b
            double cmin = Math.Min(r, Math.Min(g, b)); // minimum of r, g, b
            double diff = cmax - cmin; // diff of cmax and cmin.
            double hue = -1, saturation = -1;

            // if cmax and cmax are equal then h = 0
            if (cmax == cmin)
                hue = 0;

            // if cmax equal r then compute h
            else if (cmax == r)
                hue = (60 * ((g - b) / diff) + 360) % 360;

            // if cmax equal g then compute h
            else if (cmax == g)
                hue = (60 * ((b - r) / diff) + 120) % 360;

            // if cmax equal b then compute h
            else if (cmax == b)
                hue = (60 * ((r - g) / diff) + 240) % 360;

            // if cmax equal zero
            if (cmax == 0)
                saturation = 0;
            else
                saturation = (diff / cmax) * 100;

            // compute v
            double value = cmax * 100;
            //Console.WriteLine("(" + h + " " + s + " " + v + ")");

            return Tuple.Create((int)(hue), (int)(saturation), (int)(value));
        }
    }
}
