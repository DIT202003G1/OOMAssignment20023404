using System;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Exceptions{
	[Serializable]
	class OrderException : Exception{
		public enum exception_type{
			ORDER_NOT_FOUND,
			ORDER_FOUND,
			ORDER_ITEM_NOT_FOUND
		};
		static public Dictionary<exception_type,string> exception_type_message = new Dictionary<exception_type, string>{
			{exception_type.ORDER_FOUND,"The order does not exist in the database"},
			{exception_type.ORDER_NOT_FOUND,"The order already exist in the database"},
			{exception_type.ORDER_ITEM_NOT_FOUND,"The order item does not exist in the database"}
		};
		public OrderException(){}

		public OrderException(exception_type type):
		base(exception_type_message[type]){}
	}
}