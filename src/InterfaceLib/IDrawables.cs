namespace SecretGarden.OrderSystem.InterfaceLib{
	interface IDrawables{
		void draw();
		Color backgroundColor{get;set;}
		Color foregroundColor{get;set;}
		int X{get;set;}
		int Y{get;set;}
		int Height{get;set;}
		int Width{get;set;}
	}
}