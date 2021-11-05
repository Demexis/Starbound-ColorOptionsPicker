using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
