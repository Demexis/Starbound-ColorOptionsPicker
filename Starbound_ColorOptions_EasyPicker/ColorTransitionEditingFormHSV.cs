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
    public partial class ColorTransitionEditingFormHSV : Form
    {
        private MainForm _parent;

        private List<ListViewItem> _items = new List<ListViewItem>();
        private Color[] _originalColors;

        private int h, s, v;

        private bool _saveFlag = false;


        public ColorTransitionEditingFormHSV(MainForm parent, ListViewItem[] items)
        {
            InitializeComponent();

            this._parent = parent;
            this._items = items.ToList();

            List<Color> colors = new List<Color>();
            foreach (ListViewItem item in items)
            {
                colors.Add(item.SubItems[3].BackColor);
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
            this.pictureBox_Edited.Image = bitmap;
        }

        private void OnTrackBarValueChange()
        {
            trackBar_Hue.Value = h;
            textBox_Hue.Text = h.ToString();

            trackBar_Saturation.Value = s;
            textBox_Saturation.Text = s.ToString();

            trackBar_Value.Value = v;
            textBox_Value.Text = v.ToString();

            Bitmap bitmap = new Bitmap(pictureBox_Edited.Width, pictureBox_Edited.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    Color c =_originalColors[i];

                    // Hue Processing
                    Tuple<int, int, int> hsv = ColorProcessing.RGBtoHSV(c.R, c.G, c.B);

                    int h1 = hsv.Item1 + h;
                    if(h1 > 360)
                    {
                        h1 -= 360;
                    }
                    if(h1 < 0)
                    {
                        h1 += 360;
                    }

                    Tuple<int, int, int> rgb = ColorProcessing.HSVToRGB(h1, hsv.Item2, hsv.Item3);

                    c = Color.FromArgb(rgb.Item1, rgb.Item2, rgb.Item3);

                    // Value Processing
                    if(v < 0)
                    {
                        int r = c.R + (int)Math.Floor(c.R * (v / 100f));
                        int g = c.G + (int)Math.Floor(c.G * (v / 100f));
                        int b = c.B + (int)Math.Floor(c.B * (v / 100f));

                        c = Color.FromArgb(r, g, b);
                    }
                    else
                    {
                        int r = c.R + (int)Math.Floor((255 - c.R) * (v / 100f));
                        int g = c.G + (int)Math.Floor((255 - c.G) * (v / 100f));
                        int b = c.B + (int)Math.Floor((255 - c.B) * (v / 100f));

                        c = Color.FromArgb(r, g, b);
                    }

                    // Saturation Processing
                    hsv = ColorProcessing.RGBtoHSV(c.R, c.G, c.B);
                    int s1 = hsv.Item2;
                    if(s < 0)
                    {
                        s1 += (int)Math.Floor(s1 * (s / 100f));
                    }
                    else
                    {
                        s1 = MissingMath.Clamp(s1 + (int)Math.Floor(s1 * (s / 100f)), 0, 100);
                    }

                    rgb = ColorProcessing.HSVToRGB(hsv.Item1, s1, hsv.Item3);

                    c = Color.FromArgb(rgb.Item1, rgb.Item2, rgb.Item3);

                    _items[i].SubItems[3].BackColor = c;
                    _items[i].SubItems[4].Text = ColorProcessing.HexConverter(c);

                    SolidBrush shadowBrush = new SolidBrush(c);
                    gr.FillRectangle(shadowBrush, new Rectangle(0, (pictureBox_Edited.Height / _items.Count) * i, pictureBox_Edited.Width, (pictureBox_Edited.Height / _items.Count)));
                }
            }

            this.pictureBox_Edited.Image = bitmap;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_saveFlag)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i].SubItems[3].BackColor = _originalColors[i];
                    _items[i].SubItems[4].Text = ColorProcessing.HexConverter(_originalColors[i]);
                }
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].SubItems[3].BackColor != _originalColors[i])
                    {
                        _parent.OnChangeDone();
                    }
                }
            }

            base.OnClosing(e);
        }

        private void trackBar_Hue_Scroll(object sender, EventArgs e)
        {
            h = trackBar_Hue.Value;

            OnTrackBarValueChange();
        }

        private void trackBar_Saturation_Scroll(object sender, EventArgs e)
        {
            s = trackBar_Saturation.Value;

            OnTrackBarValueChange();
        }

        private void trackBar_Value_Scroll(object sender, EventArgs e)
        {
            v = trackBar_Value.Value;

            OnTrackBarValueChange();
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

            OnTrackBarValueChange();
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

            OnTrackBarValueChange();
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

            OnTrackBarValueChange();
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

        private void pictureBox_Original_Click(object sender, EventArgs e)
        {
            pictureBox_Original.Select();

            h = 0;
            s = 0;
            v = 0;

            Bitmap bitmap = new Bitmap(pictureBox_Edited.Width, pictureBox_Edited.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < _originalColors.Length; i++)
                {
                    Color c = _originalColors[i];

                    _items[i].SubItems[3].BackColor = c;
                    _items[i].SubItems[4].Text = ColorProcessing.HexConverter(c);

                    SolidBrush shadowBrush = new SolidBrush(c);
                    g.FillRectangle(shadowBrush, new Rectangle(0, (pictureBox_Edited.Height / _originalColors.Length) * i, pictureBox_Edited.Width, (pictureBox_Edited.Height / _originalColors.Length)));
                }
            }

            this.pictureBox_Edited.Image = bitmap;

            trackBar_Hue.Value = h;
            textBox_Hue.Text = h.ToString();

            trackBar_Saturation.Value = s;
            textBox_Saturation.Text = s.ToString();

            trackBar_Value.Value = v;
            textBox_Value.Text = v.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            PlaceForm();
            base.OnLoad(e);
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
    }
}
