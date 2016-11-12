using System.Collections.Generic;
using System.Windows;

namespace Ygo_Deck_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sql_Functions queries = new Sql_Functions();
        private static Data_Check.Format_Settings currentSetting = new Data_Check.Format_Settings("fsdf", "fsadfas", false);

        static public Data_Check.Format_Settings CurrentSetting
        {
            get
            {
                return currentSetting;
            }

            set
            {
                currentSetting = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            queries.Set_Up_Database();
            NewDeckNameTextBox.SelectionStart = 0;
            NewDeckNameTextBox.SelectionLength = NewDeckNameTextBox.Text.Length;
            DeckListtextBlock.Text = "Harpie's Feather Duster\r\n1 Vanity’s Emptiness";
        }

        private async void BuildDeckbutton_Click(object sender, RoutedEventArgs e)
        {
            string Deck_Name = NewDeckNameTextBox.Text;
            CardsNotFoundTextBlock.Text = "";
            List<string> Raw_Deck_list = new List<string>();
            List<string> Clensed_List = new List<string>();
            for (int i = 0; i < DeckListtextBlock.LineCount; i++)
            {
                string Line_From_Input = Data_Check.Remove_Line_Break(DeckListtextBlock.GetLineText(i));
                //if there is a space in the line ignore it
                if (Line_From_Input != "\r\n")
                    Raw_Deck_list.Add(Line_From_Input.Trim());
            }

            //key value pair where key are main/extra deck cards and value are side deck cards
            KeyValuePair<List<string>, List<string>> Sorted_Card_Names = Data_Check.Clense_Card_Names(Raw_Deck_list, CurrentSetting);
            Deck New_Deck = await Deck.Create_Deck(Sorted_Card_Names.Key, Sorted_Card_Names.Value, Deck_Name);
            Data_Check.Deck_Card_Count_check(New_Deck);
            DeckListtextBlock.Text = "";
            foreach (Card sr in New_Deck.Main_Deck)
            {
                DeckListtextBlock.Text += sr.Get_Card_Id().ToString() + "\n";
            }

            foreach (KeyValuePair<string, int> sr in New_Deck.Faild_Search_Cards)
            {
                CardsNotFoundTextBlock.Text += sr.Key + "\r\n";
            }
            New_Deck.Write_deck(Deck_Name + ".ydk");
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow Settings_Window = new SettingsWindow(CurrentSetting);
            Settings_Window.ShowDialog();
        }
    }
}