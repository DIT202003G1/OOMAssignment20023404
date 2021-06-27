using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib.Controls{
	class Button : ControlBase, IFocusables{
		bool focused = false;
		private string text;
		public Button(
			Window window_to_bind, 
			string id, 
			int x, 
			int y, 
			ConsoleColor foreground_color,
			ConsoleColor background_color,
			string text
		):base(
			window_to_bind,
			id,
			x,
			y,
			text.Length,
			1,
			foreground_color,
			background_color
		){
			this.text = text;
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
			Console.SetCursorPosition(absoluteX,absoluteY);
			if (focused){
				draw_focus();
				return;
			}
			Console.BackgroundColor = ElementBase.to_light(background_color);
			Console.ForegroundColor = ElementBase.to_light(foreground_color);
			Console.Write(text);
			Console.SetCursorPosition(absoluteX,absoluteY);
		}
		public void draw_focus(){
			Console.BackgroundColor = ElementBase.to_dark(background_color);
			Console.ForegroundColor = ElementBase.to_dark(foreground_color);
			Console.Write(text);
			Console.SetCursorPosition(absoluteX,absoluteY);
		}
		public ConsoleKey focus(){
			while(true){
				Console.SetCursorPosition(absoluteX,absoluteY);
				draw();
				ConsoleKeyInfo c = Console.ReadKey();
				switch (c.Key){
					case ConsoleKey.UpArrow:
						return ConsoleKey.UpArrow;
					case ConsoleKey.DownArrow:
						return ConsoleKey.DownArrow;
					case ConsoleKey.LeftArrow:
						return ConsoleKey.LeftArrow;
					case ConsoleKey.RightArrow:
						return ConsoleKey.RightArrow;
					case ConsoleKey.Enter:
						on_enter(this);
						return ConsoleKey.Enter;
				}
			}
		}
		protected override void on_create(){}
		protected virtual void on_enter(Button event_button){}
	}
}