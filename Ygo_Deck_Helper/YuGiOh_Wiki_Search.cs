using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ygo_Deck_Helper
{
    internal class YuGiOh_Wiki_Search
    {
        /// <summary>
        /// finds the cards id given its name
        /// </summary>
        /// <param name="Card_Name">the name to search ygo wiki for</param>
        /// <returns>a key value pair where the key is the id of the card and the bool is wether or not the search is sucsesful</returns>
        public static async Task<KeyValuePair<int, bool>> Grab_Card_Number(string Card_Name)
        {
            //TODO: Make Async
            try
            {
                const string YuGiOhWikiUrl = "http://yugioh.wikia.com/wiki/";
                string Wiki_Card_Name = Data_Check.Format_Card_Name_For_Wiki_Search(Card_Name);
                string Card_Url = YuGiOhWikiUrl + Wiki_Card_Name;

                var Card_Page_Get = new HtmlWeb();
                var Card_Page = Card_Page_Get.Load(Card_Url);

                //find the table row that contains card number and scrap that cell
                HtmlNode Card_Number_Node = Card_Page.DocumentNode.SelectSingleNode("//tr[contains(.,'Card Number')]");
                string Card_Number_Text = Card_Number_Node.InnerText;
                Card_Number_Text = Regex.Replace(Card_Number_Text, "[^0-9]", "");
                int Card_Number = int.Parse(Card_Number_Text);
                // return the card number and weather or not the search succeeded
                return new KeyValuePair<int, bool>(Card_Number, true);
            }
            catch
            {
                return new KeyValuePair<int, bool>(-1, false);
            }
        }
    }
}