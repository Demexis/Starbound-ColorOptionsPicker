using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker.UserControls
{
    public partial class ColorCircle : UserControl
    {
        private float _selectPointCircleRadius = 6;

        private bool _selectColorCircleFlag = false;

        private Bitmap colorCircleBitmap;

        public delegate void OnChangingHS(int hue, int saturation);
        public OnChangingHS OnHueAndSaturationChange;

        private Color _currentColor;
        public Color CurrentColor 
        {
            get => _currentColor; 
            set
            {
                _currentColor = value;

                DrawColorCircle();
                DrawSelectPoint(GetPointOnCircleFromColor(value));
            }
        }

        public ColorCircle()
        {
            InitializeComponent();

            colorCircleBitmap = GenerateColorCircleBitmap();
        }

        private Bitmap GenerateColorCircleBitmap()
        {
            float radius = pictureBox_ColorCircle.Height / 2;

            Point center = new Point(pictureBox_ColorCircle.Width / 2, pictureBox_ColorCircle.Height / 2);

            Bitmap circleBitmap = new Bitmap(pictureBox_ColorCircle.Width, pictureBox_ColorCircle.Height);

            for (int i = 0; i < pictureBox_ColorCircle.Width; i++)
            {
                for (int j = 0; j < pictureBox_ColorCircle.Height; j++)
                {
                    if (Math.Pow(i - center.X, 2) + Math.Pow(j - center.Y, 2) <= Math.Pow(radius, 2))
                    {
                        circleBitmap.SetPixel(i, j, GetColorFromCirclePoint(i, j));
                    }
                }
            }

            return circleBitmap;
        }

        public void RegenerateBitmap()
        {
            colorCircleBitmap = GenerateColorCircleBitmap();
        }

        private void OnSelectPointChange(Point mousePosRelativeToControl)
        {
            Color c = GetColorFromCirclePoint(mousePosRelativeToControl.X, mousePosRelativeToControl.Y);
            CurrentColor = c;

            Tuple<int, int, int> hsv = ColorProcessing.RGBtoHSV(c.R, c.G, c.B);

            OnHueAndSaturationChange?.Invoke(hsv.Item1, hsv.Item2);
        }

        private void DrawColorCircle()
        {
            if(!pictureBox_ColorCircle.Size.Equals(colorCircleBitmap.Size))
            {
                RegenerateBitmap();
            }

            Bitmap circleBitmap = colorCircleBitmap.Clone(new Rectangle(0, 0, colorCircleBitmap.Width, colorCircleBitmap.Height),
                                                          colorCircleBitmap.PixelFormat);

            pictureBox_ColorCircle.Image = circleBitmap;
        }

        private void DrawSelectPoint(Point mousePos)
        {
            Bitmap bitmap = (Bitmap)pictureBox_ColorCircle.Image;

            bool outOfBounds = false;

            float radius = _selectPointCircleRadius;

            for (int i = 0; i < 360; i += 6)
            {
                double theta = i * Math.PI / 180;
                int dx = (int)(radius * Math.Cos(theta));
                int dy = (int)(radius * Math.Sin(theta));

                outOfBounds = (mousePos.X + dx < 0 || mousePos.X + dx >= pictureBox_ColorCircle.Width)
                    || (mousePos.Y + dy < 0 || mousePos.Y + dy >= pictureBox_ColorCircle.Height);

                if (!outOfBounds)
                {
                    bitmap.SetPixel(mousePos.X + dx, mousePos.Y + dy, Color.Black);
                }
            }

            outOfBounds = (mousePos.X < 0 || mousePos.X >= pictureBox_ColorCircle.Width)
                    || (mousePos.Y < 0 || mousePos.Y >= pictureBox_ColorCircle.Height);

            if (!outOfBounds)
            {
                bitmap.SetPixel(mousePos.X, mousePos.Y, Color.Black);
            }

            pictureBox_ColorCircle.Image = bitmap;
        }


        private Color GetColorFromCirclePoint(int x, int y)
        {
            if (ColorCircle.IsInsideCircle(x, y, pictureBox_ColorCircle.Width))
            {
                float radius = pictureBox_ColorCircle.Width / 2f;
                Point center = new Point(pictureBox_ColorCircle.Width / 2, pictureBox_ColorCircle.Height / 2);

                double radians = Math.Atan2(y - center.Y, x - center.X);

                int hue = (int)((radians) / (2 * Math.PI) * 360);
                if (hue < 0) hue += 360;

                Color c = ColorProcessing.GetColorFromHue(hue);

                double inner_radius = Math.Sqrt(Math.Pow(x - center.X, 2) + Math.Pow(y - center.Y, 2));

                int red = c.R + (int)Math.Floor((255 - c.R) * (1.0f - (inner_radius / radius)));
                int green = c.G + (int)Math.Floor((255 - c.G) * (1.0f - (inner_radius / radius)));
                int blue = c.B + (int)Math.Floor((255 - c.B) * (1.0f - (inner_radius / radius)));

                c = Color.FromArgb(red, green, blue);

                return c;
            }
            else
            {
                return CurrentColor;
            }
        }

        private Point GetPointOnCircleFromColor(Color c)
        {
            Tuple<int, int, int> hsv = ColorProcessing.RGBtoHSV(c.R, c.G, c.B);

            Point center = new Point(pictureBox_ColorCircle.Width / 2, pictureBox_ColorCircle.Height / 2);

            double radius = hsv.Item2 / 100f * pictureBox_ColorCircle.Width / 2f;

            double radians = hsv.Item1 / 360f * Math.PI * 2;
            double degrees = radians * (180 / Math.PI);

            double theta = degrees * Math.PI / 180;
            int dx = (int)(radius * Math.Cos(theta));
            int dy = (int)(radius * Math.Sin(theta));

            return new Point(dx + center.X, dy + center.Y);
        }

        /* --- EVENTS --- */
        private void pictureBox_ColorCircle_MouseDown(object sender, MouseEventArgs e)
        {
            _selectColorCircleFlag = true;
        }

        private void pictureBox_ColorCircle_MouseUp(object sender, MouseEventArgs e)
        {
            _selectColorCircleFlag = false;
        }

        private void pictureBox_ColorCircle_Click(object sender, EventArgs e)
        {
            Point mousePosRelativeToControl = ((Control)sender).PointToClient(System.Windows.Forms.Cursor.Position);

            if (ColorCircle.IsInsideCircle(mousePosRelativeToControl.X, mousePosRelativeToControl.Y, ((Control)sender).Width))
            {
                OnSelectPointChange(mousePosRelativeToControl);
            }
        }

        private void pictureBox_ColorCircle_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_selectColorCircleFlag)
            {
                pictureBox_ColorCircle_Click(sender, e);
            }
        }

        public static bool IsInsideCircle(int x, int y, int diameter)
        {
            float radius = diameter / 2f;

            Point center = new Point(diameter / 2, diameter / 2);

            return (Math.Pow(x - center.X, 2) + Math.Pow(y - center.Y, 2) <= Math.Pow(radius, 2));
        }
    }
}
