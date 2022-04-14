using Newtonsoft.Json.Linq;
using Starbound_ColorOptions_EasyPicker.Forms;
using Starbound_ColorOptions_EasyPicker.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;

        private static Rules.Sex _sex = Rules.Sex.Male;

        public string LastDirectory = String.Empty;
        private bool _dontDraw;
        public bool RemindToSaveFlag = false;
        private bool _updateColoredImage;

        public SpriteSheetHandler GetSpriteSheetHandler => _spriteSheetHandler;
        private SpriteSheetHandler _spriteSheetHandler = new SpriteSheetHandler();

        public ColorTransitionHandler GetColorTransitionHandler => _colorTransitionHandler;
        private ColorTransitionHandler _colorTransitionHandler = new ColorTransitionHandler();

        public FileHandler GetFileHandler => _fileHandler;
        private FileHandler _fileHandler = new FileHandler();

        public ColorTransitionHandler ColorTransitionHandler => _colorTransitionHandler;

        public string LabelStatus
        {
            get
            {
                return this.label_Status.Text;
            }
            set
            {
                this.label_Status.Text = value;
            }
        }


        public MainForm()
        {
            InitializeComponent();

            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                this.Close();
            }

            InitializePictureBox();

            InitializeComboBoxes();

            InitializeRadioButtons();

            DisableAllImportantControllers();

            InitializeMiscellaneous();

            AsyncUpdate();

            AsyncUpdateColoredImage();
        }

        public Color[] GetOriginalColors()
        {
            Color[] colors = new Color[listView_OriginalSpriteColors.Items.Count];

            for(int i = 0; i < listView_OriginalSpriteColors.Items.Count; i++)
            {
                colors[i] = listView_OriginalSpriteColors.Items[i].SubItems[0].BackColor;
            }

            return colors;
        }


        #region Initialization

        private void InitializeMiscellaneous()
        {
            this.Text = AppPreferences.ApplicationName;

            pictureBox1.BackColor = spriteFrameDisplay_Original.BackColor;

            // Get the bitmap.
            Bitmap bm = new Bitmap(Properties.Resources.icon);

            // Convert to an icon and use for the form's icon.
            this.Icon = Icon.FromHandle(bm.GetHicon());

            this.label_Status.Text = "";
        }

        private void InitializePictureBox()
        {
            spriteFrameDisplay_Original.OnLeftClick += OriginalSpriteFrameClick;
            spriteFrameDisplay_Colored.OnLeftClick += ColoredSpriteFrameClick;

            spriteFrameDisplay_Original.BackColor = AppPreferences.SpriteBackColor;
            spriteFrameDisplay_Colored.BackColor = AppPreferences.SpriteBackColor;
        }

        private void InitializeRadioButtons()
        {
            if(_sex == Rules.Sex.Male)
            {
                radioButton_Male.Select();
            }
            else
            {
                radioButton_Female.Select();
            }
        }

        private void InitializeComboBoxes()
        {
            comboBox_ColorOption.Items.AddRange((String[])Enum.GetNames(typeof(Rules.ColorOptions)));
            comboBox_ColorOption.SelectedIndex = 0;

            comboBox_Pose.Items.AddRange(Rules.PosesNNumOfFrames.Keys.ToArray());
            comboBox_Pose.SelectedIndex = 0;
            OnPoseChange();
        }

        #endregion


        public void OnChangeDone()
        {
            RemindToSaveFlag = true;

            SetFlagToUpdateColoredImage();
        }

        public void OnPoseChange()
        {
            _dontDraw = true;
            comboBox_Frame.Items.Clear();

            string[] frames = new string[Rules.PosesNNumOfFrames[(string)comboBox_Pose.SelectedItem]];
            for(int i = 0; i < Rules.PosesNNumOfFrames[(string)comboBox_Pose.SelectedItem]; i++)
            {
                frames[i] = $"{i}";
            }

            comboBox_Frame.Items.AddRange(frames);

            _dontDraw = false;
            comboBox_Frame.SelectedIndex = 0;

            SetFlagToUpdateColoredImage();
        }

        private void DisableAllImportantControllers()
        {
            this.label_Status.Text = "Disabling controllers...";

            button_ColorTransition_Add.Enabled = false;
            button_ColorTransition_Remove.Enabled = false;
            comboBox_ColorOption.Enabled = false;
            comboBox_Pose.Enabled = false;
            comboBox_Frame.Enabled = false;

            groupBox1.Enabled = false;

            saveToolStripMenuItem.Enabled = false;
            colorOptionsToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Enabled = false;
            checkBox_ShowMannequin.Enabled = false;
            label_ShowMannequin.Enabled = false;

            generateColorTransitionsToolStripMenuItem.Enabled = false;

            this.label_Status.Text = "";
        }

        public void EnableAllImportantControllers()
        {
            this.label_Status.Text = "Enabling controllers...";

            comboBox_ColorOption.Enabled = true;
            comboBox_Pose.Enabled = true;
            comboBox_Frame.Enabled = true;

            groupBox1.Enabled = true;

            saveToolStripMenuItem.Enabled = true;
            colorOptionsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;
            checkBox_ShowMannequin.Enabled = true;
            label_ShowMannequin.Enabled = true;

            generateColorTransitionsToolStripMenuItem.Enabled = true;

            SetFlagToUpdateColoredImage();

            this.label_Status.Text = "";
        }

        public void AddTransitionColorsFromJSON(string file)
        {
            this.label_Status.Text = $"Adding transition colors from {file}";

            Dictionary<string, List<ColorTransitionItem>> colorTransitions = RulesProcessing.GetColorTransitionsFromJSON(file);

            foreach(string key in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!colorTransitions.ContainsKey(key)) continue;

                foreach (ColorTransitionItem item1 in colorTransitions[key])
                {
                    bool skip = false;
                    foreach(ColorTransitionItem item2 in _colorTransitionHandler[key])
                    {
                        if (item2.ColorFrom == item1.ColorFrom) skip = true;
                    }

                    if(!skip)
                    {
                        ColorTransitionItem colorTransitionItem = new ColorTransitionItem(item1.ColorFrom, item1.ColorTo);

                        _colorTransitionHandler[key].Add(colorTransitionItem);
                    }
                }
            }

            this.label_Status.Text = "";
        }

        public void AddOriginalColors()
        {
            this.label_Status.Text = "Adding original colors...";

            listView_OriginalSpriteColors.Items.Clear();

            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach (string spritePart in SpriteSheetHandler.SpriteParts)
            {
                foreach(string spriteAnimation in SpriteSheetHandler.SpriteAnimations)
                {
                    Bitmap orig = _spriteSheetHandler.GetSpritePartBitmap(spritePart);
                    if (orig == null)
                    {
                        break;
                    }

                    foreach (Point p in Rules.PosesNCoordinates[spritePart][spriteAnimation])
                    {
                        Bitmap b = orig.Clone(new Rectangle(Rules.BitmapSizeDefault * p.X, Rules.BitmapSizeDefault * p.Y, Rules.BitmapSizeDefault, Rules.BitmapSizeDefault), orig.PixelFormat);

                        bitmaps.Add(b);
                    }
                }

            }

            Color[] colors = BitmapProcessing.GetAllColorsFromBitmaps(bitmaps.ToArray());
            //colors = BitmapProcessing.SortColorsByValue(colors);

            for (int i = 0; i < colors.Length; i++)
            {
                string[] row = { string.Empty, ColorProcessing.HexConverter(colors[i]) };

                ListViewItem listViewItem = new ListViewItem(row);
                listViewItem.UseItemStyleForSubItems = false;
                listViewItem.SubItems[0].BackColor = colors[i];

                listView_OriginalSpriteColors.Items.Add(listViewItem);
            }

            this.label_Status.Text = "";
        }

        private void OpenColorTransitionEditor(params ListViewItem[] items)
        {
            this.label_Status.Text = "Editing...";
            new ColorTransitionEditingForm(this, items).ShowDialog();
            this.label_Status.Text = "";
        }

        private void OpenColorTransitionEditorHSV(params ListViewItem[] items)
        {
            this.label_Status.Text = "HSV Editing...";
            new ColorTransitionEditingFormHSV(this, items).ShowDialog();
            this.label_Status.Text = "";
        }





        //////////////////////////////////
        /*            Updates           */
        //////////////////////////////////
        ///

        #region Updates

        public void UpdateTransitionListView()
        {
            if(Enum.GetNames(typeof(Rules.ColorOptions)).ToList().Contains(comboBox_ColorOption.Text))
            {
                if(_colorTransitionHandler.ContainsKey(comboBox_ColorOption.Text))
                {
                    listView_ColorTransition.Items.Clear();

                    foreach(ColorTransitionItem colorTransitionItem in _colorTransitionHandler[comboBox_ColorOption.Text])
                    {
                        listView_ColorTransition.Items.Add(colorTransitionItem.GetListViewItem);
                    }
                }
            }

            SetFlagToUpdateColoredImage();

            this.label_ColorTransitions.Text = $"Color Transitions ({listView_ColorTransition.Items.Count})";
        }
        
        private void UpdateOriginalSprite()
        {
            if (!String.IsNullOrWhiteSpace(LastDirectory) && !_dontDraw)
            {
                _spriteSheetHandler.SetAllSpritePartsForCurrentFrame((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex, _sex);

                List<Bitmap> allBitmaps = new List<Bitmap>();
                allBitmaps.Add(SpriteSheetHandler.GetEmptyFrame());

                List<Bitmap> armorBitmaps = new List<Bitmap>();
                List<Bitmap> mannequinBitmaps = new List<Bitmap>();

                foreach (string spritePart in SpriteSheetHandler.SpriteParts)
                {
                    if (spritePart == "chestm" && _sex != Rules.Sex.Male || spritePart == "chestf" && _sex != Rules.Sex.Female)
                    {
                        continue;
                    }

                    if (_spriteSheetHandler.HumanParts.TryGetValue(spritePart, out Bitmap bH))
                    {
                        if (bH != null)
                        {
                            bH = (Bitmap)bH.Clone();

                            allBitmaps.Add(bH);
                            mannequinBitmaps.Add(bH);
                        }
                    }

                    if (_spriteSheetHandler.ArmorParts.TryGetValue(spritePart, out Bitmap bA))
                    {
                        if (bA != null)
                        {
                            bA = (Bitmap)bA.Clone();

                            allBitmaps.Add(bA);
                            armorBitmaps.Add(bA);
                        }
                    }
                }


                // Display the result.
                this.spriteFrameDisplay_Original.AllSprites = allBitmaps.ToArray();
                this.spriteFrameDisplay_Original.ClickableSprites = armorBitmaps.ToArray();
                this.spriteFrameDisplay_Original.NonClickableSprites = mannequinBitmaps.ToArray();

            }

            this.label_SpriteColors.Text = $"Original Sprite's Colors ({listView_OriginalSpriteColors.Items.Count})";
            SetFlagToUpdateColoredImage();
        }
        
        private void UpdateColoredSprite()
        {
            _spriteSheetHandler.SetAllSpritePartsForCurrentFrame((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex, _sex);

            List<Bitmap> allBitmaps = new List<Bitmap>();
            allBitmaps.Add(SpriteSheetHandler.GetEmptyFrame());

            List<Bitmap> armorBitmaps = new List<Bitmap>();
            List<Bitmap> mannequinBitmaps = new List<Bitmap>();

            foreach (string spritePart in SpriteSheetHandler.SpriteParts)
            {
                if (spritePart == "chestm" && _sex != Rules.Sex.Male || spritePart == "chestf" && _sex != Rules.Sex.Female)
                {
                    continue;
                }

                if (_spriteSheetHandler.HumanParts.TryGetValue(spritePart, out Bitmap bH))
                {
                    if (bH != null)
                    {
                        bH = (Bitmap)bH.Clone();

                        allBitmaps.Add(bH);
                        mannequinBitmaps.Add(bH);
                    }
                }

                if (_spriteSheetHandler.ArmorParts.TryGetValue(spritePart, out Bitmap bA))
                {
                    if (bA != null)
                    {
                        bA = (Bitmap)bA.Clone();

                        using (Graphics g = Graphics.FromImage(bA))
                        {
                            foreach (ListViewItem item in listView_ColorTransition.Items)
                            {
                                // Set the image attribute's color mappings
                                ColorMap[] colorMap = new ColorMap[1];
                                colorMap[0] = new ColorMap();
                                colorMap[0].OldColor = item.SubItems[0].BackColor;
                                colorMap[0].NewColor = item.SubItems[3].BackColor;

                                ImageAttributes attr = new ImageAttributes();
                                attr.SetRemapTable(colorMap);

                                // Draw using the color map
                                Rectangle rect = new Rectangle(0, 0, bA.Width, bA.Height);
                                g.DrawImage(bA, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
                            }
                        }

                        allBitmaps.Add(bA);
                        armorBitmaps.Add(bA);
                    }
                }
            }

            // Display the result.
            this.spriteFrameDisplay_Colored.AllSprites = allBitmaps.ToArray();
            this.spriteFrameDisplay_Colored.ClickableSprites = armorBitmaps.ToArray();
            this.spriteFrameDisplay_Colored.NonClickableSprites = mannequinBitmaps.ToArray();

        }

        private async void AsyncUpdate()
        {
            while(true)
            {
                button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);

                button_ColorTransition_Edit.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);
                button_ColorTransition_EditHSV.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);
                button_ColorTransition_Remove.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);

                saveToolStripMenuItem.Enabled = _spriteSheetHandler.ArmorParts.Any() || ColorTransitionHandler.CountAllTransitions > 0; // (listView_ColorTransition.Items.Count > 0);
                colorOptionsToolStripMenuItem.Enabled = _spriteSheetHandler.ArmorParts.Any() || ColorTransitionHandler.CountAllTransitions > 0; // (listView_ColorTransition.Items.Count > 0);

                //comboBox_ColorOption.Enabled = (listView_ColorTransition.Items.Count > 0);
                comboBox_ColorOption.Enabled = _spriteSheetHandler.ArmorParts.Any() || ColorTransitionHandler.CountAllTransitions > 0;

                await Task.Delay(50);
            }
        }


        public void SetFlagToUpdateColoredImage()
        {
            _updateColoredImage = true;
        }

        private async void AsyncUpdateColoredImage()
        {
            while (true)
            {
                if(_updateColoredImage)
                {
                    _updateColoredImage = false;
                }
                else
                {
                    await Task.Delay(5);
                    continue;
                }

                if (_spriteSheetHandler.Count > 0)
                {
                    try
                    {
                        UpdateColoredSprite();
                    }
                    catch (Exception ex) { /* Ignore */ }
                }
                else if (spriteFrameDisplay_Colored.AllSprites != null)
                {
                    spriteFrameDisplay_Colored.DisposeSprites();
                }

                await Task.Delay(5);
            }
        }

        #endregion


        //////////////////////////////////
        /*            Events            */
        //////////////////////////////////
        /**/

        #region Events

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileHandler.OpenSpriteFilesExplorer();
        }

        private void comboBox_Pose_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label_Status.Text = "Changing pose...";
            OnPoseChange();
            this.label_Status.Text = "";
        }

        private void comboBox_Frame_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label_Status.Text = "Changing frame...";
            UpdateOriginalSprite();
            SetFlagToUpdateColoredImage();
            this.label_Status.Text = "";
        }

        private void listView_OriginalSpriteColors_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);
        }

        private void button_ColorTransition_Add_Click(object sender, EventArgs e)
        {
            AddSelectedColorTransitions();
        }

        private void AddSelectedColorTransitions()
        {
            if (listView_OriginalSpriteColors.SelectedItems.Count > 0)
            {
                bool madeChange = false;

                for (int i = 0; i < listView_OriginalSpriteColors.SelectedItems.Count; i++)
                {
                    ListViewItem item = listView_OriginalSpriteColors.SelectedItems[i];

                    Color color = item.SubItems[0].BackColor;

                    bool skip = false;
                    foreach (ListViewItem existingItem in listView_ColorTransition.Items)
                    {
                        if (existingItem.SubItems[0].BackColor == color) skip = true;
                    }
                    if (skip) continue;
                    else madeChange = true;

                    ColorTransitionItem colorTransitionItem = new ColorTransitionItem(color, color);

                    _colorTransitionHandler[comboBox_ColorOption.Text].Add(colorTransitionItem);
                    UpdateTransitionListView();
                }

                listView_OriginalSpriteColors.SelectedItems.Clear();

                if (madeChange)
                    OnChangeDone();
            }
        }

        private void listView_OriginalSpriteColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);
        }

        private void listView_OriginalSpriteColors_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);
        }

        private void button_ColorTransition_Edit_Click(object sender, EventArgs e)
        {
            EditSelectedColorTransitions();
        }

        private void EditSelectedColorTransitions()
        {
            if (listView_ColorTransition.SelectedItems.Count > 0)
            {
                List<ListViewItem> itemList = new List<ListViewItem>();

                foreach (ListViewItem item in listView_ColorTransition.SelectedItems)
                {
                    itemList.Add(item);
                }

                // Deselect All Selected Items
                listView_ColorTransition.SelectedItems.Clear();

                OpenColorTransitionEditor(itemList.ToArray());
            }
        }

        private void button_ColorTransition_Remove_Click(object sender, EventArgs e)
        {
            if (listView_ColorTransition.SelectedItems.Count > 0)
            {
                for (int i = listView_ColorTransition.Items.Count - 1; i >= 0; i--)
                {
                    if(listView_ColorTransition.SelectedItems.Contains(listView_ColorTransition.Items[i]))
                    {
                        try
                        {
                            _colorTransitionHandler[comboBox_ColorOption.Text].RemoveAt(i);
                            UpdateTransitionListView();
                        }
                        catch(Exception ex) { }
                    }
                }

                // Deselect All Selected Items
                listView_ColorTransition.SelectedItems.Clear();

                OnChangeDone();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileHandler.CloseOpenedFiles();
        }

        public bool CloseOpenedFiles()
        {
            if (RemindToSaveFlag)
            {
                if(!_fileHandler.OpenSaveReminder())
                {
                    return false;
                }
            }

            _colorTransitionHandler.Clear();
            _spriteSheetHandler.Clear();

            this.listView_OriginalSpriteColors.Items.Clear();
            this.listView_ColorTransition.Items.Clear();

            this.spriteFrameDisplay_Original.DisposeSprites();

            this.spriteFrameDisplay_Colored.DisposeSprites();

            DisableAllImportantControllers();

            this.Text = AppPreferences.ApplicationName;

            LastDirectory = String.Empty;

            UpdateOriginalSprite();
            SetFlagToUpdateColoredImage();
            UpdateTransitionListView();

            return true;
        }

        private void radioButton_Male_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Male.Checked)
            {
                _sex = Rules.Sex.Male;
                UpdateOriginalSprite();
            }
        }

        private void radioButton_Female_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Female.Checked)
            {
                _sex = Rules.Sex.Female;
                UpdateOriginalSprite();
            }
        }

        private void comboBox_ColorOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTransitionListView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void colorOptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _fileHandler.OpenTransitionColorsImporter();
        }

        public void OriginalSpriteFrameClick(Color color)
        {
            if (_spriteSheetHandler.Count == 0) return;

            this.label_Status.Text = "Trying to take a color from the original sprite...";

            if (color.A > 0)
            {
                bool skip = false;
                for(int i = 0; i < listView_ColorTransition.Items.Count; i++)
                {
                    if (listView_ColorTransition.Items[i].SubItems[0].BackColor == color)
                    {
                        skip = true;
                    }
                }

                if (!skip)
                {
                    ColorTransitionItem colorTransitionItem = new ColorTransitionItem(color, color);

                    _colorTransitionHandler[comboBox_ColorOption.Text].Add(colorTransitionItem);
                    UpdateTransitionListView();

                    OnChangeDone();
                }

                for (int i = 0; i < listView_ColorTransition.Items.Count; i++)
                {
                    listView_ColorTransition.Items[i].Selected = false;

                    if (listView_ColorTransition.Items[i].SubItems[0].BackColor == color)
                    {
                        listView_ColorTransition.Select();
                        listView_ColorTransition.Items[i].Selected = true;
                    }
                }
            }

            this.label_Status.Text = "";
        }

        public void ColoredSpriteFrameClick(Color color)
        {
            if (_spriteSheetHandler.Count == 0) return;

            this.label_Status.Text = "Trying to take a color from the colored sprite...";

            if (color.A > 0)
            {
                bool skip = false;
                foreach (ListViewItem existingItem in listView_ColorTransition.Items)
                {
                    if (existingItem.SubItems[3].BackColor == color) skip = true;
                }

                if (!skip)
                {
                    ColorTransitionItem colorTransitionItem = new ColorTransitionItem(color, color);

                    _colorTransitionHandler[comboBox_ColorOption.Text].Add(colorTransitionItem);
                    UpdateTransitionListView();

                    OnChangeDone();
                }

                List<ListViewItem> items = new List<ListViewItem>();
                foreach (ListViewItem existingItem in listView_ColorTransition.Items)
                {
                    if (existingItem.SubItems[3].BackColor == color)
                    {
                        items.Add(existingItem);
                    }
                }

                OpenColorTransitionEditor(items.ToArray());
            }

            this.label_Status.Text = "";
        }

        private void colorOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label_Status.Text = "Exporting...";

            Dictionary<string, List<ColorTransitionItem>> exportedItems = MainForm.Instance.GetColorTransitionHandler.CloneColorTransitions;

            ColorTransitionImportExportForm colorTransitionExportingForm = new ColorTransitionImportExportForm(ColorTransitionImportExportForm.DataOperation.Export);
            colorTransitionExportingForm.ShowDialog();

            foreach (Rules.ColorOptions colorOption in AppPreferences.ExportColorOptions.Keys)
            {
                if (AppPreferences.ExportColorOptions[colorOption] == false)
                {
                    if (exportedItems.ContainsKey(colorOption.ToString()))
                    {
                        exportedItems[colorOption.ToString()].Clear();

                        //exportedItems.Remove(colorOption.ToString());
                    }
                }

            }

            if (ColorTransitionImportExportForm.Canceled) return;

            string jsonText = RulesProcessing.GetJSONStringFromColorTransitions(exportedItems);

            new OutputForm(jsonText).ShowDialog();
            this.label_Status.Text = "";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.label_Status.Text = "Closing...";

            if (!RemindToSaveFlag) return;

            e.Cancel = !_fileHandler.OpenSaveReminder();

            this.label_Status.Text = "";
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message = 
                $"SB Color Options Picker v{AppPreferences.ApplicationVersion}" + Environment.NewLine
                + "______________________________" + Environment.NewLine
                + "D. Sorochinsky (aka Demexis)" + Environment.NewLine
                + "@2021" + Environment.NewLine;

            MessageBox.Show(message, "Author", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void button_EditHSV_Click(object sender, EventArgs e)
        {
            this.label_Status.Text = "HSV Editing...";

            if (listView_ColorTransition.SelectedItems.Count > 0)
            {
                List<ListViewItem> itemList = new List<ListViewItem>();

                foreach (ListViewItem item in listView_ColorTransition.SelectedItems)
                {
                    itemList.Add(item);
                }

                // Deselect All Selected Items
                listView_ColorTransition.SelectedItems.Clear();

                OpenColorTransitionEditorHSV(itemList.ToArray());
            }

            this.label_Status.Text = "";
        }

        private void checkBox_ShowMannequin_CheckedChanged(object sender, EventArgs e)
        {
            _spriteSheetHandler.ShowMannequin = checkBox_ShowMannequin.Checked;
            UpdateOriginalSprite();
        }

        private void gitHubURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"https://github.com/Demexis/Starbound-ColorOptionsPicker");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.label_Status.Text = "Changing the background color...";

            Color previousColor = AppPreferences.SpriteBackColor;

            switch(colorDialog1.ShowDialog())
            {
                case DialogResult.OK:
                    Color c = colorDialog1.Color;

                    pictureBox1.BackColor = c;
                    spriteFrameDisplay_Colored.BackgroundColor = c;
                    spriteFrameDisplay_Original.BackgroundColor = c;
                    AppPreferences.SpriteBackColor = c;
                    break;
                default:
                    pictureBox1.BackColor = previousColor;
                    spriteFrameDisplay_Colored.BackgroundColor = previousColor;
                    spriteFrameDisplay_Original.BackgroundColor = previousColor;
                    AppPreferences.SpriteBackColor = previousColor;
                    break;
            }

            this.label_Status.Text = "";
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label_Status.Text = "Changing the settings...";

            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();

            this.label_Status.Text = "";
        }

        private void generateColorTransitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label_Status.Text = "Generating the color transitions...";

            ColorTransitionGeneratingForm colorTransitionGeneratingForm = new ColorTransitionGeneratingForm();

            colorTransitionGeneratingForm.ShowDialog();

            this.label_Status.Text = "";
        }

        private void checkBox_ShowMagnifier_CheckedChanged(object sender, EventArgs e)
        {
            this.spriteFrameDisplay_Colored.ShowMagnifier = checkBox_ShowMagnifier.Checked;
            this.spriteFrameDisplay_Original.ShowMagnifier = checkBox_ShowMagnifier.Checked;
        }

        #endregion

        private void listView_ColorTransition_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedColorTransitions();
        }

        private void listView_OriginalSpriteColors_DoubleClick(object sender, EventArgs e)
        {
            AddSelectedColorTransitions();
        }

        private void wikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"https://github.com/Demexis/Starbound-ColorOptionsPicker/wiki");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
        }
    }
}
