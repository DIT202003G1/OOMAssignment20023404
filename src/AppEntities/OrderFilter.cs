using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.AppEntities
{
	public struct OrderFilter{
		private int? order_id;
		private Datetime prepare_datetime_from;
		private Datetime prepare_datetime_until;
		private Datetime order_datetime_from;
		private Datetime order_datetime_until;
		private Customer customer;
		private Admin admin;
		private bool? is_delivery;
		private bool? completed;
		private double? price_above;
		private double? price_below;
		public int? orderID{get=>order_id;}
		public Datetime prepareDatetimeFrom{get=>prepare_datetime_from;}
		public Datetime prepareDatetimeUntil{get=>prepare_datetime_until;}
		public Datetime orderDatetimeFrom{get=>order_datetime_from;}
		public Datetime orderDatetimeUntil{get=>order_datetime_until;}
		public Customer Customer{get=>customer;}
		public Admin Admin{get=>admin;}
		public bool? isDelivery{get=>is_delivery;}
		public bool? isCompleted{get=>completed;}
		public double? priceAbove{get=>price_above;}
		public double? priceBelow{get=>price_below;}
		public static OrderFilter build(){
			OrderFilter order_filter = new OrderFilter();
			order_filter.reset_all();
			return order_filter;
		}
		public Order[] apply_filter(){
			if (order_id != null){
				try{
					Order o = OrderBook.Instance.fetch_order((int) order_id);
					return new Order[]{OrderBook.Instance.fetch_order((int) order_id)};
				} catch (Exception e){
					return new Order[]{};
				}
			}

			List<Order> orders = new List<Order>(OrderBook.Instance.fetch_orders());
			
			List<Order> to_be_filtered = new List<Order>();

			if (prepare_datetime_from != null){
				foreach(Order i in orders){
					if(i.prepareDatetime < prepare_datetime_from)
						to_be_filtered.Add(i);
				}
			}

			if (prepare_datetime_until != null){
				foreach(Order i in orders){
					if(i.prepareDatetime > prepare_datetime_from)
						to_be_filtered.Add(i);
				}
			}

			if (order_datetime_from != null){
				foreach(Order i in orders){
					if(i.orderDatetime < order_datetime_from)
						to_be_filtered.Add(i);
				}
			}

			if (order_datetime_until != null){
				foreach(Order i in orders){
					if(i.orderDatetime > order_datetime_until)
						to_be_filtered.Add(i);
				}
			}

			if (customer != null){
				foreach(Order i in orders){
					if(i.Customer.customerId != customer.customerId)
						to_be_filtered.Add(i);
				}
			}

			if (admin != null){
				foreach(Order i in orders){
					if(i.Admin.AdminId != admin.AdminId)
						to_be_filtered.Add(i);
				}
			}

			if (is_delivery != null){
				foreach(Order i in orders){
					if(i.isDelivery != is_delivery)
						to_be_filtered.Add(i);
				}
			}

			if (completed != null){
				foreach(Order i in orders){
					if(i.isCompleted != completed)
						to_be_filtered.Add(i);
				}
			}

			if (price_above != null){
				foreach(Order i in orders){
					if(i.Bill.Price < price_above)
						to_be_filtered.Add(i);
				}
			}

			if (price_below != null){
				foreach(Order i in orders){
					if(i.Bill.Price > price_below)
						to_be_filtered.Add(i);
				}
			}

			foreach (Order i in to_be_filtered){
				if (orders.Contains(i))
					orders.Remove(i);
			}

			return orders.ToArray();
		}
		public OrderFilter use_id(int id){
			order_id = id;
			return this;
		}
		public OrderFilter clear_id(){
			order_id = null;
			return this;
		}
		public OrderFilter reset_all(){
			order_id = null;
			prepare_datetime_from = null;
			prepare_datetime_until = null;
			order_datetime_from = null;
			order_datetime_until = null;
			customer = null;
			admin = null;
			is_delivery = null;
			completed = null;
			price_above = null;
			price_below = null;
			return this;
		}
		public OrderFilter prepare_between(Datetime from, Datetime until){
			prepare_datetime_from = from;
			prepare_datetime_until = until;
			return this;
		}
		public OrderFilter prepare_at(Datetime datetime){
			return prepare_between(datetime, datetime + new int[] {0,0,1,0,0,0});
		}
		public OrderFilter ordered_between(Datetime from, Datetime until){
			order_datetime_from = from;
			order_datetime_until = until;
			return this;
		}
		public OrderFilter ordered_at(Datetime date){
			return ordered_between(date, date + new int[] {0,0,1,0,0,0});
		}
		public OrderFilter ordered_by(Customer customer){
			this.customer = customer;
			return this;
		}
		public OrderFilter is_completed(){
			this.completed = true;
			return this;
		}
		public OrderFilter not_completed(){
			this.completed = false;
			return this;
		}
		public OrderFilter reset_completed(){
			this.completed = null;
			return this;
		}
		public OrderFilter delivery(){
			this.is_delivery = true;
			return this;
		}
		public OrderFilter pick_up(){
			this.is_delivery = false;
			return this;
		}
		public OrderFilter reset_delivery_type(){
			this.is_delivery = null;
			return this;
		}
		public OrderFilter placed_by_admin(Admin admin){
			this.admin = admin;
			return this;
		}
		public OrderFilter price(double above, double below){
			this.price_above = above;
			this.price_below = below;
			return this;
		}
	}
}
