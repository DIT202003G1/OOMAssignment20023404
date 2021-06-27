using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.AdminAccount;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class DeleteAdminMenu : Window{
		// (string title, int x, int y, int width, int height, ConsoleColor color)
		private List<string> Admins{
			get{
				List<AdminAccountRecord> admin_records = DBWrapper.Instance.admin_account_table.get_records();
				List<string> id_names = new List<string>();
				foreach (AdminAccountRecord i in admin_records){
					id_names.Add($"{i.primaryKey[0]}: {i.firstName} {i.lastName}");
				}
				return id_names;
			}
		}
		private int get_index_by_admin_id(int id){
			List<AdminAccountRecord> admin_records = DBWrapper.Instance.admin_account_table.get_records();
			int index = 0;
			foreach (AdminAccountRecord i in admin_records){
				if (i.primaryKey[0] == id){
					return index;
				}
				index ++;
			}
			return -1;
		}
		public DeleteAdminMenu():base("Delete Admin", 2, 1, 32, 12, ConsoleColor.Black){
			Label l_message = new Label(this, "Message", 2, 1, 14, 1, ConsoleColor.White, "Search by ID: ");
			Textbox t_idsearch = new Textbox(this, "IDSearch", 18, 1, 12, 1, ConsoleColor.Black, ConsoleColor.White,"");
			MenuList m_menu = new MenuList(this, "Admins", 2, 3, 28, 5, ConsoleColor.White, ConsoleColor.Black, Admins.ToArray());
			Button b_close = new Button(this, "Close", 2, 9, ConsoleColor.Black, ConsoleColor.White, "Close");
			t_idsearch.maxLength = 8;
			t_idsearch.allowedChars = StringUtils.numbers;
		}
		public override ConsoleKey focus(){
			// 1 = search
			// 2 = list
			// 3 = button
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				switch (focus_status){
					case 1:
						ConsoleKey r_search = textboxes["IDSearch"].focus();
						switch(r_search){
							case ConsoleKey.Enter:
								int i;
								if (textboxes["IDSearch"].Text == "") i = -1;
								else i = get_index_by_admin_id(Int32.Parse(textboxes["IDSearch"].Text));
								if (i == -1) continue;
								else menu_lists["Admins"].Index = i;
								focus_status = 2;
								continue;
							case ConsoleKey.DownArrow:
								focus_status = 2;
								continue;
						}
					break;
					case 2:
						ConsoleKey r_list = menu_lists["Admins"].focus();
						switch(r_list){
							case ConsoleKey.Enter:
								if (DBWrapper.Instance.admin_account_table.get_records().ToArray().Length == 0){
									continue;
								}
								DBWrapper.Instance.admin_account_table.get_records()[
									menu_lists["Admins"].Index
								].remove_record();
								menu_lists["Admins"].Items = Admins.ToArray();
								continue;
							case ConsoleKey.DownArrow:
								focus_status = 3;
								continue;
							case ConsoleKey.UpArrow:
								focus_status = 1;
								continue;
							case ConsoleKey.Tab:
								focus_status = 3;
								continue;
						}
					break;
					case 3:
						ConsoleKey r_button = buttons["Close"].focus();
						switch(r_button){
							case ConsoleKey.UpArrow:
								focus_status = 2;
							break;
							case ConsoleKey.Enter:
								return 0;
						}

					break;
				}
			}
		}
	}
}