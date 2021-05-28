using System;
using MySql.Data.MySqlClient;

// THIS CLASS IS A SINGLETON

namespace SecretGarden.OrderSystem.Database{
	abstract class DBTable{
		public string table_name;
		public DBTable(string table_name){
			this.table_name = table_name;
		}
		public void update_field(string field_name, dynamic value, DBWrapper.prepare_datatypes datatype, string conditions){
			string sql_statement;
			switch (datatype){
				case DBWrapper.prepare_datatypes.STRING:
					sql_statement = $"UPDATE {this.table_name} SET {field_name}='{value}' WHERE {conditions}";
				break;
				case DBWrapper.prepare_datatypes.NUMBER:
					sql_statement = $"UPDATE {this.table_name} SET {field_name}={value} WHERE {conditions}";
				break;
				case DBWrapper.prepare_datatypes.DATETIME:
					sql_statement = $"UPDATE {this.table_name} SET {field_name}={value.sqlFormat} WHERE {conditions}";
				break;
				case DBWrapper.prepare_datatypes.DATE:
					sql_statement = $"UPDATE {this.table_name} SET {field_name}={value.sqlFormatDate} WHERE {conditions}";
				break;
				default:
					throw new Exception("Unsupported datatype for the mysql field is being used.");
			}
			DBWrapper.Instance.execute_only(sql_statement);
		}
		protected bool check_exist_by_pk_name(string pk_name, int pk_id){
			using (MySqlDataReader result = DBWrapper.Instance.execute_with_result($"SELECT * FROM {table_name} WHERE {pk_name}={pk_id};")){
				return result.Read();
			}
		}
	}
}