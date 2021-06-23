using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.Customer;
using SecretGarden.OrderSystem.Exceptions;

namespace SecretGarden.OrderSystem.AppEntities{
	class Customer{
		private int customer_id;
		private string customer_first_name, customer_last_name, customer_telephone, customer_address;
		private Datetime customer_establish_date, customer_premium_start_date, customer_premium_end_date;
		public int customerId{
			get=>customer_id;
		}
		public string firstName{
			get=>customer_first_name;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).firstName = value;
				customer_first_name = value;
			}
		}
		public string lastName{
			get=>customer_last_name;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).lastName = value;
				customer_first_name = value;
			}
		}
		public string Telephone{
			get=>customer_telephone;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).Telephone = value;
				customer_telephone = value;
			}
		}
		public string Address{
			get=>customer_address;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).Address = value;
				customer_address = value;
			}
		}
		public Datetime establishDate{
			get=>customer_establish_date;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).establishDate = value;
				customer_establish_date = value;
			}
		}
		public Datetime premiumStartDate{
			get=>premiumStartDate;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).premiumeRegisterDate = value;
				customer_premium_start_date = value;
			}
		}
		public Datetime premiumEndDate{
			get=>customer_premium_end_date;
			set{
				DBWrapper.Instance.customer_table.retrieve(new int[] {customer_id}).premiumeEndDate = value;
				customer_premium_end_date = value;
			}
		}
		private Customer(int id){
			if (!DBWrapper.Instance.customer_table.exists(new int[] {id}))
				throw new CustomerException(CustomerException.exception_type.CUSTOMER_NOT_FOUND);
			CustomerRecord customer = DBWrapper.Instance.customer_table.retrieve(new int[] {id});
			this.customer_id = id;
			this.customer_first_name = customer.firstName;
			this.customer_last_name = customer.lastName;
			this.customer_address = customer.Address;
			this.customer_telephone = customer.Telephone;
			this.customer_establish_date = customer.establishDate;
			this.customer_premium_end_date = customer.premiumeEndDate;
			this.customer_premium_start_date = customer.premiumeRegisterDate;
		}
		public static Customer[] fetch_customers(){
			List<Customer> customers = new List<Customer>();
			List<CustomerRecord> records = DBWrapper.Instance.customer_table.get_records();
			foreach (CustomerRecord i in records){
				customers.Add(new Customer(i.primaryKey[0]));
			}
			return customers.ToArray();
		}
		public static Customer fetch_customer(int id){
			if (!DBWrapper.Instance.customer_table.exists(new int[] {id}))
				throw new CustomerException(CustomerException.exception_type.CUSTOMER_NOT_FOUND);
			return new Customer(
				DBWrapper.Instance.customer_table.retrieve(new int[] {id}).primaryKey[0]
			);
		}
		public static int new_customer(string first_name, string last_name, string address, string telephone, int premium_factor){
			Datetime premium_start_date = null;
			Datetime premium_end_date = null;
			if (premium_factor > 0){
				premium_start_date = (Datetime) DateTime.Now;
				premium_end_date = premium_start_date + new int[] {0,premium_factor,0,0,0,0};
			}
			DBWrapper.Instance.customer_table.new_record(
				new CustomerRecord(
					DBWrapper.Instance.customer_table,
					1,
					1,
					first_name,
					last_name,
					address,
					telephone,
					(Datetime) DateTime.Now,
					premium_start_date,
					premium_end_date
				)
			);
			List<CustomerRecord> records =  DBWrapper.Instance.customer_table.get_records();
			return records[records.ToArray().Length-1].primaryKey[0];
		}
		public bool is_premium(){
			if (this.customer_premium_end_date == null) return false;
			if (this.customer_premium_end_date > (Datetime) DateTime.Now) return true;
			return false;
		}
	}
}