using System;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Exceptions{
	[Serializable]
	class CustomerException : Exception{
		public enum exception_type{
			CUSTOMER_NOT_FOUND,
			CUSTOMER_FOUND,
		};
		static public Dictionary<exception_type,string> exception_type_message = new Dictionary<exception_type, string>{
			{exception_type.CUSTOMER_NOT_FOUND,"The customer does not exist in the database"},
			{exception_type.CUSTOMER_FOUND,"The customer already exist in the database"}
		};
		public CustomerException(){}

		public CustomerException(exception_type type):
		base(exception_type_message[type]){}
	}
}