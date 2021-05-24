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
using System.Windows.Input;

namespace DonutCP.ViewModels
{
	class MainViewModel : BaseVM
	{
		public string Picture;
		public byte[] PictureForByte;

		private List<Note> allNotes = DataServices.GetAllNotes(CurrentUserId._CurrentUserID);
		public List<Note> AllNotes
		{
			get { return allNotes; }
            set
            {
				allNotes = value;
				NotifyPropertyChanged(nameof(AllNotes));
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
		public ICommand OpenAddNewNoteWindow => new RelayCommand(obj =>
		{
			AddNewNoteWindow newWind = new AddNewNoteWindow();
			newWind.Show();
		});

		public ICommand AddNewNote => new RelayCommand(obj =>
		{
			DataServices.CreateNote(NoteName, DescriptionName, CurrentUserId._CurrentUserID);
			Window oldWind = (Window)obj;
			oldWind.Close();
		});
		private void Button_Pict(object sender, RoutedEventArgs e)
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
		}

		public BaseVM CurrentViewModel { get; }

	}
}
