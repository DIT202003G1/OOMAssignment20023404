using SecretGarden.OrderSystem.Database;

namespace SecretGarden.OrderSystem.AppEntities
{
	public class OrderItem
	{
		private Item item;
		private int quantity;
		private string remark;
		public OrderItem(Item item, int quantity, string remark){
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
