using System;

namespace SecretGarden.OrderSystem.Database.Tables.Item{
	class ItemRecord : Record, IRecord{
		private int rd_item_id;
		private ItemType rd_item_type;
		private string rd_item_name;
		private double rd_price;
		private int? rd_cake_size = null;
		public ItemRecord(
			DBTable table,
			int index,
			int item_id,
			string item_name,
			double price,
			ItemType item_type,
			int? cake_size
		):base(table, index){
			this.rd_item_id = item_id;
			this.rd_item_name = item_name;
			this.rd_price = price;
			this.rd_item_type = item_type;
			this.rd_cake_size = cake_size;
		}
		public int[] primaryKey{get=>new int[] {this.rd_item_id};}
		public string itemName{
			get=>this.rd_item_name;
			set{
				table_wrapper.update_field("item_name",value,DBWrapper.prepare_datatypes.STRING,$"item_id={primaryKey[0]}");
				this.rd_item_name = value;
			}
		}
		public ItemType itemType{
			get=>this.rd_item_type;
			set{
				table_wrapper.update_field("item_type",(int) value,DBWrapper.prepare_datatypes.NUMBER,$"item_id={primaryKey[0]}");
				this.rd_item_type = value;
			}
		}
		public int? cakeSize{
			get=>rd_cake_size;
			set{
				if (value == null)
					table_wrapper.update_field("item_type","NULL",DBWrapper.prepare_datatypes.NUMBER,$"item_id={primaryKey[0]}");
				else
					table_wrapper.update_field("item_type",value,DBWrapper.prepare_datatypes.NUMBER,$"item_id={primaryKey[0]}");
				this.rd_cake_size = value;
			}
		}
		public double Price{
			get=>this.rd_price;
			set{
				double new_value = Math.Round(value,2);
				table_wrapper.update_field("price",new_value,DBWrapper.prepare_datatypes.STRING,$"item_id={primaryKey[0]}");
				this.rd_price = value;
			}
		}
		public void remove_record(){
			DBWrapper.Instance.execute_only(
				$"DELETE FROM {table_wrapper.table_name} WHERE item_id = {this.primaryKey[0]}"
			);
		}
		public string sqlTupleDefaultPk{
			get{
				string cake_size = (rd_cake_size == null) ? "NULL" : rd_cake_size.ToString();
				return $"({this.rd_item_id}, '{this.rd_item_name}', {this.rd_price}, {(int) this.rd_item_type}, {cake_size})";
			}
		}
		public string sqlTuple{
			get{
				string cake_size = (rd_cake_size == null) ? "NULL" : rd_cake_size.ToString();
				return $"({this.rd_item_id}, '{this.rd_item_name}', {this.rd_price}, {(int) this.rd_item_type}, {cake_size})";
			}
		}
	}
}