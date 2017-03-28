using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.App.Views
{
    public interface IView
    {
        void Show();
        void Close();
    }
}
