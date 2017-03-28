using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navigation.UI.Extensions
{
    public static class TableLayoutPanelExtension
    {
        public static TableLayoutPanel WithColumnStyles(this TableLayoutPanel panel, ColumnStyle column)
        {
            panel.ColumnStyles.Add(column);

            return panel;
        }

        public static TableLayoutPanel WithRowStyles(this TableLayoutPanel panel, RowStyle row)
        {
            panel.RowStyles.Add(row);

            return panel;
        }
    }
}
