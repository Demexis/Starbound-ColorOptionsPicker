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

            richTextBox1.Text = jsonText;
        }

        private void button_CopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void button_SaveAs_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Normal text file (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    byte[] data = Encoding.ASCII.GetBytes(richTextBox1.Text);

                    myStream.Write(data, 0, data.Length);

                    myStream.Close();
                }
            }
        }
    }
}
