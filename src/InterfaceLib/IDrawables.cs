using System;

namespace SecretGarden.OrderSystem.InterfaceLib{
	interface IDrawables{
		void draw();
		ConsoleColor backgroundColor{get;set;}
		ConsoleColor foregroundColor{get;set;}
		int X{get;set;}
		int Y{get;set;}
		int Height{get;set;}
		int Width{get;set;}
	}
}