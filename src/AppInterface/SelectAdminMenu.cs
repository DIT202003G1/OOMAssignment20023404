using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class SelectAdminMenu : Window{
		Label prompt_label;
		Label search_label;
		Button cancel_button;
		Textbox search_textbox;
		MenuList admin_list;
		public SelectAdminMenu():base("Select Admin", 2, 1, 34, 13, ConsoleColor.Black){
			prompt_label = new Label(this, "prompt_label", 2, 1, 20, 1, ConsoleColor.White, "Choose an admin");
			search_label = new Label(this, "search_label", 2, 3, 13, 1, ConsoleColor.White, "Search by ID:");
			cancel_button = new Button(this, "cancel_button", 26, 1, ConsoleColor.Black, ConsoleColor.White, "Cancel");
			search_textbox = new Textbox(this, "search_textbox", 18, 3, 14, 1, ConsoleColor.Black, ConsoleColor.White, "");
			admin_list = new MenuList(this, "admin_list", 2, 5, 30, 6, ConsoleColor.Black ,ConsoleColor.White, listItems);

			search_textbox.maxLength = 8;
			search_textbox.allowedChars = "1234567890";
		}
		private string[] listItems{
			get{
				Admin[] admins = Admin.fetch_admins();
				List<string> data = new List<string>();
				foreach (Admin i in admins){
					data.Add($"{i.AdminId} - {i.firstName} {i.lastName}");
				}
				return data.ToArray();
			}
		}
		public Admin selectedAdmin{
			get{
				Admin[] admins = Admin.fetch_admins();
				return admins[admin_list.Index];
			}
		}
		public override ConsoleKey focus(){
			// 1 - cancel_button
			// 2 - search_textbox
			// 3 - admin_list
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 1:
						c = cancel_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 2;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Escape;
						continue;
					case 2:
						c = search_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 3;
						else if (c == ConsoleKey.UpArrow) focus_status = 1;
						else if (c == ConsoleKey.Enter){
							int search_id = Int32.Parse(search_textbox.Text);
							bool exists = DBWrapper.Instance.admin_account_table.exists(new int[] {search_id});
							if (exists) {
								focus_status = 3;
								Admin[] admins = Admin.fetch_admins();
								for (int i = 0; i < admins.Length; i ++){
									if (admins[i].AdminId == search_id){
										admin_list.Index = i;
										break;
									}
								}
							}
							else continue;
						}
						continue;
					case 3:
						c = admin_list.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 2;
						if (c == ConsoleKey.Enter) return ConsoleKey.Enter; 
						continue;
				}
			}
		}
	}
}