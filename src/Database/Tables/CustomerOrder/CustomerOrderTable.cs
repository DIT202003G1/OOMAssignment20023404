using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;

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
						result.GetInt32("admin_id")
					));
					index ++;
				}
				return records;
			}
		}

		public bool exists(int pk_id) => check_exist_by_pk_name("order_id",pk_id);
		public CustomerOrderRecord retrave(int pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE order_id = {pk_id}")){
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
						result.GetInt32("admin_id")
					);
				}
				else return null;
			}
		}
		public void update(CustomerOrderRecord value){
			if (exists(value.primaryKey)){
				retrave(value.primaryKey).customerID = value.customerID;
				retrave(value.primaryKey).orderDatetime = value.orderDatetime;
				retrave(value.primaryKey).prepareDatetime = value.prepareDatetime;
				retrave(value.primaryKey).isDelivery = value.isDelivery;
				retrave(value.primaryKey).adminId = value.adminId;
			}
			else{
				new_record(value);
			}
		}

		public void new_record(CustomerOrderRecord record){
			Console.WriteLine($"INSERT INTO {this.table_name} VALUES {record.sqlTulpe}");
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTulpe}");
		}
	}
}