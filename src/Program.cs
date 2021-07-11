using System;

using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.AppEntities;
using SecretGarden.OrderSystem.AppInterface;

namespace SecretGarden.OrderSystem{
	class Program{
		static void Main(string[] args){
			DBConfig.init();
			init_and_velidate_database();
			init_initial_admin_account();
			LoginWindow login_window = new LoginWindow();
			login_window.focus();
		}
		static void init_and_velidate_database(){
			while (true){
				try{
					DBWrapper.Instance.connect(DBConfig.Hostname, DBConfig.Username, DBConfig.Password, DBConfig.DBName);
					Console.ResetColor();
					Console.Clear();
					break;
				}catch{
					Console.ResetColor();
					Console.Clear();
					MessageBox err = new MessageBox(new string[] {"Unable to connect to database.", "Press enter to edit the configs"},"Database Error");
					err.focus();
					DatabaseSettingsMenu dsm = new DatabaseSettingsMenu();
					dsm.focus();
				}
			}
		}
		static void init_initial_admin_account(){
			while (Admin.fetch_admins().Length == 0){
				Console.ResetColor();
				Console.Clear();
				MessageBox err = new MessageBox(new string[] {"No default account has been set.", "Press enter to set a default account"},"Default Admin Account");
				err.focus();
				Console.ResetColor();
				Console.Clear();
				NewAdminMenu nam = new NewAdminMenu();
				nam.focus();
			}
			Console.ResetColor();
			Console.Clear();
		}
	}
}