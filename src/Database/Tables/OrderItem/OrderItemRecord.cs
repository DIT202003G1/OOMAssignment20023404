namespace SecretGarden.OrderSystem.Database.Tables.OrderItem{
	class OrderItemRecord : Record, IRecord{
		int rd_order_id, rd_item_id, rd_quantity;
		string rd_customization = "";
		public OrderItemRecord(
			DBTable table,
			int index,
			int order_id,
			int item_id,
			int quantity,
			string customization
		):base(table, index){
			this.rd_order_id = order_id;
			this.rd_item_id = item_id;
			this.rd_quantity = quantity;
			this.rd_customization = customization;
		}
		public int[] primaryKey{get=>new int[] {rd_order_id, rd_item_id};}
		public int Quantity{
			get=>rd_quantity;
			set{
				table_wrapper.update_field("quantity",value,DBWrapper.prepare_datatypes.NUMBER,$"order_id={this.primaryKey[0]} AND item_id={this.primaryKey[1]}");
				this.rd_quantity = value;
			}
		}
		public string Customization{
			get=>rd_customization;
			set{
				table_wrapper.update_field("customization",value,DBWrapper.prepare_datatypes.STRING,$"order_id={this.primaryKey[0]} AND item_id={this.primaryKey[1]}");
			}
		}
		public void remove_record(){
			DBWrapper.Instance.execute_only(
				$"DELETE FROM {table_wrapper.table_name} WHERE order_id = {this.primaryKey[0]} AND item_id = {this.primaryKey[1]}"
			);
		}
		public string sqlTupleDefaultPk{
			get=>sqlTuple;
		}
		public string sqlTuple{
			get{
				return $"({this.rd_order_id}, '{this.rd_item_id}', {this.rd_quantity}, '{this.rd_customization}')";
			}
		}
	}
}