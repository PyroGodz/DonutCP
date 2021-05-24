using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DonutCP.ViewModels
{
    class HomeViewModel : BaseVM
    {
        public string WelcomeMessage => "Welcome to my application";
        public ICommand NavigateAccountCommand { get; }
    }
}
