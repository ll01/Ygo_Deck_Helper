using System;
using System.Data;
using System.Data.SQLite;

namespace Ygo_Deck_Helper
{
    internal class Sql_Functions
    {
        public Sql_Functions(string Database_File_Path)
        {
            Data_Source = "Data Source = " + Database_File_Path + ";";
        }

        public Sql_Functions()
        {
            // if no database given use internal one;
            Data_Source = "Data Source = Cards.cdb;";
        }

        private string data_Source;
        static public Card_Database Card_Database = new Card_Database();

        internal string Data_Source
        {
            get
            {
                return data_Source;
            }

            set
            {
                data_Source = value;
            }
        }

        public enum Data_Table_Select
        {
            datas,
            texts,
        }

        /// <summary>
        /// get the latest database and convert it to type dataview
        /// </summary>
        /// <param name="_Data_Grid_View"> The data view to be set </param>
        public void Refresh(DataTable _Data_Grid_View, Data_Table_Select Table_Select)
        {
            try
            {
                SQLiteConnection Card_Connection = new SQLiteConnection(Data_Source);
                Card_Connection.Open();
                string command;
                if (Table_Select == Data_Table_Select.datas)
                {
                    command = "SELECT * FROM datas";
                    SQLiteCommand Grab_Data_Command = new SQLiteCommand(command, Card_Connection);

                    SQLiteDataReader dr = Grab_Data_Command.ExecuteReader();
                    _Data_Grid_View.Load(dr);
                    _Data_Grid_View = Card_Database.datas;
                }
                else
                {
                    command = "SELECT * FROM texts";
                    SQLiteCommand Grab_Data_Command = new SQLiteCommand(command, Card_Connection);

                    SQLiteDataReader dr = Grab_Data_Command.ExecuteReader();
                    Card_Database.texts.Load(dr);
                    _Data_Grid_View = Card_Database.texts;
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public Card_Database Set_Up_Database()
        {
            Refresh(Card_Database.texts, Data_Table_Select.texts);
            Refresh(Card_Database.datas, Data_Table_Select.datas);
            return Card_Database;
        }
    }
}