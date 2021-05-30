using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.InterfaceLib.Controls;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib{
	class Window : ElementBase{
		private string title;
		public Dictionary<string,Label> labels = new Dictionary<string, Label>();
		public Dictionary<string,Textbox> textboxes = new Dictionary<string, Textbox>();
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
			Console.SetCursorPosition(X,Y);
			Console.BackgroundColor = ElementBase.to_dark(background_color);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(StringUtils.hide_by_max_width(title, width));
			foreach (Label i in labels.Values) i.draw();
			foreach (Textbox i in textboxes.Values) i.draw();
		}
		public void register_control(Label control){
			labels.Add(control.Id,control);
		}
		public void register_control(Textbox control){
			textboxes.Add(control.Id,control);
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