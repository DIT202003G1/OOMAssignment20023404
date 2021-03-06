using System;

using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class NewAdminMenu : Window{
		public NewAdminMenu():base("New Admin", 2, 1, 34, 10, ConsoleColor.Black){
			Label l_fname = new Label(this, "Firstname", 2, 1, 14, 1, ConsoleColor.White, "First Name");
			Label l_lname = new Label(this, "Lastname", 2, 3, 14, 1, ConsoleColor.White, "Last Name");
			Label l_pw = new Label(this, "Password", 2, 5, 14, 1, ConsoleColor.White, "Password");
			Textbox t_fname = new Textbox(this, "Firstname", 16, 1, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Textbox t_lname = new Textbox(this, "Lastname", 16, 3, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Textbox t_pw = new Textbox(this, "Password", 16, 5, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Button b_save = new Button(this, "Save", 2, 7, ConsoleColor.Black, ConsoleColor.White, " Save ");
			Button b_cancel = new Button(this, "Cancel", 9, 7, ConsoleColor.Black, ConsoleColor.White, "Cancel");
			t_pw.passwordChar = '*';
		}
		public override ConsoleKey focus(){
			//1 = fname
			//2 = lname
			//3 = pw
			//4 = save
			//5 = cancel
			focus_status = 1;
			while (true){
				draw();
				switch (focus_status){
					case 1:
						ConsoleKey r_fname = textboxes["Firstname"].focus();
						switch (r_fname){
							case ConsoleKey.DownArrow:
								focus_status = 2;
								continue;
							case ConsoleKey.Enter:
								focus_status = 2;
								continue;
						}
					break;
					case 2:
						ConsoleKey r_lname = textboxes["Lastname"].focus();
						switch (r_lname){
							case ConsoleKey.DownArrow:
								focus_status = 3;
								continue;
							case ConsoleKey.UpArrow:
								focus_status = 1;
								continue;
							case ConsoleKey.Enter:
								focus_status = 3;
								continue;
						}
					break;
					case 3:
						ConsoleKey r_pw = textboxes["Password"].focus();
						switch (r_pw){
							case ConsoleKey.DownArrow:
								focus_status = 4;
								continue;
							case ConsoleKey.UpArrow:
								focus_status = 2;
								continue;
							case ConsoleKey.Enter:
								focus_status = 4;
								continue;
						}
					break;
					case 4:
						ConsoleKey r_save = buttons["Save"].focus();
						switch (r_save){
							case ConsoleKey.UpArrow:
								focus_status = 3;
								continue;
							case ConsoleKey.RightArrow:
								focus_status = 5;
								continue;
							case ConsoleKey.Enter:
								if (textboxes["Firstname"].Text.Trim() == "" || textboxes["Lastname"].Text.Trim() == "" || textboxes["Password"].Text == ""){
									this.height = 12;
									buttons["Save"].Y = 9;
									buttons["Cancel"].Y = 9;
									Label l_err = new Label(this, "Error", 2, 7, 30, 1, ConsoleColor.White, "  All fields are required  ");
									l_err.backgroundColor = ConsoleColor.Red;
									continue;
								}
								int admin_id = Admin.new_admin(textboxes["Firstname"].Text, textboxes["Lastname"].Text, textboxes["Password"].Text);
								new MessageBox(new string[] {"The ID for the new account is:",admin_id.ToString()}, "New Admin").focus();
								return 0;
						}
					break;
					case 5:
						ConsoleKey r_cancel = buttons["Cancel"].focus();
						switch (r_cancel){
							case ConsoleKey.UpArrow:
								focus_status = 3;
								continue;
							case ConsoleKey.LeftArrow:
								focus_status = 4;
								continue;
							case ConsoleKey.Enter:
								return 0;
						}
					break;
				}
			}
		}
	}
}
