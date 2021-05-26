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
    public class NoteViewModel : BaseVM
    {

        private string textNote;
        public string TextNote
        {
            get { return textNote; }
            set
            {
                textNote = value;
                NotifyPropertyChanged(nameof(TextNote));
            }
        }

		public ICommand SaveTextNote => new RelayCommand(obj =>
		{
			

			string resultStr = "Не создано";
			if (TextNote == null || TextNote == "")
			{
			}
			else
			{

				resultStr = DataServices.SaveTextToNote(TextNote, CurrentUserId._CurrentNote.Id);
				ShowMessageUser("Сохранено");
				Window oldWind = (Window)obj;
				CurrentUserId._CurrentNote.Text_note = resultStr;
				oldWind.Close();
			}
		});

		private void ShowMessageUser(string message)
		{
			MessageView messageView = new MessageView(message);
			messageView.ShowDialog();
		}
	}
}
