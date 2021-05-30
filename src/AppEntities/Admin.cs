using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Database.Tables.AdminAccount;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Exceptions;
using SecretGarden.OrderSystem.Misc;

namespace SecretGarden.OrderSystem.AppEntities{
	class Admin{
		private int id;
		private Admin(int id){this.id = id;}
		public static Admin login(int id, string password){
			if(! DBWrapper.Instance.admin_account_table.exists(new int[]{id})) throw new AdminAccountException(AdminAccountException.exception_type.ADMIN_NOT_FOUND);
			string salt = DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).passwordSalt;
			string hash = DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).passwordHash;
			if (PasswordUtils.velidate_password(salt,password,hash)) return new Admin(id);
			throw new AdminAccountException(AdminAccountException.exception_type.AUTHENTICATION_ERROR);
		}
		public static int new_admin(string first_name, string last_name, string password){
			string salt = PasswordUtils.generate_salt();
			string hash = PasswordUtils.hash_password(salt, password);
			AdminAccountRecord record = new AdminAccountRecord(
				DBWrapper.Instance.admin_account_table,
				0,
				0,
				first_name,
				last_name,
				salt,
				hash,
				(Datetime) DateTime.Today
			);
			DBWrapper.Instance.admin_account_table.append_record(record);
			List<AdminAccountRecord> r = DBWrapper.Instance.admin_account_table.get_records();
			return r[r.Count - 1].primaryKey[0];
		}
		public string firstName{
			get{
				return DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).firstName;
			}
			set{
				DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).firstName = value;
			}
		}
		public string lastName{
			get{
				return DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).lastName;
			}
			set{
				DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).lastName = value;
			}
		}
		public int Id{
			get=>id;
		}
		public Datetime establishDate{
			get=>DBWrapper.Instance.admin_account_table.retrieve(new int[]{id}).establishDate;
		}
	}
}