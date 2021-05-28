using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.Database.Tables.Customer{
	class CustomerTable : DBTable, IDBTable<CustomerRecord>{
		public CustomerTable():base("customer"){}
		public List<CustomerRecord> get_records(){
			List<CustomerRecord> records = new List<CustomerRecord>();
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name}")){
				int index = 0;
				while (result.Read()){ 
					records.Add(new CustomerRecord(
						this,
						index,
						result.GetInt16("customer_id"),
						result.GetString("first_name"),
						result.GetString("last_name"),
						result.GetString("address"),
						result.GetString("telephone"),
						(Datetime) result.GetDateTime("establish_date"),
						result.IsDBNull(6) ? null : (Datetime) result.GetDateTime("premium_register_date"),
						result.IsDBNull(7) ? null : (Datetime) result.GetDateTime("premium_end_date")
					));
					index ++;
				}
				return records;
			}
		}

		public bool exists(int pk_id) => check_exist_by_pk_name("customer_id",pk_id);

		public CustomerRecord retrave(int pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE customer_id = {pk_id}")){
				if (result.Read()){
					return new CustomerRecord(
						this,
						0,
						result.GetInt16("customer_id"),
						result.GetString("first_name"),
						result.GetString("last_name"),
						result.GetString("address"),
						result.GetString("telephone"),
						(Datetime) result.GetDateTime("establish_date"),
						result.IsDBNull(6) ? null : (Datetime) result.GetDateTime("premium_register_date"),
						result.IsDBNull(7) ? null : (Datetime) result.GetDateTime("premium_end_date")
					);
				}
				else return null;
			}
		}

		public void update(CustomerRecord value){
			if (exists(value.primaryKey)){
				retrave(value.primaryKey).firstName = value.firstName;
				retrave(value.primaryKey).lastName = value.lastName;
				retrave(value.primaryKey).Address = value.Address;
				retrave(value.primaryKey).Telephone = value.Telephone;
				retrave(value.primaryKey).establishDate = value.establishDate;
				retrave(value.primaryKey).premiumeRegisterDate = value.premiumeRegisterDate;
				retrave(value.primaryKey).premiumeEndDate = value.premiumeEndDate;
			}
			else{
				new_record(value);
			}
		}

		public void new_record(CustomerRecord record){
			Console.WriteLine($"INSERT INTO {this.table_name} VALUES {record.sqlTulpe}");
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTulpe}");
		}
	}
}