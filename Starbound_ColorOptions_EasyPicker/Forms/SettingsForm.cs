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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            this.label_Status.Text = "";

            this.checkBox_IgnoreHead.Checked = AppPreferences.IgnoreHeadFiles;
            this.checkBox_IgnoreChest.Checked = AppPreferences.IgnoreChestFiles;
            this.checkBox_IgnoreLegs.Checked = AppPreferences.IgnoreLegsFiles;
            this.checkBox_IgnoreBack.Checked = AppPreferences.IgnoreBackFiles;
            this.checkBox_IgnoreMasks.Checked = AppPreferences.IgnoreMasks;

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.options_icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            trackBar_TransparencyCut.Value = AppPreferences.TransparencyCut;
            textBox_TransparencyCut.Text = AppPreferences.TransparencyCut.ToString();
        }

        private void textBox_TransparencyCut_TextChanged(object sender, EventArgs e)
        {
            if (!textBox_TransparencyCut.Focused) return;

            try
            {
                AppPreferences.TransparencyCut = (byte)Mathf.Clamp(int.Parse(textBox_TransparencyCut.Text), 0, 255);
                trackBar_TransparencyCut.Value = AppPreferences.TransparencyCut;
                textBox_TransparencyCut.Text = AppPreferences.TransparencyCut.ToString();
            }
            catch(Exception ex)
            {
                textBox_TransparencyCut.Text = AppPreferences.TransparencyCut.ToString();
            }
        }

        private void trackBar_TransparencyCut_Scroll(object sender, EventArgs e)
        {
            if (!trackBar_TransparencyCut.Focused) return;

            AppPreferences.TransparencyCut = (byte)trackBar_TransparencyCut.Value;
            textBox_TransparencyCut.Text = AppPreferences.TransparencyCut.ToString();
        }

        private void checkBox_IgnoreHead_CheckedChanged(object sender, EventArgs e)
        {
            AppPreferences.IgnoreHeadFiles = ((CheckBox)sender).Checked;
        }

        private void checkBox_IgnoreChest_CheckedChanged(object sender, EventArgs e)
        {
            AppPreferences.IgnoreChestFiles = ((CheckBox)sender).Checked;
        }

        private void checkBox_IgnoreLegs_CheckedChanged(object sender, EventArgs e)
        {
            AppPreferences.IgnoreLegsFiles = ((CheckBox)sender).Checked;
        }

        private void checkBox_IgnoreBack_CheckedChanged(object sender, EventArgs e)
        {
            AppPreferences.IgnoreBackFiles = ((CheckBox)sender).Checked;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox_IgnoreMasks_CheckedChanged(object sender, EventArgs e)
        {
            AppPreferences.IgnoreMasks = ((CheckBox)sender).Checked;
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
