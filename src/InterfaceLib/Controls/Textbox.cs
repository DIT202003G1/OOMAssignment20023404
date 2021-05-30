using System;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib.Controls{
	class Textbox : ControlBase, IFocusables{
		string text;
		Boolean focused = false;
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
				draw_focuse();
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
		public void draw_focuse(){
			Rectangle a = new Rectangle(absoluteX,absoluteY,height,width,ElementBase.to_dark(backgroundColor));
			a.draw();
			Console.SetCursorPosition(absoluteX,absoluteY);
			Console.BackgroundColor = ElementBase.to_dark(backgroundColor);
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(displayText);
		}
		public int focuse(){
			//1 = break
			while (true){
				if (displayText.Length == width){
					Console.SetCursorPosition(absoluteX + displayText.Length-1, absoluteY);
				}
				else{
					Console.SetCursorPosition(absoluteX + displayText.Length, absoluteY);
				}
				char c = Console.ReadKey().KeyChar;
				if(StringUtils.printables.Contains(c)){
					text += c;
				}
				else{
					switch ((int) c){
						case 127:
							if (text.Length > 0)
							text = text.Substring(0,Text.Length - 1);
						break;
						case 13:
							return 1;
					}
				}
				draw();
			}
		}
	}
}