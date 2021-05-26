using DonutCP.Model;
using DonutCP.Model.DataServices;
using DonutCP.Services;
using DonutCP.View.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DonutCP.ViewModels
{
	class MainViewModel : BaseVM
	{
		private string picture;
		public string Picture
		{
			get { return picture; }
			set
			{
				picture= value;
				NotifyPropertyChanged(nameof(Picture));
			}
		}
		private byte[] pictureForByte;
		public byte[] PictureForByte
        {
			get { return pictureForByte; }
            set
            {
				pictureForByte = value;
				NotifyPropertyChanged(nameof(PictureForByte));
            }
        }

		private List<Note> allNotes = DataServices.GetAllNotes(CurrentUserId._CurrentUserID);
		public List<Note> AllNotes
		{
			get { return allNotes; }
            set
            {
				allNotes = value;
				NotifyPropertyChanged("AllNotes");
            }
		}

		private List<NoteAccess> allAuthorNote = DataServices.GetAuthorAccessNotes(CurrentUserId._CurrentUserID);
		public List<NoteAccess> AllAuthorNote
		{
			get { return allAuthorNote; }
			set
			{
                allAuthorNote = value;
				NotifyPropertyChanged(nameof(AllAuthorNote));
			}
		}
		//private List<Note> allAccessNotes = DataServices.GetAllAccessNotes(CurrentUserId._CurrentUserID);
		//public List<Note> AllAccessNotes
		//{
		//	get { return allAccessNotes; }
		//	set
		//	{
		//		allAccessNotes = value;
		//		NotifyPropertyChanged(nameof(allAccessNotes));
		//	}
		//}

		private string noteName;
		public string NoteName
		{
			get { return noteName; }
			set
			{
				noteName = value;
				NotifyPropertyChanged(nameof(NoteName));
			}
		}

		private string descriptionName;
		public string DescriptionName
		{
			get { return descriptionName; }
			set
			{
				descriptionName = value;
				NotifyPropertyChanged(nameof(DescriptionName));
			}
		}
		public string noteAuthor;
		public string NoteAuthor
        {
            get { return noteAuthor; }
            set
            {
				noteAuthor = value;
				NotifyPropertyChanged(nameof(NoteAuthor));
			}
        }

		private string userNameToAccess;
		public string UserNameToAccess
		{
			get { return userNameToAccess; }
			set
			{
				userNameToAccess = value;
				NotifyPropertyChanged(nameof(UserNameToAccess));
			}
		}

		public ICommand OpenAddNewNoteWindow => new RelayCommand(obj =>
		{
			AddNewNoteWindow newWind = new AddNewNoteWindow();
			newWind.Show();
		});

		

		//св-во для выделенных элементов
		public TabItem SelectedTabItem { get; set; } 
		public Note SelectedNote { get; set; }
		public NoteAccess SelectedAccess { get; set; }

		public ICommand AddNewNote => new RelayCommand(obj =>
		{
			string resultStr = "Не создано";
			if (NoteName == null && DescriptionName == null)
			{

            }
			else
			{
				resultStr =  DataServices.CreateNote(NoteName, DescriptionName, CurrentUserId._CurrentUserID);
				ShowMessageUser(resultStr);
				Window oldWind = (Window)obj;
				UpdateAllDataView();
				SetNullValuesToProperties();
				oldWind.Close();
			}
		});

		public ICommand AddPicture => new RelayCommand(obj =>
		{
			try
			{
				OpenFileDialog dialog = new OpenFileDialog()
				{
					CheckFileExists = false,
					CheckPathExists = true,
					Multiselect = false,
					Title = "Выберите файл"
				};
				dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, .WMF)|.bmp;.jpg;.gif; *jpg; *.tif; *.png; *.ico; *.emf; *.wmf";
				Picture = dialog.FileName;
				string path = Picture;
				byte[] imageBytes = File.ReadAllBytes(path);
				PictureForByte = imageBytes;
			}
			catch (Exception)
			{
				System.Windows.MessageBox.Show("Ошибка добавления картинки");
			}
		});

		public ICommand OpenNoteWindow => new RelayCommand(obj =>
		{
			if (SelectedTabItem.Name == "NoteTab" && SelectedNote != null)
			{
				CurrentUserId._CurrentNote = SelectedNote;
				NoteWindow newWind = new NoteWindow();
				var a = new NoteViewModel();
				a.TextNote = CurrentUserId._CurrentNote.Text_note;
				newWind.DataContext = a;
				newWind.Show();
			}

			//если выноска
			if (SelectedTabItem.Name == "AccessNoteTab" && SelectedAccess != null)
			{
				CurrentUserId._CurrentNoteID = SelectedAccess.NoteId;
				CurrentUserId._CurrentNote = DataServices.FindNessNote(CurrentUserId._CurrentUserID,CurrentUserId._CurrentNoteID );
				NoteWindow newWind = new NoteWindow();
				var a = new NoteViewModel();
				a.TextNote = CurrentUserId._CurrentNote.Text_note;
				newWind.DataContext = a;
				newWind.Show();
			}
		});
		public ICommand OpenEditeWindow => new RelayCommand(obj =>
		{
			string resultStr = "Ничего не выбрано";
			//если заметка
			if (SelectedTabItem.Name == "NoteTab" && SelectedNote != null)
			{
				CurrentUserId._CurrentNote = SelectedNote;
				EditNoteWindow newWind = new EditNoteWindow();
				newWind.Show();
			}
			//если выноска
			else if (SelectedTabItem.Name == "AccessNoteTab" && SelectedAccess != null)
			{
				CurrentUserId._CurrentNoteID = SelectedAccess.NoteId;
				CurrentUserId._CurrentNote = DataServices.FindNessNote(CurrentUserId._CurrentUserID, CurrentUserId._CurrentNoteID);
				EditNoteWindow newWind = new EditNoteWindow();
				newWind.Show();
			}
			else
			{
				SetNullValuesToProperties();
				ShowMessageUser(resultStr);
			}
		});
		public ICommand DeleteNote => new RelayCommand(obj =>
		{
			string resultStr = "Ничего не выбрано";
			//если заметка
			if(SelectedTabItem.Name == "NoteTab" && SelectedNote != null)
            {
				resultStr = DataServices.DeleteNote(SelectedNote);
				UpdateAllDataView();
            }
			//если выноска
			else if (SelectedTabItem.Name == "AccessNoteTab" && SelectedAccess != null)
			{
				CurrentUserId._CurrentNoteID = SelectedAccess.NoteId;
				CurrentUserId._CurrentNote = DataServices.FindNessNote(CurrentUserId._CurrentUserID, CurrentUserId._CurrentNoteID);
				resultStr = DataServices.DeleteAccessNote(SelectedAccess);
				resultStr = DataServices.DeleteNote(CurrentUserId._CurrentNote);
				UpdateAllAccessNoteView();
			}
			else
            {
				ShowMessageUser(resultStr);
            }
		});

		public ICommand EditItem => new RelayCommand(obj =>
		{
			string resultStr = "Ничего не выбрано";
			resultStr = DataServices.EditNote(CurrentUserId._CurrentNote, NoteName, DescriptionName, UserNameToAccess);
			UpdateAllDataView();
			ShowMessageUser(resultStr);
		});


        #region updateviews
		private void UpdateAllDataView()
        {
			UpdateAllNotesView();
		}
		private void SetNullValuesToProperties()
        {
			//для заметок
			NoteName = null;
			DescriptionName = null;
        }
		private void UpdateAllAccessNoteView()
        {
			AllAuthorNote = DataServices.GetAuthorAccessNotes(CurrentUserId._CurrentUserID);
			MainWindow.AllHightLightsView.ItemsSource = null;
			MainWindow.AllHightLightsView.Items.Clear();
			MainWindow.AllHightLightsView.ItemsSource = AllAuthorNote;
			MainWindow.AllHightLightsView.Items.Refresh();
		}
		private void UpdateAllNotesView()
        {
			AllNotes = DataServices.GetAllNotes(CurrentUserId._CurrentUserID);
			MainWindow.AllNotesView.ItemsSource = null;
			MainWindow.AllNotesView.Items.Clear();
			MainWindow.AllNotesView.ItemsSource = AllNotes;
			MainWindow.AllNotesView.Items.Refresh();
        }
        #endregion

        private void ShowMessageUser(string message)
        {
			MessageView messageView = new MessageView(message);
			messageView.ShowDialog();
        }
		public BaseVM CurrentViewModel { get; }

		public ICommand LogOut => new RelayCommand(obj =>
		{
            try
            {
				string resultStr = "Выход успешно выполнен";
				Window oldWind = (Window)obj;
				Login newLoginWindow = new Login();
				newLoginWindow.Show();
				oldWind.Close();
				ShowMessageUser(resultStr);
			}
            catch
            {
				ShowMessageUser("Произошла ошибка");
            }
		});
	}
}
