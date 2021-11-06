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
            "Bsleeve",
            "back",
            "pants",
            "head",
            "chestm",
            "chestf",
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

        private Dictionary<string, Bitmap> _originalSpriteBitmaps = new Dictionary<string, Bitmap>();

        public Dictionary<string, Bitmap> HumanParts = new Dictionary<string, Bitmap>();
        public Dictionary<string, Bitmap> ArmorParts = new Dictionary<string, Bitmap>();


        public void Clear()
        {
            _originalSpriteBitmaps.Clear();
            ArmorParts.Clear();
            HumanParts.Clear();
        }

        public void Add(string spritePart, Bitmap spriteBitmap)
        {
            _originalSpriteBitmaps.Add(spritePart, spriteBitmap);
        }

        public int Count { get { return _originalSpriteBitmaps.Count; } }

        public Bitmap GetSpriteBitmap(string spritePart)
        {
            Bitmap result = null;

            if(_originalSpriteBitmaps.TryGetValue(spritePart, out result)) { }

            return result;
        }

        //public bool TrySetSpriteBitmap(string spritePart, Bitmap bitmap)
        //{
        //    if(_originalSpriteBitmaps.ContainsKey(spritePart))
        //    {
        //        if(_originalSpriteBitmaps[spritePart] != null)
        //            _originalSpriteBitmaps[spritePart].Dispose();

        //        _originalSpriteBitmaps[spritePart] = bitmap;

        //        return true;
        //    }

        //    return false;
        //}

        public void GetAllSpritePartsForCurrentFrame(string pose, int frame, Rules.Sex sex)
        {
            HumanParts = new Dictionary<string, Bitmap>();
            ArmorParts = new Dictionary<string, Bitmap>();

            foreach (string spritePart in SpriteSheetHandler.SpriteParts)
            {
                bool ignore = (spritePart == "chestm" && sex == Rules.Sex.Female) || (spritePart == "chestf" && sex == Rules.Sex.Male);

                if (ShowMannequin && !ignore)
                {
                    Bitmap humanPart = null;

                    switch(spritePart)
                    {
                        case "Bsleeve":
                            humanPart = Properties.Resources.human_Bsleeve;
                            break;
                        case "Fsleeve":
                            humanPart = Properties.Resources.human_Fsleeve;
                            break;
                        case "head":
                            humanPart = Properties.Resources.human_head;
                            break;
                        case "pants":
                            if(sex == Rules.Sex.Male)
                            {
                                humanPart = Properties.Resources.human_pantsm;
                            }
                            else if(sex == Rules.Sex.Female)
                            {
                                humanPart = Properties.Resources.human_pantsf;
                            }
                            break;
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

                            System.Drawing.Imaging.PixelFormat format = this.GetSpriteBitmap(spritePart).PixelFormat;

                            // Clone a portion of the Bitmap object.
                            Bitmap bPart = this.GetSpriteBitmap(spritePart).Clone(rectangle, format);

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
