using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class PaymentMenu : Window{
		Label payment_method_label;
		Label price_label;
		Label discount_label;
		Textbox payment_method_textbox;
		Label price_data_label;
		Label discount_data_label;
		Button save;
		Button cancel;
		public PaymentMenu(double price, bool discount, string payment_method):base("Payment Details", 2, 1, 45, 10, ConsoleColor.Black){
			init(price, discount, payment_method);
		}
		public PaymentMenu(double price, bool discount):base("Payment Details", 2, 1, 45, 10, ConsoleColor.Black){
			init(price, discount, "");
		}
		private void init(double price, bool discount, string payment_method){
			payment_method_label = new Label(this, "payment_method", 2, 1, 16, 1, ConsoleColor.White, "Payment Method:");
			price_label = new Label(this, "price_label", 2, 3, 14, 1, ConsoleColor.White, "Total Price:");
			discount_label = new Label(this, "discount_label", 2, 5, 17, 1, ConsoleColor.White, "Premium Discount:");
			save = new Button(this, "save", 2, 7, ConsoleColor.Black, ConsoleColor.White, " Save ");
			cancel = new Button(this, "cancel", 9, 7, ConsoleColor.Black, ConsoleColor.White, "Cancel");

			payment_method_textbox = new Textbox(this, "payment_method_textbox", 25, 1, 18, 1, ConsoleColor.Black, ConsoleColor.White, payment_method);

			string formatted_price = price.ToString(".00");
			string price_data = $"RM {formatted_price}";
			price_data_label = new Label(this, "price_data_label", 43-price_data.Length, 3, price_data.Length, 1, ConsoleColor.White, price_data);
			discount_data_label = new Label(this, "discount_data_label", 40, 5, 3, 1, ConsoleColor.White, (discount ? "YES" : " NO"));
		}
		public string paymentMethod{
			get=>payment_method_textbox.Text.Trim();
		}

		public override ConsoleKey focus(){
			// 1 - payment_method
			// 2 - save
			// 3 - cancel
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 1:
						c = payment_method_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 2;
						continue;
					case 2:
						c = save.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 1;
						else if (c == ConsoleKey.RightArrow) focus_status = 3;
						else if (c == ConsoleKey.Enter){
							if (paymentMethod == ""){
								this.height=12;
								Label err = new Label(this, "err", 2, 9, 41, 1, ConsoleColor.White, "Payment Method cannot be empty");
								err.backgroundColor = ConsoleColor.Red;
							}
							else return ConsoleKey.Enter;
						}
						continue;
					case 3:
						c = cancel.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 1;
						else if (c == ConsoleKey.LeftArrow) focus_status = 2;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Escape;
						continue;
				}
			}
		}
	}
}