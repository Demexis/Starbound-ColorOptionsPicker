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
    public partial class MergeReplaceOrCancelForm : Form
    {
        public MergeReplaceOrCancelForm()
        {
            InitializeComponent();

            button_Merge.DialogResult = DialogResult.Yes;
            button_Replace.DialogResult = DialogResult.No;
            button_Cancel.DialogResult = DialogResult.Cancel;

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.options_icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());
        }

        private void button_Merge_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button_Replace_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override void OnLoad(EventArgs e)
        {
            PlaceForm();
            base.OnLoad(e);
        }

        private void PlaceForm()
        {
            Form mainForm = Application.OpenForms[nameof(MainForm)];

            this.Left = mainForm.Right - (mainForm.Right - mainForm.Left) / 2 - (this.Right - this.Left) / 2;
            this.Top = mainForm.Bottom - (mainForm.Bottom - mainForm.Top) / 2 - (this.Bottom - this.Top) / 2;
        }
    }
}
