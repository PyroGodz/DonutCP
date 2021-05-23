using System.ComponentModel;

namespace DonutCP.View.Windows
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : OnPropertyVM;
    public class OnPropertyVM : INotifyPropertyChanged
    {
        public virtual void Dispose() { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
