using System;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class LoginWindow : Window{
		// (string title, int x, int y, int width, int height, ConsoleColor color)
		public LoginWindow():base("SecretGarden Order System 1.0", 2, 1, 36, 10, ConsoleColor.Black){
			Label l_id = new Label(this, "ID", 2, 1, 2, 1, ConsoleColor.White, "ID");
			Label l_pw = new Label(this, "Password", 2, 4, 8, 1, ConsoleColor.White, "Password");
			Textbox t_id = new Textbox(this, "ID", 2, 2, 32, 1, ConsoleColor.Black, ConsoleColor.White, "");
			Textbox t_pw = new Textbox(this, "Password", 2, 5, 32, 1, ConsoleColor.Black, ConsoleColor.White, "");
			Button b_login = new Button(this, "Login", 2, 7, ConsoleColor.Black, ConsoleColor.White, "Login");
			t_id.allowedChars = StringUtils.numbers;
			t_id.maxLength = 8;
			t_pw.passwordChar = '*';
		}
		public override int focus(){
			draw();
			focus_status = 1;
			//status 1 = ID
			//status 2 = PW
			//status 3 = BUTTON
			while (true){
				switch (focus_status){
					case 1:
						int id_status = textboxes["ID"].focus();
						switch (id_status){
							case 1:
								focus_status=2;
								continue;
							case 3:
								focus_status=2;
								continue;
						}
					break;
					case 2:
						int pw_status = textboxes["Password"].focus();
						switch (pw_status){
							case 1:
								focus_status=3;
								continue;
							case 2:
								focus_status=1;
								continue;
							case 3:
								focus_status=3;
								continue;
						}
					break;
					case 3:
						int button_status = buttons["Login"].focus();
						switch (button_status){
							case 5:
								return 0;
							case 1:
								focus_status=2;
								continue;
						}
					break;
				}
			}
		}
	}
}