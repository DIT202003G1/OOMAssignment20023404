using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class FilterOptionsMenu : Window{
		OrderFilter order_filter;
		Label order_id_label;
		Label admin_label;
		Label customer_label;
		Label order_date_label;
		Label prepare_date_label;
		Label completed_label;
		Label delivery_type_label;
		Textbox order_id_textbox;
		Button admin_button;
		Button customer_button;
		Button order_date_from_button;
		Button order_date_until_button;
		Button prepare_date_from_button;
		Button prepare_date_until_button;
		Button completed_button;
		Button delivery_type_button;
		Button order_id_clear_button;
		Button admin_clear_button;
		Button customer_clear_button;
		Button order_date_clear_button;
		Button prepare_date_clear_button;
		Button completed_clear_button;
		Button delivery_clear_button;
		Button done_button;
		Button overall_clear_button;
		public FilterOptionsMenu(OrderFilter order_filter):base("Filter Options...", 2, 1, 45, 18, ConsoleColor.Black){
			this.order_filter = order_filter;

			order_id_label = new Label(this, "order_id_label", 2, 1, 10, 1, ConsoleColor.White, "Order ID");
			admin_label = new Label(this, "admin_label", 2, 3, 10, 1, ConsoleColor.White, "Admin");
			customer_label = new Label(this, "customer_label", 2, 5, 10, 1, ConsoleColor.White, "Customer");
			order_date_label = new Label(this, "order_date_label", 2, 7, 10, 1, ConsoleColor.White, "Order Date");
			prepare_date_label = new Label(this, "prepare_date_label", 2, 9, 15, 1, ConsoleColor.White, "Prepare Date");
			completed_label = new Label(this, "completed_label", 2, 11, 10, 1, ConsoleColor.White, "Completed");
			delivery_type_label = new Label(this, "delivery_type_label", 2, 13, 15, 1, ConsoleColor.White, "Delivery Type");

			order_id_textbox = new Textbox(this, "order_id_textbox", 25, 1, 15, 1, ConsoleColor.Black, ConsoleColor.White, "");
			admin_button = new Button(this, "admin_button", 25, 3, ConsoleColor.Black, ConsoleColor.White, "               ");
			customer_button = new Button(this, "customer_button", 25, 5, ConsoleColor.Black, ConsoleColor.White, "               ");
			order_date_from_button = new Button(this, "order_date_from_button", 25, 7, ConsoleColor.Black, ConsoleColor.White, "       ");
			order_date_until_button = new Button(this, "order_date_until_button", 33, 7, ConsoleColor.Black, ConsoleColor.White, "       ");
			prepare_date_from_button = new Button(this, "prepare_date_from_button", 25, 9, ConsoleColor.Black, ConsoleColor.White, "       ");
			prepare_date_until_button = new Button(this, "prepare_date_until_button", 33, 9, ConsoleColor.Black, ConsoleColor.White, "       ");
			completed_button = new Button(this, "completed_button", 25, 11, ConsoleColor.Black, ConsoleColor.White, "               ");
			delivery_type_button = new Button(this, "delivery_type_button", 25, 13, ConsoleColor.Black, ConsoleColor.White, "               ");

			order_id_clear_button = new Button(this, "order_id_clear_button", 41, 1, ConsoleColor.Black, ConsoleColor.White, "ｘ");
			admin_clear_button = new Button(this, "admin_clear_button", 41, 3, ConsoleColor.Black, ConsoleColor.White, "ｘ");
			customer_clear_button = new Button(this, "customer_clear_button", 41, 5, ConsoleColor.Black, ConsoleColor.White, "ｘ");
			order_date_clear_button = new Button(this, "order_date_clear_button", 41, 7, ConsoleColor.Black, ConsoleColor.White, "ｘ");
			prepare_date_clear_button = new Button(this, "prepare_date_clear_button", 41, 9, ConsoleColor.Black, ConsoleColor.White, "ｘ");
			completed_clear_button = new Button(this, "completed_clear_button", 41, 11, ConsoleColor.Black, ConsoleColor.White, "ｘ");
			delivery_clear_button = new Button(this, "delivery_clear_button", 41, 13, ConsoleColor.Black, ConsoleColor.White, "ｘ");

			done_button = new Button(this, "done_button", 2, 15, ConsoleColor.Black, ConsoleColor.White, " Done ");
			overall_clear_button = new Button(this, "overall_clear_button", 9, 15, ConsoleColor.Black, ConsoleColor.White, "Clear All");

			order_id_textbox.maxLength = 8;
			order_id_textbox.allowedChars = "1234567890";
		}
		public OrderFilter orderFilter{
			get=>order_filter;
		}
		protected override void on_draw(){
			string order_id_data;
			if (order_filter.orderID == null)
				order_id_data = "";
			else
				order_id_data = order_filter.orderID.ToString();
			order_id_textbox.Text = order_id_data;

			string admin_button_data;
			if (order_filter.Admin == null)
				admin_button_data = "Not Set...     ";
			else
				admin_button_data = "Set            ";
			admin_button.Text = admin_button_data;

			string customer_button_data;
			if (order_filter.Customer == null)
				customer_button_data = "Not Set...     ";
			else
				customer_button_data = "Set            ";
			customer_button.Text = customer_button_data;

			string prepare_date_from_button_data;
			if (order_filter.prepareDatetimeFrom == null)
				prepare_date_from_button_data = "Not Set";
			else
				prepare_date_from_button_data = "Set    ";
			prepare_date_from_button.Text = prepare_date_from_button_data;

			string prepare_date_until_button_data;
			if (order_filter.prepareDatetimeUntil == null)
				prepare_date_until_button_data = "Not Set";
			else
				prepare_date_until_button_data = "Set    ";
			prepare_date_until_button.Text = prepare_date_until_button_data;

			string order_date_from_button_data;
			if (order_filter.orderDatetimeFrom == null)
				order_date_from_button_data = "Not Set";
			else
				order_date_from_button_data = "Set    ";
			order_date_from_button.Text = order_date_from_button_data;

			string order_date_until_button_data;
			if (order_filter.orderDatetimeUntil == null)
				order_date_until_button_data = "Not Set";
			else
				order_date_until_button_data = "Set    ";
			order_date_until_button.Text = order_date_until_button_data;

			string completed_button_data;
			if (order_filter.isCompleted == null)
				completed_button_data = "Not Set...     ";
			else if ((bool) order_filter.isCompleted)
				completed_button_data = "Completed      ";
			else
				completed_button_data = "Not Completed  ";
			completed_button.Text = completed_button_data;

			string delivery_type_button_data;
			if (order_filter.isDelivery == null)
				delivery_type_button_data = "Not Set...     ";
			else if ((bool) order_filter.isDelivery)
				delivery_type_button_data = "Delivery       ";
			else
				delivery_type_button_data = "Pick Up        ";
			delivery_type_button.Text = delivery_type_button_data;
		}
		public override ConsoleKey focus(){
			// 1 - id
			// 2 - admin
			// 3 - customer
			// 4 - orderdate
			// 5 - preparedate
			// 6 - completed
			// 7 - delivery
			// 8 - action buttons
			focus_status = 10;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 10:
						c = order_id_textbox.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 20;
						else if (c == ConsoleKey.RightArrow) focus_status = 19;
						if (order_id_textbox.Text.Trim() == ""){
							order_filter.clear_id();
						}
						else{
							order_filter.use_id(Int32.Parse(order_id_textbox.Text));
						}
						continue;
					case 19:
						c = order_id_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 29;
						else if (c == ConsoleKey.LeftArrow) focus_status = 10;
						else if (c == ConsoleKey.Enter){
							order_filter.clear_id();
						}
						continue;
					case 20:
						c = admin_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 30;
						else if (c == ConsoleKey.UpArrow) focus_status = 10;
						else if (c == ConsoleKey.RightArrow) focus_status = 29;
						else if (c == ConsoleKey.Enter){
							SelectAdminMenu sam = new SelectAdminMenu();
							ConsoleKey r = sam.focus();
							if (r == ConsoleKey.Enter){
								order_filter.placed_by_admin(sam.selectedAdmin);
							}
						}
						continue;
					case 29:
						c = admin_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 39;
						else if (c == ConsoleKey.UpArrow) focus_status = 19;
						else if (c == ConsoleKey.LeftArrow) focus_status = 20;
						else if (c == ConsoleKey.Enter){
							order_filter.placed_by_admin(null);
						}
						continue;
					case 30:
						c = customer_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 40;
						else if (c == ConsoleKey.UpArrow) focus_status = 20;
						else if (c == ConsoleKey.RightArrow) focus_status = 39;
						else if (c == ConsoleKey.Enter){
							SelectCustomerMenu scm = new SelectCustomerMenu(false);
							ConsoleKey r = scm.focus();
							if (r == ConsoleKey.Enter){
								Customer[] customers = Customer.fetch_customers();
								Customer customer = customers[scm.menu_lists["customers_list"].Index];
								order_filter.ordered_by(customer);
							}
						}
						continue;
					case 39:
						c = customer_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 49;
						else if (c == ConsoleKey.UpArrow) focus_status = 29;
						else if (c == ConsoleKey.LeftArrow) focus_status = 30;
						else if (c == ConsoleKey.Enter){
							order_filter.ordered_by(null);
						}
						continue;
					case 40:
						c = order_date_from_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 50;
						else if (c == ConsoleKey.UpArrow) focus_status = 30;
						else if (c == ConsoleKey.RightArrow) focus_status = 41;
						else if (c == ConsoleKey.Enter){
							Datetime temp;
							if (order_filter.orderDatetimeFrom != null) temp = order_filter.orderDatetimeFrom;
							else temp = (Datetime) DateTime.Today;
							DateEditingMenu dem = new DateEditingMenu(temp, "Order Datetime From ...", true, false);
							dem.focus();
							Datetime until = order_filter.orderDatetimeUntil;
							order_filter.ordered_between(new Datetime(dem.Year, dem.Month, dem.Day, dem.Hour, dem.Minute, dem.Second), until);
						}
						continue;
					case 41:
						c = order_date_until_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 51;
						else if (c == ConsoleKey.UpArrow) focus_status = 30;
						else if (c == ConsoleKey.RightArrow) focus_status = 49;
						else if (c == ConsoleKey.LeftArrow) focus_status = 40;
						else if (c == ConsoleKey.Enter){
							Datetime temp;
							if (order_filter.orderDatetimeUntil != null) temp = order_filter.orderDatetimeUntil;
							else temp = (Datetime) DateTime.Today;
							DateEditingMenu dem = new DateEditingMenu(temp, "Order Datetime Until ...", true, false);
							dem.focus();
							Datetime from = order_filter.orderDatetimeFrom;
							order_filter.ordered_between(from, new Datetime(dem.Year, dem.Month, dem.Day, dem.Hour, dem.Minute, dem.Second));
						}
						continue;
					case 49:
						c = order_date_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 59;
						else if (c == ConsoleKey.UpArrow) focus_status = 39;
						else if (c == ConsoleKey.LeftArrow) focus_status = 41;
						else if (c == ConsoleKey.Enter){
							order_filter.ordered_between(null, null);
						}
						continue;
					case 50:
						c = prepare_date_from_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 60;
						else if (c == ConsoleKey.UpArrow) focus_status = 40;
						else if (c == ConsoleKey.RightArrow) focus_status = 51;
						else if (c == ConsoleKey.Enter){
							Datetime temp;
							if (order_filter.prepareDatetimeFrom != null) temp = order_filter.prepareDatetimeFrom;
							else temp = (Datetime) DateTime.Today;
							DateEditingMenu dem = new DateEditingMenu(temp, "Prepare Datetime From ...", true, false);
							dem.focus();
							Datetime until = order_filter.prepareDatetimeUntil;
							order_filter.prepare_between(new Datetime(dem.Year, dem.Month, dem.Day, dem.Hour, dem.Minute, dem.Second), until);
						}
						continue;
					case 51:
						c = prepare_date_until_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 60;
						else if (c == ConsoleKey.UpArrow) focus_status = 41;
						else if (c == ConsoleKey.RightArrow) focus_status = 59;
						else if (c == ConsoleKey.LeftArrow) focus_status = 50;
						else if (c == ConsoleKey.Enter){
							Datetime temp;
							if (order_filter.prepareDatetimeUntil != null) temp = order_filter.prepareDatetimeUntil;
							else temp = (Datetime) DateTime.Today;
							DateEditingMenu dem = new DateEditingMenu(temp, "Prepare Datetime Until ...", true, false);
							dem.focus();
							Datetime from = order_filter.prepareDatetimeFrom;
							order_filter.prepare_between(from, new Datetime(dem.Year, dem.Month, dem.Day, dem.Hour, dem.Minute, dem.Second));
						}
						continue;
					case 59:
						c = prepare_date_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 69;
						else if (c == ConsoleKey.UpArrow) focus_status = 49;
						else if (c == ConsoleKey.LeftArrow) focus_status = 51;
						else if (c == ConsoleKey.Enter){
							order_filter.prepare_between(null, null);
						}
						continue;
					case 60:
						c = completed_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 70;
						else if (c == ConsoleKey.UpArrow) focus_status = 50;
						else if (c == ConsoleKey.RightArrow) focus_status = 69;
						else if (c == ConsoleKey.Enter){
							if (order_filter.isCompleted != null){
								if ((bool) order_filter.isCompleted) order_filter.not_completed();
								else order_filter.is_completed();
							}
							else order_filter.is_completed();
						}
						continue;
					case 69:
						c = completed_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 79;
						else if (c == ConsoleKey.UpArrow) focus_status = 59;
						else if (c == ConsoleKey.LeftArrow) focus_status = 60;
						else if (c == ConsoleKey.Enter){
							order_filter.reset_completed();
						}
						continue;
					case 70:
						c = delivery_type_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 80;
						else if (c == ConsoleKey.UpArrow) focus_status = 60;
						else if (c == ConsoleKey.RightArrow) focus_status = 79;
						else if (c == ConsoleKey.Enter){
							if (order_filter.isDelivery != null){
								if ((bool) order_filter.isDelivery) order_filter.pick_up();
								else order_filter.delivery();
							}
							else order_filter.delivery();
						}
						continue;
					case 79:
						c = delivery_clear_button.focus();
						if (c == ConsoleKey.DownArrow) focus_status = 81;
						else if (c == ConsoleKey.UpArrow) focus_status = 69;
						else if (c == ConsoleKey.LeftArrow) focus_status = 70;
						else if (c == ConsoleKey.Enter){
							order_filter.reset_delivery_type();
						}
						continue;
					case 80:
						c = done_button.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 70;
						else if (c == ConsoleKey.RightArrow) focus_status = 81;
						else if (c == ConsoleKey.Enter) return ConsoleKey.Enter;
						continue;
					case 81:
						c = overall_clear_button.focus();
						if (c == ConsoleKey.UpArrow) focus_status = 79;
						else if (c == ConsoleKey.LeftArrow) focus_status = 80;
						else if (c == ConsoleKey.Enter){
							order_filter.reset_all();
						}
						continue;
				}
			}
		}
	}
}