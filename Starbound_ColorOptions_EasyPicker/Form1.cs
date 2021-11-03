using Newtonsoft.Json.Linq;
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
    public partial class MainForm : Form
    {
        private static Rules.Sex _sex = Rules.Sex.Male;

        private string _lastDirectory = String.Empty;
        private bool _dontDraw;
        private bool _remindToSaveFlag = false;

        private Dictionary<string, Bitmap> _originalSpriteBitmaps = new Dictionary<string, Bitmap>()
        {
            ["Bsleeve"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault),
            ["back"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault),
            ["pants"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault),
            ["head"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault),
            ["chestm"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault),
            ["chestf"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault),
            ["Fsleeve"] = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault)
        };

        private Dictionary<string, bool> _activeBitmaps = new Dictionary<string, bool>();

        private Dictionary<string, List<ListViewItem>> _colorTransitions;

        public MainForm()
        {
            InitializeComponent();

            InitializePictureBox();

            InitializeComboBoxes();

            InitializeRadioButtons();

            InitializeColorTransitionDictionary();

            DisableAllImportantControllers();

            AsyncUpdate();
        }

        public void OnChangeDone()
        {
            _remindToSaveFlag = true;
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

        private async void AsyncUpdate()
        {
            while(true)
            {
                button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);

                button_ColorTransition_Edit.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);
                button_ColorTransition_Remove.Enabled = (listView_ColorTransition.SelectedItems.Count > 0);

                if(_activeBitmaps.Count > 0)
                {
                    try
                    {
                        ShowColoredSprite((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex);
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

        private void InitializeColorTransitionDictionary()
        {
            _colorTransitions = new Dictionary<string, List<ListViewItem>>()
            {
                [nameof(Rules.ColorOptions.Default)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Black)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Grey)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.White)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Red)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Orange)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Yellow)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Green)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Blue)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Purple)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Pink)] = new List<ListViewItem>(),
                [nameof(Rules.ColorOptions.Brown)] = new List<ListViewItem>()
            };
        }

        private void InitializeComboBoxes()
        {
            comboBox_ColorOption.Items.AddRange((String[])Enum.GetNames(typeof(Rules.ColorOptions)));
            comboBox_ColorOption.SelectedIndex = 0;

            comboBox_Pose.Items.AddRange(Rules.PosesNNumOfFrames.Keys.ToArray());
            comboBox_Pose.SelectedIndex = 0;
            OnPoseChange();
        }

        private Rectangle GetFrameRect(string key, string pose, int frame)
        {
            Rectangle rectangle = new Rectangle(Rules.PosesNCoordinates[key][pose][frame].X * Rules.BitmapSizeDefault,
                                                Rules.PosesNCoordinates[key][pose][frame].Y * Rules.BitmapSizeDefault,
                                                Rules.BitmapSizeDefault,
                                                Rules.BitmapSizeDefault);

            Point offset = Rules.PosesNOffsets[key][pose][frame];

            rectangle.X -= offset.X;
            rectangle.Width += offset.X;

            rectangle.Y += offset.Y;
            rectangle.Height -= offset.Y;

            return rectangle;
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

        private void OnFrameChange()
        {
            if(!String.IsNullOrWhiteSpace(_lastDirectory) && !_dontDraw)
                ShowOriginalSprite((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex);
        }

        private void OnGenderChange()
        {
            if (!String.IsNullOrWhiteSpace(_lastDirectory) && !_dontDraw)
                ShowOriginalSprite((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex);
        }

        private void ShowOriginalSprite(string pose, int frame)
        {
            Bitmap result = GetMergedOriginalBitmap(pose, frame);
            result = BitmapProcessing.GetInterpolatedBitmap(result, this.pictureBox_OriginalSprite.Size);

            // Display the result.
            this.pictureBox_OriginalSprite.Image = result;

            AddOriginalColors();
        }

        private void ShowColoredSprite(string pose, int frame)
        {
            Bitmap result = GetMergedOriginalBitmap(pose, frame);
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

        private Bitmap GetMergedOriginalBitmap(string pose, int frame)
        {
            Bitmap result = new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault);

            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach (string key in _originalSpriteBitmaps.Keys)
            {
                bool ignore = (key == "chestm" && _sex == Rules.Sex.Female) || (key == "chestf" && _sex == Rules.Sex.Male);

                if (!ignore && _activeBitmaps.ContainsKey(key))
                {
                    Rectangle rectangle = GetFrameRect(key, pose, frame);

                    System.Drawing.Imaging.PixelFormat format = _originalSpriteBitmaps[key].PixelFormat;

                    // Clone a portion of the Bitmap object.
                    Bitmap bPart = _originalSpriteBitmaps[key].Clone(rectangle, format);

                    bitmaps.Add(bPart);
                }
            }

            result = BitmapProcessing.GetMergedBitmaps(bitmaps.ToArray());

            return result;
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
        }

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

                    Console.WriteLine($"Folder: {folderPath}");
                    Console.WriteLine($"--------------------");

                    string[] files = Directory.GetFiles(folderPath);

                    foreach(string file in files)
                    {
                        Console.WriteLine(file);
                    }
                    Console.WriteLine($"--------------------");

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    bool foundAtLeastOneFile = false;

                    List<string> searchingFileNames = new List<string>() 
                    { 
                        "Bsleeve", 
                        "chestf", 
                        "chestm", 
                        "Fsleeve", 
                        "head", 
                        "pants", 
                        "back"
                    };

                    _activeBitmaps.Clear();
                    Console.WriteLine($"Matching:");
                    Console.WriteLine($"--------------------");
                    foreach (string file in searchingFileNames)
                    {

                        Console.WriteLine($"Full Path: {folderPath + @"\" + file + ".png"}");
                        if (files.Contains(folderPath + @"\" + file + ".png"))
                        {
                            foundAtLeastOneFile = true;

                            Bitmap b = new Bitmap(folderPath + @"\" + file + ".png");

                            Console.WriteLine($"Before: {_originalSpriteBitmaps[file].Width}, {_originalSpriteBitmaps[file].Height}");
                            _originalSpriteBitmaps[file] = b.Clone(new Rectangle(0, 0, b.Width, b.Height), b.PixelFormat);
                            Console.WriteLine($"After: {_originalSpriteBitmaps[file].Width}, {_originalSpriteBitmaps[file].Height}");

                            Console.WriteLine($"Result: OK. File: {file}");

                            _activeBitmaps.Add(file, true);
                        }
                    }
                    Console.WriteLine($"--------------------");

                    if (foundAtLeastOneFile)
                    {
                        InitializeColorTransitionDictionary();

                        _lastDirectory = folderPath;
                        this.Text = $"{_lastDirectory} - SB Color Options Picker";

                        AddOriginalColors();

                        string[] chestFiles = System.IO.Directory.GetFiles(folderPath, "*.chest");
                        string[] headFiles = System.IO.Directory.GetFiles(folderPath, "*.head");
                        string[] legsFiles = System.IO.Directory.GetFiles(folderPath, "*.legs");
                        string[] backFiles = System.IO.Directory.GetFiles(folderPath, "*.back");

                        if(chestFiles.Length > 0)
                            AddTransitionColorsFromJSON(chestFiles[0]);

                        if (headFiles.Length > 0)
                            AddTransitionColorsFromJSON(headFiles[0]);

                        if (legsFiles.Length > 0)
                            AddTransitionColorsFromJSON(legsFiles[0]);

                        if (backFiles.Length > 0)
                            AddTransitionColorsFromJSON(backFiles[0]);
                    }
                    UpdateTransitionListView();

                    OnPoseChange();
                    EnableAllImportantControllers();
                }
            }
        }

        private void AddTransitionColorsFromJSON(string file)
        {
            Dictionary<string, List<Color>> transitionsFromColor = new Dictionary<string, List<Color>>();
            Dictionary<string, List<Color>> transitionsToColor = new Dictionary<string, List<Color>>();

            JObject json = JObject.Parse(File.ReadAllText(file));

            JToken colorOptionToken;

            if (json.TryGetValue("colorOptions", out colorOptionToken))
            {
                //Console.WriteLine("JArray: " + colorOptionToken.ToString());

                JArray colorArray = JArray.Parse(colorOptionToken.ToString());

                int length = Math.Min(colorArray.Count, Enum.GetNames(typeof(Rules.ColorOptions)).Length);

                int i = 0;
                foreach (JToken child in colorArray)
                {
                    if (i >= Enum.GetNames(typeof(Rules.ColorOptions)).Length)
                        break;

                    try
                    {
                        List<Color> transitionFromColor = new List<Color>();
                        List<Color> transitionToColor = new List<Color>();

                        JObject keyValuePairs = JObject.Parse(child.ToString());

                        if (i == 1) Console.WriteLine("ColorOption: " + ((Rules.ColorOptions)i).ToString());
                        if (i == 1) Console.WriteLine("JToken Child: " + child.ToString());

                        foreach (KeyValuePair<string, JToken> pair in keyValuePairs)
                        {
                            string keyHexCode, valueHexCode;
                            Color keyColor, valueColor;
                            if (i == 1) Console.WriteLine("JToken Pair: " + pair.ToString());
                            keyHexCode = '#' + pair.Key;
                            valueHexCode = '#' + (string)pair.Value;
                            keyColor = System.Drawing.ColorTranslator.FromHtml(keyHexCode);
                            valueColor = System.Drawing.ColorTranslator.FromHtml(valueHexCode);

                            if (i == 1) Console.WriteLine("Not Contains: " + (!transitionFromColor.Contains(keyColor)).ToString());
                            if (!transitionFromColor.Contains(keyColor))
                            {
                                transitionFromColor.Add(keyColor);
                                transitionToColor.Add(valueColor);
                            }
                        }

                        transitionsFromColor.Add(((Rules.ColorOptions)i).ToString(), transitionFromColor);
                        transitionsToColor.Add(((Rules.ColorOptions)i).ToString(), transitionToColor);

                        i++;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            foreach(string key in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!transitionsFromColor.ContainsKey(key)) continue;
                if (!transitionsToColor.ContainsKey(key)) continue;

                List<Color> cFrom = transitionsFromColor[key];
                List<Color> cTo = transitionsToColor[key];

                Console.WriteLine(cFrom.Count);

                for (int i = 0; i < cFrom.Count; i++)
                {
                    bool skip = false;
                    foreach(ListViewItem item in _colorTransitions[key])
                    {
                        if (item.SubItems[0].BackColor == cFrom[i]) skip = true;
                    }

                    if(!skip)
                    {
                        string[] row = { string.Empty, ColorProcessing.HexConverter(cFrom[i]), string.Empty, string.Empty, ColorProcessing.HexConverter(cTo[i]) };

                        ListViewItem listViewItem = new ListViewItem(row);
                        listViewItem.UseItemStyleForSubItems = false;
                        listViewItem.SubItems[0].BackColor = cFrom[i];
                        listViewItem.SubItems[3].BackColor = cTo[i];

                        _colorTransitions[key].Add(listViewItem);
                    }
                }
            }
        }

        private Dictionary<string, List<ListViewItem>> GetListViewItemsFromJSON(string filePath)
        {
            Dictionary<string, List<Color>> transitionsFromColor = new Dictionary<string, List<Color>>();
            Dictionary<string, List<Color>> transitionsToColor = new Dictionary<string, List<Color>>();

            JObject json = JObject.Parse(File.ReadAllText(filePath));

            JToken colorOptionToken;

            if (json.TryGetValue("colorOptions", out colorOptionToken))
            {
                //Console.WriteLine("JArray: " + colorOptionToken.ToString());

                JArray colorArray = JArray.Parse(colorOptionToken.ToString());

                int length = Math.Min(colorArray.Count, Enum.GetNames(typeof(Rules.ColorOptions)).Length);

                int i = 0;
                foreach (JToken child in colorArray)
                {
                    if (i >= Enum.GetNames(typeof(Rules.ColorOptions)).Length)
                        break;

                    try
                    {
                        List<Color> transitionFromColor = new List<Color>();
                        List<Color> transitionToColor = new List<Color>();

                        JObject keyValuePairs = JObject.Parse(child.ToString());

                        Console.WriteLine(keyValuePairs.ToString());

                        foreach (KeyValuePair<string, JToken> pair in keyValuePairs)
                        {
                            string keyHexCode, valueHexCode;
                            Color keyColor, valueColor;
                            Console.WriteLine("JToken Pair: " + pair.ToString());
                            keyHexCode = '#' + pair.Key;
                            valueHexCode = '#' + (string)pair.Value;
                            keyColor = System.Drawing.ColorTranslator.FromHtml(keyHexCode);
                            valueColor = System.Drawing.ColorTranslator.FromHtml(valueHexCode);

                            Console.WriteLine("Not Contains: " + (!transitionFromColor.Contains(keyColor)).ToString());
                            if (!transitionFromColor.Contains(keyColor))
                            {
                                transitionFromColor.Add(keyColor);
                                transitionToColor.Add(valueColor);
                            }
                        }

                        transitionsFromColor.Add(((Rules.ColorOptions)i).ToString(), transitionFromColor);
                        transitionsToColor.Add(((Rules.ColorOptions)i).ToString(), transitionToColor);

                        i++;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            Dictionary<string, List<ListViewItem>> listViewItems = new Dictionary<string, List<ListViewItem>>();

            foreach(string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!transitionsFromColor.ContainsKey(colorEnum)) continue;
                if (!transitionsToColor.ContainsKey(colorEnum)) continue;

                List<ListViewItem> items = new List<ListViewItem>();

                for(int i = 0; i < transitionsFromColor[colorEnum].Count; i++)
                {
                    string[] row = { string.Empty, ColorProcessing.HexConverter(transitionsFromColor[colorEnum][i]), string.Empty, string.Empty, ColorProcessing.HexConverter(transitionsToColor[colorEnum][i])};

                    ListViewItem item = new ListViewItem(row);
                    item.UseItemStyleForSubItems = false;
                    item.SubItems[0].BackColor = transitionsFromColor[colorEnum][i];
                    item.SubItems[3].BackColor = transitionsToColor[colorEnum][i];

                    items.Add(item);
                }

                listViewItems.Add(colorEnum, items);
            }

            return listViewItems;
        }

        private void AddOriginalColors()
        {
            Bitmap b = GetMergedOriginalBitmap((string)comboBox_Pose.SelectedItem, comboBox_Frame.SelectedIndex);

            listView_OriginalSpriteColors.Items.Clear();
            
            Color[] colors = BitmapProcessing.GetAllColorsFromBitmaps(b);
            //colors = BitmapProcessing.SortColorsByValue(colors);

            for(int i = 0; i < colors.Length; i++)
            {
                string[] row = { string.Empty, ColorProcessing.HexConverter(colors[i]) };

                ListViewItem listViewItem = new ListViewItem(row);

                listView_OriginalSpriteColors.Items.Add(listViewItem);
                listView_OriginalSpriteColors.Items[i].UseItemStyleForSubItems = false;
                listView_OriginalSpriteColors.Items[i].SubItems[0].BackColor = colors[i];
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception("OLOL");
        }

        private void comboBox_Pose_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPoseChange();
        }

        private void comboBox_Frame_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFrameChange();
        }

        private void listView_OriginalSpriteColors_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            button_ColorTransition_Add.Enabled = (listView_OriginalSpriteColors.SelectedItems.Count > 0);
        }

        private void button_ColorTransition_Add_Click(object sender, EventArgs e)
        {
            if(listView_OriginalSpriteColors.SelectedItems.Count > 0)
            {
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

                    string[] row = { string.Empty, ColorProcessing.HexConverter(color), string.Empty, string.Empty, ColorProcessing.HexConverter(color) };

                    ListViewItem transitionItem = new ListViewItem(row);
                    transitionItem.UseItemStyleForSubItems = false;
                    transitionItem.SubItems[0].BackColor = color;
                    transitionItem.SubItems[3].BackColor = color;

                    _colorTransitions[comboBox_ColorOption.Text].Add(transitionItem);
                    //listView_ColorTransition.Items.Add(transitionItem);
                    UpdateTransitionListView();
                }

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

                OpenColorTransitionEditor(itemList.ToArray());
            }
        }

        private void OpenColorTransitionEditor(params ListViewItem[] items)
        {
            new ColorTransitionEditingForm(this, items).ShowDialog();
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
                            _colorTransitions[comboBox_ColorOption.Text].RemoveAt(i);
                            UpdateTransitionListView();
                            //listView_ColorTransition.Items.RemoveAt(i);
                        }
                        catch(Exception ex) { }
                    }
                }

                OnChangeDone();
            }
        }

        private void comboBox_ColorOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTransitionListView();
        }

        private void UpdateTransitionListView()
        {
            if(Enum.GetNames(typeof(Rules.ColorOptions)).ToList().Contains(comboBox_ColorOption.Text))
            {
                if(_colorTransitions != null && _colorTransitions.ContainsKey(comboBox_ColorOption.Text))
                {
                    listView_ColorTransition.Items.Clear();
                    listView_ColorTransition.Items.AddRange(_colorTransitions[comboBox_ColorOption.Text].ToArray());
                }
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

                string[] row = { string.Empty, ColorProcessing.HexConverter(c), string.Empty, string.Empty, ColorProcessing.HexConverter(c) };

                ListViewItem transitionItem = new ListViewItem(row);
                transitionItem.UseItemStyleForSubItems = false;
                transitionItem.SubItems[0].BackColor = c;
                transitionItem.SubItems[3].BackColor = c;

                _colorTransitions[comboBox_ColorOption.Text].Add(transitionItem);
                //listView_ColorTransition.Items.Add(transitionItem);
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
                    string[] row = { string.Empty, ColorProcessing.HexConverter(c), string.Empty, string.Empty, ColorProcessing.HexConverter(c) };

                    ListViewItem transitionItem = new ListViewItem(row);
                    transitionItem.UseItemStyleForSubItems = false;
                    transitionItem.SubItems[0].BackColor = c;
                    transitionItem.SubItems[3].BackColor = c;

                    _colorTransitions[comboBox_ColorOption.Text].Add(transitionItem);
                    //listView_ColorTransition.Items.Add(transitionItem);
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

        private void radioButton_Male_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Male.Checked)
            {
                _sex = Rules.Sex.Male;
                OnGenderChange();
            }
        }

        private void radioButton_Female_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Female.Checked)
            {
                _sex = Rules.Sex.Female;
                OnGenderChange();
            }
        }

        private void colorOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string jsonText = GetJSONStringFromTransitions();

            new OutputForm(jsonText).ShowDialog();

            //Console.WriteLine(json.ToString());
        }

        private string GetJSONStringFromTransitions()
        {
            JObject json = new JObject();

            JArray jArray = new JArray();
            foreach (string colorOption in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                JObject keyValuePairs = new JObject();
                foreach (ListViewItem item in _colorTransitions[colorOption])
                {
                    KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(item.SubItems[1].Text.Substring(1), item.SubItems[4].Text.Substring(1));
                    JToken jToken = JToken.FromObject(keyValuePair);

                    keyValuePairs.Add(new JProperty(keyValuePair.Key, keyValuePair.Value));
                }

                jArray.Add(keyValuePairs);
            }

            json.Add("colorOptions", jArray);

            return json.ToString();

            //Console.WriteLine(json.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    string jsonText = GetJSONStringFromTransitions();

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

        private void colorOptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //string fileContent = string.Empty;
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

                    ////Read the contents of the file into a stream
                    //Stream fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
                else
                {
                    return;
                }
            }

            Dictionary<string, List<ListViewItem>> importedItems = GetListViewItemsFromJSON(filePath);

            Console.WriteLine("COUNT: " + importedItems.Count);

            bool importConflict = false;
            int transitionItemsCount = 0;
            foreach(string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!importedItems.ContainsKey(colorEnum)) continue;

                transitionItemsCount += this._colorTransitions[colorEnum].Count;

                foreach (ListViewItem item1 in importedItems[colorEnum])
                {
                    foreach (ListViewItem item2 in this._colorTransitions[colorEnum])
                    {
                        if(item1.SubItems[0].BackColor == item2.SubItems[0].BackColor)
                        {
                            importConflict = true;
                        }
                    }
                }
            }

            Console.WriteLine(transitionItemsCount);

            if(transitionItemsCount > 0)
            {
                MergeReplaceOrCancelForm importModeForm = new MergeReplaceOrCancelForm();
                importModeForm.ShowDialog();

                if (importModeForm.DialogResult == DialogResult.Yes)
                {
                    if(!importConflict)
                    {
                        foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                        {
                            if (!importedItems.ContainsKey(colorEnum)) continue;

                            foreach (ListViewItem item1 in importedItems[colorEnum])
                            {
                                this._colorTransitions[colorEnum].Add(item1);
                            }
                        }

                        UpdateTransitionListView();
                    }
                    else
                    {
                        string text = "Conflicting records found. Do you want conflicting imported records to replace the current ones?";
                        switch(MessageBox.Show(text, "Transition Conflict", MessageBoxButtons.YesNoCancel))
                        {
                            case DialogResult.Yes:
                                foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                                {
                                    if (!importedItems.ContainsKey(colorEnum)) continue;

                                    foreach (ListViewItem item1 in importedItems[colorEnum])
                                    {
                                        bool replaced = false;

                                        foreach (ListViewItem item2 in this._colorTransitions[colorEnum])
                                        {
                                            if (item1.SubItems[0].BackColor == item2.SubItems[0].BackColor)
                                            {
                                                item2.SubItems[3].BackColor = item1.SubItems[3].BackColor;
                                                item2.SubItems[4].Text = ColorProcessing.HexConverter(item2.SubItems[3].BackColor);

                                                replaced = true;
                                                break;
                                            }
                                        }

                                        if(!replaced)
                                        {
                                            this._colorTransitions[colorEnum].Add(item1);
                                        }
                                    }
                                }
                                break;
                            case DialogResult.No:
                                foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
                                {
                                    foreach (ListViewItem item1 in importedItems[colorEnum])
                                    {
                                        bool skip = false;

                                        foreach (ListViewItem item2 in this._colorTransitions[colorEnum])
                                        {
                                            if (item1.SubItems[0].BackColor == item2.SubItems[0].BackColor)
                                            {
                                                skip = true;
                                                break;
                                            }
                                        }

                                        if (!skip)
                                        {
                                            this._colorTransitions[colorEnum].Add(item1);
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
                else if(importModeForm.DialogResult == DialogResult.No)
                {
                    this._colorTransitions = importedItems;
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

                    foreach (ListViewItem item1 in importedItems[colorEnum])
                    {
                        this._colorTransitions[colorEnum].Add(item1);
                    }
                }

                UpdateTransitionListView();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeColorTransitionDictionary();

            DisableAllImportantControllers();

            UpdateTransitionListView();

            this.pictureBox_OriginalSprite.Image = null;
            this.pictureBox_ColoredSprite.Image = null;

            _activeBitmaps.Clear();

            this.listView_OriginalSpriteColors.Clear();
        }
    }
}
