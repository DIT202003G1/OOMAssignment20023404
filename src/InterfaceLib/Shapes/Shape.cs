using System;

namespace SecretGarden.OrderSystem.InterfaceLib.Shapes{
	abstract class Shape : IDrawables{
		protected ConsoleColor background_color = ConsoleColor.White;
		protected ConsoleColor foreground_color = ConsoleColor.Black;
		protected int x, y, width, height;
		public Shape(){}
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