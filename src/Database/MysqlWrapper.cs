using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

// THIS CLASS IS A SINGLETON

namespace SecretGarden.OrderSystem.Database{
	class MysqlWrapper{
		private string username, password, dbname, hostname;
		private static MysqlWrapper instance = null;
		private MySqlConnection sql_connection = null;
		private MysqlWrapper(){}
		private string connectionCs{
		// public string connection_cs{
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
		public static MysqlWrapper Instance{
			get{
				if (MysqlWrapper.instance == null) MysqlWrapper.instance = new MysqlWrapper();
				return MysqlWrapper.instance;
			}
		}
		public void connect(string hostname, string username, string password, string dbname){
			this.hostname = hostname;
			this.username = username;
			this.password = password;
			this.dbname = dbname;
			this.sql_connection = new MySqlConnection(this.connection_cs);
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