using System.Collections.Generic;
using System;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Exceptions;
using SecretGarden.OrderSystem.Database.Tables.Customer;

namespace SecretGarden.OrderSystem.AppEntities{
	class Customer{
		private int id;
		private Customer(int id){this.id = id;}
		public static List<Customer> get_all_customers(){
			List<Customer> customers = new List<Customer>();
			List<CustomerRecord> customer_records = DBWrapper.Instance.customer_table.get_records();
			foreach(CustomerRecord r in customer_records){
				customers.Add(
					new Customer(r.primaryKey[0])
				);
			}
			return customers;
		}
		public static Customer retrieve(int id){
			if (DBWrapper.Instance.customer_order_table.exists(new int[]{id}))
				throw new AdminAccountException(AdminAccountException.exception_type.ADMIN_NOT_FOUND);
			return new Customer(id);
		}
		public int Id{get=>id;}
		public static int new_customer(string first_name, string last_name, string address, string telephone, int premium_multiplier = 0){
			Datetime premium_register_date = null;
			Datetime premium_end_date = null;
			if (premium_multiplier >= 1){
				premium_register_date = (Datetime) DateTime.Today;
				premium_end_date = premium_register_date + new int[]{premium_multiplier*2, 0, 0, 0, 0, 0};
			}
			CustomerRecord record = new CustomerRecord(
				DBWrapper.Instance.customer_table,
				0,
				0,
				first_name,
				last_name,
				address,
				telephone,
				(Datetime) DateTime.Today,
				premium_register_date,
				premium_end_date
			);
			DBWrapper.Instance.customer_table.append_record(record);
			List<CustomerRecord> r = DBWrapper.Instance.customer_table.get_records();
			return r[r.Count - 1].primaryKey[0];
		}
		public string firstName{
			get{
				return DBWrapper.Instance.customer_table.retrieve(new int[]{id}).firstName;
			}
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[]{id}).firstName = value;
			}
		}
		public string lastName{
			get{
				return DBWrapper.Instance.customer_table.retrieve(new int[]{id}).firstName;
			}
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[]{id}).firstName = value;
			}
		}
		public bool is_premium(){
			CustomerRecord r = DBWrapper.Instance.customer_table.retrieve(new int[]{id});
			if (r.premiumeRegisterDate == null || r.premiumeRegisterDate == null) return false;
			if(r.premiumeEndDate < (Datetime) DateTime.Today) return false;
			return true;
		}
		public void extend_premium(int premium_multiplier){
			CustomerRecord r = DBWrapper.Instance.customer_table.retrieve(new int[]{id});
			if(is_premium()){
				r.premiumeEndDate += new int[]{2*premium_multiplier, 0, 0, 0, 0, 0}; 
			}
			else{
				r.premiumeRegisterDate = ((Datetime) DateTime.Today); 
				r.premiumeEndDate = r.premiumeRegisterDate + new int[]{2*premium_multiplier, 0, 0, 0, 0, 0}; 
			}
		}
		public void new_order(){}
	}
}