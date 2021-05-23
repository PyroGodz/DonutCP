using DonutCP.Model;
using DonutCP.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 

namespace DonutCP.ViewModels
{
    class LoginViewModel : OnPropertyVM
    {

        

        private void OpenAddNoteWindowMethod()
        {
            MainWindow newNote = new MainWindow();
            SetCenterPositionAndOpen(newNote);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}
