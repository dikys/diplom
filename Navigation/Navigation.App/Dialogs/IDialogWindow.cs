using System;
using Navigation.App.Dialogs.Elements;

namespace Navigation.App.Dialogs
{
    public interface IDialogWindow
    {
        DialogElement[] Elements { get; }

        ResultOfDialog OpenDialog();
        void Close();
    }
}
