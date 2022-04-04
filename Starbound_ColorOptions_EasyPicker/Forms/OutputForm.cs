using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker
{
    public partial class OutputForm : Form
    {
        public OutputForm(string jsonText)
        {
            InitializeComponent();

            this.label_Status.Text = "";

            richTextBox1.Text = jsonText;

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.sb_cop_icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());
        }

        private void button_CopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void button_SaveAs_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            DateTime localDate = DateTime.Now;

            saveFileDialog1.FileName = $"colorOptions-{localDate.Year}_{localDate.Month}_{localDate.Day}-{localDate.Hour}_{localDate.Minute}_{localDate.Second}";
            saveFileDialog1.Filter = "Normal text file (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form form = Application.OpenForms["MainForm"];
                MainForm mainForm = form != null ? (MainForm)form : null;
                if (mainForm != null)
                {
                    mainForm.RemindToSaveFlag = false;
                }

                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    byte[] data = Encoding.ASCII.GetBytes(richTextBox1.Text);

                    myStream.Write(data, 0, data.Length);

                    myStream.Close();
                }
            }
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



            this.Left = MainForm.Instance.Left + MainForm.Instance.Width / 2 - this.Width / 2;
            this.Top = MainForm.Instance.Top + MainForm.Instance.Height / 2 - this.Height / 2;
        }
    }
}
