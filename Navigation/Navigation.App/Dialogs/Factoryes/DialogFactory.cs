using System;
using Navigation.App.Dialogs.Elements;

namespace Navigation.App.Dialogs.Factoryes
{
    public class DialogFactory : IDialogFactory
    {
        private readonly Type _dialogType;

        public DialogFactory(IDialogWindow dialogWindowType)
        {
            _dialogType = dialogWindowType.GetType();
        }

        public IDialogWindow CreateDialog(params DialogElement[] elements)
        {
            return (IDialogWindow)Activator.CreateInstance(_dialogType, elements);
        }
    }
}
