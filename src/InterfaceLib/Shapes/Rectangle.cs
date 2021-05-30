using System;

namespace SecretGarden.OrderSystem.InterfaceLib.Shapes{
	class Rectangle : ElementBase{
		public Rectangle(
			int x,
			int y,
			int height,
			int width,
			ConsoleColor background_color
		){
			this.x = x;
			this.y = y;
			this.height = height;
			this.width = width;
			this.background_color = background_color;
		}
		public override void draw(){
			for (int pointer_y = Y; pointer_y < Y+height; pointer_y++){
				Console.SetCursorPosition(X,pointer_y);
				Console.BackgroundColor = backgroundColor;
				for (int pointer_x = 0; pointer_x < width; pointer_x++){
					Console.Write(" ");
				}
				Console.ResetColor();
			}
		}
	}
}