using System.Windows;
using System.IO;

namespace Ygo_Deck_Helper
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Data_Check.Format_Settings New_Settings;

        public SettingsWindow(Data_Check.Format_Settings Current_Settings)
        {
            InitializeComponent();
            New_Settings = Current_Settings;
            CardDatabasePathTextBlock.Text = New_Settings.Card_Database_Filepath;
            SavePathTextBlock.Text = New_Settings.Deck_Save_Path;
            IsReversedChecKBox.IsChecked = New_Settings.isReversed;
        }


        private void IsReversedChecKBox_Changed(object sender, RoutedEventArgs e)
        {
            //IsReversedChecKBox.IsChecked an be null so we check it to try find its value THEN use its state
            if (!IsReversedChecKBox.IsChecked.HasValue)
            {
                IsReversedChecKBox.IsChecked = IsInitialized;
            }
            else
            {
                New_Settings.isReversed = (bool)IsReversedChecKBox.IsChecked;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.CurrentSetting = New_Settings;
        }

		private void SetDeckPathButton_Click(object sender, RoutedEventArgs e)
		{
			
			using (var FolderPicker = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = FolderPicker.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK)
				{
					SavePathTextBlock.Text = FolderPicker.SelectedPath; 
				}
			}
		}

		private void SetCardDatabasePathButton_Click(object sender, RoutedEventArgs e)
		{
			var FilePicker = new Microsoft.Win32.OpenFileDialog();
			if (FilePicker.ShowDialog() == true)
				CardDatabasePathTextBlock.Text = FilePicker.FileName;
			
		}
	}
}