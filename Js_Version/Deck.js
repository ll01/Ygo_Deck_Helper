'use strict';
var rx = require('rx'); 
var Deck = (function () {
	
	function Deck(Deck_Name) {
		this.mDeck_Name =  Deck_Name;
	}
	
	Deck.Create_Deck = function (List_Of_Main_Extra_Cards, list_of_Side_Cards, Deck_Name) {
		
		var new_Deck = new Deck(Deck_Name);
		List_Of_Main_Extra_Cards.forEach(card_name => function(){
			
			var new_Card_Query = Deck.
		} )
	}
})();