using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.Database.Tables.CustomerOrder{
	class CustomerOrderRecord : Record, IRecord{
		private int rd_order_id, rd_customer_id, customer_id, rd_admin_id;
		private Datetime rd_order_datetime, rd_prepare_datetime;
		private bool rd_is_delivery, rd_completed, rd_paid;
		private string rd_payment_method;
		public CustomerOrderRecord(
			DBTable table,
			int index,
			int order_id, 
			int customer_id, 
			Datetime order_datetime,
			Datetime prepare_datetime,
			bool is_delivery,
			bool completed,
			int admin_id,
			string payment_method,
			bool paid
		):base(table, index){
			this.rd_order_id = order_id;
			this.rd_customer_id = customer_id;
			this.rd_order_datetime = order_datetime;
			this.rd_prepare_datetime = prepare_datetime;
			this.rd_is_delivery = is_delivery;
			this.rd_completed = completed;
			this.rd_admin_id = admin_id;
			this.rd_payment_method = payment_method;
			this.rd_paid = paid;
		}
		public int[] primaryKey{get=>new int[] {rd_order_id};}
		public int customerID{
			get=>rd_customer_id;
			set{
				table_wrapper.update_field("customer_id",value,DBWrapper.prepare_datatypes.NUMBER,$"order_id={primaryKey[0]}");
				this.rd_customer_id = value;
			}
		}
		public int adminId{
			get=>rd_admin_id;
			set{
				table_wrapper.update_field("admin_id",value,DBWrapper.prepare_datatypes.NUMBER,$"order_id={primaryKey[0]}");
				this.rd_admin_id = value;
			}
		}
		public Datetime orderDatetime{
			get=>rd_order_datetime;
			set{
				table_wrapper.update_field("order_datetime",value.sqlFormat,DBWrapper.prepare_datatypes.STRING,$"order_id={primaryKey[0]}");
				this.rd_order_datetime = value;
			}
		}
		public Datetime prepareDatetime{
			get=>rd_prepare_datetime;
			set{
				table_wrapper.update_field("prepare_datetime",value.sqlFormat,DBWrapper.prepare_datatypes.STRING,$"order_id={primaryKey[0]}");
				this.rd_prepare_datetime = value;
			}
		}
		public bool isDelivery{
			get=>rd_is_delivery;
			set{
				table_wrapper.update_field("is_delivery",value?1:0,DBWrapper.prepare_datatypes.NUMBER,$"order_id={primaryKey[0]}");
				this.rd_is_delivery = value;
			}
		}
		public bool Completed{
			get=>rd_completed;
			set{
				table_wrapper.update_field("completed",value?1:0,DBWrapper.prepare_datatypes.NUMBER,$"order_id={primaryKey[0]}");
				this.rd_completed = value;
			}
		}
		public string paymentMethod{
			get=>this.rd_payment_method;
			set{
				table_wrapper.update_field("payment_method",value,DBWrapper.prepare_datatypes.STRING,$"order_id={primaryKey[0]}");
				this.rd_payment_method = value;
			}
		}
		public bool Paid{
			get=>this.rd_paid;
			set{
				table_wrapper.update_field("paid",value?1:0,DBWrapper.prepare_datatypes.NUMBER,$"order_id={primaryKey[0]}");
				this.rd_completed = value;
			}
		}
		public string sqlTuple{
			get{
				int completed = this.rd_completed?1:0;
				int is_delivery = this.rd_completed?1:0;
				int paid = this.rd_paid?1:0;
				return $"({this.rd_order_id}, {this.rd_customer_id}, '{this.rd_order_datetime.sqlFormat}', '{this.rd_prepare_datetime.sqlFormat}', {completed}, {is_delivery}, {this.rd_admin_id}, '{this.rd_payment_method}', {paid})";
			}
		}
		public string sqlTupleDefaultPk{
			get{
				int completed = this.rd_completed?1:0;
				int is_delivery = this.rd_completed?1:0;
				int paid = this.rd_paid?1:0;
				return $"(DEFAULT, {this.rd_customer_id}, '{this.rd_order_datetime.sqlFormat}', '{this.rd_prepare_datetime.sqlFormat}', {completed}, {is_delivery}, {this.rd_admin_id}, '{this.rd_payment_method}', {paid})";
			}
		}
		public void remove_record(){
			DBWrapper.Instance.execute_only(
				$"DELETE FROM {table_wrapper.table_name} WHERE order_id = {this.primaryKey[0]}"
			);
		}

	}
}