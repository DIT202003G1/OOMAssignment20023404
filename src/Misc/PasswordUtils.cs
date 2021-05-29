using System.Security.Cryptography;
using System.Text;
using System;

namespace SecretGarden.OrderSystem.Misc{
	static class PasswordUtils{
		private static string salt_chars = "1234567890abccdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public static string generate_salt(){
			Random r = new Random();
			string salt = "";
			for(int i = 0; i < 5; i++){
				int index = r.Next(0,salt_chars.Length);
				salt += salt_chars.Substring(index, index);
			}
			return salt;
		}
		public static string hash_password(string salt, string password){
			string salted = salt + password;
			byte[] salted_bytes = ASCIIEncoding.ASCII.GetBytes(salted);
			byte[] hashed_bytes = new SHA256CryptoServiceProvider().ComputeHash(salted_bytes);
			return byte_to_string(hashed_bytes);
		}
		public static bool velidate_password(string salt, string password, string hashed){
			string inputted_hashed = hash_password(salt, password);
			return inputted_hashed == hashed;
		}

		//Reference Source: https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/compute-hash-values
		private static string byte_to_string(byte[] arrInput){
			int i;
			StringBuilder sOutput = new StringBuilder(arrInput.Length);
			for (i=0;i < arrInput.Length; i++)
			{
				sOutput.Append(arrInput[i].ToString("X2"));
			}
			return sOutput.ToString();
		}
	}
}