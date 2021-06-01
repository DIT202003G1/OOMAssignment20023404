using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Exceptions;

namespace SecretGarden.OrderSystem.AppEntities{
	class OrderItem{
		Order order;
		private OrderItem(int order_id, int item_id, int quantity = 0){}
		private static OrderItem retrieve(int order_id, int item_id){
			if (DBWrapper.Instance.order_item_table.exists(new int[] {order_id, item_id})){
				return new OrderItem(order_id,item_id);
			}
			return new OrderItem(
				order_id,
				item_id,
				DBWrapper.Instance.order_item_table.retrieve(new int[] {order_id,item_id}).Quantity
			);
		}
	}
}
