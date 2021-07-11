using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class OrderSelectionMenu : Window{
		Button back_button;
		Button filter_button;
		MenuList order_list;
		OrderFilter order_filter;
		public OrderSelectionMenu():base("Select an Order", 2, 1, 45, 10, ConsoleColor.Black){
			order_filter = OrderFilter.build().not_completed();
			
			back_button = new Button(this, "back_button", 2, 1, ConsoleColor.Black, ConsoleColor.White,	" Back ");
			filter_button = new Button(this, "filter_button", 29, 1, ConsoleColor.Black, ConsoleColor.White, "Filter Options");
			order_list = new MenuList(this, "order_list", 2, 3, 41, 5, ConsoleColor.White, ConsoleColor.Black, listItems);
		}
		protected override void on_draw(){
			order_list.Items = listItems;
		}
		public string[] listItems{
			get{
				List<string> data = new List<string>();
				Order[] orders = order_filter.apply_filter();
				foreach (Order i in orders){
					data.Add($"{i.Id} - {i.Customer.firstName} {i.Customer.lastName}");
				}
				return data.ToArray();
			}
		}
		public Order selectedOrder{
			get{
				Order[] orders = order_filter.apply_filter();
				return orders[order_list.Index];
			}
		}
		public override ConsoleKey focus(){
			// 1 - back button
			// 2 - filter_button
			// 3 - list
			focus_status = 1;
			while(true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 1:
						c = back_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 3;
						else if (c == ConsoleKey.RightArrow) focus_status = 2;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Escape;
						continue;
					case 2:
						c = filter_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 3;
						else if (c == ConsoleKey.LeftArrow) focus_status = 1;
						else if (c == ConsoleKey.Enter){
							FilterOptionsMenu scm = new FilterOptionsMenu(order_filter);
							scm.focus();
							this.order_filter = scm.orderFilter;
							draw();
						}
						continue;
					case 3:
						c = order_list.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 1;
						else if (c == ConsoleKey.Enter){
							EditOrderMenu eom = new EditOrderMenu(selectedOrder);
							eom.focus();
						}
						continue;
				}
			}
		}
	}
}