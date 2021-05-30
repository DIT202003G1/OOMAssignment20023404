using System;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;

namespace SecretGarden.OrderSystem.InterfaceLib.Controls{
	class Window : ElementBase{
		private string title;
		public Window(string title, int x, int y, int width, int height, ConsoleColor color){
			this.background_color = ElementBase.to_light(color);
			this.foreground_color = ConsoleColor.Black;
			this.title = title;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}
		public override void draw(){
			Rectangle base_plate = new Rectangle(X, Y, Height, Width, background_color);
			Rectangle title_plate = new Rectangle(X, Y, 1, Width, ElementBase.to_dark(background_color));
			base_plate.draw();
			title_plate.draw();
			if (title.Length <= width){
				Console.SetCursorPosition(X,Y);
				Console.BackgroundColor = ElementBase.to_dark(background_color);
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(title);
			}
		}
		public string Title{
			get=>title;
			set{
				this.title = value;
				draw();
			}
		}
	}
}