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

        private Dictionary<string, Bitmap> _originalSpriteBitmaps = new Dictionary<string, Bitmap>()
        {
            [SpriteParts[0]] = null,
            [SpriteParts[1]] = null,
            [SpriteParts[2]] = null,
            [SpriteParts[3]] = null,
            [SpriteParts[4]] = null,
            [SpriteParts[5]] = null,
            [SpriteParts[6]] = null
        };

        public List<string> ActiveSpriteParts = new List<string>();



        public Bitmap GetSpriteBitmap(string spritePart)
        {
            Bitmap result = null;

            if(_originalSpriteBitmaps.TryGetValue(spritePart, out result)) { }

            return result;
        }

        public bool TrySetSpriteBitmap(string spritePart, Bitmap bitmap)
        {
            if(_originalSpriteBitmaps.ContainsKey(spritePart))
            {
                if(_originalSpriteBitmaps[spritePart] != null)
                    _originalSpriteBitmaps[spritePart].Dispose();

                _originalSpriteBitmaps[spritePart] = bitmap;

                return true;
            }

            return false;
        }

        public Bitmap GetMergedOriginalBitmap(string pose, int frame, Rules.Sex sex)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();

            foreach (string spritePart in SpriteSheetHandler.SpriteParts)
            {
                bool ignore = (spritePart == "chestm" && sex == Rules.Sex.Female) || (spritePart == "chestf" && sex == Rules.Sex.Male);

                //Rectangle rectangle = RulesProcessing.GetFrameRect(spritePart, pose, frame);

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
                        bitmaps.Add(bPart);
                    }
                }

                if (!ignore && ActiveSpriteParts.Contains(spritePart))
                {
                    Rectangle rectangle = RulesProcessing.GetFrameRect(spritePart, pose, frame);

                    System.Drawing.Imaging.PixelFormat format = this.GetSpriteBitmap(spritePart).PixelFormat;

                    // Clone a portion of the Bitmap object.
                    Bitmap bPart = this.GetSpriteBitmap(spritePart).Clone(rectangle, format);

                    bitmaps.Add(bPart);
                }
            }

            Bitmap result = BitmapProcessing.GetMergedBitmaps(bitmaps.ToArray());

            return result;
        }
    }
}
