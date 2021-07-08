using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class AddOrderItemMenu : Window{
		Item[] item_mask;
		Button cancel_button;
		MenuList item_list;
		public AddOrderItemMenu(Item[] item_mask):base("Edit Item", 2, 1, 45, 10, ConsoleColor.Black){
			this.item_mask = item_mask;
			cancel_button = new Button(this, "cancel_button", 37, 1, ConsoleColor.Black, ConsoleColor.White, "Cancel");
			item_list = new MenuList(this, "item_list", 2, 3, 41, 5, ConsoleColor.Black, ConsoleColor.White, itemListNames);
		}
		private string[] itemListNames{
			get{
				List<string> itemNames = new List<string>();
				foreach (Item i in item_mask){
					itemNames.Add(i.Name);
				}
				return itemNames.ToArray();
			}
		}
		public Item selectedItem{
			get=>item_mask[item_list.Index];
		}
		public override ConsoleKey focus(){
			// 1 - cancel_button
			// 2 - list
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch(focus_status){
					case 1:
						c = cancel_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 2;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Escape;
						continue;
					case 2:
						c = item_list.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 1;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Enter;
						continue;
				}
			}
		}
	}
}