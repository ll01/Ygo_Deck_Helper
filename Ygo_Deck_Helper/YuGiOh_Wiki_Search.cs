﻿using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

namespace Ygo_Deck_Helper
{
	internal class YuGiOh_Wiki_Search
	{
		const string YuGiOhWikiUrl = "http://yugioh.wikia.com/wiki/";
		// Only use when string already contains /wiki/ url inside
		const string YuGiOhWikiUrl_LOCAL = "http://yugioh.wikia.com/";

		public enum YuGiOh_Wiki_Data_Field
		{
		Name_en = 0,
		Name_cn = 1,
		Name_de = 2,
		Name_it = 3,
		Name_kr = 4,
		Name_jp = 5,
		Name_Translated = 6,
		Card_Type = 7,
		Attribute = 8,
		Type_List = 9,
		Level_Rank  = 10,
		Scale = 11,
		Stat_Line = 12,
		Passcode = 13,
		Effect_Type_List = 14,
		Current_Banlist_Status  = 15,
		Effect_Text = 16,
		TCG_en_Set_Code = 17,
		OCG_jp_Set_Code = 18,
		ArchType = 19,
		TCG_Rarity = 20,
		OCG_Rarity = 21,
		YuGiOh_Wiki_URL = 22,
		}


		/// <summary>
		/// finds the cards id given its name
		/// </summary>
		/// <param name="Card_Name">the name to search ygo wiki for</param>
		/// <returns>a key value pair where the key is the id of the card and the bool is wether or not the search is sucsesful</returns>
		public static async Task<KeyValuePair<int, bool>> Grab_Card_Number(string Card_Name, YuGiOh_Wiki_Data_Field Data_Field )
		{
			string xPath_Query = null;
			switch (Data_Field)
			{
				YuGiOh_Wiki_Data_Field.Card_Number: 
				 xPath_Query =  "//tr[contains(.,'Card Number')]";
				 break; 
				default:
			}
			//TODO: Make Async
			try
			{

				string Wiki_Card_Name = Data_Check.Format_Card_Name_For_Wiki_Search(Card_Name);
				string Card_Url = YuGiOhWikiUrl + Wiki_Card_Name;

				var Card_Page_Get = new HtmlWeb();
				var Card_Page = Card_Page_Get.Load(Card_Url);

				//find the table row that contains card number and scrap that cell
				HtmlNode Card_Number_Node = Card_Page.DocumentNode.SelectSingleNode(xPath_Query);
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

		public static List<string> Scrape_Wiki_Name_List_Page(string Current_Wiki_Card_Page_Url)
		{
			List<string> Url_List = null;
			
				var Card_List_Page_Get = new HtmlWeb();
				var Card_List_Page = Card_List_Page_Get.Load(Current_Wiki_Card_Page_Url);
				HtmlNode YuGioh_Wiki_Main_DataNode = Card_List_Page.GetElementbyId("mw-pages");
				var Card_Collection = YuGioh_Wiki_Main_DataNode.SelectNodes("//div[@class='mw-content-ltr']//table//ul//li").Nodes();
				Url_List = new List<string>();
				foreach (HtmlNode Node in Card_Collection)
				{
					string card_Url = Node.OuterHtml.Split('"')[1];
					Url_List.Add(card_Url);
				}
				return Url_List;
		}


		public static async Task<bool> Scrape_All_Cards()
		{
			

			string Current_Wiki_Card_Page_Url = YuGiOhWikiUrl + "Category:Duel_Monsters_cards";

			// this code scrapes the first page for the number of pages it needs to scrape 
			var Card_List_Page_Get = new HtmlWeb();
			var Card_List_Page = Card_List_Page_Get.Load(Current_Wiki_Card_Page_Url);
			HtmlNode YuGioh_Wiki_Main_DataNode = Card_List_Page.GetElementbyId("mw-pages");
			string page_Count_String = YuGioh_Wiki_Main_DataNode.SelectSingleNode("//div[@class='wikia-paginator']//ul//li[7]//a[@class='paginator-page']").InnerText;
			int page_Count = int.Parse(page_Count_String);


			List<Task<List<string>>> Scrape_Task_List = new List<Task<List<string>>>();
			for (int i = 1; i < page_Count; i++)
			{
				 var Current_Task = Task.Run(() => Scrape_Wiki_Name_List_Page(Current_Wiki_Card_Page_Url + "?page=" + i));
				Scrape_Task_List.Add(Current_Task);
			}
			await Task.WhenAll(Scrape_Task_List.ToArray());
			return true;


		}
	}
}