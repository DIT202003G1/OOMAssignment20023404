using System;
using System.Linq;

namespace SecretGarden.OrderSystem.Misc{
	class Datetime{
		private int year, month, day, hr, min, sec;
		// private static int[] month_ref = {31,28,31};
		public static bool validate_month_day_count(int year, int month, int day){
			int month_day_count = get_day_count(year,month);
			if (day >= 1 && day <= month_day_count)
				return true;
			return false;
		}
		public static int get_day_count(int year, int month){
			if (Array.Exists<int>(new int[7]{1,3,5,7,8,10,12}, e => (e == month)))
				return 31;
			if (Array.Exists<int>(new int[4]{4,6,9,11}, e=>(e==month)))
				return 30;
			if (month == 2){
				return (year % 4 == 0) ? 29 : 28;
			}
			throw new Exception("Invalid month");
		}
		public Datetime(int year, int month, int day, int hr, int min, int sec){
			this.year = year;
			if (month > 12 || month < 1) throw new Exception("Invalid Month");
			this.month = month;
			if (!validate_month_day_count(year, month, day)) throw new Exception("Invalid Day");
			this.day = day;
			if (hr > 23 || hr < 0) throw new Exception("Invalid Hour");
			this.hr = hr;
			if (min > 59 || min < 0) throw new Exception("Invalid Minutes");
			this.min = min;
			if (sec > 59 || sec < 0) throw new Exception("Invalid Seconds");
			this.sec = sec;
		}
		public string sqlFormat{
			get{return $"{year}-{month}-{day} {hr}:{min}:{sec}";}
		}
		public string sqlFormatDate{
			get{return $"{year}-{month}-{day}";}
		}
		public static implicit operator Datetime(DateTime d) => new Datetime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
		public static Datetime operator +(Datetime target, int[] step){
			int new_sec = target.sec + step[5];
			int new_min = target.min + step[4];
			int new_hr = target.hr + step[3];

			while (new_sec > 59){
				new_min ++;
				new_sec -= 60;
			}

			while (new_min > 59){
				new_hr ++;
				new_min -= 60;
			}

			int unit_day = 0;

			while (new_hr > 23){
				unit_day = 1;
				new_min -= 24;
			}

			int new_year = target.year + step[0];
			int new_month = target.month + step[1];
			while (new_month > 12){
				new_year ++;
				new_month -= 12;
			}
			int new_day = target.day + step[2] + unit_day;
			while (new_day > get_day_count(new_year, new_month)){
				new_month ++;
				new_day -= get_day_count(new_year, new_month - 1);
			}
			while (new_month > 12){
				new_year ++;
				new_month -= 12;
			}

			// Console.Out.WriteLine($"{new_year},{new_month},{new_day},{new_hr},{new_min},{new_sec},");

			return new Datetime(new_year, new_month, new_day, new_hr, new_min, new_sec);
		}
	}
}