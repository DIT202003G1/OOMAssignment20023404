using System;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class DatabaseSettingsMenu : Window{
		public DatabaseSettingsMenu():base("Database Settings", 4, 3, 40, 15, ConsoleColor.Black){
			Label l_message1 = new Label(this, "Message_1", 2, 1, 36, 1, ConsoleColor.White, "Change only if you know what you're");
			Label l_message2 = new Label(this, "Message_2", 2, 2, 36, 1, ConsoleColor.White, "doing!");
			Label l_ip_addr = new Label(this, "IPAddress", 2, 4, 13, 1, ConsoleColor.White, "IP Address");
			Label l_username = new Label(this, "Username", 2, 6, 13, 1, ConsoleColor.White, "Username");
			Label l_password = new Label(this, "Password", 2, 8, 13, 1, ConsoleColor.White, "Password");
			Label l_name = new Label(this, "DBName", 2, 10, 13, 1, ConsoleColor.White, "Database Name");
			Textbox t_ip_addr = new Textbox(this, "IPAddress", 17, 4, 21, 1, ConsoleColor.Black, ConsoleColor.White, DBConfig.Hostname);
			Textbox t_username = new Textbox(this, "Username", 17, 6, 21, 1, ConsoleColor.Black, ConsoleColor.White,DBConfig.Username);
			Textbox t_password = new Textbox(this, "Password", 17, 8, 21, 1, ConsoleColor.Black, ConsoleColor.White, DBConfig.Password);
			Textbox t_name = new Textbox(this, "DBName", 17, 10, 21, 1, ConsoleColor.Black, ConsoleColor.White, DBConfig.DBName);
			Button b_save = new Button(this, "Save", 2, 12, ConsoleColor.Black, ConsoleColor.White, "Save");
			t_password.passwordChar = '*';
		}
		private int velidate(){
			if (textboxes["IPAddress"].Text.Trim() == "") return 1;
			if (textboxes["Username"].Text.Trim() == "") return 2;
			if (textboxes["DBName"].Text.Trim() == "") return 3;
			return 0;
		}
		private void save(){
			DBConfig.Hostname = textboxes["IPAddress"].Text.Trim();
			DBConfig.Username = textboxes["Username"].Text.Trim();
			DBConfig.Password = textboxes["Password"].Text;
			DBConfig.DBName = textboxes["DBName"].Text.Trim();
		}
		public override int focus(){
			// 1 = hostname
			// 2 = username
			// 3 = password
			// 4 = dbname
			// 5 = save
			focus_status = 1;
			while (true){
				draw();
				switch (focus_status){
					case 1:
						int r_hostname = textboxes["IPAddress"].focus();
						switch(r_hostname){
							case 1:
								focus_status = 2;
								continue;
							case 3:
								focus_status = 2;
								continue;
						}
						continue;
					case 2:
						int r_username = textboxes["Username"].focus();
						switch(r_username){
							case 1:
								focus_status = 3;
								continue;
							case 2:
								focus_status = 1;
								continue;
							case 3:
								focus_status = 3;
								continue;
						}
						continue;
					case 3:
						int r_password = textboxes["Password"].focus();
						switch(r_password){
							case 1:
								focus_status = 4;
								continue;
							case 2:
								focus_status = 2;
								continue;
							case 3:
								focus_status = 4;
								continue;
						}
						continue;
					case 4:
						int r_dbname = textboxes["DBName"].focus();
						switch(r_dbname){
							case 1:
								focus_status = 5;
								continue;
							case 2:
								focus_status = 3;
								continue;
							case 3:
								focus_status = 5;
								continue;
						}
						continue;
					case 5:
						//up = 1
						//down = 2
						//left = 3
						//right = 4
						//enter = 5
						int r_save = buttons["Save"].focus();
						switch(r_save){
							case 1:
								focus_status = 4;
								continue;
							case 5:
								if (velidate() == 0){
									save();
									return 0;
								}
								string failed_field_name = "";
								switch (velidate()){
									case 1:
										failed_field_name = "IP Address";
									break;
									case 2:
										failed_field_name = "Username";
									break;
									case 3:
										failed_field_name = "Database Name";
									break;
								}
								Label l_err = new Label(this, "Error", 2, 12, 36, 1, ConsoleColor.White, $"Missing Required Field: {failed_field_name}");
								l_err.backgroundColor = ConsoleColor.Red;
								this.Height = 17;
								buttons["Save"].Y = 14;
								continue;
						}
						continue;
						
				}
			}
		}
	}
}