using DonutCP.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonutCP.Services.Navigators
{
    public enum ViewType
    {
        Login,
        Home,
        Portfolio,
        Buy,
        Sell
    }

    public interface INavigator
    {
        OnPropertyVM CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
