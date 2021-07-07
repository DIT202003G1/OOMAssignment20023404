using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.Customer;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class SelectCustomerMenu : NewOrderMenu{
		public override ConsoleKey focus(){
			Title = "Select Customer";
			return base.focus();
		}
	}
}