using DonutCP.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DonutCP.ViewModels
{
    class RegistrationViewModel : INotifyPropertyChanged
    {


        private Users user;
        private string nickname;
        private string email;
        public Users User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public string Nick
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged(Nick);
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(Email);
            }
        }
    }
}
