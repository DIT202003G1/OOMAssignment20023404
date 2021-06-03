using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class MessageBox : Window{
		// (string title, int x, int y, int width, int height, ConsoleColor color)
	public MessageBox(string[] lines, string title):base(title, 4, 3, 0, 5+lines.Length, ConsoleColor.Black){
			this.width = get_longest_length(lines) + 4;
			int index = 0;
			foreach (string i in lines){
				new Label(this, $"Line {index}", 2, 1+index, get_longest_length(lines), 1, ConsoleColor.White, i);
				index ++;
			}
			new Button(this, "Confirm", 2, 2 + lines.Length, ConsoleColor.Black, ConsoleColor.White, "Confirm");
		}
		private int get_longest_length(string[] lines){
			int length = 0;
			foreach (string i in lines){
				if (i.Length > length){
					length = i.Length;
				}
			}
			return length;
		}
		public override int focus(){
			draw();
			while (buttons["Confirm"].focus() != 5);
			return 0;
		}
	}
}