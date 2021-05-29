using System;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Exceptions{
	[Serializable]
	class AdminAccountException : Exception{
		public enum exception_type{
			ADMIN_NOT_FOUND
		};
		static public Dictionary<exception_type,string> exception_type_message = new Dictionary<exception_type, string>{
			{exception_type.ADMIN_NOT_FOUND,"The admin account does not exist in the database"},
		};
		public AdminAccountException(){}

		public AdminAccountException(exception_type type):
		base(exception_type_message[type]){}
	}
}