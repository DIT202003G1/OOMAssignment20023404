using System;

namespace SecretGarden.OrderSystem.InterfaceLib{
	interface IFocusables{
		void draw_focus();
		ConsoleKey focus();
	}
}