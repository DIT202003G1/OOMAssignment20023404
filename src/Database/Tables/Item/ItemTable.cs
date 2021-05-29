using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace SecretGarden.OrderSystem.Database.Tables.Item{
	class ItemTable : DBTable, IDBTable<ItemRecord>{
		public ItemTable():base("item"){}
		public bool exists(int[] pk_id) => check_exist_by_pk_name("item_id",pk_id[0]);
		public List<ItemRecord> get_records(){
			List<ItemRecord> records = new List<ItemRecord>();
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name}")){
				int index = 0;
				while (result.Read()){ 
					records.Add(new ItemRecord(
						this,
						index,
						result.GetInt32("item_id"),
						result.GetString("item_name"),
						Math.Round(result.GetDouble("price"), 2)
					));
					index ++;
				}
				return records;
			}
		}
		public ItemRecord retrave(int[] pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE item_id = {pk_id[0]}")){
				if (result.Read()){
					return new ItemRecord(
						this,
						0,
						result.GetInt32("item_id"),
						result.GetString("item_name"),
						Math.Round(result.GetDouble("price"), 2)
					);
				}
				else return null;
			}
		}
		public void update(ItemRecord value){
			if (exists(value.primaryKey)){
				retrave(value.primaryKey).itemName = value.itemName;
				retrave(value.primaryKey).Price = value.Price;
			}
			else{
				new_record(value);
			}
		}
		public void new_record(ItemRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTuple}");
		}
	}

}