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
    public partial class ColorTransitionGeneratingForm : Form
    {
        public enum GenerateMethod { HSV, Saturation, RGB_Coefficients }


        public ColorTransitionGeneratingForm()
        {
            InitializeComponent();

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.options_icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());

            foreach(string methodName in Enum.GetNames(typeof(GenerateMethod)))
            {
                this.comboBox1.Items.Add(methodName);
            }

            this.comboBox1.SelectedIndex = 0;
        }

        private void button_Generate_Click(object sender, EventArgs e)
        {
            if(Enum.TryParse(this.comboBox1.SelectedItem.ToString(), out GenerateMethod method))
            {
                switch(method)
                {
                    case GenerateMethod.HSV:
                        GenerateByHSV();
                        break;

                    case GenerateMethod.Saturation:

                        break;

                    case GenerateMethod.RGB_Coefficients:

                        break;
                    default:
                        break;
                }
            }

            this.Close();
        }

        private void GenerateByHSV()
        {
            if(checkBox_Replace.Checked)
                MainForm.Instance.ColorTransitionHandler.Clear();

            foreach(string colorOptionName in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if(Enum.TryParse(colorOptionName, out Rules.ColorOptions colorOption))
                {
                    switch (colorOption)
                    {
                        case Rules.ColorOptions.Default:
                            break;
                        case Rules.ColorOptions.Black:
                            GenerateByHSVHelper(colorOptionName, 0, 0, 0.5f);
                            break;
                        case Rules.ColorOptions.Grey:
                            GenerateByHSVHelper(colorOptionName, 0, 0, 1);
                            break;                               
                        case Rules.ColorOptions.White:           
                            GenerateByHSVHelper(colorOptionName, 0, 0, 1.35f);
                            break;                               
                        case Rules.ColorOptions.Red:             
                            GenerateByHSVHelper(colorOptionName, 0, 1, 1);
                            break;                               
                        case Rules.ColorOptions.Orange:          
                            GenerateByHSVHelper(colorOptionName, 30, 1, 1);
                            break;                               
                        case Rules.ColorOptions.Yellow:          
                            GenerateByHSVHelper(colorOptionName, 60, 1, 1);
                            break;                               
                        case Rules.ColorOptions.Green:           
                            GenerateByHSVHelper(colorOptionName, 120, 1, 1);
                            break;                               
                        case Rules.ColorOptions.Blue:            
                            GenerateByHSVHelper(colorOptionName, 210, 1, 1);
                            break;                               
                        case Rules.ColorOptions.Purple:          
                            GenerateByHSVHelper(colorOptionName, 270, 1, 1);
                            break;                               
                        case Rules.ColorOptions.Pink:            
                            GenerateByHSVHelper(colorOptionName, 300, 1, 1);
                            break;
                        case Rules.ColorOptions.Brown:
                            GenerateByHSVHelper(colorOptionName, 30, 1, 1);
                            break;
                        default:
                            break;
                    }
                }
            }

            MainForm.Instance.UpdateTransitionListView();
        }

        private void GenerateByHSVHelper(string colorOptionName, int hue, float cSaturation, float cValue)
        {
            List<Color> transitionColors = new List<Color>();
            foreach(ColorTransitionItem item in MainForm.Instance.ColorTransitionHandler[colorOptionName])
            {
                transitionColors.Add(item.ColorTo);
            }

            foreach (Color color in MainForm.Instance.GetOriginalColors())
            {
                if (!checkBox_Replace.Checked && MainForm.Instance.ColorTransitionHandler[colorOptionName].Any((x) => x.ColorFrom == color)) continue;
                
                Color colorTo = color;

                int S = Mathf.Clamp((int)(ColorProcessing.GetColorSaturation(colorTo) * cSaturation), 0, 100);
                int V = Mathf.Clamp((int)(ColorProcessing.GetColorValue(colorTo) * cValue), 0, 100);

                Tuple<int, int, int> rgb = ColorProcessing.HSVToRGB(hue, S, V);

                colorTo = Color.FromArgb(rgb.Item1, rgb.Item2, rgb.Item3);

                if (checkBox_SkipDependent.Checked && transitionColors.Contains(color)) continue;

                transitionColors.Add(colorTo);

                ColorTransitionItem colorTransitionItem = new ColorTransitionItem(color, colorTo);
                MainForm.Instance.ColorTransitionHandler[colorOptionName].Add(colorTransitionItem);
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
