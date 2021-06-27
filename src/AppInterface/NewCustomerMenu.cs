using System;

using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class NewCustomerMenu : Window{
		private int premium_factor = 0;
		public NewCustomerMenu():base("New Admin", 2, 1, 34, 14, ConsoleColor.Black){
			Label l_fname = new Label(this, "Firstname", 2, 1, 14, 1, ConsoleColor.White, "First Name");
			Label l_lname = new Label(this, "Lastname", 2, 3, 14, 1, ConsoleColor.White, "Last Name");
			Label l_addreess = new Label(this, "Address", 2, 5, 14, 1, ConsoleColor.White, "Address");
			Label l_telephone = new Label(this, "Telephone", 2, 7, 14, 1, ConsoleColor.White, "Telephone");
			Label l_premium = new Label(this, "Premium", 2, 9, 14, 1, ConsoleColor.White, "Premium");
			Textbox t_fname = new Textbox(this, "Firstname", 16, 1, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Textbox t_lname = new Textbox(this, "Lastname", 16, 3, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Textbox t_address = new Textbox(this, "Address", 16, 5, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Textbox t_telephone = new Textbox(this, "Telephone", 16, 7, 16, 1, ConsoleColor.Black,ConsoleColor.White, "");
			Textbox t_premiume = new Textbox(this, "Premium", 19, 9, 10, 1, ConsoleColor.Black,ConsoleColor.White, " 0 Years");
			Button b_premium_add = new Button(this, "PremiumAdd", 16, 9, ConsoleColor.Black, ConsoleColor.White, "＋");
			Button b_premium_sub = new Button(this, "PremiumSub", 30, 9, ConsoleColor.Black, ConsoleColor.White, "－");
			Button b_save = new Button(this, "Save", 2, 11, ConsoleColor.Black, ConsoleColor.White, " Save ");
			Button b_cancel = new Button(this, "Cancel", 9, 11, ConsoleColor.Black, ConsoleColor.White, "Cancel");
		}
		private int premiumYear{
			get=>premium_factor*2;
		}
		private int validate_fields(){
			// 0 - valid
			// 1 - fname
			// 2 - lname
			// 3 - address
			// 4 - telephone
			if (this.textboxes["Firstname"].Text.Trim() == "") return 1;
			if (this.textboxes["Lastname"].Text.Trim() == "") return 2;
			if (this.textboxes["Address"].Text.Trim() == "") return 3;
			if (this.textboxes["Telephone"].Text.Trim() == "") return 4;
			return 0;
		}
		public override ConsoleKey focus(){
			// 1 - fname
			// 2 - lname
			// 3 - addrees
			// 4 - telephone
			// 5 - +
			// 6 - -
			// 7 - save
			// 8 - cancel
			focus_status = 1;
			if (this.labels.ContainsKey("warn"))
				this.labels.Remove("warn");
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				switch(focus_status){
					case 1:
						ConsoleKey r_fname = this.textboxes["Firstname"].focus();
						if (r_fname == ConsoleKey.Enter) focus_status = 2;
						else if (r_fname == ConsoleKey.DownArrow) focus_status = 2;
						continue;
					case 2:
						ConsoleKey r_lname = this.textboxes["Lastname"].focus();
						if (r_lname == ConsoleKey.Enter) focus_status = 3;
						else if (r_lname == ConsoleKey.DownArrow) focus_status = 3;
						else if (r_lname == ConsoleKey.UpArrow) focus_status = 1;
						continue;
					case 3:
						ConsoleKey r_address = this.textboxes["Address"].focus();
						if (r_address == ConsoleKey.Enter) focus_status = 4;
						else if (r_address == ConsoleKey.DownArrow) focus_status = 4;
						else if (r_address == ConsoleKey.UpArrow) focus_status = 2;
						continue;
					case 4:
						ConsoleKey r_telephone = this.textboxes["Telephone"].focus();
						if (r_telephone == ConsoleKey.Enter) focus_status = 5;
						else if (r_telephone == ConsoleKey.DownArrow) focus_status = 5;
						else if (r_telephone == ConsoleKey.UpArrow) focus_status = 3;
						continue;
					case 5:
						ConsoleKey r_premium_add = this.buttons["PremiumAdd"].focus();
						if (r_premium_add == ConsoleKey.DownArrow) focus_status = 7;
						else if (r_premium_add == ConsoleKey.UpArrow) focus_status = 4;
						else if (r_premium_add == ConsoleKey.RightArrow) focus_status = 6;
						else if (r_premium_add == ConsoleKey.Enter) {
							premium_factor ++;
							this.textboxes["Premium"].Text = $" {this.premiumYear} Years";
						};
						continue;
					case 6:
						ConsoleKey r_premium_sub = this.buttons["PremiumSub"].focus();
						if (r_premium_sub == ConsoleKey.DownArrow) focus_status = 7;
						else if (r_premium_sub == ConsoleKey.UpArrow) focus_status = 4;
						else if (r_premium_sub == ConsoleKey.LeftArrow) focus_status = 5;
						else if (r_premium_sub == ConsoleKey.Enter && premium_factor > 0) {
							premium_factor --;
							this.textboxes["Premium"].Text = $" {this.premiumYear} Years";
						}
						continue;
					case 7:
						ConsoleKey r_save = this.buttons["Save"].focus();
						if (r_save == ConsoleKey.Enter) {
							if (validate_fields() == 0){
								int new_customer_id = Customer.new_customer(
									this.textboxes["Firstname"].Text.Trim(),
									this.textboxes["Lastname"].Text.Trim(),
									this.textboxes["Address"].Text.Trim(),
									this.textboxes["Telephone"].Text.Trim(),
									this.premium_factor
								);
								new MessageBox(new string[] {"The ID for the new account is:",new_customer_id.ToString()}, "New Customer").focus();
								return ConsoleKey.Enter;
							}
							else{
								this.Height = 16;
								this.buttons["Save"].Y = 13;
								this.buttons["Cancel"].Y = 13;
								string[] field_names = new string[]{
									"First name",
									"Last name",
									"Address",
									"Telephone",
								};
								Label l_warn = new Label(this, "warn", 11, 2, 30, 1, ConsoleColor.White, field_names[validate_fields()] + " cannot be empty");
								l_warn.backgroundColor = ConsoleColor.DarkRed;
							}
						}
						else if (r_save == ConsoleKey.UpArrow) focus_status = 5;
						else if (r_save == ConsoleKey.RightArrow) focus_status = 8;
						continue;
					case 8:
						ConsoleKey r_cancel = this.buttons["Cancel"].focus();
						if (r_cancel == ConsoleKey.Enter) return ConsoleKey.Enter;
						else if (r_cancel == ConsoleKey.UpArrow) focus_status = 5;
						else if (r_cancel == ConsoleKey.LeftArrow) focus_status = 7;
						continue;
				}
			}
			return ConsoleKey.Enter;
		}
	}
}
