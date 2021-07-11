using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class MainMenu : Window{
		Admin admin;
		public MainMenu(Admin admin):base("Menu", 2, 1, 32, 10, ConsoleColor.Black){
			this.admin = admin;
			Label l_message = new Label(this, "Message", 2, 1, 28, 1, ConsoleColor.White, $"Logged in as: {admin.firstName} {admin.lastName}");
			List<string> items = new List<String>(){
				"New Order",
				"Manage Orders",
				"Manage Customers",
				"Admin Settings",
				"Logout"
			};
			MenuList m_menu = new MenuList(this, "Menu", 2, 3, 28, 5, ConsoleColor.White, ConsoleColor.Black, items.ToArray());
		}
		public override ConsoleKey focus(){
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey status = menu_lists["Menu"].focus();
				if (status == ConsoleKey.Enter) {
					Console.SetCursorPosition(0,0);
					Console.WriteLine(menu_lists["Menu"].Index);
					switch (menu_lists["Menu"].Index){
						case 0:
							NewOrderMenu nom = new NewOrderMenu(this.admin);
							nom.focus();
							break;
						case 1:
							OrderSelectionMenu osm = new OrderSelectionMenu();
							osm.focus();
							break;
						case 2:
							while (true){
								SelectCustomerMenu scm = new SelectCustomerMenu(true);
								if (scm.focus() == ConsoleKey.Enter){
									Customer customer = Customer.fetch_customers()[scm.menu_lists["customers_list"].Index];
									ManageCustomerAccountMenu mcam = new ManageCustomerAccountMenu(customer);
									mcam.focus();
									continue;
								}
								else break;
							}
							break;
						case 3:
							AdminSettingsMenu asm = new AdminSettingsMenu();
							asm.focus();
							break;
						case 4:
							return ConsoleKey.Enter;
					}
				};
			}
		}
	}
}