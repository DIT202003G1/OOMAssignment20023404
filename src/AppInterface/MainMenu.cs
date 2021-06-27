using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class MainMenu : Window{
		// (string title, int x, int y, int width, int height, ConsoleColor color)
		public MainMenu():base("Menu", 4, 3, 32, 10, ConsoleColor.Black){
			Label l_message = new Label(this, "Message", 2, 1, 28, 1, ConsoleColor.White, "Logged in as: ");
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
				ConsoleKey status = menu_lists["Menu"].focus();
				if (status == ConsoleKey.Enter) break;
			}
			return ConsoleKey.Enter;
		}
	}
}