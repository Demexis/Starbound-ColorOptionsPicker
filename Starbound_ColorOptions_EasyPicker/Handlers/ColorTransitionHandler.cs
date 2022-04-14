using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker
{
    public class ColorTransitionHandler
    {
        private Dictionary<string, List<ColorTransitionItem>> _colorTransitions = new Dictionary<string, List<ColorTransitionItem>>()
        {
            [nameof(Rules.ColorOptions.Default)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Black)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Grey)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.White)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Red)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Orange)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Yellow)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Green)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Blue)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Purple)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Pink)] = new List<ColorTransitionItem>(),
            [nameof(Rules.ColorOptions.Brown)] = new List<ColorTransitionItem>()
        };

        public Dictionary<string, List<ColorTransitionItem>> GetColorTransitions
        {
            get { return this._colorTransitions; }
        }

        public Dictionary<string, List<ColorTransitionItem>> CloneColorTransitions
        {
            get 
            {
                Dictionary<string, List<ColorTransitionItem>> clonedColorTransitions = new Dictionary<string, List<ColorTransitionItem>>();
                foreach(string key in _colorTransitions.Keys)
                {
                    clonedColorTransitions.Add(key, new List<ColorTransitionItem>());

                    foreach(ColorTransitionItem item in _colorTransitions[key])
                    {
                        clonedColorTransitions[key].Add(item);
                    }
                }

                return clonedColorTransitions; 
            }
        }

        public List<ColorTransitionItem> this[string key]
        {
            get => _colorTransitions[key];
        }

        public bool ContainsKey(string key)
        {
            return _colorTransitions.ContainsKey(key);
        }

        public int GetTransitionItemsCount()
        {
            int count = 0;

            foreach(List<ColorTransitionItem> items in _colorTransitions.Values)
            {
                count += items.Count;
            }

            return count;
        }

        public void Clear()
        {
            _colorTransitions = new Dictionary<string, List<ColorTransitionItem>>()
            {
                [nameof(Rules.ColorOptions.Default)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Black)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Grey)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.White)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Red)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Orange)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Yellow)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Green)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Blue)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Purple)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Pink)] = new List<ColorTransitionItem>(),
                [nameof(Rules.ColorOptions.Brown)] = new List<ColorTransitionItem>()
            };
        }

        public bool CheckIfTransitionsWillHaveConflict(Dictionary<string, List<ColorTransitionItem>> colorTransitions)
        {
            bool importConflict = false;
            foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!colorTransitions.ContainsKey(colorEnum)) continue;

                foreach (ColorTransitionItem item1 in colorTransitions[colorEnum])
                {
                    foreach (ColorTransitionItem item2 in this[colorEnum])
                    {
                        if (item1.ColorFrom == item2.ColorFrom)
                        {
                            importConflict = true;
                        }
                    }
                }
            }

            return importConflict;
        }

        public void AddTransitionsFromDictionary(Dictionary<string, List<ColorTransitionItem>> importedItems)
        {
            foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!importedItems.ContainsKey(colorEnum)) continue;

                foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                {
                    this[colorEnum].Add(item1);
                }
            }
        }

        public void MergeTransitions(Dictionary<string, List<ColorTransitionItem>> importedItems, bool skipConflicts)
        {
            foreach (string colorEnum in Enum.GetNames(typeof(Rules.ColorOptions)))
            {
                if (!importedItems.ContainsKey(colorEnum)) continue;

                foreach (ColorTransitionItem item1 in importedItems[colorEnum])
                {
                    bool replaced = false;

                    foreach (ColorTransitionItem item2 in this[colorEnum])
                    {
                        if (item2.ColorFrom == item1.ColorFrom)
                        {
                            if(!skipConflicts)
                            {
                                item2.ColorTo = item1.ColorTo;
                            }

                            replaced = true;
                            break;
                        }
                    }

                    if (!replaced)
                    {
                        this[colorEnum].Add(item1);
                    }
                }
            }
        }
        
        public int CountAllTransitions
        {
            get
            {
                return _colorTransitions.Sum((x) => x.Value.Count);
            }
        }
    }
}
