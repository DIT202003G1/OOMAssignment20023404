using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.Customer;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class SelectCustomerMenu : NewOrderMenu{
		private bool new_options;
		public SelectCustomerMenu(bool new_option = true):base(){
			this.new_options = new_option;
			if (!new_option){
				this.buttons.Remove("newcutomer");
			}
		}
		public override ConsoleKey focus(){
			// 1 - new
			// 2 - cancel
			// 3 - search
			// 4 - list
			Title = "Select Customer";
			this.focus_status = 1;
			if (!new_options){
				cancel.X = 2;
			}
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				switch(focus_status){
					case 1:
						if (!new_options){
							focus_status = 2;
							continue;
						}
						ConsoleKey r_new = new_customer.focus();
						if (r_new == ConsoleKey.DownArrow) focus_status = 3;
						else if (r_new == ConsoleKey.RightArrow) focus_status = 2;
						else if (r_new == ConsoleKey.Enter){
							new NewCustomerMenu().focus();
							customers_list.Items = listItems;
							draw();
						};
						continue;
					case 2:
						ConsoleKey r_cancel = cancel.focus();
						if (r_cancel == ConsoleKey.DownArrow) focus_status = 3;
						else if (r_cancel == ConsoleKey.LeftArrow) focus_status = 1;
						else if (r_cancel == ConsoleKey.Enter) return ConsoleKey.Escape;
						continue;
					case 3:
						ConsoleKey r_search = search_box.focus();
						if (r_search == ConsoleKey.UpArrow) focus_status = 1;
						else if (r_search == ConsoleKey.DownArrow) focus_status = 4;
						else if (r_search == ConsoleKey.Enter){
							customers_list.Items = listItems;
							draw();
							List<CustomerRecord> records = DBWrapper.Instance.customer_table.get_records();
							int index = -1;
							foreach(CustomerRecord i in records){
								if (i.primaryKey[0].ToString() == search_box.Text){
									index = i.index;
									break;
								}
							}
							if (index != -1){
								customers_list.Index = index;
								focus_status = 4;
							}
							continue;
						};
						continue;
					case 4:
						ConsoleKey r_list = customers_list.focus();
						if (r_list == ConsoleKey.UpArrow) focus_status = 3;
						else if (r_list == ConsoleKey.Enter) return ConsoleKey.Enter;
						continue;
				}
			}
		}
	}
}