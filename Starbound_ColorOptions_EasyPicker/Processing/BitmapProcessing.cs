using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public class BitmapProcessing
    {
        public static Bitmap GetInterpolatedBitmap(Bitmap original, Size desiredSize)
        {
            Bitmap result = new Bitmap(desiredSize.Width, desiredSize.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                Point[] dest =
                {
                    new Point(0, 0),
                    new Point(desiredSize.Width, 0),
                    new Point(0, desiredSize.Height),
                };
                Rectangle source = new Rectangle(0, 0, original.Width, original.Height);

                g.DrawImage(original, dest, source, GraphicsUnit.Pixel);
            }
            return result;
        }

        public static Bitmap GetMergedBitmaps(params Bitmap[] bitmaps)
        {
            Point maxSize = Point.Empty;
            foreach(Bitmap bitmap in bitmaps)
            {
                if(bitmap.Width > maxSize.X)
                {
                    maxSize.X = bitmap.Width;
                }

                if(bitmap.Height > maxSize.Y)
                {
                    maxSize.Y = bitmap.Height;
                }
            }

            Bitmap result = new Bitmap(maxSize.X, maxSize.Y);

            using(Graphics g = Graphics.FromImage(result))
            {
                foreach (Bitmap bitmap in bitmaps)
                {
                    Point[] dest =
                    {
                        new Point(0, 0),
                        new Point(bitmap.Width, 0),
                        new Point(0, bitmap.Height),
                    };
                    Rectangle source = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                    g.DrawImage(bitmap, dest, source, GraphicsUnit.Pixel);
                }
            }

            return result;
        }

        public static Color[] GetAllColorsFromBitmaps(params Bitmap[] sources)
        {
            List<Color> colors = new List<Color>();

            foreach(Bitmap source in sources)
            {
                for(int i = 0; i < source.Width; i++)
                {
                    for(int j = 0; j < source.Height; j++)
                    {
                        Color c = source.GetPixel(i, j);

                        if (!colors.Contains(c) && c.A != 0) // Ignore Transparent Color
                        {
                            colors.Add(c);
                        }
                    }
                }
            }

            return colors.ToArray();
        }

        public static Color[] SortColorsByValue(params Color[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = i + 1; j < colors.Length; j++)
                {
                    int iValue = GetValueFromColor(colors[i]);
                    int jValue = GetValueFromColor(colors[j]);

                    if (jValue > iValue)
                    {
                        Color temp = colors[i];
                        colors[i] = colors[j];
                        colors[j] = temp;
                    }
                }
            }

            return colors;
        }

        public static int GetValueFromColor(Color color)
        {
            int[] rgb = { color.R, color.G, color.B };

            Array.Sort(rgb);
            Array.Reverse(rgb);

            int value = rgb[0];

            return value;
        }
    }
}
