using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Exceptions;
using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.CustomerOrder;

namespace SecretGarden.OrderSystem.AppEntities{
	class Order{
		private Admin admin;
		private Customer customer;
		private int id;
		private Order(int id, Admin admin, Customer customer){
			this.admin = admin;
			this.customer = customer;
			this.id = id;
		}
		public static int new_order(Admin admin, Customer customer, bool is_delivery, Datetime prepare_time = null){
			DBWrapper.Instance.customer_order_table.append_record(
				new CustomerOrderRecord(
					DBWrapper.Instance.customer_order_table,
					0,
					0,
					customer.Id,
					(Datetime) DateTime.Now,
					(prepare_time == null) ? 
						((Datetime) DateTime.Now) + new int[] {0,0,0,0,30,0} :
						prepare_time,
					is_delivery,
					false,
					admin.Id
				)
			);
			List<CustomerOrderRecord> r = DBWrapper.Instance.customer_order_table.get_records();
			return r[r.Count - 1].primaryKey[0];
		}
		public static Order retrieve(int id){
			if (!DBWrapper.Instance.customer_order_table.exists(new int[] {id})){
				throw new OrderException(OrderException.exception_type.ORDER_NOT_FOUND);
			}
			int admin_id = DBWrapper.Instance.customer_order_table.retrieve(new int[] {id}).adminId;
			int customer_id = DBWrapper.Instance.customer_order_table.retrieve(new int[] {id}).customerID;
			return new Order(
				id,
				Admin.retrieve(admin_id),
				Customer.retrieve(customer_id)
			);
		}
	}
}