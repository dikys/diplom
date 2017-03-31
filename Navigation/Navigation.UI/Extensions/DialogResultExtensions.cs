using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Navigation.App.Dialogs;

namespace Navigation.UI.Extensions
{
    public static class DialogResultExtensions
    {
        public static ResultOfDialog ToResultOfDialog(this DialogResult dialogResult)
        {
            switch (dialogResult)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    return ResultOfDialog.Yes;
                default:
                    return ResultOfDialog.No;
            }
        }
    }
}
