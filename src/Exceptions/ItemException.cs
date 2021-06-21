using System;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Exceptions{
	[Serializable]
	class ItemException : Exception{
		public enum exception_type{
			ITEM_NOT_FOUND,
		};
		static public Dictionary<exception_type,string> exception_type_message = new Dictionary<exception_type, string>{
			{exception_type.ITEM_NOT_FOUND,"The item does not found in the database"}
		};
		public ItemException(){}
		public ItemException(exception_type type):
		base(exception_type_message[type]){}
	}
}