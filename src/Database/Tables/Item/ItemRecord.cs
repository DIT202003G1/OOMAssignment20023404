using System;

namespace SecretGarden.OrderSystem.Database.Tables.Item{
	class ItemRecord : Record, IRecord{
		private int rd_item_id;
		private string rd_item_name;
		private double rd_price;
		public ItemRecord(
			DBTable table,
			int index,
			int item_id,
			string item_name,
			double price
		):base(table, index){
			this.rd_item_id = item_id;
			this.rd_item_name = item_name;
			this.rd_price = price;
		}
		public int[] primaryKey{get=>new int[] {this.rd_item_id};}
		public string itemName{
			get=>this.rd_item_name;
			set{
				table_wrapper.update_field("item_name",value,DBWrapper.prepare_datatypes.STRING,$"item_id={primaryKey[0]}");
				this.rd_item_name = value;
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
				return $"({this.rd_item_id}, '{this.rd_item_name}', {this.rd_price})";
			}
		}
		public string sqlTuple{
			get{
				return $"({this.rd_item_id}, '{this.rd_item_name}', {this.rd_price})";
			}
		}
	}
}