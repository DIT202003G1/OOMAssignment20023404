using System.Collections.Generic;
using MySql.Data.MySqlClient;

using SecretGarden.OrderSystem.Database.Tables.AdminAccount;
using SecretGarden.OrderSystem.Database.Tables.Customer;

// THIS CLASS IS A SINGLETON

namespace SecretGarden.OrderSystem.Database{
	class DBWrapper{
		private string username, password, dbname, hostname;
		private static DBWrapper instance = null;
		private MySqlConnection sql_connection = null;
		public AdminAccountTable admin_account_table = new AdminAccountTable();
		public CustomerTable customer_table = new CustomerTable();
		private DBWrapper(){

		}
		public enum prepare_datatypes{STRING, NUMBER, DATE, DATETIME} 
		private string connectionCs{
			get{
				return $"server={this.hostname};userid={this.username};password={this.password};database={this.dbname}";
			}
		}
		public List<string> tableNames{
			get{
				MySqlDataReader sqlreuslt = this.execute_with_result("show tables;");
				List<string> tables = new List<string>();
				while (sqlreuslt.Read()){
					tables.Add(sqlreuslt.GetString(0));
				}
				sqlreuslt.Close();
				return tables;
			}
		}
		private void cleanup(){
			this.hostname = null;
			this.username = null;
			this.password = null;
			this.dbname = null;
		}
		public static DBWrapper Instance{
			get{
				if (DBWrapper.instance == null) DBWrapper.instance = new DBWrapper();
				return DBWrapper.instance;
			}
		}
		public void connect(string hostname, string username, string password, string dbname){
			this.hostname = hostname;
			this.username = username;
			this.password = password;
			this.dbname = dbname;
			this.sql_connection = new MySqlConnection(this.connectionCs);
			this.sql_connection.Open();
		}
		public void execute_only(string sql_statement){
			using (MySqlCommand c = new MySqlCommand(sql_statement, this.sql_connection)){
				c.ExecuteNonQuery();
			}
		}
		public MySqlDataReader execute_with_result(string sql_statement){
			using (MySqlCommand c = new MySqlCommand(sql_statement, this.sql_connection)){
				MySqlDataReader rdr = c.ExecuteReader();
				return rdr;
			}
		}
		public void close(){
			this.sql_connection.Close();
			this.sql_connection = null;
		}
	}
}