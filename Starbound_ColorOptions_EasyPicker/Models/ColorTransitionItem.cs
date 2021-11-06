using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starbound_ColorOptions_EasyPicker
{
    public class ColorTransitionItem
    {
        private ListViewItem _listViewItem;

        // Getters
        public ListViewItem GetListViewItem { get { return _listViewItem; } }

        public Color ColorFrom 
        { 
            get { return _listViewItem.SubItems[0].BackColor; }
            set
            {
                GetListViewItem.SubItems[0].BackColor = value;
                GetListViewItem.SubItems[1].Text = ColorProcessing.HexConverter(value);
            }
        }
        public Color ColorTo 
        { 
            get { return _listViewItem.SubItems[3].BackColor; }
            set
            {
                GetListViewItem.SubItems[3].BackColor = value;
                GetListViewItem.SubItems[4].Text = ColorProcessing.HexConverter(value);
            }
        }

        public string GetHexColorFrom { get { return _listViewItem.SubItems[1].Text; } }
        public string GetHexColorTo { get { return _listViewItem.SubItems[4].Text; } }

        public string GetExportHexColorFrom { get { return GetHexColorFrom.Substring(1); } }
        public string GetExportHexColorTo { get { return GetHexColorTo.Substring(1); } }


        public ColorTransitionItem(Color colorFrom, Color colorTo)
        {
            string[] row = { string.Empty, ColorProcessing.HexConverter(colorFrom), string.Empty, string.Empty, ColorProcessing.HexConverter(colorTo) };

            this._listViewItem = new ListViewItem(row);

            _listViewItem.UseItemStyleForSubItems = false;

            _listViewItem.SubItems[0].BackColor = colorFrom;

            _listViewItem.SubItems[3].BackColor = colorTo;
        }
    }
}
