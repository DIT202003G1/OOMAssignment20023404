using System;
using System.Collections.Generic;
using System.Linq;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Database.Tables.AdminAccount;
using SecretGarden.OrderSystem.Exceptions;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.AppEntities
{
	public class Admin
	{
		private int admin_id;
		private string admin_first_name;
		private string admin_last_name;
		private string admin_password_hash;
		private string admin_password_salt;
		private Datetime admin_establish_date;

		public int AdminId => admin_id;

		public string firstName
		{
			get => admin_first_name;
			set
			{
				DBWrapper.Instance.admin_account_table.retrieve(new[] {admin_id}).firstName = value;
				admin_first_name = value;
			}
		}

		public string lastName
		{
			get => admin_last_name;
			set
			{
				DBWrapper.Instance.admin_account_table.retrieve(new[] {admin_id}).lastName = value;
				admin_last_name = value;
			}
		}

		public Datetime establishDate{
			get=>admin_establish_date;
		}

		public string Password
		{
			set
			{
				admin_password_salt = PasswordUtils.generate_salt();
				admin_password_hash = PasswordUtils.hash_password(admin_password_salt, value);
				AdminAccountRecord record = DBWrapper.Instance.admin_account_table.retrieve(new[] {admin_id});
				record.passwordHash = admin_password_hash;
				record.passwordSalt = admin_password_salt;
			}
		}

		private Admin(int id)
		{
			admin_id = id;

			var record = DBWrapper.Instance.admin_account_table.retrieve(new[] {id});
			admin_first_name = record.firstName;
			admin_last_name = record.lastName;
			admin_establish_date = record.establishDate;
		}

		public static Admin[] fetch_admins()
		{
			var adminRecords = DBWrapper.Instance.admin_account_table.get_records();
			
			var admins = new List<Admin>();
			foreach (var record in adminRecords)
			{
				admins.Add(new Admin(record.primaryKey[0]));
			}

			return admins.ToArray();
		}

		public static Admin fetch_admin(int id)
		{
			return new(id);
		}

		public static Admin login(int id, string password)
		{
			var record = DBWrapper.Instance.admin_account_table.retrieve(new[] {id});
			if (PasswordUtils.validate_password(record.passwordSalt, password, record.passwordHash))
				return new Admin(id);

			throw new AdminAccountException();
		}

		public static int new_admin(string firstName, string lastName, string password)
		{
			var passwordSalt = PasswordUtils.generate_salt();
			var passwordHash = PasswordUtils.hash_password(passwordSalt, password);

			var record = new AdminAccountRecord(
				DBWrapper.Instance.admin_account_table,
				-1, -1,
				firstName, lastName,
				passwordSalt, passwordHash,
				DateTime.Now
			);

			DBWrapper.Instance.admin_account_table.append_record(record);

			var records = DBWrapper.Instance.admin_account_table.get_records();
			return records[records.ToArray().Length - 1].primaryKey[0];
		}
	}
}