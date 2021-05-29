using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Database.Tables.OrderItem{
	class OrderItemTable : DBTable, IDBTable<OrderItemRecord>{
		public OrderItemTable():base("order_item"){}
		public List<OrderItemRecord> get_records(){
			List<OrderItemRecord> records = new List<OrderItemRecord>();
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name}")){
				int index = 0;
				while (result.Read()){ 
					records.Add(new OrderItemRecord(
						this,
						index,
						result.GetInt32("order_id"),
						result.GetInt32("item_id"),
						result.GetInt32("quantity")
					));
					index ++;
				}
				return records;
			}
		}

		public bool exists(int[] pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {table_name} WHERE order_id={pk_id[0]} AND item_id={pk_id[1]};")){
				return result.Read();
			}
		}
		public OrderItemRecord retrave(int[] pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE item_id = {pk_id[0]}")){
				if (result.Read()){
					return new OrderItemRecord(
						this,
						0,
						result.GetInt32("order_id"),
						result.GetInt32("item_id"),
						result.GetInt32("quantity")
					);
				}
				else return null;
			}
		}
		public void new_record(OrderItemRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTuple}");
		}
		public void update(OrderItemRecord value){
			if (exists(value.primaryKey)){
				retrave(value.primaryKey).Quantity = value.Quantity;
			}
			else{
				new_record(value);
			}
		}

	}
	
}