using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Database{
	static class DBConfig{
		private readonly static int ENCRYPTION_MODIFIER = 7;
		private readonly static string DEFAULT_HOSTNAME = "localhost";
		private readonly static string DEFAULT_USERNAME = "root";
		private readonly static string DEFAULT_PASSWORD = "";
		private readonly static string DEFAULT_DBNAME = "secretgarden";
		private readonly static string CONFIG_PATH = "./DBConfig";

		private static string encrypt(string content){
			char[] chars = content.ToCharArray();
			List<string> encrypted = new List<string>();
			int index = ENCRYPTION_MODIFIER;
			foreach(char i in chars){
				int num_data = (int) i;
				int multiplied = i * ENCRYPTION_MODIFIER;
				int additioned = multiplied + index;
				string encoded = additioned.ToString("X");
				encrypted.Add(encoded);
				index += ENCRYPTION_MODIFIER;
			}
			return String.Join(" ", encrypted);
		}
		private static string decrypt(string content){
			string[] blocks = content.Trim().Split(" ");
			string output = "";
			int index = ENCRYPTION_MODIFIER;
			foreach(string i in blocks){
				int num_data = Int32.Parse(i, System.Globalization.NumberStyles.HexNumber);
				int subtracted = num_data - index;
				int devided = subtracted / ENCRYPTION_MODIFIER;
				char decoded = (char) devided;
				output += decoded;
				index += ENCRYPTION_MODIFIER;
			}
			return output;
		}
		private static void wrtie_default_config(){
			if (!File.Exists(CONFIG_PATH)) File.Create(CONFIG_PATH).Close();
			using (FileStream f = new FileStream(CONFIG_PATH, FileMode.Truncate)){
				string contents = encrypt(
					DEFAULT_HOSTNAME + 
					"\n" + 
					DEFAULT_USERNAME + 
					"\n" + 
					DEFAULT_PASSWORD + 
					"\n" + 
					DEFAULT_DBNAME
				);
				byte[] byte_contents = Encoding.ASCII.GetBytes(contents.ToCharArray());
				f.Write(byte_contents);
				f.Close();
			}
		}
		private static string read_config(){
			using (StreamReader f = new StreamReader(CONFIG_PATH)){
				string content = f.ReadToEnd();
				f.Close();
				return content;
			}
		}
		private static bool velidate_config_file(){
			try{
				string content = read_config();
				string[] decrypted = decrypt(content).Split("\n");
				return decrypted.Length == 4;
			} catch (Exception) {
				return false;
			}
		}
		public static void init(){
			if (!File.Exists(CONFIG_PATH)) wrtie_default_config();
			else if (!velidate_config_file()) wrtie_default_config();
		}
		public static void update_field(int index, string val){
			string content = read_config();
			string[] decrypted = decrypt(content).Split("\n");
			decrypted[index] = val;
			using (FileStream f = new FileStream(CONFIG_PATH, FileMode.Truncate)){
				string write_content = encrypt(String.Join('\n', decrypted));
				byte[] byte_contents = Encoding.ASCII.GetBytes(write_content.ToCharArray());
				f.Write(byte_contents);
				f.Close();
			}
		}
		public static string Hostname{
			get=>decrypt(read_config()).Split("\n")[0];
			set{
				update_field(0, value);
			}
		}
		public static string Username{
			get=>decrypt(read_config()).Split("\n")[1];
			set{
				update_field(1, value);
			}
		}
		public static string Password{
			get=>decrypt(read_config()).Split("\n")[2];
			set{
				update_field(2, value);
			}
		}
		public static string DBName{
			get=>decrypt(read_config()).Split("\n")[3];
			set{
				update_field(3, value);
			}
		}
	}
}