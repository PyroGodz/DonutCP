using DonutCP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DonutCP.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllNotesView;
        public static ListView AllHightLightsView;
        public static ListView AllAccessView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            AllNotesView = ViewAllNotes;
            AllHightLightsView = ViewAccessNotes;
        }
    }
}
