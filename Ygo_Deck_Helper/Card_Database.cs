using System.Data;

namespace Ygo_Deck_Helper
{
    internal class Card_Database
    {
        private DataTable Datas;
        private DataTable Texts;

        public DataTable datas
        {
            get
            {
                return Datas;
            }

            set
            {
                Datas = value;
            }
        }

        public DataTable texts
        {
            get
            {
                return Texts;
            }

            set
            {
                Texts = value;
            }
        }

        public Card_Database()
        {
            datas = new DataTable();
            texts = new DataTable();
        }
    }
}