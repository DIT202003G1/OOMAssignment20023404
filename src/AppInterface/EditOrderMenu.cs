using System;
using System.Collections.Generic;
using System.Xml;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface
{
	class EditOrderMenu : Window
	{
		Order order;
		Label order_label;
		Label completed_label;
		Label type_label;
		Label customer_label;
		Label order_datetime_label;
		Label prepare_datetime_label;
		Label paid_label;
		Label total_price_label;
		Label completed_data_label;
		Label type_data_label;
		Label customer_data_label;
		Label order_datetime_data_label;
		Label prepare_datetime_data_label;
		Label paid_data_label;
		Label total_price_data_label;
		Button delete_order_button;
		Button change_completed_button;
		Button change_type_button;
		Button change_customer_button;
		Button change_prepare_datetime_button;
		Button pay_button;
		Button add_item_button;
		Button save_button;
		Label select_an_item_to_edit_label;
		MenuList items_menu_list;

		private string[] listItemInString{
			get{

				var strings = new List<String>(order.Items.Count);
				foreach (var item in order.Items){
					string data = $"{item.Item.Name} * {item.Quantity}";
					if (item.Remark.Trim() != ""){
						data += $" [{item.Remark.Trim()}]";
					} 
					strings.Add(data);
				}
				return strings.ToArray();
			}
		}

		public EditOrderMenu(Order order) : base("Edit Order", 2, 1, 45, 20, ConsoleColor.Black){
			this.order = order;

			order_label = new Label(this, "order_label", 2, 1, 17, 1, ConsoleColor.White, $"Order ID: #{order.Id}");
			delete_order_button = new Button(this, "delete_order_button", 36, 1, ConsoleColor.Black, ConsoleColor.White, " Delete ");
			completed_label = new Label(this, "completed_label", 2, 2, 10, 1, ConsoleColor.White, "Completed:");
			change_completed_button = new Button(this, "change_completed_button", 36, 2, ConsoleColor.Black, ConsoleColor.White, " Change ");
			type_label = new Label(this, "type_label", 2, 3, 5, 1, ConsoleColor.White, "Type:");
			change_type_button = new Button(this, "change_type_button", 36, 3, ConsoleColor.Black, ConsoleColor.White, " Change ");
			customer_label = new Label(this, "customer_label", 2, 4, 9, 1, ConsoleColor.White, "Customer:");
			change_customer_button = new Button(this, "change_customer_button", 36, 4, ConsoleColor.Black, ConsoleColor.White, " Change ");
			order_datetime_label = new Label(this, "order_datetime_label", 2, 5, 16, 1, ConsoleColor.White, "Order Datetime:");
			prepare_datetime_label = new Label(this, "prepare_datetime_label", 2, 6, 18, 1, ConsoleColor.White, "Prepare Datetime:");
			change_prepare_datetime_button = new Button(this, "change_prepare_datetime_button", 36, 6, ConsoleColor.Black, ConsoleColor.White, " Change ");
			paid_label = new Label(this, "paid_label", 2, 7, 5, 1, ConsoleColor.White, "Paid:");
			pay_button = new Button(this, "pay_button", 39, 7, ConsoleColor.Black, ConsoleColor.White, " Set ");
			total_price_label = new Label(this, "total_price_label", 2, 9, 12, 1, ConsoleColor.White, "Total Price:");
			add_item_button = new Button(this, "add_item_button", 27, 9, ConsoleColor.Black, ConsoleColor.White, " Add Item ");
			save_button = new Button(this, "save_button", 38, 9, ConsoleColor.Black, ConsoleColor.White, " Save ");
			select_an_item_to_edit_label = new Label(this, "select_an_item_to_edit_label", 2, 11, 24, 1, ConsoleColor.White, "(Select an item to edit)");
			items_menu_list = new MenuList(this, "items_menu", 2, 12, 41, 6, ConsoleColor.White, ConsoleColor.Black, listItemInString);

			completed_data_label = new Label(this, "completed_data_label", 13, 2, 3, 1, ConsoleColor.White, "");
			type_data_label = new Label(this, "type_data_label", 8, 3, 8, 1, ConsoleColor.White, "");
			customer_data_label = new Label(this, "customer_data_label", 12, 4, 20, 1, ConsoleColor.White, "");
			order_datetime_data_label = new Label(this, "order_datetime_data_label", 18, 5, 16, 1, ConsoleColor.White, "");
			prepare_datetime_data_label = new Label(this, "prepare_datetime_data_label", 20, 6, 16, 1, ConsoleColor.White, "");
			paid_data_label = new Label(this, "paid_data_label", 8, 7, 3, 1, ConsoleColor.White, "");
			total_price_data_label = new Label(this, "total_price_data_label", 15, 9, 16, 1, ConsoleColor.White, "");
		}

		protected override void on_draw(){
			update_data();
		}

		private void update_data(){
			completed_data_label.Text = this.order.isCompleted ? "YES" : "NO";
			type_data_label.Text = this.order.isDelivery ? "Delivery" : "Pick up";
			customer_data_label.Text = $"{this.order.Customer.firstName} {this.order.Customer.lastName}";
			Datetime order_datetime = this.order.orderDatetime;
			Datetime prepare_datetime = this.order.prepareDatetime;
			order_datetime_data_label.Text = $"{order_datetime.Day}-{order_datetime.Month}-{order_datetime.Year} {order_datetime.Hour}:{order_datetime.Minute}";
			prepare_datetime_data_label.Text = $"{prepare_datetime.Day}-{prepare_datetime.Month}-{prepare_datetime.Year} {prepare_datetime.Hour}:{prepare_datetime.Minute}";
			paid_data_label.Text = this.order.Bill.isPaid ? "YES" : "NO";
			total_price_data_label.Text = this.order.Bill.Price.ToString();
			items_menu_list.Items = listItemInString;
		}

		public override ConsoleKey focus(){
			// 1 - delete
			// 2 - completed
			// 3 - type
			// 4 - customer
			// 5 - prepare datetime
			// 51 - pay
			// 6 - add item
			// 7 - save
			// 8 - list
			focus_status = 1;
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				switch (focus_status){
					case 1:
						ConsoleKey r_delete = delete_order_button.focus();
						if (r_delete == ConsoleKey.DownArrow) focus_status = 2;
						else if (r_delete == ConsoleKey.Enter){
							order.remove_order();
							return ConsoleKey.Enter;
						}
						continue;
					case 2:
						ConsoleKey r_completed = change_completed_button.focus();
						if (r_completed == ConsoleKey.DownArrow) focus_status = 3;
						else if (r_completed == ConsoleKey.UpArrow) focus_status = 1;
						else if (r_completed == ConsoleKey.Enter){
							order.isCompleted = !order.isCompleted;
						}
						continue;
					case 3:
						ConsoleKey r_type = change_type_button.focus();
						if (r_type == ConsoleKey.DownArrow) focus_status = 4;
						else if (r_type == ConsoleKey.UpArrow) focus_status = 2;
						else if (r_type == ConsoleKey.Enter){
							order.isDelivery = !order.isDelivery;
						}
						continue;
					case 4:
						ConsoleKey r_customer = change_customer_button.focus();
						if (r_customer == ConsoleKey.DownArrow) focus_status = 5;
						else if (r_customer == ConsoleKey.UpArrow) focus_status = 3;
						else if (r_customer == ConsoleKey.Enter){
							SelectCustomerMenu selectCustomerMenu = new SelectCustomerMenu();
							ConsoleKey c = selectCustomerMenu.focus();
							if (c == ConsoleKey.Enter){
								int index = selectCustomerMenu.menu_lists["customers_list"].Index;
								int customer_id = DBWrapper.Instance.customer_table.get_records()[index].primaryKey[0];
								Customer customer = Customer.fetch_customer(customer_id);
								order.Customer = customer;
							}
						}
						continue;
					case 5:
						ConsoleKey r_prepare = change_prepare_datetime_button.focus();
						if (r_prepare == ConsoleKey.DownArrow) focus_status = 51;
						else if (r_prepare == ConsoleKey.UpArrow) focus_status = 4;
						else if (r_prepare == ConsoleKey.Enter){
							DateEditingMenu dem = new DateEditingMenu(order.prepareDatetime, "Edit Prepare Time", true, false);
							dem.focus();
							order.prepareDatetime = new Datetime(dem.Year, dem.Month, dem.Day, dem.Hour, dem.Minute, dem.Second);
						}
						continue;
					case 51:
						ConsoleKey r_pay = pay_button.focus();
						if (r_pay == ConsoleKey.DownArrow) focus_status = 6;
						else if (r_pay == ConsoleKey.UpArrow) focus_status = 5;
						else if (r_pay == ConsoleKey.Enter){
							PaymentMenu payment_menu;
							if (order.Bill.isPaid)
								payment_menu = new PaymentMenu(order.Bill.Price, order.Customer.is_premium(), order.Bill.paymentMethod);
							else
								payment_menu = new PaymentMenu(order.Bill.Price, order.Customer.is_premium());
							ConsoleKey c = payment_menu.focus();
							if (c == ConsoleKey.Enter){
								order.Bill.isPaid = true;
								order.Bill.paymentMethod = payment_menu.paymentMethod;
							}
						}
						continue;
					case 6:
						ConsoleKey r_add_item = add_item_button.focus();
						if (r_add_item == ConsoleKey.DownArrow) focus_status = 8;
						else if (r_add_item == ConsoleKey.RightArrow) focus_status = 7;
						else if (r_add_item == ConsoleKey.UpArrow) focus_status = 51;
						else if (r_add_item == ConsoleKey.Enter){
							List<Item> mask = new List<Item>();
							foreach (Item i in Item.fetch_items()){
								if (! order.item_exists(i))
									mask.Add(i);
							}
							AddOrderItemMenu aoim = new AddOrderItemMenu(mask.ToArray());
							ConsoleKey c = aoim.focus();
							if (c == ConsoleKey.Enter){
								order.add_item(aoim.selectedItem, 1, "");
							}
						}
						continue;
					case 7:
						ConsoleKey r_save = save_button.focus();
						if (r_save == ConsoleKey.DownArrow) focus_status = 8;
						else if (r_save == ConsoleKey.LeftArrow) focus_status = 6;
						else if (r_save == ConsoleKey.UpArrow) focus_status = 51;
						else if (r_save == ConsoleKey.Enter) return ConsoleKey.Enter;
						continue;
					case 8:
						ConsoleKey r_list = items_menu_list.focus();
						if (r_list == ConsoleKey.UpArrow) focus_status = 6;
						else if (r_list == ConsoleKey.Enter){
							EditOrderItemMenu eoim = new EditOrderItemMenu(order.Items[items_menu_list.Index]);
							ConsoleKey c = eoim.focus();
							if (c == ConsoleKey.Enter){
								order.Items[items_menu_list.Index].Quantity = eoim.Quantity;
								order.Items[items_menu_list.Index].Remark = eoim.Remark;
							}
							else if (c == ConsoleKey.Delete){
								order.remove_item(order.Items[items_menu_list.Index].Item);
								if (items_menu_list.Items.Length <= 0) focus_status = 7;
								else items_menu_list.Index = 1;
							}
						}
						continue;
				}
			}
		}
	}
}