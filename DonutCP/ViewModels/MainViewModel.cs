using DonutCP.Model;
using DonutCP.Model.DataServices;
using DonutCP.Services;
using DonutCP.View.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
		public High_Lights SelectedHightLight { get; set; }

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
			else if (SelectedTabItem.Name == "HightLightTab" && SelectedHightLight != null)
			{
				CurrentUserId._CurrentHightLight = SelectedHightLight;
				EditHightLightWindow newWind = new EditHightLightWindow();
				newWind.Show();
			}
			else
			{
				SetNullValuesToProperties();
				ShowMessageUser(resultStr);
			}
		});

		public ICommand EditItem => new RelayCommand(obj =>
		{
			string resultStr = "Ничего не выбрано";
			resultStr = DataServices.EditNote(CurrentUserId._CurrentNote, NoteName, DescriptionName, UserNameToAccess);
			UpdateAllDataView();
		});

		public ICommand EditHightLight => new RelayCommand(obj =>
		{
			string resultStr = "Ничего не выбрано";
			resultStr = DataServices.DeleteHightLight(CurrentUserId._CurrentHightLight);
			UpdateAllDataView();
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
			if (SelectedTabItem.Name == "HightLightTab" && SelectedHightLight != null)
			{
				resultStr = DataServices.DeleteHightLight(SelectedHightLight);
				UpdateAllDataView();
			}
			SetNullValuesToProperties();
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
