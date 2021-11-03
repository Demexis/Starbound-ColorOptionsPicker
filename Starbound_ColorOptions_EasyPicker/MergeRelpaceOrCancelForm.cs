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
    }
}
