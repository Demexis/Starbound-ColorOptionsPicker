using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker.Forms
{
    public partial class ColorTransitionImportExportForm : Form
    {
        public static bool Canceled = false;

        public enum DataOperation { Import, Export };
        private DataOperation _dataOperation;

        private Dictionary<Rules.ColorOptions, bool> _colorOptions = new Dictionary<Rules.ColorOptions, bool>();

        public ColorTransitionImportExportForm(DataOperation dataOperation)
        {
            InitializeComponent();

            _dataOperation = dataOperation;

            this.Text = $"{_dataOperation.ToString()}ing Color Options";

            switch(_dataOperation)
            {
                case DataOperation.Import:
                    _colorOptions = AppPreferences.ImportColorOptions;
                    break;
                case DataOperation.Export:
                    _colorOptions = AppPreferences.ExportColorOptions;
                    break;
                default:
                    break;
            }

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.options_icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());

            Canceled = true;
        }

        private void ColorTransitionImportingForm_Load(object sender, EventArgs e)
        {
            foreach(string colorOption in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if(Enum.TryParse(colorOption, out Rules.ColorOptions colorOptionEnum))
                {
                    //CheckBox checkBox = new CheckBox() { Text = colorOption, Checked = AppPreferences.ImportColorOptions[colorOptionEnum] };
                    //checkBox.CheckedChanged += (s, ev) => { AppPreferences.ImportColorOptions[colorOptionEnum] = checkBox.Checked; };

                    this.checkedListBox1.Items.Add(colorOption, _colorOptions[colorOptionEnum]);
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Canceled = false;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if(Enum.TryParse(checkedListBox1.Items[i].ToString(), out Rules.ColorOptions colorOption))
                {
                    if(_colorOptions.ContainsKey(colorOption))
                    {
                        _colorOptions[colorOption] = checkedListBox1.GetItemCheckState(i) == CheckState.Checked;
                    }
                }
            }

            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Canceled = true;
            this.Close();
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
