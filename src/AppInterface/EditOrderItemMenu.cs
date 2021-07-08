using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class EditOrderItemMenu : Window{
		Label item_prompt_label;
		Button inc_button;
		Button dec_button;
		Button remove_button;
		Button save_button;
		Textbox remarks_textbox;
		Textbox quantity_textbox;
		public EditOrderItemMenu(OrderItem order_item):base("Edit Item", 2, 1, 45, 9, ConsoleColor.Black){
			item_prompt_label = new Label(this, "item_prompt_label", 2, 1, 41, 1, ConsoleColor.White, $"Edit the following item: {order_item.Item.Name}");
			inc_button = new Button(this, "inc_button", 2, 3, ConsoleColor.Black, ConsoleColor.White, "＋");
			quantity_textbox = new Textbox(this, "quantity_textbox", 5, 3, 9, 1, ConsoleColor.Black, ConsoleColor.White, order_item.Quantity.ToString());
			dec_button = new Button(this, "dec_button", 15, 3, ConsoleColor.Black, ConsoleColor.White, "－");
			save_button = new Button(this, "save_button", 37, 3, ConsoleColor.Black, ConsoleColor.White, " Save ");
			remove_button = new Button(this, "remove_button", 30, 3, ConsoleColor.Black, ConsoleColor.White, "Remove");
			remarks_textbox = new Textbox(this, "temarks_textbox", 2, 5, 41, 1, ConsoleColor.Black, ConsoleColor.White,	order_item.Remark);
		}
		public int Quantity{
			get=>Int16.Parse(quantity_textbox.Text);
		}
		public string Remark{
			get=>remarks_textbox.Text;
		}
		private void increase(){
			quantity_textbox.Text = (Quantity + 1).ToString();
		}
		private void decrease(){
			if (Quantity > 1)
				quantity_textbox.Text = (Quantity - 1).ToString(); 
		}
		public override ConsoleKey focus(){
			// 1 - inc_button
			// 2 - dec_button
			// 3 - remove_button
			// 4 - save_button
			// 5 - remarks
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 1:
						c = inc_button.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 2;
						else if (c == ConsoleKey.DownArrow) focus_status = 5;
						else if (c == ConsoleKey.Enter) increase();
						continue;
					case 2:
						c = dec_button.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 3;
						else if (c == ConsoleKey.LeftArrow) focus_status = 1;
						else if (c == ConsoleKey.DownArrow) focus_status = 5;
						else if (c == ConsoleKey.Enter) decrease();
						continue;
					case 3:
						c = remove_button.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 4;
						else if (c == ConsoleKey.LeftArrow) focus_status = 2;
						else if (c == ConsoleKey.DownArrow) focus_status = 5;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Delete;
						continue;
					case 4:
						c = save_button.focus();
						if (c == ConsoleKey.LeftArrow) focus_status = 3;
						else if (c == ConsoleKey.DownArrow) focus_status = 5;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Enter;
						continue;
					case 5:
						c = remarks_textbox.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 1;
						continue;
				}
			}
		}
	}
}