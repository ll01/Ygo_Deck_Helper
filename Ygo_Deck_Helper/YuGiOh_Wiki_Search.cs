using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

namespace Ygo_Deck_Helper
{
	internal class YuGiOh_Wiki_Search
	{
		const string YuGiOhWikiUrl = "http://yugioh.wikia.com/wiki/";




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

		public static async Task<bool> Scrape_All_Cards()
		{

			string Current_Wiki_Card_Page_Url = YuGiOhWikiUrl + "Category:TCG_cards";

			var Card_List_Page_Get = new HtmlWeb();
			int Page_Count =  
			var Card_List_Page = Card_List_Page_Get.Load(Current_Wiki_Card_Page_Url);
			var Card_Collection = Card_List_Page.GetElementbyId("mw-pages").SelectNodes("//div[@class='mw-content-ltr']//table//ul//li").Nodes();
			foreach (HtmlNode Node in Card_Collection)
			{
				string card_Url = Node.OuterHtml.Split('"')[1];
			}

			return true;


		}
	}
}