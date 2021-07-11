using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class AdminSettingsMenu : Window{
		public AdminSettingsMenu():base("Admin Settings", 2, 1, 32, 8, ConsoleColor.Black){
			List<string> items = new List<String>(){
				"New Admin",
				"Delete Admin",
				"Update My Admin Account",
				"Database Settings",
				"Back"
			};
			MenuList m_menu = new MenuList(this, "Menu", 2, 1, 28, 5, ConsoleColor.White, ConsoleColor.Black, items.ToArray());
		}
		public override ConsoleKey focus(){
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey status = menu_lists["Menu"].focus();
				if (status == ConsoleKey.Enter) {
					int option = menu_lists["Menu"].Index;
					switch (option){
						case 0:
							NewAdminMenu nam = new NewAdminMenu();
							nam.focus();
							continue;
						case 1:
							DeleteAdminMenu dam = new DeleteAdminMenu();
							dam.focus();
							continue;
						case 2:
							SelectAdminMenu sam = new SelectAdminMenu();
							if (sam.focus() == ConsoleKey.Enter){
								EditAdminAccountMenu eaam = new EditAdminAccountMenu(sam.selectedAdmin);
								eaam.focus();
							}
							continue;
						case 3:
							DatabaseSettingsMenu dsm = new DatabaseSettingsMenu();
							dsm.focus();
							MessageBox message_box = new MessageBox(new string[] {"Restart the application to reload the settings."}, "Database Settings");
							message_box.focus();
							continue;
						case 4:
							return ConsoleKey.Enter;
					}
				}
			}
		}
	}
}