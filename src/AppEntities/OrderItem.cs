using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Exceptions;

namespace SecretGarden.OrderSystem.AppEntities{
	class OrderItem{
		private Order order;
		private int item_id, quantity;
		private OrderItem(Order order, int item_id, int quantity = 0){
			this.order = order;
			this.quantity = quantity;
		}
		public static OrderItem retrieve(Order order, int item_id){
			if (!DBWrapper.Instance.order_item_table.exists(new int[] {order.orderID, item_id})){
				return new OrderItem(order,item_id);
			}
			return new OrderItem(
				order,
				item_id,
				DBWrapper.Instance.order_item_table.retrieve(new int[] {order.orderID,item_id}).Quantity
			);
		}
		public string itemName{
			get=>DBWrapper.Instance.item_table.retrieve(new int[] {item_id}).itemName;
		}
		public int Quantity{
			get=>DBWrapper.Instance.order_item_table.retrieve(new int[] {order.orderID, item_id}).Quantity;
			set{
				DBWrapper.Instance.order_item_table.retrieve(new int[] {order.orderID, item_id}).Quantity = value;
			}
		}
		public void remove_item(){
			DBWrapper.Instance.order_item_table.retrieve(new int[] {order.orderID, item_id}).remove_record();
		}
	}
}
