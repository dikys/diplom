using System;

namespace Navigation.App.Common
{
    public interface IView
    {
        void Show();
        void Close();

        /// <summary>
        /// Установить фокус на элемент
        /// </summary>
        void Focus();

        event Action Closed;
    }
}
