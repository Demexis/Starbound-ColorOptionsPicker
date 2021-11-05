using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private static Rules.Sex _sex = Rules.Sex.Male;

        private string _lastDirectory = String.Empty;
        private bool _dontDraw;
        private bool _remindToSaveFlag = false;

        private SpriteSheetHandler _spriteSheetHandler = new SpriteSheetHandler();
        private ColorTransitionHandler _colorTransitionHandler = new ColorTransitionHandler();

        public MainForm()
        {
            InitializeComponent();

            InitializePictureBox();

            InitializeComboBoxes();

            InitializeRadioButtons();

            DisableAllImportantControllers();

            InitializeMiscellaneous();

            AsyncUpdate();

        }

        public void OnChangeDone()
        {
            _remindToSaveFlag = true;
        }

        private void InitializeMiscellaneous()
        {
            this.Text = AppPreferences.ApplicationName;

            pictureBox1.BackColor = pictureBox_OriginalSprite.BackColor;
        }

        private void InitializePictureBox()
        {
            pictureBox_OriginalSprite.BackColor = AppPreferences.SpriteBackColor;
            pictureBox_ColoredSprite.BackColor = AppPreferences.SpriteBackColor;
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

        private void OnPoseChange()
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
        }

        private void DisableAllImportantControllers()
        {
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
        }

        private void EnableAllImportantControllers()
        {
            comboBox_ColorOption.Enabled = true;
            comboBox_Pose.Enabled = true;
            comboBox_Frame.Enabled = true;

            groupBox1.Enabled = true;

            saveToolStripMenuItem.Enabled = true;
            colorOptionsToolStripMenuItem.Enabled = true;
            closeToolStripMenuItem.Enabled = true;
            checkBox_ShowMannequin.Enabled = true;
        }

        private void AddTransitionColorsFromJSON(string file)
        {
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
        }

        private void AddOriginalColors()
        {
            listView_OriginalSpriteColors.Items.Clear();

            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach (string spritePart in SpriteSheetHandler.SpriteParts)
            {
                foreach(string spriteAnimation in SpriteSheetHandler.SpriteAnimations)
                {
                    Bitmap orig = _spriteSheetHandler.GetSpriteBitmap(spritePart);
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

            //Bitmap b = _spriteSheetHandler.GetMergedOriginalBitmap((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex, _sex);

            //listView_OriginalSpriteColors.Items.Clear();

            //Color[] colors = BitmapProcessing.GetAllColorsFromBitmaps(b);
            ////colors = BitmapProcessing.SortColorsByValue(colors);

            //for(int i = 0; i < colors.Length; i++)
            //{
            //    string[] row = { string.Empty, ColorProcessing.HexConverter(colors[i]) };

            //    ListViewItem listViewItem = new ListViewItem(row);
            //    listViewItem.UseItemStyleForSubItems = false;
            //    listViewItem.SubItems[0].BackColor = colors[i];

            //    listView_OriginalSpriteColors.Items.Add(listViewItem);
            //}
        }

        private void OpenColorTransitionEditor(params ListViewItem[] items)
        {
            new ColorTransitionEditingForm(this, items).ShowDialog();
        }

        private void OpenColorTransitionEditorHSV(params ListViewItem[] items)
        {
            new ColorTransitionEditingFormHSV(this, items).ShowDialog();
        }





        //////////////////////////////////
        /*            Updates           */
        //////////////////////////////////
        ///

        private void UpdateTransitionListView()
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
        }
        
        private void UpdateOriginalSprite()
        {
            if (!String.IsNullOrWhiteSpace(_lastDirectory) && !_dontDraw)
            {
                Bitmap result = _spriteSheetHandler.GetMergedOriginalBitmap((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex, _sex);
                result = BitmapProcessing.GetInterpolatedBitmap(result, this.pictureBox_OriginalSprite.Size);

                // Display the result.
                this.pictureBox_OriginalSprite.Image = result;

                //AddOriginalColors(); // !!!
            }
        }
        
        private void UpdateColoredSprite()
        {
            Bitmap result = _spriteSheetHandler.GetMergedOriginalBitmap((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex, _sex);
            for(int i = 0; i < result.Width; i++)
            {
                for (int j = 0; j < result.Height; j++)
                {
                    foreach(ListViewItem item in listView_ColorTransition.Items)
                    {
                        if(result.GetPixel(i, j) == item.SubItems[0].BackColor)
                        {
                            result.SetPixel(i, j, item.SubItems[3].BackColor);
                        }
                    }
                }
            }

            result = BitmapProcessing.GetInterpolatedBitmap(result, this.pictureBox_ColoredSprite.Size);

            // Display the result.
            this.pictureBox_ColoredSprite.Image = result;
        }

        private async void AsyncUpdate()
        {
            while(true)
            {
                button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);

                button_ColorTransition_Edit.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);
                button_ColorTransition_EditHSV.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);
                button_ColorTransition_Remove.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);

                if(_spriteSheetHandler.ActiveSpriteParts.Count > 0)
                {
                    try
                    {
                        UpdateColoredSprite();
                    }
                    catch(Exception e) { /* Ignore */ }
                }
                else if(pictureBox_ColoredSprite.Image != null)
                {
                    pictureBox_ColoredSprite.Image = null;
                }

                await Task.Delay(100);
            }
        }

        


        //////////////////////////////////
        /*            Events            */
        //////////////////////////////////
        /**/

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set validate names and check file exists to false otherwise windows will
                // not let you select "Folder Selection."
                openFileDialog.ValidateNames = false;
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                // Always default to Folder Selection.
                openFileDialog.FileName = "Folder Selection";

                //openFileDialog.InitialDirectory = "c:\\";
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(openFileDialog.FileName);

                    string[] files = Directory.GetFiles(folderPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    bool foundAtLeastOneFile = false;

                    _spriteSheetHandler.ActiveSpriteParts.Clear();
                    foreach (string spritePart in SpriteSheetHandler.SpriteParts)
                    {
                        string fullPath = folderPath + @"\" + spritePart + ".png";

                        if (files.Contains(fullPath))
                        {
                            foundAtLeastOneFile = true;

                            Bitmap b = new Bitmap(folderPath + @"\" + spritePart + ".png");

                            _spriteSheetHandler.TrySetSpriteBitmap(spritePart, b.Clone(new Rectangle(0, 0, b.Width, b.Height), b.PixelFormat));

                            b.Dispose();
                            _spriteSheetHandler.ActiveSpriteParts.Add(spritePart);
                        }
                    }

                    if (foundAtLeastOneFile)
                    {
                        _colorTransitionHandler.Clear();

                        _lastDirectory = folderPath;
                        this.Text = $"{_lastDirectory} - {AppPreferences.ApplicationName}";

                        AddOriginalColors();

                        string[] chestFiles = System.IO.Directory.GetFiles(folderPath, "*.chest");
                        string[] headFiles = System.IO.Directory.GetFiles(folderPath, "*.head");
                        string[] legsFiles = System.IO.Directory.GetFiles(folderPath, "*.legs");
                        string[] backFiles = System.IO.Directory.GetFiles(folderPath, "*.back");

                        if (chestFiles.Length > 0)
                        {
                            try
                            {
                                AddTransitionColorsFromJSON(chestFiles[0]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"The file {Path.GetFileName(chestFiles[0])} has an invalid structure, is damaged or the \"colorOptions\" key is missing.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                        }
                        if (headFiles.Length > 0)
                        {
                            try
                            {
                                AddTransitionColorsFromJSON(headFiles[0]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"The file {Path.GetFileName(headFiles[0])} has an invalid structure, is damaged or the \"colorOptions\" key is missing.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                        }

                        if (legsFiles.Length > 0)
                        {
                            try
                            {
                                AddTransitionColorsFromJSON(legsFiles[0]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"The file {Path.GetFileName(legsFiles[0])} has an invalid structure, is damaged or the \"colorOptions\" key is missing.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                        }

                        if (backFiles.Length > 0)
                        {
                            try
                            {
                                AddTransitionColorsFromJSON(backFiles[0]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"The file {Path.GetFileName(backFiles[0])} has an invalid structure, is damaged or the \"colorOptions\" key is missing.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            }
                        }

                        EnableAllImportantControllers();

                        UpdateTransitionListView();

                        OnPoseChange();
                    }
                }
            }
        }

        private void comboBox_Pose_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPoseChange();
        }

        private void comboBox_Frame_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOriginalSprite();
        }

        private void listView_OriginalSpriteColors_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);
        }

        private void button_ColorTransition_Add_Click(object sender, EventArgs e)
        {
            if(listView_OriginalSpriteColors.SelectedItems.Count > 0)
            {
                bool madeChange = false;

                for(int i = 0; i < listView_OriginalSpriteColors.SelectedItems.Count; i++)
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

                if(madeChange)
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
            if(listView_ColorTransition.SelectedItems.Count > 0)
            {
                List<ListViewItem> itemList = new List<ListViewItem>();

                foreach(ListViewItem item in listView_ColorTransition.SelectedItems)
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
            if (_remindToSaveFlag)
            {
                switch (MessageBox.Show("You have unsaved changes. Would you like to save the changes?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        string jsonText = RulesProcessing.GetJSONStringFromColorTransitionHandler(_colorTransitionHandler);

                        Stream myStream;
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                        saveFileDialog1.Filter = "Normal text file (*.txt)|*.txt|All files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 1;
                        saveFileDialog1.RestoreDirectory = true;

                        switch (saveFileDialog1.ShowDialog())
                        {
                            case DialogResult.OK:
                                _remindToSaveFlag = false;
                                
                                if ((myStream = saveFileDialog1.OpenFile()) != null)
                                {
                                    byte[] data = Encoding.ASCII.GetBytes(jsonText);

                                    myStream.Write(data, 0, data.Length);

                                    myStream.Close();
                                }
                                break;
                            default:
                                return;
                        }

                        break;
                    case DialogResult.No:
                        _remindToSaveFlag = false;

                        break;
                    default:
                        break;
                }
            }

            _colorTransitionHandler.Clear();
            _spriteSheetHandler.ActiveSpriteParts.Clear();

            this.listView_OriginalSpriteColors.Items.Clear();
            this.listView_ColorTransition.Items.Clear();

            this.pictureBox_OriginalSprite.Image = null;
            this.pictureBox_ColoredSprite.Image = null;

            DisableAllImportantControllers();

            this.Text = AppPreferences.ApplicationName;

            _lastDirectory = String.Empty;
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
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Normal text file (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            try 
            {
                Dictionary<string, List<ColorTransitionItem>> importedItems = RulesProcessing.GetColorTransitionsFromJSON(filePath);

                bool importConflict = false;
                int transitionItemsCount = 0;
                foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                {
                    if (!importedItems.ContainsKey(colorEnum)) continue;

                    transitionItemsCount += this._colorTransitionHandler[colorEnum].Count;

                    foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                    {
                        foreach (ColorTransitionItem item2 in this._colorTransitionHandler[colorEnum])
                        {
                            if (item1.ColorFrom == item2.ColorFrom)
                            {
                                importConflict = true;
                            }
                        }
                    }
                }

                if (transitionItemsCount > 0)
                {
                    MergeReplaceOrCancelForm importModeForm = new MergeReplaceOrCancelForm();
                    importModeForm.ShowDialog();

                    if (importModeForm.DialogResult == DialogResult.Yes)
                    {
                        if (!importConflict)
                        {
                            foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                            {
                                if (!importedItems.ContainsKey(colorEnum)) continue;

                                foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                                {
                                    this._colorTransitionHandler[colorEnum].Add(item1);
                                }
                            }

                            UpdateTransitionListView();
                        }
                        else
                        {
                            string text = "Conflicting records found. Do you want conflicting imported records to replace the current ones?";
                            switch (MessageBox.Show(text, "Transition Conflict", MessageBoxButtons.YesNoCancel))
                            {
                                case DialogResult.Yes:
                                    foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                                    {
                                        if (!importedItems.ContainsKey(colorEnum)) continue;

                                        foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                                        {
                                            bool replaced = false;

                                            foreach (ColorTransitionItem item2 in this._colorTransitionHandler[colorEnum])
                                            {
                                                if (item1.ColorFrom == item2.ColorFrom)
                                                {
                                                    item2.ColorTo = item1.ColorTo;

                                                    replaced = true;
                                                    break;
                                                }
                                            }

                                            if (!replaced)
                                            {
                                                this._colorTransitionHandler[colorEnum].Add(item1);
                                            }
                                        }
                                    }
                                    break;
                                case DialogResult.No:
                                    foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                                    {
                                        foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                                        {
                                            bool skip = false;

                                            foreach (ColorTransitionItem item2 in this._colorTransitionHandler[colorEnum])
                                            {
                                                if (item1.ColorFrom == item2.ColorFrom)
                                                {
                                                    skip = true;
                                                    break;
                                                }
                                            }

                                            if (!skip)
                                            {
                                                this._colorTransitionHandler[colorEnum].Add(item1);
                                            }
                                        }
                                    }
                                    break;
                                case DialogResult.Cancel:
                                    return;
                                default:
                                    return;
                            }

                            UpdateTransitionListView();
                        }
                    }
                    else if (importModeForm.DialogResult == DialogResult.No)
                    {
                        _colorTransitionHandler.Clear();

                        foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                        {
                            if (!importedItems.ContainsKey(colorEnum)) continue;

                            foreach (ColorTransitionItem item in importedItems[colorEnum])
                            {
                                _colorTransitionHandler[colorEnum].Add(item);
                            }
                        }

                        UpdateTransitionListView();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                    {
                        if (!importedItems.ContainsKey(colorEnum)) continue;

                        foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                        {
                            this._colorTransitionHandler[colorEnum].Add(item1);
                        }
                    }

                    UpdateTransitionListView();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("The file has an invalid structure, is damaged or the \"colorOptions\" key is missing.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void pictureBox_OriginalSprite_Click(object sender, EventArgs e)
        {
            Bitmap b = (Bitmap)pictureBox_OriginalSprite.Image;

            if (b == null) return;

            Point mousePosRelativeToControl = ((Control)sender).PointToClient(System.Windows.Forms.Cursor.Position);

            Color c = b.GetPixel(mousePosRelativeToControl.X, mousePosRelativeToControl.Y);

            if(c.A > 0)
            {
                bool skip = false;
                foreach (ListViewItem existingItem in listView_ColorTransition.Items)
                {
                    if (existingItem.SubItems[0].BackColor == c) skip = true;
                }
                if (skip) return;

                ColorTransitionItem colorTransitionItem = new ColorTransitionItem(c, c);

                _colorTransitionHandler[comboBox_ColorOption.Text].Add(colorTransitionItem);
                UpdateTransitionListView();

                OnChangeDone();
            }
        }

        private void pictureBox_ColoredSprite_Click(object sender, EventArgs e)
        {
            Bitmap b = (Bitmap)pictureBox_ColoredSprite.Image;

            if (b == null) return;

            Point mousePosRelativeToControl = ((Control)sender).PointToClient(System.Windows.Forms.Cursor.Position);

            Color c = b.GetPixel(mousePosRelativeToControl.X, mousePosRelativeToControl.Y);

            if (c.A > 0)
            {
                bool skip = false;
                foreach (ListViewItem existingItem in listView_ColorTransition.Items)
                {
                    if (existingItem.SubItems[3].BackColor == c) skip = true;
                }
                if (!skip)
                {
                    ColorTransitionItem colorTransitionItem = new ColorTransitionItem(c, c);

                    _colorTransitionHandler[comboBox_ColorOption.Text].Add(colorTransitionItem);
                    UpdateTransitionListView();

                    OnChangeDone();
                }

                List<ListViewItem> items = new List<ListViewItem>();
                foreach (ListViewItem existingItem in listView_ColorTransition.Items)
                {
                    if (existingItem.SubItems[3].BackColor == c)
                    {
                        items.Add(existingItem);
                    }
                }

                OpenColorTransitionEditor(items.ToArray());
            }
        }

        private void colorOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string jsonText = RulesProcessing.GetJSONStringFromColorTransitionHandler(_colorTransitionHandler);

            new OutputForm(jsonText).ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_remindToSaveFlag) return;

            switch (MessageBox.Show("You have unsaved changes. Would you like to save the changes?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    string jsonText = RulesProcessing.GetJSONStringFromColorTransitionHandler(_colorTransitionHandler);

                    Stream myStream;
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.Filter = "Normal text file (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;

                    switch (saveFileDialog1.ShowDialog())
                    {
                        case DialogResult.OK:
                            if ((myStream = saveFileDialog1.OpenFile()) != null)
                            {
                                byte[] data = Encoding.ASCII.GetBytes(jsonText);

                                myStream.Write(data, 0, data.Length);

                                myStream.Close();
                            }
                            break;
                        default:
                            e.Cancel = true;
                            break;
                    }

                    break;
                case DialogResult.No:
                    break;
                default:
                    break;

            }
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
                System.Diagnostics.Process.Start(@"https://github.com/Demexis/Starbound---Color-Options-Picker");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string html = "<!doctype html><html lang='en'> <head> <meta charset='utf - 8'> <title>D. Sorochinsky - HTML 1.PW - Battlestar Galactica</title> <meta name='description' content='Small HTML doc'> <meta name='viewport' content='width = device - width, initial - scale = 1'> <link rel='stylesheet' href='https://fonts.googleapis.com/css2?family=Roboto:wght@300;400&display=swap'> </head><style>* { box-sizing: border-box;}html { font-family: -applesystem, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sansserif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'; font-size: 14px; color: #212121; line-height: 1.5;}body { margin: 0; background-image: url('https://wallpaperbat.com/img/225446-minimalist-space-wallpaper-top-free-minimalist-space.png'); background-repeat: no-repeat; background-attachment: fixed; background-size: cover;}h1, h2, h3 { font-family: 'Roboto', cursive; padding: 1rem;}header { background-color: rgb(72, 100, 100); padding: 4rem 1rem; border: 2px solid rgb(0, 217, 255); border-radius: 5px; color: #c7ffe0; box-shadow: 0px 0px 40px rgba(255, 255, 255, 0.596);}nav { background-color: rgb(45, 65, 151); padding: 1rem; border: 2px solid rgb(0, 217, 255); border-radius: 5px; box-shadow: 0px 0px 40px rgba(255, 255, 255, 0.596);}footer { background-color: rgb(0, 0, 0); padding: 4rem 1rem; margin-top: 4rem; border: 2px solid rgb(0, 217, 255); border-radius: 5px; color: #c7ffe0; box-shadow: 0px 0px 40px rgba(255, 255, 255, 0.596);}header p, footer p { margin: 0;}nav ul { margin: 0; padding: 0;}nav ul li { list-style-type: none; display: inline-block;}nav ul li::after { content: ' § ';}nav ul li:last-child::after { content: '';}nav ul li a { color:rgb(255, 190, 50);text-decoration:none}main { padding: 0 1rem;}main p { text-align: justify; padding: 1rem;}.related-panel h3 { text-align: center;}.related-panel img { display: block; margin-left: auto; margin-right: auto; max-width: 100%;}figure { display: block; width: 100%; margin: 0; position: relative;}figure img { display: block; width: 100%;}figure figcaption { position: absolute; left: 0; bottom: 0; display: block; width: 100%; padding: 0.5rem 1rem; background-color: rgba(0, 0, 0, 0.3); color: white; font-family: 'Roboto', cursive;}@media screen and (min-width: 992px) { html { font-size: 16px; } main { padding: 0; } .container { display: block; width: 992px; margin-left: auto; margin-right: auto; } .container_body { display: block; width: 992px; margin-left: auto; margin-right: auto; background-color: rgba(203, 213, 218, 0.98); border: 2px solid rgb(0, 217, 255); border-radius: 5px; box-shadow: 0px 0px 40px rgba(255, 255, 255, 0.596); } } .columns-2 { display: flex; flex-direction: row; flex-wrap: nowrap; justify-content: space-between; align-items: stretch;}.columns-2 article { flex: 2; margin-right: 0.5rem;}.columns-2 aside { flex: 1; margin-left: 0.5rem;}.related-grid { display: grid; grid-template-columns: 1fr 1fr 1fr; column-gap: 1rem; row-gap: 1rem;}.related-panel { padding: 1rem;}table.modern { /* platums */ width: 100%; /* likvidē atstarpi starp šūnām */ border-collapse: collapse; /* tabulas ārējais rāmis */ border: 1px solid #01579B;}/* tabulas galvenes šūnas */table.modern th { /* šūnu rāmis */ border: 1px solid #01579B; /* atkāpe */ padding: 0.25rem 0.5rem; /* fona krāsa */ background-color: #01579B; /* teksta krāsa */ color: #FFFFFF; /* teksta līdzināšana */ text-align: left;}/* tabulas datu šūnas */table.modern td { /* šūnu rāmis */ border: 1px solid #01579B; /* atkāpe */ padding: 0.25rem 0.5rem; /* teksta krāsa */ color: #212121;}/* hipersaites tabulas datu šūnās */table.modern td a { /* teksta krāsa */ color: #2d8f70; /* teksta biezums */ font-weight: bold;}/* tabulas rindu 'zebras' krāsojums *//* selektors 'tr:nth-child(even)' atlasa katru otro tabulas rindu */table.modern tr:nth-child(even) td { /* fona krāsa */ background-color: #E1F5FE;}/* tabulas rindu izcēluma (hover) krāsojums *//* selektors 'tr:hover' darbojas tikai tad, kad kursors atrodas virs tabulas rindas */table.modern tr:hover td { /* fona krāsa */ background-color: #B3E5FC;}</style> <body> <!-- galvene --> <header class='container'> <p>1. Practical Work</p> </header> <!-- navigācijas bloks --> <nav class='container'> <ul> <li> <a href='https://en.wikipedia.org/wiki/Battlestar_Galactica' target='_blank'>Wikipedia</a> </li> <li> <a href='https://galactica.fandom.com/wiki/Battlestar_Galactica_Wiki' target='_blank'>Wiki Fandom</a> </li> <li> <a href='https://www.google.com/search?q=battlestar+galactica&sxsrf=AOaemvL-CzyGr_JFWwqgs7JhPbyGDQ9M2A%3A1634932835705&source=hp&ei=YxhzYfzNKKOorgT6opf4AQ&iflsig=ALs-wAMAAAAAYXMmcySlm3TBLgFwXj6TIdHPTVxg_rZ0&oq=battlestar+galactica&gs_lcp=Cgdnd3Mtd2l6EAMYADIECCMQJzIECCMQJzIECCMQJzIFCC4QywEyBQgAEIAEMgUILhCABDIFCC4QgAQyBQgAEIAEMgUILhCABDIFCAAQgAQ6BAgAEEM6BQgAEJECOgoIABCABBCHAhAUOgQILhBDOgoILhDHARDRAxBDOgsILhCABBDHARDRAzoFCAAQywFQnwJY0hBgshpoAHAAeACAAYkBiAHxCZIBBDEzLjKYAQCgAQE&sclient=gws-wiz' target='_blank'>Google Search</a> </li> <li> <a href='https://git-scm.com/' target='_blank'>Git</a> </li> </ul> </nav> <!-- saturs --> <main class='container_body'> <div class='columns-2'> <article> <h1>Battlestar Galactica</h1> <figure> <img src='https://www.kotaku.com.au/content/uploads/sites/3/2017/08/02/lekiwrpl78nqsmih8j8a.jpg' alt='Battlestar_Galactica_Logo'> <figcaption>Battlestar Galactica</figcaption> </figure> <p><em>Battlestar Galactica</em> (BSG) is an American military science fiction television series, and part of the Battlestar Galactica franchise.</p> <p>A group of humans aboard a battleship, Battlestar Galactica, are forced to abandon their planet after being attacked by Cylons. They try to evade the Cylons while searching for their true home, Earth.</p> </article> <aside> <h2>Characters</h2> <table class='modern'> <tr> <th>Full Name</th> <th>Homeworld</th> </tr> <tr> <td>&ensp;William 'Bill' Adama</td> <td>&ensp;Caprica</td> </tr> <tr> <td>&ensp;Leland Adama</td> <td>&ensp;Caprica</td> </tr> <tr> <td>&ensp;Gaius Baltar</td> <td>&ensp;Aerilon</td> </tr> <tr> <td>&ensp;Kara Thrace</td> <td>&ensp;Caprica</td> </tr> <tr> <td>&ensp;Laura Roslin</td> <td>&ensp;Caprica</td> </tr> <tr> <td>&ensp;Helena Cain</td> <td>&ensp;Tauron</td> </tr> <tr> <td>&ensp;Sharon Valerii</td> <td>&ensp;The Colony</td> </tr> </table> </aside> </div> <section> <h2>Main characters</h2> <div class='related-grid'> <section class='related-panel'> <h3>William 'Bill' Adama</h3> <img src='https://static.wikia.nocookie.net/galactica/images/c/ce/William_Adama_headshot.jpg/revision/latest/scale-to-width-down/338?cb=20100326215247' alt='William'> <p>Admiral William Adama was an officer in the Colonial Fleet. Born to a Caprican family of Tauron heritage, Adama was conscripted into military service during the Cylon War, in which he served as a Raptor and Viper pilot under the callsign 'Husker'.</p> </section> <section class='related-panel'> <h3>Leland Adama</h3> <img src='https://static.wikia.nocookie.net/galactica/images/3/37/LeeAdama.jpg/revision/latest/scale-to-width-down/350?cb=20090601114012' alt='Leland'> <p>Leland Joseph Adama, commonly referred to as just 'Lee' or by his call sign of 'Apollo', is a former Colonial Fleet Reserve officer who became the Caprican delegate to the Quorum of Twelve, then later the interim President of the Twelve Colonies of Kobol. He is the sole surviving son of William Adama.</p> </section> <section class='related-panel'> <h3>Gaius Baltar</h3> <img src='https://static.wikia.nocookie.net/galactica/images/3/3b/BaltarSeason4.png/revision/latest/scale-to-width-down/350?cb=20180805033032' alt='Gaius'> <p>Dr. Gaius Baltar was an accomplished computer scientist of Aerelonean descent. Shunning his farming background, Baltar became a celebrity figure with political connections, which enabled a successful push for the re-introduction of software networking in military vessels in the aftermath of the Cylon War.</p> </section> <section class='related-panel'> <h3>Laura Roslin</h3> <img src='https://static.wikia.nocookie.net/galactica/images/2/27/LauraRoslinS4.jpg/revision/latest/scale-to-width-down/350?cb=20180805034141' alt='Laura'> <p>Laura Roslin was the President of the United Colonies of Kobol, having served as Secretary of Education under President Richard Adar, and sworn into office as the highest ranked government official who survived the Fall of the Twelve Colonies. Roslin subsequently became the political leader of the remnants of the Colonial fleet, along with Commander William Adama.</p> </section> <section class='related-panel'> <h3>Kara Thrace</h3> <img src='https://static.wikia.nocookie.net/galactica/images/5/55/Kara_Thrace.jpg/revision/latest/scale-to-width-down/350?cb=20090601070409' alt='Kara'> <p>Kara 'Starbuck' Thrace was a Viper pilot in the Colonial Fleet. She is a gifted Viper pilot, with an attitude that at times thwarts her career advancement.</p> </section> </div> </section> </main> <!-- kājene --> <footer> <p class='container'>Devid Sorochinsky, VeA, 2021</p> </footer> </body></html>";
            File.WriteAllText("tempHtml.html", html);

            //System.Diagnostics.Process.Start("http:");

            System.Diagnostics.Process.Start("tempHtml.html");

            Thread.Sleep(1000);
            //System.Diagnostics.Process.Start("about:Blank", html);

            if(File.Exists("tempHtml.html"))
            {
                File.Delete("tempHtml.html");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Color previousColor = pictureBox1.BackColor;

            switch(colorDialog1.ShowDialog())
            {
                case DialogResult.OK:
                    Color c = colorDialog1.Color;

                    pictureBox1.BackColor = c;
                    pictureBox_ColoredSprite.BackColor = c;
                    pictureBox_OriginalSprite.BackColor = c;
                    break;
                default:
                    pictureBox1.BackColor = previousColor;
                    pictureBox_ColoredSprite.BackColor = previousColor;
                    pictureBox_OriginalSprite.BackColor = previousColor;
                    break;
            }
        }
    }
}
