using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.Database.Tables.AdminAccount{
	class AdminAccountRecord : Record, IRecord{
		private int rd_admin_id;
		private string rd_first_name, rd_last_name, rd_password_salt, rd_password_hash;
		private Datetime rd_establish_date;
		public AdminAccountRecord(
			DBTable table,
			int index,
			int admin_id, 
			string first_name,
			string last_name,
			string password_salt,
			string password_hash,
			Datetime establish_date
		):base(table, index){
			this.rd_admin_id = admin_id;
			this.rd_first_name = first_name;
			this.rd_last_name = last_name;
			this.rd_password_salt = password_salt;
			this.rd_password_hash = password_hash;
			this.rd_establish_date = establish_date;
		}
		public int[] primaryKey{get=>new int[] {rd_admin_id,};}
		public string firstName{
			get=>rd_first_name;
			set{
				table_wrapper.update_field("first_name",value,DBWrapper.prepare_datatypes.STRING,$"admin_id={primaryKey[0]}");
				this.rd_first_name = value;
			}
		}
		public string lastName{
			get=>rd_last_name;
			set{
				table_wrapper.update_field("last_name",value,DBWrapper.prepare_datatypes.STRING,$"admin_id={primaryKey[0]}");
				this.rd_last_name = value;
			}
		}
		public string passwordSalt{
			get=>rd_password_salt;
			set{
				table_wrapper.update_field("password_salt",value,DBWrapper.prepare_datatypes.STRING,$"admin_id={primaryKey[0]}");
				this.rd_password_salt = value;
			}
		}
		public string passwordHash{
			get=>rd_password_hash;
			set{
				table_wrapper.update_field("password_hash",value,DBWrapper.prepare_datatypes.STRING,$"admin_id={primaryKey[0]}");
				this.rd_password_hash = value;
			}
		}
		public Datetime establishDate{
			get=>rd_establish_date;
			set{
				table_wrapper.update_field("establish_date",value.sqlFormatDate,DBWrapper.prepare_datatypes.STRING,$"admin_id={primaryKey[0]}");
				this.rd_establish_date = value;
			}
		}
		public string sqlTuple{
			get{return $"({this.rd_admin_id}, '{this.rd_first_name}', '{this.rd_last_name}', '{this.rd_password_salt}', '{this.rd_password_hash}', '{this.rd_establish_date.sqlFormatDate}')";}
		}
		public string sqlTupleDefaultPk{
			get{return $"(DEFAULT, '{this.rd_first_name}', '{this.rd_last_name}', '{this.rd_password_salt}', '{this.rd_password_hash}', '{this.rd_establish_date.sqlFormatDate}')";}
		}
		public void remove_record(){
			DBWrapper.Instance.execute_only(
				$"DELETE FROM admin_account WHERE admin_id = {this.primaryKey[0]}"
			);
		}
	}
}