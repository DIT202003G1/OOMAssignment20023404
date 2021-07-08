using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib.Shapes;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.InterfaceLib.Controls{
	class MenuList : ControlBase, IFocusables{
		bool focused = false;
		int index = 0;
		private string[] items;
		public MenuList(
			Window window_to_bind, 
			string id, 
			int x, 
			int y, 
			int width, 
			int max_height, 
			ConsoleColor foreground_color,
			ConsoleColor background_color,
			string[] items
		):base(
			window_to_bind,
			id,
			x,
			y,
			width,
			max_height,
			foreground_color,
			background_color
		){
			this.items = items;
			window_to_bind.register_control(this);
		}
		public string[] Items{
			get=>items;
			set{
				this.items = value;
				if (index >= items.Length){
					index = items.Length - 1;
				}
				draw();
			}
		}
		public int Index{
			get=>index;
			set{
				index = value;
				draw();
			}
		}


		private void draw_item(string item, bool selected = false){
			Console.ForegroundColor = foreground_color;
			Console.BackgroundColor = background_color;
			Console.Write(" ");
			Console.BackgroundColor = window.backgroundColor;
			if (selected){
				
				Console.ForegroundColor = background_color;
				Console.Write(" ");
				Console.BackgroundColor = background_color;
				Console.ForegroundColor = foreground_color;
			}
			else{
				Console.Write(" ");
			}
			item = " " + item; 
			if (item.Length > width - 2){
				Console.Write(StringUtils.hide_by_max_width(item, width-2));
			}
			else{
				Console.Write(item);
				for (int i = 0; i < width - 2 - item.Length; i ++){
					Console.Write(" ");
				}
			}
		}
		public override void draw(){
			Rectangle b = new Rectangle(absoluteX, absoluteY, height, width, window.backgroundColor);
			b.draw();
			if (height >= items.Length){
				int display_index = 0;
				for (int i = 0; i < items.Length; i++){
					Console.SetCursorPosition(absoluteX, absoluteY + display_index);
					draw_item(items[i], i == index);
					display_index ++;
				}
			}
			else{
				int m1,m2,lower_bound, upper_bound;
				m2 = height / 2;
				m1 = m2 - ((height % 2 == 0) ? 1 : 0 );
				if (index < m1){
					lower_bound = 0;
					upper_bound = height;
				}
				else if(index >= items.Length - m2){
					upper_bound = items.Length;
					lower_bound = upper_bound - height;
				}
				else{
					Console.SetCursorPosition(0,0);
					upper_bound = index + m2 + 1;
					lower_bound = index - m1;
				}
				int display_index = 0;
				for (int i = lower_bound; i<upper_bound; i++){
					Console.SetCursorPosition(absoluteX, absoluteY + display_index);
					draw_item(items[i], i == index);
					display_index ++;
				}
			}
			if (focused) {
				draw_focus();
			}
		}
		public void draw_focus(){
			Console.SetCursorPosition(0,0);
			int m1,m2;
			m2 = height / 2;
			m1 = m2 - ((height % 2 == 0) ? 1 : 0 );
			if (height < items.Length){
				// Console.WriteLine("F");
				Console.BackgroundColor = background_color;
				Console.ForegroundColor = foreground_color;
				if(index >= items.Length - m2 - 1){
					Console.SetCursorPosition(absoluteX,absoluteY);
					Console.Write("▲");
				}
				else if(index <= m1){
					Console.SetCursorPosition(absoluteX,absoluteY+height-1);
					Console.Write("▼");
				}
				else{
					Console.SetCursorPosition(absoluteX,absoluteY);
					Console.Write("▲");
					Console.SetCursorPosition(absoluteX,absoluteY+height-1);
					Console.Write("▼");

				}
				Console.ResetColor();
			}
		}
		protected int selectionAbsoluteY{
			get{
				int m1,m2;
				m2 = height / 2;
				m1 = m2 - ((height % 2 == 0) ? 1 : 0 );
				if (index < m1){
					return absoluteY+index;
				}
				if(index >= items.Length - m2 - 1){
					return absoluteY+height-(items.Length-index);
				}
				else{
					return absoluteY + m1;
				}
			}
		}
		public ConsoleKey focus(){
			// 1 = confirmed
			// 2 = prev
			// 3 = up
			// 4 = down
			focused = true;
			draw();
			while (true){
				Console.SetCursorPosition(absoluteX, selectionAbsoluteY);
				ConsoleKeyInfo c = Console.ReadKey();
				switch (c.Key){
					case ConsoleKey.UpArrow:
						if (index > 0) Index --;
						else {
							focused = false;
							return ConsoleKey.UpArrow;
						}
					break;
					case ConsoleKey.DownArrow:
						if (index < items.Length - 1) Index ++;
						else return ConsoleKey.DownArrow;
					break;
					case ConsoleKey.Enter:
						focused = false;
						return ConsoleKey.Enter;
					case ConsoleKey.Tab:
						focused = false;
						return ConsoleKey.Tab;
				}
			}
		}
		protected override void on_create(){}
	}
}