using DonutCP.Model.DataServices;
using DonutCP.Services;
using DonutCP.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DonutCP.ViewModels
{
    class LoginViewModel : BaseVM
    {
        private string userName;
        public string Username
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        #region open window commands
        private RelayCommand openRegistrationWnd;
        public RelayCommand OpenRegistrationWnd
        {
            get
            {
                return openRegistrationWnd ?? new RelayCommand(obj =>
                {
                    OpenRegistrationWindowMethod();
                });
            }
        }

        public ICommand OpenMainLogin => new RelayCommand(obj =>
            {
                try
                {
                    string resultStr = DataServices.LoginUser(Username, Password);
                    if (resultStr != "Ошибка" && resultStr != "Не заполнено поле" && resultStr != "Неверный логин или пароль" && resultStr != "Пустое поле")
                    {
                        CurrentUserId._CurrentUserID = Convert.ToInt32(resultStr);
                        ShowMessageUser("Успешный вход!");
                        Window oldWind = (Window)obj;
                        MainWindow newMainWindowWindow = new MainWindow();
                        newMainWindowWindow.Show();
                        oldWind.Close();
                    }
                    else ShowMessageUser(resultStr); ;
                }
                catch
                {

                }
            });

        private void ShowMessageUser(string message)
        {
            MessageView messageView = new MessageView(message);
            messageView.ShowDialog();
        }

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
        }

        public ICommand OpenRegistrationLogin => new RelayCommand(obj =>
        {
            Window oldWind = (Window)obj;
            Registration newMainWindowWindow = new Registration();
            newMainWindowWindow.Show();
            oldWind.Close();
        });

        private RelayCommand openMainWindowWnd;
        public RelayCommand OpenMainWindownWnd
        {
            get
            {
                return openMainWindowWnd ?? new RelayCommand(obj =>
                {
                    OpenMainWindowWindowMethod();
                });
            }
        }
        #endregion

        private void OpenRegistrationWindowMethod()
        {
            Registration newRegistrationWindow = new Registration();
            SetCenterPositionAndOpen(newRegistrationWindow);
        }


        private void OpenMainWindowWindowMethod()
        {
            MainWindow newMainWindowWindow = new MainWindow();
            SetCenterPositionAndOpen(newMainWindowWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }


    }
}
