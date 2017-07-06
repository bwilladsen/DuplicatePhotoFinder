using System;
using System.Windows;
using System.Windows.Forms;

namespace DuplicatePhotoFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PhotoFinderViewModel photoFinderViewModel = new PhotoFinderViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = photoFinderViewModel;
        }

        private void OpenFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Choose the folder where your pictures reside:";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                picturePathTextbox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
