using System;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib.Controls{
	class Textbox : ControlBase, IFocusables{
		private string text;
		private Boolean focused = false;
		private string allowed_chars = StringUtils.Printables;
		private int length_restriction = -1;
		public Textbox(
			Window window_to_bind, 
			string id, 
			int x, 
			int y, 
			int width, 
			int height, 
			ConsoleColor foreground_color,
			ConsoleColor background_color,
			string text
		):base(
			window_to_bind,
			id,
			x,
			y,
			width,
			height,
			foreground_color,
			background_color
		){
			this.text=text;
			window_to_bind.register_control(this);
		}
		public string Text{
			get=>text;
			set{
				this.text = value;
				draw();
			}
		}
		public override void draw(){
			if (focused){
				draw_focus();
				return;
			}
			Rectangle a = new Rectangle(absoluteX,absoluteY,height,width,backgroundColor);
			a.draw();
			Console.SetCursorPosition(absoluteX,absoluteY);
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(displayText);
		}
		public string displayText{
			get{
				if (text.Length <= width) return text;
				return text.Substring(text.Length - width, width);
			}
		}
		public int maxLength{
			get=>length_restriction;
			set{
				length_restriction = value;
				if (text.Length > value){
					text = text.Substring(0,value);
					draw();
				}
			}
		}
		public string allowedChars{
			get=>allowed_chars;
			set{allowed_chars = value;}
		}
		public void draw_focus(){
			Rectangle a = new Rectangle(absoluteX,absoluteY,height,width,ElementBase.to_dark(backgroundColor));
			a.draw();
			Console.SetCursorPosition(absoluteX,absoluteY);
			Console.BackgroundColor = ElementBase.to_dark(backgroundColor);
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(displayText);
		}
		public int focus(){
			// 1 = ENTER
			// 2 = UP
			// 3 = DOWN
			while (true){
				if (displayText.Length == width){
					Console.SetCursorPosition(absoluteX + displayText.Length-1, absoluteY);
				}
				else{
					Console.SetCursorPosition(absoluteX + displayText.Length, absoluteY);
				}
				ConsoleKeyInfo c = Console.ReadKey();
				if(allowedChars.Contains(c.KeyChar)){
					if (maxLength < 0 || text.Length < maxLength) text += c.KeyChar;
				}
				else{
					switch (c.Key){
						case ConsoleKey.Backspace:
							if (text.Length > 0)
							text = text.Substring(0,Text.Length - 1);
						break;
						case ConsoleKey.Enter:
							return 1;
						case ConsoleKey.UpArrow:
							return 2;
						case ConsoleKey.DownArrow:
							return 3;
					}
				}
				draw();
			}
		}

		protected override void on_create(){}
	}
}