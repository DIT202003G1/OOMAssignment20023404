using System.Security.Cryptography;
using System.Text;
using System;

namespace SecretGarden.OrderSystem.Misc{
	static class StringUtils{
		public static string numbers = "1234567890";
		public static string printables = " `-=~_+[]\\;',./{}|:\"<>?!@#$%^&*()abccdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"+numbers;
		public static string hide_by_max_width(string content, int width){
			if (content.Length <= width){
				return content;
			}
			else if (width <= 3){
				return content.Substring(0,3);
			}
			else{
				return content.Substring(0, width - 3) + "...";
			}
		}
	}
}