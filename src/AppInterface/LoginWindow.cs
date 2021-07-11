using System;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.AppEntities;
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
		public override ConsoleKey focus(){
			//status 1 = ID
			//status 2 = PW
			//status 3 = BUTTON
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				switch (focus_status){
					case 1:
						ConsoleKey id_status = textboxes["ID"].focus();
						switch (id_status){
							case ConsoleKey.DownArrow:
								focus_status=2;
								continue;
							case ConsoleKey.Enter:
								focus_status=2;
								continue;
						}
					break;
					case 2:
						ConsoleKey pw_status = textboxes["Password"].focus();
						switch (pw_status){
							case ConsoleKey.DownArrow:
								focus_status=3;
								continue;
							case ConsoleKey.UpArrow:
								focus_status=1;
								continue;
							case ConsoleKey.Enter:
								focus_status=3;
								continue;
						}
					break;
					case 3:
						ConsoleKey button_status = buttons["Login"].focus();
						switch (button_status){
							case ConsoleKey.Enter:
								try{
									Admin admin = Admin.login(Int32.Parse(textboxes["ID"].Text), textboxes["Password"].Text);
									MainMenu main_menu = new MainMenu(admin);
									main_menu.focus();
									textboxes["Password"].Text="";
									focus_status = 2;
									continue;
								}catch (Exceptions.AdminAccountException){
									new LoginErrorWindow("ID or Password Incorrect").focus();
								}catch (Exception e){
									Console.ResetColor();Console.Clear();
									MessageBox err = new MessageBox(new string[]{"The following exception has been raised","",StringUtils.hide_by_max_width(e.ToString().Split("\n")[0], 45),"","Please report this to the developers","Press enter to see tracebacks"}, "Fatel Error");
									err.focus();
									Console.ResetColor();Console.Clear();
									Console.SetCursorPosition(0,0);
									Console.WriteLine(e.ToString());
									Console.WriteLine(e.StackTrace);
									Console.ReadKey();
									MessageBox post_err = new MessageBox(new string[]{"Press enter to proceed to the login page"}, "Fatel Error");
									post_err.focus();
								}
								continue;
							case ConsoleKey.UpArrow:
								focus_status=2;
								continue;
						}
					break;
				}
			}
		}
	}
}