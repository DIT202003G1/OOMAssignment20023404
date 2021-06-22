using SecretGarden.OrderSystem.Database;

namespace SecretGarden.OrderSystem.AppEntity
{
	public class OrderItem
	{
		private Item item;
		private int quantity;
		public OrderItem(Item item, int quantity){
			this.item = item;
			this.quantity = quantity;
		}
		private string remark;
		public Item Item{
			get=>item;
		}
		public int Quantity
		{
			get => quantity;
			set
			{
				DBWrapper.Instance.order_item_table.retrieve(new[] {item.Id}).Quantity = value;
				quantity = value;
			}
		}
		public string Remark
		{
			get => remark;
			set
			{
				DBWrapper.Instance.order_item_table.retrieve(new[] {item.Id}).Customization = value;
				remark = value;
			}
		}
	}
}
