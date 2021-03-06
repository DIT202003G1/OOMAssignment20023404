using MySql.Data.MySqlClient;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Exceptions;

namespace SecretGarden.OrderSystem.Database.Tables.CustomerOrder{
	class CustomerOrderTable : DBTable, IDBTable<CustomerOrderRecord>{
		public CustomerOrderTable():base("customer_order"){}
	
		public List<CustomerOrderRecord> get_records(){
			List<CustomerOrderRecord> records = new List<CustomerOrderRecord>();
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name}")){
				int index = 0;
				while (result.Read()){ 
					records.Add(new CustomerOrderRecord(
						this,
						index,
						result.GetInt32("order_id"),
						result.GetInt32("customer_id"),
						(Datetime) result.GetDateTime("order_datetime"),
						(Datetime) result.GetDateTime("prepare_datetime"),
						(result.GetInt16("is_delivery") == 1) ? true : false,
						(result.GetInt16("completed") == 1) ? true : false,
						result.GetInt32("admin_id"),
						result.GetString("payment_method"),
						(result.GetInt16("paid") == 1) ? true : false
					));
					index ++;
				}
				return records;
			}
		}

		public bool exists(int[] pk_id) => check_exist_by_pk_name("order_id",pk_id[0]);
		public CustomerOrderRecord retrieve(int[] pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE order_id = {pk_id[0]}")){
				if (result.Read()){
					return new CustomerOrderRecord(
						this,
						0,
						result.GetInt32("order_id"),
						result.GetInt32("customer_id"),
						(Datetime) result.GetDateTime("order_datetime"),
						(Datetime) result.GetDateTime("prepare_datetime"),
						(result.GetInt16("is_delivery") == 1) ? true : false,
						(result.GetInt16("completed") == 1) ? true : false,
						result.GetInt32("admin_id"),
						result.GetString("payment_method"),
						(result.GetInt16("paid") == 1) ? true : false
					);
				}
				else throw new OrderException(OrderException.exception_type.ORDER_NOT_FOUND);
			}
		}
		public void update(CustomerOrderRecord value){
			if (exists(value.primaryKey)){
				retrieve(value.primaryKey).customerID = value.customerID;
				retrieve(value.primaryKey).orderDatetime = value.orderDatetime;
				retrieve(value.primaryKey).prepareDatetime = value.prepareDatetime;
				retrieve(value.primaryKey).isDelivery = value.isDelivery;
				retrieve(value.primaryKey).adminId = value.adminId;
			}
			else{
				new_record(value);
			}
		}
		public void append_record(CustomerOrderRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTupleDefaultPk}");
		}
		public void new_record(CustomerOrderRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTuple}");
		}
	}
}