using System;

using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class EditAdminAccountMenu : Window{
		Admin admin;
		Label admin_id_label;
		Label first_name_label;
		Label last_name_label;
		Label new_password_label;
		Label old_password_label;
		Textbox admin_id_textbox;
		Textbox first_name_textbox;
		Textbox last_name_textbox;
		Textbox new_password_textbox;
		Textbox old_password_textbox;
		Button save_button;
		Button cancel_button;
		public EditAdminAccountMenu(Admin admin):base("Edit my Admin Account", 2, 1, 34, 15, ConsoleColor.Black){
			this.admin = admin;

			admin_id_label = new Label(this, "admin_id_label", 2, 1, 13, 1, ConsoleColor.White, "ID (fixed):");
			first_name_label = new Label(this, "first_name_label", 2, 3, 13, 1, ConsoleColor.White, "First Name:");
			last_name_label = new Label(this, "last_name_label", 2, 5, 13, 1, ConsoleColor.White, "Last Name:");
			new_password_label = new Label(this, "new_password_label", 2, 7, 13, 1, ConsoleColor.White, "New Password:");
			old_password_label = new Label(this, "old_password_label", 2, 9, 32, 1, ConsoleColor.White, "Please enter your old password");

			admin_id_textbox = new Textbox(this, "admin_id_textbox", 20, 1, 12, 1, ConsoleColor.Black, ConsoleColor.White, admin.AdminId.ToString());
			first_name_textbox = new Textbox(this, "first_name_textbox", 20, 3, 12, 1, ConsoleColor.Black, ConsoleColor.White, admin.firstName);
			last_name_textbox = new Textbox(this, "last_name_textbox", 20, 5, 12, 1, ConsoleColor.Black, ConsoleColor.White, admin.lastName);
			new_password_textbox = new Textbox(this, "new_password_textbox", 20, 7, 12, 1, ConsoleColor.Black, ConsoleColor.White, "");
			old_password_textbox = new Textbox(this, "old_password_textbox", 2, 10, 30, 1, ConsoleColor.Black, ConsoleColor.White, "");

			save_button = new Button(this, "save_button", 2, 12, ConsoleColor.Black, ConsoleColor.White, " Save ");
			cancel_button = new Button(this, "cancel_button", 9, 12, ConsoleColor.Black, ConsoleColor.White, "Cancel");

			old_password_textbox.passwordChar = '*';
			new_password_textbox.passwordChar = '*';
		}
		private bool velidate_fields(){
			if (first_name_textbox.Text.Trim() == "") return false;
			if (last_name_textbox.Text.Trim() == "") return false;
			return true;
		}
		private void display_err(string message){
			this.Height = 17;
			save_button.Y = 14;
			cancel_button.Y = 14;
			if (! this.labels.ContainsKey("err")){
				Label err = new Label(this, "err", 2, 12, 30, 1, ConsoleColor.White, message);
				err.backgroundColor = ConsoleColor.Red;
			}
			else{
				this.labels["err"].Text = message;
			}
		}
		private bool velidate_old_passowrd(){
			try{
				Admin.login(admin.AdminId, old_password_textbox.Text);
				return true;
			} catch{
				return false;
			}
		}
		public override ConsoleKey focus(){
			// 1 - fn
			// 2 - ln
			// 3 - new
			// 4 - old
			// 5 - save
			// 6 - cancel
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 1:
						c = first_name_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status ++;
						continue;
					case 2:
						c = last_name_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status ++;
						else if (c == ConsoleKey.UpArrow) focus_status --;
						continue;
					case 3:
						c = new_password_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status ++;
						else if (c == ConsoleKey.UpArrow) focus_status --;
						continue;
					case 4:
						c = old_password_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status ++;
						else if (c == ConsoleKey.UpArrow) focus_status --;
						continue;
					case 5:
						c = save_button.focus();
						if (c == ConsoleKey.RightArrow) focus_status ++;
						else if (c == ConsoleKey.UpArrow) focus_status --;
						else if (c == ConsoleKey.Enter){
							if(velidate_fields()){
								if (velidate_old_passowrd()){
									admin.firstName = first_name_textbox.Text.Trim();
									admin.lastName = last_name_textbox.Text.Trim();
									if (new_password_textbox.Text != ""){
										admin.Password = new_password_textbox.Text;
									}
									return ConsoleKey.Enter;
								}
								else{
									display_err("Old Password Incorrect");
								}
							}
							else{
								display_err("Names cannot be empty");
							}
						}
						continue;
					case 6:
						c = cancel_button.focus();
						if (c == ConsoleKey.LeftArrow) focus_status --;
						else if (c == ConsoleKey.UpArrow) focus_status = 4;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Escape;
						continue;
				}
			}
		}
	}
}
