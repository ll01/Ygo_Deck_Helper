namespace Ygo_Deck_Helper
{
    internal class Card
    {
        private int Card_Id;
        private int Type_Number;
        private string Card_Name;

        /// <summary>
        /// Creates new card object
        /// </summary>
        /// <param name="Passcode">Konami ID of card </param>
        /// <param name="Type_Number">Ygo Pro has specific numbers assigned based of the type of card </param>
        /// <param name="Card_Name">Official Konami Name of card  </param>
        public Card(int Passcode, int Type_Number, string Card_Name)
        {
            this.Card_Id = Passcode;
            this.Type_Number = Type_Number;
            this.Card_Name = Card_Name;
        }

        public int Get_Card_Id()
        {
            return Card_Id;
        }

        public int Get_Card_Type_Number()
        {
            return Type_Number;
        }

        public string Get_Card_Name()
        {
            return Card_Name;
        }
    }
}