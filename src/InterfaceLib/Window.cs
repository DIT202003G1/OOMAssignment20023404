using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.InterfaceLib.Controls;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib{
	abstract class Window : ElementBase,IFocusables{
		private string title;
		protected int focus_status = 0;
		public Dictionary<string,Label> labels = new Dictionary<string, Label>();
		public Dictionary<string,Textbox> textboxes = new Dictionary<string, Textbox>();
		public Dictionary<string,MenuList> menu_lists = new Dictionary<string, MenuList>();
		public Dictionary<string,Button> buttons = new Dictionary<string, Button>();
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
			foreach (MenuList i in menu_lists.Values) i.draw();
			foreach (Button i in buttons.Values) i.draw();
			draw_focus();
		}
		public virtual void draw_focus(){}
		public abstract int focus();
		public void register_control(Label control){
			if (labels.ContainsKey(control.Id)){
				labels[control.Id] = control;
			}
			else labels.Add(control.Id,control);
		}
		public void register_control(Textbox control){
			if (textboxes.ContainsKey(control.Id)){
				textboxes[control.Id] = control;
			}
			else textboxes.Add(control.Id,control);
		}
		public void register_control(MenuList control){
			if (menu_lists.ContainsKey(control.Id)){
				menu_lists[control.Id] = control;
			}
			else menu_lists.Add(control.Id,control);
		}
		public void register_control(Button control){
			if (buttons.ContainsKey(control.Id)){
				buttons[control.Id] = control;
			}
			buttons.Add(control.Id,control);
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