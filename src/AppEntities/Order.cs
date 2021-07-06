using System.Collections.Generic;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.CustomerOrder;
using SecretGarden.OrderSystem.Database.Tables.OrderItem;
using SecretGarden.OrderSystem.Database.Tables.AdminAccount;
using SecretGarden.OrderSystem.Database.Tables.Customer;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.AppEntities{
	class Order{
		private int order_id;
		private List<OrderItem> items = new List<OrderItem>();
		private Datetime order_datetime;
		private Datetime prepare_datetime;
		private bool delivery;
		private bool completed;
		private Admin admin;
		private Customer customer;
		private Bill bill;
		public Order(int id){
			CustomerOrderRecord record = DBWrapper.Instance.customer_order_table.retrieve(new int[] {id});
			AdminAccountRecord admin_record = DBWrapper.Instance.admin_account_table.retrieve(new int[] {record.adminId});
			CustomerRecord customer_record = DBWrapper.Instance.customer_table.retrieve(new int[] {record.customerID});
			order_id = id;
			order_datetime = record.orderDatetime;
			prepare_datetime = record.prepareDatetime;
			delivery = record.isDelivery;
			completed = record.Completed;
			admin = Admin.fetch_admin(admin_record.primaryKey[0]);
			customer = Customer.fetch_customer(admin_record.primaryKey[0]);

			bill = new Bill(this);

			List<OrderItemRecord> orderItemRecords = DBWrapper.Instance.order_item_table.get_records();
			foreach (OrderItemRecord i in orderItemRecords){
				if (i.primaryKey[0] == id){
					OrderItem orderItem = new OrderItem(Item.fetch_item(i.primaryKey[1]), this, i.Quantity, i.Customization);
					orderItem.Remark = i.Customization;
					this.items.Add(orderItem);
				}
			}
		}
		public void add_item(Item item, int quantity, string remarks){
			DBWrapper.Instance.order_item_table.append_record(
				new OrderItemRecord(
					DBWrapper.Instance.order_item_table,
					0,
					order_id,
					item.Id,
					quantity,
					remarks
				)
			);
			items.Add(new OrderItem(item, this, quantity, remarks));
		}
		public void add_item(Item item, int quantity){
			add_item(item, quantity, "");
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
		public Bill Bill{
			get=>bill;
		}
	}
}