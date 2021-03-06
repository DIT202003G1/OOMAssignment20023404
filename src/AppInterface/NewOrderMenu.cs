using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.Customer;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class NewOrderMenu : Window{
		Admin admin;
		protected Label search_label; 
		protected Label selection_label; 
		protected Button new_customer;
		protected Button cancel;
		protected Textbox search_box;
		protected MenuList customers_list;
		public NewOrderMenu(Admin admin):base("New Order", 2, 1, 34, 13, ConsoleColor.Black){
			this.admin = admin;
			this.selection_label = new Label(this, "selection", 2, 1, 17, 1, ConsoleColor.White, "Choose a customer");
			this.new_customer = new Button(this, "newcutomer", 22, 1, ConsoleColor.Black, ConsoleColor.White, "New");
			this.cancel = new Button(this, "cancel", 26, 1, ConsoleColor.Black, ConsoleColor.White, "Cancel");
			this.search_label = new Label(this, "search", 2, 3, 17, 1, ConsoleColor.White, "Search by ID:");
			this.search_box = new Textbox(this, "search", 18, 3, 14, 1, ConsoleColor.Black, ConsoleColor.White, "");
			this.customers_list = new MenuList(this, "customers_list", 2, 5, 30, 6, ConsoleColor.White, ConsoleColor.Black, listItems);
			this.search_box.allowedChars = "1234567890";
			this.search_box.maxLength = 6;
		}
		protected string[] listItems{
			get{
				List<CustomerRecord> orders = DBWrapper.Instance.customer_table.get_records();
				List<string> items = new List<string>();
				foreach (CustomerRecord i in orders){
					items.Add($"{i.primaryKey[0]}: {i.firstName} {i.lastName}");
				}
				return items.ToArray();
			}
		}
		public override ConsoleKey focus(){
			// 1 - new
			// 2 - cancel
			// 3 - search
			// 4 - list
			this.focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				switch(focus_status){
					case 1:
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
						else if (r_list == ConsoleKey.Enter){
							Customer customer = Customer.fetch_customers()[customers_list.Index];
							int order_id = OrderBook.Instance.make_order(customer, admin, (Datetime) DateTime.Now, true);
							Order order = OrderBook.Instance.fetch_order(order_id);
							EditOrderMenu eom = new EditOrderMenu(order);
							eom.focus();
						};
						continue;
				}
			}
		}
	}
}