using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class AdminSettingsMenu : Window{
		public AdminSettingsMenu():base("Admin Settings", 10, 5, 32, 8, ConsoleColor.Black){
			List<string> items = new List<String>(){
				"New Admin",
				"Delete Admin",
				"Update Password",
				"Database Settings",
				"Back"
			};
			MenuList m_menu = new MenuList(this, "Menu", 2, 1, 28, 5, ConsoleColor.White, ConsoleColor.Black, items.ToArray());
		}
		public override int focus(){
			Console.Clear();
			draw();
			while (true){
				int status = menu_lists["Menu"].focus();
				if (status == 1) break;
			}
			return menu_lists["Menu"].Index;
		}
	}
}