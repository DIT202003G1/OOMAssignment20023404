using System;

namespace SecretGarden.OrderSystem.InterfaceLib{
	abstract class ControlBase : ElementBase{
		private string id;
		private Window window;
		public ControlBase(
			Window window_to_bind, 
			string id, 
			int x, 
			int y, 
			int width, 
			int height, 
			ConsoleColor foreground_color,
			ConsoleColor background_color
		){
			this.background_color = background_color;
			this.foreground_color = foreground_color;
			this.id = id;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.window = window_to_bind;
			on_create();
		}
		public string Id{get=>id;}
		public int absoluteX{get=>window.X+x;}
		public int absoluteY{get=>window.Y+y+1;}
		public abstract override void draw();

		//HANDLERS
		protected abstract void on_create();
	}
}