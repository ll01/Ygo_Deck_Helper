/*jshint es6: True */
'use strict';

var Card = function() {

	function Card(Card_Id, Type_Number, Card_Name) {
        this.mCard_Id  = Card_Id;
        this.mType_Number = Type_Number;
        this.mCard_Name = Card_Name;
	}
	
Card.prototype.Get_card_id = function() {
	
	return this.Get_card_id;
}

Card.prototype.Get_TypeNumber  = function() {
	return this.Get_TypeNumber;
}

Card.prototype.Get_card_name  = function() {
	return this.mCard_Name;
}
	
}();
