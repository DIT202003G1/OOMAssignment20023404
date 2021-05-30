using System;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib.Controls{
	class Label : ControlBase{
		string text;
		public Label(
			Window window_to_bind, 
			string id, 
			int x, 
			int y, 
			int width, 
			int height, 
			ConsoleColor color,
			string text
		):base(
			window_to_bind,
			id,
			x,
			y,
			width,
			height,
			ElementBase.to_dark(color),
			window_to_bind.backgroundColor
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
			Rectangle a = new Rectangle(absoluteX,absoluteY,height,width,backgroundColor);
			a.draw();
			Console.SetCursorPosition(absoluteX,absoluteY);
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(StringUtils.hide_by_max_width(text, width));
		}
		protected override void on_create(){}
	}
}