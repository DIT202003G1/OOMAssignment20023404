using System;

using SecretGarden.OrderSystem.Database;

namespace SecretGarden.OrderSystem.AppEntities{
	public class Bill{
		private static readonly double DISCOUNT_FACTOR = 0.95;
		private string payment_method;
		private bool paid;
		private Order order;
		public Bill(Order order){
			this.order = order;
			this.paid = false;
			this.payment_method = "";
		}
		public string paymentMethod{
			get=>payment_method;
			set{
				DBWrapper.Instance.customer_order_table.retrieve(new int[] {order.Id}).paymentMethod = value;
				payment_method = value;
			}
		}
		public bool isPaid{
			get=>paid;
			set{
				DBWrapper.Instance.customer_order_table.retrieve(new int[] {order.Id}).Paid = value;
				paid = value;
			}
		}
		public double Price{
			get{
				double price = 0;
				foreach (OrderItem i in order.Items)
					price += i.Item.Price;
				if (order.Customer.is_premium()) return Math.Round(price*DISCOUNT_FACTOR,2);
				return Math.Round(price, 2);
			}
		}
	}
}