using SecretGarden.OrderSystem.Database;

namespace SecretGarden.OrderSystem.AppEntities
{
	public class OrderItem
	{
		private Item item;
		private int quantity;
		private string remark;

		private Order order;
		public OrderItem(Item item, Order order, int quantity, string remark){
			this.order = order;
			this.item = item;
			this.quantity = quantity;
			this.remark = remark;
		}
		public Item Item{
			get=>item;
		}
		public int Quantity
		{
			get => quantity;
			set
			{
				DBWrapper.Instance.order_item_table.retrieve(new[] {order.Id ,item.Id}).Quantity = value;
				quantity = value;
			}
		}
		public string Remark
		{
			get => remark;
			set
			{
				DBWrapper.Instance.order_item_table.retrieve(new[] {order.Id, item.Id}).Customization = value;
				remark = value;
			}
		}

		public Order Order{
			get=>order;
		}
	}
}
