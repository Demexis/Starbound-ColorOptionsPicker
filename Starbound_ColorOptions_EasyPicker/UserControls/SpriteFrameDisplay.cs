﻿using System;
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
    public partial class SpriteFrameDisplay : UserControl
    {
        public SpriteSheetHandler SpriteSheetRef;

        private Bitmap[] _allSprites;

        private Bitmap[] _clickableSprites;

        private Bitmap[] _nonClickableSprites;

        public Bitmap[] AllSprites { get { return _allSprites; } set { _allSprites = value; if (value != null) this.pictureBox1.Image = BitmapProcessing.GetInterpolatedBitmap(BitmapProcessing.GetMergedBitmaps(value), this.Size); } }
        //public Bitmap[] AllSprites { get { return _allSprites; } set { _allSprites = value; Draw(); } }
        public Bitmap[] ClickableSprites { get { return _clickableSprites; } set { _clickableSprites = value; } }
        public Bitmap[] NonClickableSprites { get { return _nonClickableSprites; } set { _nonClickableSprites = value; } }

        public Action<Color> OnLeftClick;
        public Action<Color> OnRightClick;

        private bool _mouseInside;
        private Point _mousePos;

        public bool ShowMagnifier = true;

        public Color BackgroundColor
        {
            set
            {
                this.BackColor = value;
                this.pictureBox1.BackColor = value;
            }
        }

        public SpriteFrameDisplay()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        public void DisposeSprites()
        {
            this.pictureBox1.Image.Dispose();
            this.pictureBox1.Image = null;

            if (this.AllSprites != null)
            {
                for (int i = 0; i < AllSprites.Length; i++)
                {
                    AllSprites[i].Dispose();
                    AllSprites[i] = null;
                }
            }

            this.AllSprites = null;

            if (this.ClickableSprites != null)
            {
                for(int i = 0; i < ClickableSprites.Length; i++)
                {
                    ClickableSprites[i].Dispose();
                    ClickableSprites[i] = null;
                }
            }

            this.ClickableSprites = null;

            if (this.NonClickableSprites != null)
            {
                for (int i = 0; i < NonClickableSprites.Length; i++)
                {
                    NonClickableSprites[i].Dispose();
                    NonClickableSprites[i] = null;
                }
            }

            this.NonClickableSprites = null;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (ClickableSprites == null || ClickableSprites.Length == 0 || ClickableSprites.Contains(null)) return;

            if (e.Button == MouseButtons.Left)
            {
                OnLeftClick?.Invoke(BitmapProcessing.GetInterpolatedBitmap(BitmapProcessing.GetMergedBitmaps(ClickableSprites), this.Size).GetPixel(e.Location.X, e.Location.Y));
            }
            else if (e.Button == MouseButtons.Right)
            {
                OnRightClick?.Invoke(BitmapProcessing.GetInterpolatedBitmap(BitmapProcessing.GetMergedBitmaps(ClickableSprites), this.Size).GetPixel(e.Location.X, e.Location.Y));
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            _mousePos = e.Location;

            pictureBox1.Refresh();

            Repaint();

            //int leftWidth = pictureBox1.Width;
            //int leftHeight = pictureBox1.Height;
            //int x = 0;
            //int y = 0;

            //for(int i = 0; i < Rules.BitmapSizeDefault; i++)
            //{
            //    int xW = leftWidth / (Rules.BitmapSizeDefault - i);

            //    for (int j = 0; j < Rules.BitmapSizeDefault; j++)
            //    {
            //        int yH = leftHeight / (Rules.BitmapSizeDefault - j);

            //        g.DrawRectangle(new Pen(Color.Red), new Rectangle(x, y, xW, yH));

            //        if(e.X >= x && e.X < x + xW && e.Y >= y && e.Y < y + yH)
            //        {
            //            g.DrawRectangle(new Pen(Color.White), new Rectangle(x, y, xW, yH));
            //        }


            //        y += yH;
            //        leftHeight -= yH;
            //    }

            //    leftHeight = pictureBox1.Height;
            //    y = 0;

            //    x += xW;
            //    leftWidth -= xW;
            //}

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Refresh();

            _mouseInside = false;
        }

        private void Repaint()
        {
            if (!_mouseInside || !ShowMagnifier) return;

            Graphics g = pictureBox1.CreateGraphics();

            int pixWidth = pictureBox1.Width / Rules.BitmapSizeDefault;
            int pixHeight = pictureBox1.Height / Rules.BitmapSizeDefault;

            int x = Mathf.Clamp(_mousePos.X - pixWidth / 2, 0, pictureBox1.Width - 1);
            int y = Mathf.Clamp(_mousePos.Y - pixHeight / 2, 0, pictureBox1.Height - 1);

            x = Mathf.Clamp(x + pixWidth, 0, pictureBox1.Width - 1) - pixWidth;
            y = Mathf.Clamp(y + pixHeight, 0, pictureBox1.Height - 1) - pixHeight;

            int width = Mathf.Clamp(x + pixWidth, 0, pictureBox1.Width - 1) - x;
            int height = Mathf.Clamp(y + pixHeight, 0, pictureBox1.Height - 1) - y;

            Rectangle mouseRect = new Rectangle(x, y, width, height);

            g.DrawRectangle(new Pen(Color.White), mouseRect);

            int viewRectWidth = width * 8;
            int viewRectHeight = height * 8;

            Rectangle viewRect = new Rectangle(x + width * 2, y - height * 2 - viewRectHeight, viewRectWidth, viewRectHeight);

            if (viewRect.X + viewRect.Width >= pictureBox1.Width - 1)
            {
                viewRect.X = x - width * 2 - viewRect.Width;
            }

            if (viewRect.Y <= 0)
            {
                viewRect.Y = y + height * 2;
            }

            g.DrawRectangle(new Pen(Color.White), viewRect);

            if (pictureBox1.Image != null)
            {
                // Clone a portion of the Bitmap object.
                Bitmap cloneBitmap = ((Bitmap)pictureBox1.Image).Clone(mouseRect, pictureBox1.Image.PixelFormat);

                cloneBitmap = BitmapProcessing.GetInterpolatedBitmap(cloneBitmap, viewRect.Size);

                // Draw the cloned portion of the Bitmap object.
                g.DrawImage(cloneBitmap, viewRect.X + 2, viewRect.Y + 2);
            }

            g.DrawRectangle(new Pen(Color.White), new Rectangle(viewRect.X + viewRect.Width / 2 - 1, viewRect.Y + viewRect.Height / 2 - 1, 2, 2));
            g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(viewRect.X + viewRect.Width / 2, viewRect.Y + viewRect.Height / 2, 1, 1));

            g.Dispose();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Repaint();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            _mouseInside = true;
        }
    }
}
