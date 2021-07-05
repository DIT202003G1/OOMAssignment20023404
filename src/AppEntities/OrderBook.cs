using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Exceptions;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.CustomerOrder;

namespace SecretGarden.OrderSystem.AppEntities{
	class OrderBook{
		private static OrderBook instance;
		private OrderBook(){
			List<CustomerOrderRecord> orderRecords = DBWrapper.Instance.customer_order_table.get_records();
			foreach (CustomerOrderRecord i in orderRecords){
				this.orders.Add(new Order(i.primaryKey[0]));
			}
		}
		private List<Order> orders = new List<Order>();
		public List<Order> fetch_orders() => orders;
		public Order fetch_order(int id){
			foreach (Order i in orders){
				if (i.Id == id) return i;
			}
			throw new OrderException(OrderException.exception_type.ORDER_NOT_FOUND);
		}
		public int make_order(Customer customer, Admin admin, Datetime prepare_datetime, bool is_delivery){
			return make_order(customer,admin,prepare_datetime,is_delivery,(Datetime) DateTime.Now);
		}
		public int make_order(Customer customer, Admin admin, Datetime prepare_datetime, bool is_delivery, Datetime order_datetime){
			DBWrapper.Instance.customer_order_table.new_record(
				new CustomerOrderRecord(
					DBWrapper.Instance.customer_order_table,
					1,
					1,
					customer.customerId,
					(Datetime) order_datetime,
					prepare_datetime,
					is_delivery,
					false,
					admin.AdminId,
					"",
					false
				)
			);
			List<CustomerOrderRecord> records = DBWrapper.Instance.customer_order_table.get_records();
			int id = records[records.ToArray().Length - 1].primaryKey[0];
			orders.Add(new Order(id));
			return id;
		}
		public static OrderBook Instance{
			get{
				if (instance == null) instance = new OrderBook();
				return instance;
			}
		}
	}
}