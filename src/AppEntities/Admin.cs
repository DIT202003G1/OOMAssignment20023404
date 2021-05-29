using System;

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
			string salt = DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).passwordSalt;
			string hash = DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).passwordHash;
			if (PasswordUtils.velidate_password(salt,password,hash)) return new Admin(id);
			throw new AdminAccountException(AdminAccountException.exception_type.AUTHENTICATION_ERROR);
		}
		public static void new_admin(int id, string first_name, string last_name, string password){
			if(DBWrapper.Instance.admin_account_table.exists(new int[]{id})) throw new AdminAccountException(AdminAccountException.exception_type.ADMIN_FOUND);
			string salt = PasswordUtils.generate_salt();
			string hash = PasswordUtils.hash_password(salt, password);
			AdminAccountRecord record = new AdminAccountRecord(
				DBWrapper.Instance.admin_account_table,
				0,
				id,
				first_name,
				last_name,
				salt,
				hash,
				(Datetime) DateTime.Today
			);
			DBWrapper.Instance.admin_account_table.update(record);
		}
		public string firstName{
			get{
				return DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).firstName;
			}
			set{
				DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).firstName = value;
			}
		}
		public string lastName{
			get{
				return DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).lastName;
			}
			set{
				DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).lastName = value;
			}
		}
		public int Id{
			get=>id;
		}
		public Datetime establishDate{
			get=>DBWrapper.Instance.admin_account_table.retrave(new int[]{id}).establishDate;
		}
	}
}