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
						result.GetInt32("admin_id"),
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
				retrave(value.primaryKey).firstName = value.firstName;
				retrave(value.primaryKey).lastName = value.lastName;
				retrave(value.primaryKey).passwordSalt = value.passwordSalt;
				retrave(value.primaryKey).passwordHash = value.passwordHash;
				retrave(value.primaryKey).establishDate = value.establishDate;
			}
			else{
				new_record(value);
			}
		}
		public bool exists(int pk_id) => check_exist_by_pk_name("admin_id",pk_id);
		public void new_record(AdminAccountRecord record){
			DBWrapper.Instance.execute_only($"INSERT INTO {this.table_name} VALUES {record.sqlTuple}");
		}
	}
}