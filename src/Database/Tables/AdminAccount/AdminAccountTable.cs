using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.Database.Tables.AdminAccount{
	class AdminAccountTable : DBTable, IDBTable<AdminAccountRecord>{
		public AdminAccountTable():base("admin_account"){}
		public List<AdminAccountRecord> get_records(){
			List<AdminAccountRecord> records = new List<AdminAccountRecord>();
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name}")){
				int index = 0;
				while (result.Read()){ 
					records.Add(new AdminAccountRecord(
						this,
						index,
						result.GetInt16("admin_id"),
						result.GetString("first_name"),
						result.GetString("last_name"),
						result.GetString("password_salt"),
						result.GetString("password_hash"),
						(Datetime) result.GetDateTime("establish_date")
					));
					index ++;
				}
				return records;
			}
		}
		public AdminAccountRecord retrave(int pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {this.table_name} WHERE admin_id = {pk_id}")){
				if (result.Read()){
					return new AdminAccountRecord(
						this,
						0,
						result.GetInt32("admin_id"),
						result.GetString("first_name"),
						result.GetString("last_name"),
						result.GetString("password_salt"),
						result.GetString("password_hash"),
						(Datetime) result.GetDateTime("establish_date")
					);
				}
				else return null;
			};
		}
		public void update(AdminAccountRecord value){
			if (exists(value.primaryKey)){
				update_field("admin_id",value.primaryKey,DBWrapper.prepare_datatypes.NUMBER,$"admin_id={value.primaryKey}");
				update_field("first_name",value.firstName,DBWrapper.prepare_datatypes.STRING,$"admin_id={value.primaryKey}");
				update_field("last_name",value.lastName,DBWrapper.prepare_datatypes.STRING,$"admin_id={value.primaryKey}");
				update_field("password_salt",value.passwordSalt,DBWrapper.prepare_datatypes.STRING,$"admin_id={value.primaryKey}");
				update_field("password_hash",value.passwordSalt,DBWrapper.prepare_datatypes.STRING,$"admin_id={value.primaryKey}");
				update_field("establish_date",value.establishDate.sqlFormatDate,DBWrapper.prepare_datatypes.STRING,$"admin_id={value.primaryKey}");
			}
			else{
				new_record(value);
			}
		}
		public bool exists(int pk_id) => check_exist_by_pk_name("admin_id",pk_id);
		public void new_record(AdminAccountRecord record){
			Console.WriteLine($"INSERT INTO {this.table_name} VALUES {record.sqlTulpe}");
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTulpe}");
		}
	}
}