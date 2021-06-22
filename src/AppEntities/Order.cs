using System.Collections.Generic;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.AppEntity{
	class Order{
		private int order_id;
		private List<OrderItem> items = new List<OrderItem>();
		private Datetime order_datetime;
		private Datetime prepare_datetime;
		private bool delivery;
		private bool completed;
		private Admin admin;
		private Customer customer;
		public void add_item(Item item, int quantity){
			items.Add(new OrderItem(item, quantity));
		}
		public void remove_item(Item item){
			foreach(OrderItem i in items){
				if (i.Item.Id == item.Id){
					DBWrapper.Instance.order_item_table.retrieve(new int[] {order_id, i.Item.Id}).remove_record();
					items.Remove(i);
					break;
				}
			}
		}
		public bool item_exists(Item item){
			foreach(OrderItem i in items){
				if (i.Item.Id == item.Id){
					return true;
				}
			}
			return false;
		}
		public List<OrderItem> Items{
			get=>items;
		}
		public Customer Customer{
			get=>customer;
		}
		public Admin Admin{
			get=>admin;
		}
		public int Id{
			get=>order_id;
		}
		public Datetime orderDatetime{
			get=>order_datetime;
		}
		public Datetime prepareDatetime{
			get=>prepare_datetime;
			set{
				DBWrapper.Instance.customer_order_table.retrieve(new int[] {order_id}).prepareDatetime = value;
				prepare_datetime = value;
			}
		}
		public bool isDelivery{
			get=>delivery;
			set{
				DBWrapper.Instance.customer_order_table.retrieve(new int[] {order_id}).isDelivery = value;
				delivery = value;
			}
		}
		public bool isCompleted{
			get=>completed;
			set{
				DBWrapper.Instance.customer_order_table.retrieve(new int[] {order_id}).Completed = value;
				completed = value;
			}
		}
	}
}