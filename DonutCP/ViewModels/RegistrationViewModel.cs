using DonutCP.Model;
using DonutCP.Model.DataServices;
using DonutCP.Services;
using DonutCP.View.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DonutCP.ViewModels
{
    class RegistrationViewModel : BaseVM
    {


        private Users user;
        private string nickname;
        private string email;
        private string password;
        public Users User
        {
            get { return user; }
            set
            {
                user = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }
        public string Nick
        {
            get { return nickname; }
            set
            {
                nickname = value;
                NotifyPropertyChanged(Nick);
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged(Email);
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(Password);
            }
        }

        public ICommand OpenLoginRegistration => new RelayCommand(obj =>
        {
            string resultStr = "Что-то пошло не так";
            resultStr = DataServices.CreateUser(Nick, Email, Password);
            ShowMessageUser(resultStr);
            Window oldWind = (Window)obj;
            Login newMainWindowWindow = new Login();
            newMainWindowWindow.Show();
            oldWind.Close();
        });

        private void ShowMessageUser(string message)
        {
            MessageView messageView = new MessageView(message);
            messageView.ShowDialog();
        }
    }
}
