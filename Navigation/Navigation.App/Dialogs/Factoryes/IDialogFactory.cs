using System;
using Navigation.App.Dialogs.Elements;

namespace Navigation.App.Dialogs.Factoryes
{
    public interface IDialogFactory
    {
        IDialogWindow CreateDialog(params DialogElement[] elements);
    }
}
