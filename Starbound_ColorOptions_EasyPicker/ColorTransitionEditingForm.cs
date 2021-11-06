using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker
{
    public partial class ColorTransitionEditingForm : Form
    {
        private MainForm _parent;

        private List<ListViewItem> _items = new List<ListViewItem>();
        private Color[] _originalColors;

        private int r, g, b, h, s, v;

        private float _selectPointCircleRadius = 6;

        private bool _saveFlag = false;

        private bool _selectColorCircleFlag = false;

        private Bitmap colorCircleBitmap;

        public bool TryRemoveListViewItems(params ListViewItem[] items)
        {
            bool atLeastOneRemoved = false;

            foreach(ListViewItem item in items)
            {
                if(_items.Contains(item))
                {
                    item.SubItems[2].BackColor = Color.Transparent;
                    item.SubItems[2].Text = string.Empty;
                    _items.Remove(item);
                    atLeastOneRemoved = true;
                }
            }

            return atLeastOneRemoved;
        }

        private void trackBar_Hue_Scroll(object sender, EventArgs e)
        {
            h = trackBar_Hue.Value;

            OnHSVTrackBarChanged();
        }

        private void trackBar_Saturation_Scroll(object sender, EventArgs e)
        {
            s = trackBar_Saturation.Value;

            OnHSVTrackBarChanged();
        }

        private void trackBar_Value_Scroll(object sender, EventArgs e)
        {
            v = trackBar_Value.Value;

            OnHSVTrackBarChanged();
        }

        private void trackBar_Red_Scroll(object sender, EventArgs e)
        {
            r = trackBar_Red.Value;

            OnRGBTrackBarChanged();
        }

        private void trackBar_Green_Scroll(object sender, EventArgs e)
        {
            g = trackBar_Green.Value;

            OnRGBTrackBarChanged();
        }
        private void trackBar_Blue_Scroll(object sender, EventArgs e)
        {
            b = trackBar_Blue.Value;

            OnRGBTrackBarChanged();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            _saveFlag = true;
            this.Close();
        }


        private void OnRGBTrackBarChanged()
        {
            Tuple<int, int, int> hsv = ColorProcessing.RGBtoHSV(r, g, b);

            h = hsv.Item1;
            s = hsv.Item2;
            v = hsv.Item3;

            OnTrackBarValueChange();
        }

        private void OnHSVTrackBarChanged()
        {
            Tuple<int, int, int> rgb = ColorProcessing.HSVToRGB(h, s, v);
            r = rgb.Item1;
            g = rgb.Item2;
            b = rgb.Item3;

            OnTrackBarValueChange();
        }

        private void textBox_Red_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_Red.Focused) return;

            try
            {
                int rParse = int.Parse(textBox_Red.Text);
                rParse = MissingMath.Clamp(rParse, 0, 255);
                r = rParse;
            }
            catch (Exception ex) { }

            OnRGBTrackBarChanged();
        }

        private void textBox_Green_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_Green.Focused) return;

            try
            {
                int gParse = int.Parse(textBox_Green.Text);
                gParse = MissingMath.Clamp(gParse, 0, 255);
                g = gParse;
            }
            catch (Exception ex) { }

            OnRGBTrackBarChanged();
        }

        private void textBox_Blue_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_Blue.Focused) return;

            try
            {
                int bParse = int.Parse(textBox_Blue.Text);
                bParse = MissingMath.Clamp(bParse, 0, 255);
                b = bParse;
            }
            catch (Exception ex) { }

            OnRGBTrackBarChanged();
        }

        private void textBox_Hue_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_Hue.Focused) return;

            try
            {
                int hParse = int.Parse(textBox_Hue.Text);
                hParse = MissingMath.Clamp(hParse, 0, 360);
                h = hParse;
            }
            catch (Exception ex) { }

            OnHSVTrackBarChanged();
        }

        private void textBox_Saturation_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_Saturation.Focused) return;

            try
            {
                int sParse = int.Parse(textBox_Saturation.Text);
                sParse = MissingMath.Clamp(sParse, 0, 100);
                s = sParse;
            }
            catch (Exception ex) { }

            OnHSVTrackBarChanged();
        }

        private void textBox_Value_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_Value.Focused) return;

            try
            {
                int vParse = int.Parse(textBox_Value.Text);
                vParse = MissingMath.Clamp(vParse, 0, 100);
                v = vParse;
            }
            catch (Exception ex) { }

            OnHSVTrackBarChanged();
        }

        private void OnTrackBarValueChange()
        {
            trackBar_Red.Value = r;
            textBox_Red.Text = r.ToString();

            trackBar_Green.Value = g;
            textBox_Green.Text = g.ToString();

            trackBar_Blue.Value = b;
            textBox_Blue.Text = b.ToString();


            trackBar_Hue.Value = h;
            textBox_Hue.Text = h.ToString();

            trackBar_Saturation.Value = s;
            textBox_Saturation.Text = s.ToString();

            trackBar_Value.Value = v;
            textBox_Value.Text = v.ToString();

            Color c = Color.FromArgb(r, g, b);
            
            DrawSelectPointOnColorCircle(GetPointOnCircleFromColor(c), 5);

            this.pictureBox_Edited.BackColor = c;

            foreach (ListViewItem item in _items)
            {
                item.SubItems[3].BackColor = c;
                item.SubItems[4].Text = ColorProcessing.HexConverter(c);
            }
        }

        public ColorTransitionEditingForm(MainForm parent, ListViewItem[] items)
        {
            InitializeComponent();

            colorCircleBitmap = GetColorCircleBitmap();

            this._parent = parent;
            this._items = items.ToList();

            List<Color> colors = new List<Color>();
            foreach(ListViewItem item in items)
            {
                colors.Add(item.SubItems[3].BackColor);
                item.SubItems[2].BackColor = Color.FromArgb(255, 212, 127);
                item.SubItems[2].Text = "E";
            }
            _originalColors = colors.ToArray();

            //Color c = this._items[0].SubItems[3].BackColor;

            Bitmap bitmap = new Bitmap(pictureBox_Original.Width, pictureBox_Original.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < colors.Count; i++)
                {
                    Color customColor = Color.FromArgb(colors[i].R, colors[i].G, colors[i].B);
                    SolidBrush shadowBrush = new SolidBrush(customColor);
                    g.FillRectangle(shadowBrush, new Rectangle(0, (pictureBox_Original.Height / colors.Count) * i, pictureBox_Original.Width, (pictureBox_Original.Height / colors.Count)));
                }
            }


            this.pictureBox_Original.Image = bitmap;

            r = colors[0].R;
            g = colors[0].G;
            b = colors[0].B;

            OnRGBTrackBarChanged();
            DrawSelectPointOnColorCircle(GetPointOnCircleFromColor(colors[0]), _selectPointCircleRadius);

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.options_icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());
        }

        private void pictureBox_ColorCircle_Click(object sender, EventArgs e)
        {
            Point mousePosRelativeToControl = ((Control)sender).PointToClient(System.Windows.Forms.Cursor.Position);

            if (MissingMath.IsInsideCircle(mousePosRelativeToControl.X, mousePosRelativeToControl.Y, ((Control)sender).Width, ((Control)sender).Height))
            {
                OnSelectPointChange(mousePosRelativeToControl);
                DrawSelectPointOnColorCircle(mousePosRelativeToControl, _selectPointCircleRadius);
            }
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(_selectColorCircleFlag)
            {
                Point mousePosRelativeToControl = ((Control)sender).PointToClient(System.Windows.Forms.Cursor.Position);

                if (MissingMath.IsInsideCircle(mousePosRelativeToControl.X, mousePosRelativeToControl.Y, ((Control)sender).Width, ((Control)sender).Height))
                {
                    OnSelectPointChange(mousePosRelativeToControl);
                    DrawSelectPointOnColorCircle(mousePosRelativeToControl, _selectPointCircleRadius);
                }
            }
        }

        private void OnSelectPointChange(Point mousePosRelativeToControl)
        {
            Color c = GetColorFromCirclePoint(mousePosRelativeToControl.X, mousePosRelativeToControl.Y);

            Tuple<int, int, int> hsv = ColorProcessing.RGBtoHSV(c.R, c.G, c.B);

            h = hsv.Item1;
            s = hsv.Item2;
            Console.WriteLine("Value: " + v.ToString());
            //v = v; // Value isn't changing

            OnHSVTrackBarChanged();
        }

        private void DrawSelectPointOnColorCircle(Point mousePos, float radius)
        {
            // TODO >: Remove selection out of circle

            DrawColorCircle();

            Bitmap bitmap = (Bitmap)pictureBox_ColorCircle.Image;

            bool outOfBounds = false;

            for (int i = 0; i < 360; i += 6)
            {
                double theta = i * Math.PI / 180;
                int dx = (int)(radius * Math.Cos(theta));
                int dy = (int)(radius * Math.Sin(theta));

                outOfBounds = (mousePos.X + dx < 0 || mousePos.X + dx >= pictureBox_ColorCircle.Width)
                    || (mousePos.Y + dy < 0 || mousePos.Y + dy >= pictureBox_ColorCircle.Height);

                if(!outOfBounds)
                {
                    bitmap.SetPixel(mousePos.X + dx, mousePos.Y + dy, Color.Black);
                }

                //Console.WriteLine(dx.ToString() + " " + dy.ToString());
            }

            outOfBounds = (mousePos.X < 0 || mousePos.X >= pictureBox_ColorCircle.Width)
                    || (mousePos.Y < 0 || mousePos.Y >= pictureBox_ColorCircle.Height);

            if (!outOfBounds)
            {
                bitmap.SetPixel(mousePos.X, mousePos.Y, Color.Black);
            }

            pictureBox_ColorCircle.Image = bitmap;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _selectColorCircleFlag = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _selectColorCircleFlag = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(!_saveFlag)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i].SubItems[3].BackColor = _originalColors[i];
                    _items[i].SubItems[4].Text = ColorProcessing.HexConverter(_originalColors[i]);
                    AsyncRemoveMarker(Color.Red);
                }
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    AsyncRemoveMarker(Color.LightGreen);

                    if (_items[i].SubItems[3].BackColor != _originalColors[i])
                    {
                        _parent.OnChangeDone();
                    }
                }
            }

            base.OnClosing(e);
        }

        private void pictureBox_Original_Click(object sender, EventArgs e)
        {
            Point mousePosRelativeToControl = ((Control)sender).PointToClient(System.Windows.Forms.Cursor.Position);

            Color c = ((Bitmap)pictureBox_Original.Image).GetPixel(mousePosRelativeToControl.X, mousePosRelativeToControl.Y);

            r = c.R;
            g = c.G;
            b = c.B;

            OnRGBTrackBarChanged();
        }

        protected override void OnLoad(EventArgs e)
        {
            PlaceForm();
            base.OnLoad(e);
        }

        private Bitmap GetColorCircleBitmap()
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

        private void DrawColorCircle()
        {
            Bitmap circleBitmap = colorCircleBitmap.Clone(new Rectangle(0, 0, colorCircleBitmap.Width, colorCircleBitmap.Height), 
                                                          colorCircleBitmap.PixelFormat);

            pictureBox_ColorCircle.Image = circleBitmap;
        }

        private Color GetColorFromCirclePoint(int x, int y)
        {
            if(MissingMath.IsInsideCircle(x, y, pictureBox_ColorCircle.Width, pictureBox_ColorCircle.Height))
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
                return pictureBox_Edited.BackColor;
            }
        }

        private Point GetPointOnCircleFromColor(Color c)
        {
            Point center = new Point(pictureBox_ColorCircle.Width / 2, pictureBox_ColorCircle.Height / 2);

            double radius = s / 100f * pictureBox_ColorCircle.Width / 2f;

            double radians = h / 360f * Math.PI * 2;
            double degrees = radians * (180 / Math.PI);

            double theta = degrees * Math.PI / 180;
            int dx = (int)(radius * Math.Cos(theta));
            int dy = (int)(radius * Math.Sin(theta));

            return new Point(dx + center.X, dy + center.Y);
        }

        private void PlaceForm()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;



            this.Left = (_parent.Right + this.Width > rightmost.WorkingArea.Right ? (_parent.Left - this.Width < 0 ? _parent.Left + _parent.Width : _parent.Left - this.Width) : _parent.Left + _parent.Width);
            this.Top = _parent.Top;
        }

        private async void AsyncRemoveMarker(Color c)
        {
            List<ListViewItem> listViewItems = _items;

            foreach(ListViewItem item in listViewItems)
            {
                item.SubItems[2].BackColor = c;
                item.SubItems[2].Text = string.Empty;
            }

            await Task.Delay(100);

            foreach (ListViewItem item in listViewItems)
            {
                if(item.SubItems[2].BackColor == c)
                {
                    item.SubItems[2].BackColor = Color.Transparent;
                }
            }
        }
    }
}
