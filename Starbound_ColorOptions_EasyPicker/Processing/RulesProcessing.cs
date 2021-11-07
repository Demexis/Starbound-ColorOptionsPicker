using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public static class RulesProcessing
    {
        public static Rectangle GetFrameRect(string key, string pose, int frame)
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

        public static string GetJSONStringFromColorTransitionHandler(ColorTransitionHandler colorTransitionHandler)
        {
            JObject json = new JObject();

            JArray jArray = new JArray();
            foreach (string colorOption in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                JObject keyValuePairs = new JObject();
                foreach (ColorTransitionItem item in colorTransitionHandler[colorOption])
                {
                    KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(item.GetExportHexColorFrom, item.GetExportHexColorTo);
                    JToken jToken = JToken.FromObject(keyValuePair);

                    keyValuePairs.Add(new JProperty(keyValuePair.Key, keyValuePair.Value));
                }

                jArray.Add(keyValuePairs);
            }

            json.Add("colorOptions", jArray);

            return json.ToString();

            //Console.WriteLine(json.ToString());
        }

        /// <exception cref="Exception"></exception>
        public static Dictionary<string, List<ColorTransitionItem>> GetColorTransitionsFromJSON(string filePath)
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

                        //Console.WriteLine(keyValuePairs.ToString());

                        foreach (KeyValuePair<string, JToken> pair in keyValuePairs)
                        {
                            string keyHexCode, valueHexCode;
                            Color keyColor, valueColor;
                            //Console.WriteLine("JToken Pair: " + pair.ToString());
                            keyHexCode = '#' + pair.Key;
                            valueHexCode = '#' + (string)pair.Value;
                            keyColor = System.Drawing.ColorTranslator.FromHtml(keyHexCode);
                            valueColor = System.Drawing.ColorTranslator.FromHtml(valueHexCode);

                            //Console.WriteLine("Not Contains: " + (!transitionFromColor.Contains(keyColor)).ToString());
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

            Dictionary<string, List<ColorTransitionItem>> colorTransitions = new Dictionary<string, List<ColorTransitionItem>>();

            foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!transitionsFromColor.ContainsKey(colorEnum)) continue;
                if (!transitionsToColor.ContainsKey(colorEnum)) continue;

                List<ColorTransitionItem> items = new List<ColorTransitionItem>();

                for (int i = 0; i < transitionsFromColor[colorEnum].Count; i++)
                {
                    ColorTransitionItem item = new ColorTransitionItem(transitionsFromColor[colorEnum][i], transitionsToColor[colorEnum][i]);

                    items.Add(item);
                }

                colorTransitions.Add(colorEnum, items);
            }

            return colorTransitions;
        }
    }
}
