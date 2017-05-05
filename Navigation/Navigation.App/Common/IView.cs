using System;

namespace Navigation.App.Common
{
    public interface IView
    {
        void Show();
        void Close();

        event Action ViewClosed;
    }
}
