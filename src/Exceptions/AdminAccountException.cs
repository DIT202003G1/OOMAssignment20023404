using System;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Exceptions{
	[Serializable]
	class AdminAccountException : Exception{
		public enum exception_type{
			ADMIN_NOT_FOUND,
			AUTHENTICATION_ERROR
		};
		static public Dictionary<exception_type,string> exception_type_message = new Dictionary<exception_type, string>{
			{exception_type.ADMIN_NOT_FOUND,"The admin account does not exist in the database"},
			{exception_type.AUTHENTICATION_ERROR,"The login information is not authenticated (You might have used a wrong password)"},
		};
		public AdminAccountException(){}

		public AdminAccountException(exception_type type):
		base(exception_type_message[type]){}
	}
}