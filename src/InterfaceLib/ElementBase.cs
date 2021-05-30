using System;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.InterfaceLib{
	abstract class ElementBase : IDrawables{
		public static Dictionary<ConsoleColor,ConsoleColor> color_type_dictionary = new Dictionary<ConsoleColor,ConsoleColor>(){
			{ConsoleColor.DarkGray,ConsoleColor.Black},
			{ConsoleColor.Red,ConsoleColor.DarkRed},
			{ConsoleColor.Green,ConsoleColor.DarkGreen},
			{ConsoleColor.Blue,ConsoleColor.DarkBlue},
			{ConsoleColor.Yellow,ConsoleColor.DarkYellow},
			{ConsoleColor.Magenta,ConsoleColor.DarkMagenta},
			{ConsoleColor.Cyan,ConsoleColor.DarkCyan},
			{ConsoleColor.White,ConsoleColor.Gray}
		};
		public static ConsoleColor to_dark(ConsoleColor c){
			if (color_type_dictionary.ContainsKey(c)){
				return color_type_dictionary[c];
			}
			return c;
		}
		public static ConsoleColor to_light(ConsoleColor c){
			if (color_type_dictionary.ContainsValue(c)){
				foreach(ConsoleColor i in color_type_dictionary.Keys){
					if (color_type_dictionary[i] == c){
						return i;
					}
				}
				return c;
			}
			return c;
		}
		protected ConsoleColor background_color = ConsoleColor.White;
		protected ConsoleColor foreground_color = ConsoleColor.Black;
		protected int x, y, width, height;
		public abstract void draw();
		public ConsoleColor backgroundColor{
			get=>this.background_color;
			set{
				this.background_color = value;
				draw();
			}
		}
		public ConsoleColor foregroundColor{
			get=>this.foreground_color;
			set{
				this.foreground_color = value;
				draw();
			}
		}
		public int X{
			get=>this.x;
			set{
				this.x=value;
				draw();
			}
		}
		public int Y{
			get=>this.y;
			set{
				this.y=value;
				draw();
			}
		}
		public int Width{
			get=>this.width;
			set{
				this.width=value;
				draw();
			}
		}
		public int Height{
			get=>this.height;
			set{
				this.height=value;
				draw();
			}
		}

	}
}