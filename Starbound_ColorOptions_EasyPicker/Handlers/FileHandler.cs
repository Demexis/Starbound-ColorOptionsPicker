using Newtonsoft.Json.Linq;
using Starbound_ColorOptions_EasyPicker.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker.Handlers
{
    public class FileHandler
    {

        public bool FilesUploadedStatus = false;

        public string LastDirectory = string.Empty;

        public void OpenSpriteFilesExplorer()
        {
            MainForm.Instance.LabelStatus = "Opening...";

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
                    if (FilesUploadedStatus)
                    {
                        MainForm.Instance.CloseOpenedFiles();

                        if (MainForm.Instance.RemindToSaveFlag)
                        {
                            openFileDialog.Dispose();
                            return;
                        }

                        FilesUploadedStatus = false;
                    }

                    MainForm.Instance.LabelStatus = "Looking for files...";

                    string folderPath = Path.GetDirectoryName(openFileDialog.FileName);

                    //string[] files = Directory.GetFiles(folderPath);
                    DirectoryInfo dir = new DirectoryInfo(folderPath);
                    FileInfo[] files = dir.GetFiles();

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    MainForm.Instance.GetSpriteSheetHandler.Clear();

                    bool foundAtLeastOneFile = false;

                    Dictionary<string, Bitmap> foundSprites = new Dictionary<string, Bitmap>();
                    foreach (FileInfo file in files)
                    {
                        foreach (string spritePart in SpriteSheetHandler.SpriteParts)
                        {
                            if (file.Name.ToLower() == (spritePart + ".png").ToLower())
                            {
                                foundAtLeastOneFile = true;

                                Image img;
                                using (var bmpTemp = new Bitmap(file.FullName))
                                {
                                    img = new Bitmap(bmpTemp);
                                }

                                Bitmap b = (Bitmap)img;
                                //Bitmap b = new Bitmap(file.FullName);

                                foundSprites.Add(spritePart, b.Clone(new Rectangle(0, 0, b.Width, b.Height), b.PixelFormat));

                                //_spriteSheetHandler.Add(spritePart, b.Clone(new Rectangle(0, 0, b.Width, b.Height), b.PixelFormat));
                                b.Dispose();
                            }
                        }
                    }


                    MainForm.Instance.GetColorTransitionHandler.Clear();

                    if (foundAtLeastOneFile)
                    {
                        LastDirectory = folderPath;
                        MainForm.Instance.LastDirectory = folderPath;

                        MainForm.Instance.Text = $"{LastDirectory} - {AppPreferences.ApplicationName}";

                        MainForm.Instance.LabelStatus = "Checking .chest, .head, .legs, .back files...";

                        string[] chestFiles = { };
                        string[] headFiles = { };
                        string[] legsFiles = { };
                        string[] backFiles = { };

                        try
                        {
                            chestFiles = System.IO.Directory.GetFiles(folderPath, "*.chest");
                        }
                        catch(Exception ex) { /* Skip... */ }

                        try
                        {
                            headFiles = System.IO.Directory.GetFiles(folderPath, "*.head");
                        }
                        catch (Exception ex) { /* Skip... */ }

                        try
                        {
                            legsFiles = System.IO.Directory.GetFiles(folderPath, "*.legs");
                        }
                        catch (Exception ex) { /* Skip... */ }

                        try
                        {
                            backFiles = System.IO.Directory.GetFiles(folderPath, "*.back");
                        }
                        catch (Exception ex) { /* Skip... */ }


                        HandleSpecificTypeOfFile(AppPreferences.IgnoreChestFiles, folderPath, chestFiles, foundSprites, 
                            new string[] { "Bsleeve", "chestm", "chestf", "Fsleeve", "chest" });

                        HandleSpecificTypeOfFile(AppPreferences.IgnoreHeadFiles, folderPath, headFiles, foundSprites,
                            new string[] { "head" });

                        HandleSpecificTypeOfFile(AppPreferences.IgnoreLegsFiles, folderPath, legsFiles, foundSprites,
                            new string[] { "pants" });

                        HandleSpecificTypeOfFile(AppPreferences.IgnoreBackFiles, folderPath, backFiles, foundSprites,
                            new string[] { "back" });

                        
                        FilesUploadedStatus = true;
                        MainForm.Instance.EnableAllImportantControllers();
                    }
                    else
                    {
                        MainForm.Instance.Text = $"{AppPreferences.ApplicationName}";
                    }

                    MainForm.Instance.AddOriginalColors();

                    MainForm.Instance.UpdateTransitionListView();

                    MainForm.Instance.OnPoseChange();
                }
            }

            MainForm.Instance.LabelStatus = "";
        }

        public void HandleSpecificTypeOfFile(bool ignoreValue, string folderPath, string[] files, Dictionary<string, Bitmap> foundSprites, string[] subParts)
        {
            if (!ignoreValue)
            {
                if (files.Length > 0)
                {
                    try
                    {
                        try
                        {
                            MainForm.Instance.AddTransitionColorsFromJSON(files[0]);
                        }
                        catch (Exception ex) { /* Ignore */ }

                        Bitmap mask = null;

                        if (!AppPreferences.IgnoreMasks)
                        {
                            try
                            {
                                JObject json = JObject.Parse(File.ReadAllText(files[0]));

                                Console.WriteLine(json.ToString());

                                if (json.TryGetValue("mask", out JToken maskToken))
                                {
                                    if (maskToken.Type == JTokenType.String)
                                    {
                                        if (File.Exists(folderPath + @"\" + maskToken.ToString()))
                                        {
                                            Image img;
                                            using (var bmpTemp = new Bitmap(folderPath + @"\" + maskToken.ToString()))
                                            {
                                                img = new Bitmap(bmpTemp);
                                            }

                                            mask = (Bitmap)img;
                                            //mask = new Bitmap(folderPath + @"\" + maskToken.ToString());
                                        }
                                    }
                                }
                            }
                            catch (Exception ex) { /* Ignore */ }
                        }

                        foreach(string subPart in subParts)
                        {
                            if (foundSprites.TryGetValue(subPart, out Bitmap subPartBitmap))
                            {
                                MainForm.Instance.GetSpriteSheetHandler.Add(subPart, subPartBitmap, mask);
                            }
                            else
                            {
                                MainForm.Instance.GetSpriteSheetHandler.Add(subPart);
                            }
                        }

                        if (mask != null)
                        {
                            mask.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"The file {Path.GetFileName(chestFiles[0])} has an invalid structure, is damaged or the \"colorOptions\" key is missing. \n\nException message: {ex.Message}", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    foreach (string subPart in subParts)
                    {
                        if (foundSprites.TryGetValue(subPart, out Bitmap subPartBitmap))
                        {
                            MainForm.Instance.GetSpriteSheetHandler.Add(subPart, subPartBitmap);
                        }
                        else
                        {
                            MainForm.Instance.GetSpriteSheetHandler.Add(subPart);
                        }
                    }
                }
            }
            else
            {
                foreach (string subPart in subParts)
                {
                    // Exception
                    if (subPart.Equals("chest")) continue;

                    MainForm.Instance.GetSpriteSheetHandler.Add(subPart);
                }
            }
        }

        public bool OpenSaveReminder()
        {
            switch (MessageBox.Show("You have unsaved changes. Would you like to save the changes?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
            {
                case DialogResult.Cancel:
                    return false;
                case DialogResult.Yes:
                    OpenTransitionColorsSaver();

                    return true;
                case DialogResult.No:
                    MainForm.Instance.RemindToSaveFlag = false;

                    return true;
                default:
                    return false;
            }
        }

        public void OpenTransitionColorsSaver()
        {
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

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            DateTime localDate = DateTime.Now;

            saveFileDialog1.FileName = $"colorOptions-{localDate.Year}_{localDate.Month}_{localDate.Day}-{localDate.Hour}_{localDate.Minute}_{localDate.Second}";
            saveFileDialog1.Filter = "Normal text file (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.OK:
                    MainForm.Instance.RemindToSaveFlag = false;

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
        }

        public void CloseOpenedFiles()
        {
            FilesUploadedStatus = false;
            MainForm.Instance.CloseOpenedFiles();
        }

        public void OpenTransitionColorsImporter()
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

                ColorTransitionImportExportForm colorTransitionImportingForm = new ColorTransitionImportExportForm(ColorTransitionImportExportForm.DataOperation.Import);
                colorTransitionImportingForm.ShowDialog();

                foreach(Rules.ColorOptions colorOption in AppPreferences.ImportColorOptions.Keys)
                {
                    if(AppPreferences.ImportColorOptions[colorOption] == false)
                    {
                        if(importedItems.ContainsKey(colorOption.ToString()))
                        {
                            importedItems.Remove(colorOption.ToString());
                        }
                    }

                }

                if (ColorTransitionImportExportForm.Canceled) return;

                ColorTransitionHandler colorTransitionHandler = MainForm.Instance.GetColorTransitionHandler;

                bool importConflict = colorTransitionHandler.CheckIfTransitionsWillHaveConflict(importedItems);

                //if (Enum.GetNames(typeof(Rules.ColorOptions)).Any((x) => importedItems.ContainsKey(x)))
                if (colorTransitionHandler.CountAllTransitions > 0 && importedItems.Sum((x) => x.Value.Count) > 0)
                {
                    MergeReplaceOrCancelForm importModeForm = new MergeReplaceOrCancelForm();
                    importModeForm.ShowDialog();

                    if (importModeForm.DialogResult == DialogResult.Yes)
                    {
                        if (!importConflict)
                        {
                            colorTransitionHandler.AddTransitionsFromDictionary(importedItems);

                            MainForm.Instance.UpdateTransitionListView();
                        }
                        else
                        {
                            string text = "Conflicting records found. Do you want conflicting imported records to replace the current ones?";
                            switch (MessageBox.Show(text, "Transition Conflict", MessageBoxButtons.YesNoCancel))
                            {
                                case DialogResult.Yes:
                                    colorTransitionHandler.MergeTransitions(importedItems, false);
                                    break;
                                case DialogResult.No:
                                    colorTransitionHandler.MergeTransitions(importedItems, true);
                                    break;
                                case DialogResult.Cancel:
                                    return;
                                default:
                                    return;
                            }

                            MainForm.Instance.UpdateTransitionListView();
                        }
                    }
                    else if (importModeForm.DialogResult == DialogResult.No)
                    {
                        colorTransitionHandler.Clear();

                        colorTransitionHandler.AddTransitionsFromDictionary(importedItems);

                        MainForm.Instance.UpdateTransitionListView();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    colorTransitionHandler.AddTransitionsFromDictionary(importedItems);

                    MainForm.Instance.UpdateTransitionListView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file has an invalid structure, is damaged or the \"colorOptions\" key is missing.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
