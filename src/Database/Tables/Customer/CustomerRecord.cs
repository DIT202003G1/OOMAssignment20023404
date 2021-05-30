using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.Database.Tables.Customer{
	class CustomerRecord : Record, IRecord{
		private int rd_customer_id;
		private string rd_first_name, rd_last_name, rd_address, rd_telephone;
		private Datetime rd_establish_date, rd_premium_register_date, rd_premium_end_date;
		public CustomerRecord(
			DBTable table,
			int index,
			int customer_id, 
			string first_name,
			string last_name,
			string address,
			string telephone,
			Datetime establish_date,
			Datetime premium_register_date = null,
			Datetime premium_end_date = null
		):base(table, index){
			this.rd_customer_id = customer_id;
			this.rd_first_name = first_name;
			this.rd_last_name = last_name;
			this.rd_address = address;
			this.rd_telephone = telephone;
			this.rd_establish_date = establish_date;
			this.rd_premium_register_date = premium_register_date;
			this.rd_premium_end_date = premium_end_date;
		}
		public int[] primaryKey{get=>new int[] {rd_customer_id};}
		public string firstName{
			get=>rd_first_name;
			set{
				table_wrapper.update_field("first_name",value,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_first_name = value;
			}
		}
		public string lastName{
			get=>rd_last_name;
			set{
				table_wrapper.update_field("last_name",value,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_last_name = value;
			}
		}
		public string Address{
			get=>rd_address;
			set{
				table_wrapper.update_field("address",value,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_address = value;
			}
		}
		public string Telephone{
			get=>rd_telephone;
			set{
				table_wrapper.update_field("telephone",value,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_telephone = value;
			}
		}
		public Datetime establishDate{
			get=>rd_establish_date;
			set{
				table_wrapper.update_field("establish_date",value.sqlFormatDate,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_establish_date = value;
			}
		}
		public Datetime premiumeRegisterDate{
			get=>rd_premium_register_date;
			set{
				if (value == null) table_wrapper.update_field("premium_register_date","null",DBWrapper.prepare_datatypes.NUMBER,$"customer_id={primaryKey[0]}");
				else table_wrapper.update_field("premium_register_date",value.sqlFormatDate,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_premium_register_date = value;
			}
		}
		public Datetime premiumeEndDate{
			get=>rd_premium_end_date;
			set{
				if (value == null) table_wrapper.update_field("premium_end_date","null",DBWrapper.prepare_datatypes.NUMBER,$"customer_id={primaryKey[0]}");
				else table_wrapper.update_field("premium_end_date",value.sqlFormatDate,DBWrapper.prepare_datatypes.STRING,$"customer_id={primaryKey[0]}");
				this.rd_premium_end_date = value;
			}
		}
		public string sqlTuple{
			get{
				string register_date = (this.rd_premium_register_date == null) ? "null" : "'" + this.rd_premium_register_date.sqlFormatDate + "'";
				string end_date = (this.rd_premium_end_date == null) ? "null" : "'" + this.rd_premium_end_date.sqlFormatDate + "'";
				return $"({this.rd_customer_id}, '{this.rd_first_name}', '{this.rd_last_name}', '{this.rd_address}', '{this.rd_telephone}', '{this.rd_establish_date.sqlFormatDate}', {register_date}, {end_date})";
			}
		}
		public string sqlTupleDefaultPk{
			get{
				string register_date = (this.rd_premium_register_date == null) ? "null" : "'" + this.rd_premium_register_date.sqlFormatDate + "'";
				string end_date = (this.rd_premium_end_date == null) ? "null" : "'" + this.rd_premium_end_date.sqlFormatDate + "'";
				return $"(Default, '{this.rd_first_name}', '{this.rd_last_name}', '{this.rd_address}', '{this.rd_telephone}', '{this.rd_establish_date.sqlFormatDate}', {register_date}, {end_date})";
			}
		}
		public void remove_record(){
			DBWrapper.Instance.execute_only(
				$"DELETE FROM {table_wrapper.table_name} WHERE customer_id = {this.primaryKey[0]}"
			);
		}
	}
}