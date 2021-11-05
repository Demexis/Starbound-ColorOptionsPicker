using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public static class MissingMath
    {
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static bool IsInsideCircle(int x, int y, int width, int height)
        {
            float radius = height / 2f;

            Point center = new Point(width / 2, height / 2);

            return (Math.Pow(x - center.X, 2) + Math.Pow(y - center.Y, 2) <= Math.Pow(radius, 2));
        }
    }
}
