using System;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class LoginErrorWindow : Window{
		// (string title, int x, int y, int width, int height, ConsoleColor color)
		public LoginErrorWindow(string content):base("Login Error", 4, 3, 32, 6, ConsoleColor.Red){
			Label l_msg = new Label(this,"Message",2,1,28,1, ConsoleColor.White, StringUtils.hide_by_max_width(content, 28));
			Button b_ok = new Button(this, "OK", 2, 3, ConsoleColor.Black, ConsoleColor.White, "  OK  ");
		}
		public override ConsoleKey focus(){
			draw();
			while (buttons["OK"].focus() != ConsoleKey.Enter);
			return ConsoleKey.Enter;
		}
	}
}