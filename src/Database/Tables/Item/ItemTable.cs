using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

using SecretGarden.OrderSystem.Exceptions;

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
						Math.Round(result.GetDouble("price"), 2),
						(ItemType) result.GetInt16("item_type"),
						result.IsDBNull(4) ? null : result.GetInt16("cake_size")
					));
					index ++;
				}
				return records;
			}
		}
		public ItemRecord retrieve(int[] pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE item_id = {pk_id[0]}")){
				if (result.Read()){
					return new ItemRecord(
						this,
						0,
						result.GetInt32("item_id"),
						result.GetString("item_name"),
						Math.Round(result.GetDouble("price"), 2),
						(ItemType) result.GetInt16("item_type"),
						result.IsDBNull(4) ? null : result.GetInt16("cake_size")
					);
				}
				else throw new ItemException(ItemException.exception_type.ITEM_NOT_FOUND);
			}
		}
		public void update(ItemRecord value){
			if (exists(value.primaryKey)){
				retrieve(value.primaryKey).itemName = value.itemName;
				retrieve(value.primaryKey).Price = value.Price;
			}
			else{
				new_record(value);
			}
		}
		public void append_record(ItemRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTupleDefaultPk}");
		}
		public void new_record(ItemRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTuple}");
		}
	}

}