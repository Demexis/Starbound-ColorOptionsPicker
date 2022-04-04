using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public class SpriteSheetHandler
    {
        public bool ShowMannequin = true;

        public static readonly string[] SpriteParts =
        {
            "back",
            "Bsleeve",
            "pants",
            "head",
            "chestm",
            "chestf",
            "chest",
            "Fsleeve"
        };

        public static readonly string[] SpriteAnimations =
        {
            "Idle",
            "Duck",
            "Walk",
            "Run",
            "Jump",
            "Swim"
        };

        public static Dictionary<string, Bitmap> MannequinParts = new Dictionary<string, Bitmap>();
        private Dictionary<string, Bitmap> _originalSpriteBitmaps = new Dictionary<string, Bitmap>();

        public Dictionary<string, Bitmap> HumanParts = new Dictionary<string, Bitmap>();
        public Dictionary<string, Bitmap> ArmorParts = new Dictionary<string, Bitmap>();


        public void Clear()
        {
            foreach(Bitmap bO in _originalSpriteBitmaps.Values)
            {
                bO.Dispose();
            }
            _originalSpriteBitmaps.Clear();

            foreach (Bitmap bA in ArmorParts.Values)
            {
                bA.Dispose();
            }
            ArmorParts.Clear();

            foreach (Bitmap bH in HumanParts.Values)
            {
                bH.Dispose();
            }
            HumanParts.Clear();

            foreach (Bitmap bM in MannequinParts.Values)
            {
                bM.Dispose();
            }
            MannequinParts.Clear();
        }

        public void Add(string spritePart, Bitmap spriteBitmap = null, Bitmap mask = null)
        {
            switch (spritePart)
            {
                case "Bsleeve":
                    MannequinParts.Add("Bsleeve", Properties.Resources.human_Bsleeve);
                    break;
                case "pants":
                    MannequinParts.Add("pants", Properties.Resources.human_pants);
                    break;
                case "head":
                    MannequinParts.Add("head", Properties.Resources.human_head);
                    break;
                case "chestm":
                    MannequinParts.Add("chestm", Properties.Resources.human_chestm);
                    break;
                case "chestf":
                    MannequinParts.Add("chestf", Properties.Resources.human_chestf);
                    break;
                case "chest":
                    MannequinParts.Add("chestm", Properties.Resources.human_chestm);
                    MannequinParts.Add("chestf", Properties.Resources.human_chestf);
                    break;
                case "Fsleeve":
                    MannequinParts.Add("Fsleeve", Properties.Resources.human_Fsleeve);
                    break;
            }

            try
            {
                if (mask != null && spritePart == "chest" && MannequinParts.ContainsKey("chestm") && mask.Width == Rules.BitmapSizeDefault && mask.Height == Rules.BitmapSizeDefault)
                {
                    foreach (string pose in SpriteSheetHandler.SpriteAnimations)
                    {
                        foreach (Point p in Rules.PosesNCoordinates[spritePart][pose])
                        {
                            Point offset = new Point(p.X * Rules.BitmapSizeDefault, p.Y * Rules.BitmapSizeDefault);

                            for (int i = 0; i < mask.Width; i++)
                            {
                                for (int j = 0; j < mask.Height; j++)
                                {
                                    if (mask.GetPixel(i, j).A == 0)
                                    {
                                        //Console.WriteLine("Setting");
                                        MannequinParts["chestm"].SetPixel(offset.X + i, offset.Y + j, Color.Transparent);
                                        MannequinParts["chestf"].SetPixel(offset.X + i, offset.Y + j, Color.Transparent);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (mask != null && MannequinParts.ContainsKey(spritePart) && mask.Width == Rules.BitmapSizeDefault && mask.Height == Rules.BitmapSizeDefault)
                {
                    foreach (string pose in SpriteSheetHandler.SpriteAnimations)
                    {
                        foreach (Point p in Rules.PosesNCoordinates[spritePart][pose])
                        {
                            Point offset = new Point(p.X * Rules.BitmapSizeDefault, p.Y * Rules.BitmapSizeDefault);

                            for (int i = 0; i < mask.Width; i++)
                            {
                                for (int j = 0; j < mask.Height; j++)
                                {
                                    if (mask.GetPixel(i, j).A == 0)
                                    {
                                        MannequinParts[spritePart].SetPixel(offset.X + i, offset.Y + j, Color.Transparent);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { /* Ignore */ }

            try
            {
                if (spriteBitmap != null)
                {
                    // Cut Transparency
                    foreach (string pose in SpriteSheetHandler.SpriteAnimations)
                    {
                        foreach (Point p in Rules.PosesNCoordinates[spritePart][pose])
                        {
                            Point offset = new Point(p.X * Rules.BitmapSizeDefault, p.Y * Rules.BitmapSizeDefault);

                            for (int i = 0; i < Rules.BitmapSizeDefault; i++)
                            {
                                for (int j = 0; j < Rules.BitmapSizeDefault; j++)
                                {
                                    if (spriteBitmap.GetPixel(offset.X + i, offset.Y + j).A < AppPreferences.TransparencyCut)
                                    {
                                        spriteBitmap.SetPixel(offset.X + i, offset.Y + j, Color.Transparent);
                                    }
                                }
                            }
                        }
                    }

                    _originalSpriteBitmaps.Add(spritePart, spriteBitmap);
                }
            }
            catch(Exception ex) { /* Ignore */ }
        }

        public int Count { get { return _originalSpriteBitmaps.Count; } }

        public Bitmap GetSpritePartBitmap(string spritePart)
        {
            Bitmap result = null;

            if(_originalSpriteBitmaps.TryGetValue(spritePart, out result)) { }

            return result;
        }

        public void SetAllSpritePartsForCurrentFrame(string pose, int frame, Rules.Sex sex)
        {
            HumanParts = new Dictionary<string, Bitmap>();
            ArmorParts = new Dictionary<string, Bitmap>();

            foreach (string spritePart in SpriteSheetHandler.SpriteParts)
            {
                bool ignore = (spritePart == "chestm" && sex == Rules.Sex.Female) || (spritePart == "chestf" && sex == Rules.Sex.Male);

                if (ShowMannequin && !ignore)
                {
                    Bitmap humanPart = null;

                    if(MannequinParts.ContainsKey(spritePart))
                    {
                        humanPart = MannequinParts[spritePart];
                    }

                    if(humanPart != null)
                    {
                        Rectangle rectangle = RulesProcessing.GetFrameRect(spritePart, pose, frame);
                        System.Drawing.Imaging.PixelFormat format = humanPart.PixelFormat;

                        Bitmap bPart = humanPart.Clone(rectangle, format);
                        HumanParts.Add(spritePart, bPart);
                    }
                }

                if (!ignore)
                {
                    if(_originalSpriteBitmaps.ContainsKey(spritePart))
                    {
                        if(_originalSpriteBitmaps.ContainsKey(spritePart) && _originalSpriteBitmaps[spritePart] != null)
                        {
                            Rectangle rectangle = RulesProcessing.GetFrameRect(spritePart, pose, frame);

                            System.Drawing.Imaging.PixelFormat format = this.GetSpritePartBitmap(spritePart).PixelFormat;

                            // Clone a portion of the Bitmap object.
                            Bitmap bPart = this.GetSpritePartBitmap(spritePart).Clone(rectangle, format);

                            ArmorParts.Add(spritePart, bPart);
                        }
                    }
                }
            }

        }

        public static Bitmap GetEmptyFrame()
        {
            return new Bitmap(Rules.BitmapSizeDefault, Rules.BitmapSizeDefault);
        }
    }
}
