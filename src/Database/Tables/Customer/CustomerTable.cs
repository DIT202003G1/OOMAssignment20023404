using MySql.Data.MySqlClient;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Exceptions;

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
						result.GetInt32("customer_id"),
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

		public bool exists(int[] pk_id) => check_exist_by_pk_name("customer_id",pk_id[0]);

		public CustomerRecord retrieve(int[] pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE customer_id = {pk_id[0]}")){
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
				else throw new CustomerException(CustomerException.exception_type.CUSTOMER_NOT_FOUND);
			}
		}

		public void update(CustomerRecord value){
			if (exists(value.primaryKey)){
				retrieve(value.primaryKey).firstName = value.firstName;
				retrieve(value.primaryKey).lastName = value.lastName;
				retrieve(value.primaryKey).Address = value.Address;
				retrieve(value.primaryKey).Telephone = value.Telephone;
				retrieve(value.primaryKey).establishDate = value.establishDate;
				retrieve(value.primaryKey).premiumeRegisterDate = value.premiumeRegisterDate;
				retrieve(value.primaryKey).premiumeEndDate = value.premiumeEndDate;
			}
			else{
				new_record(value);
			}
		}
		public void append_record(CustomerRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTupleDefaultPk}");
		}
		public void new_record(CustomerRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTuple}");
		}
	}
}