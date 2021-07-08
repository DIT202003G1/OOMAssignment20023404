using System;
using System.Collections.Generic;

using SecretGarden.OrderSystem.Misc;
using SecretGarden.OrderSystem.InterfaceLib;
using SecretGarden.OrderSystem.InterfaceLib.Controls;

namespace SecretGarden.OrderSystem.AppInterface{
	class DateEditingMenu : Window{
		private Datetime original_datetime;
		private bool has_time;
		private bool has_second;
		private Textbox year;
		private Textbox month;
		private Textbox day;
		private Textbox hr;
		private Textbox min;
		private Textbox sec;
		Button save;
		public DateEditingMenu(Datetime datetime, string title, bool has_time, bool has_second):base(title, 4, 3, 45, 5, ConsoleColor.Black){
			this.original_datetime = datetime;
			this.has_time = has_time;
			this.has_second = has_second;
			string prompt = "D  M  Y   ";
			day = new Textbox(this, "day", 2, 2, 2, 1, ConsoleColor.Black, ConsoleColor.White, datetime.Day.ToString("00"));
			month = new Textbox(this, "month", 5, 2, 2, 1, ConsoleColor.Black, ConsoleColor.White, datetime.Month.ToString("00"));
			year = new Textbox(this, "year", 8, 2, 4, 1, ConsoleColor.Black, ConsoleColor.White, datetime.Year.ToString("0000"));
			day.maxLength = 2;
			month.maxLength = 2;
			year.maxLength = 4;
			new Label(this, "seperator_1", 4, 2, 1, 1, ConsoleColor.Black, "-");
			new Label(this, "seperator_2", 7, 2, 1, 1, ConsoleColor.Black, "-");
			if (has_time){
				prompt += " h  m ";
				hr = new Textbox(this, "hr", 13, 2, 2, 1, ConsoleColor.Black, ConsoleColor.White, datetime.Hour.ToString("00"));
				new Label(this, "seperator_3", 15, 2, 1, 1, ConsoleColor.Black, ":");
				min = new Textbox(this, "min", 16, 2, 2, 1, ConsoleColor.Black, ConsoleColor.White, datetime.Minute.ToString("00"));
				hr.maxLength = 2;
				min.maxLength = 2;
				if (has_second){
					prompt += " s ";
					new Label(this, "seperator_4", 18, 2, 1, 1, ConsoleColor.Black, ":");
					sec = new Textbox(this, "sec", 19, 2, 2, 1, ConsoleColor.Black, ConsoleColor.White, datetime.Second.ToString("00"));
					sec.maxLength = 2;
				}
			}
			new Label (this, "prompt", 2, 1, 19, 1, ConsoleColor.White, prompt);
			save = new Button(this, "save", 39, 2, ConsoleColor.Black, ConsoleColor.White, "Save");
			foreach (Textbox i in this.textboxes.Values){
				i.allowedChars = "1234567890";
			}
		}
		public int Year{
			get=>Int16.Parse(year.Text);
		}
		public int Month{
			get=>Int16.Parse(month.Text);
		}
		public int Day{
			get=>Int16.Parse(day.Text);
		}
		public int Hour{
			get{
				if (has_time) return Int16.Parse(hr.Text);
				else return original_datetime.Hour;
			}
		}
		public int Minute{
			get{
				if (has_time) return Int16.Parse(min.Text);
				else return original_datetime.Minute;
			}
		}
		public int Second{
			get{
				if (has_time && has_second) return Int16.Parse(sec.Text);
				else return original_datetime.Second;
			}
		}

		public override ConsoleKey focus(){
			// 1 - day
			// 2 - month
			// 3 - year
			// 4 - hr
			// 5 - min
			// 6 - sec
			// 7 - save
			focus_status = 1;
			foreach (Label i in this.labels.Values){
				if (i.Id.Substring(0,3) == "sep")
					i.backgroundColor = ConsoleColor.Gray;
			}
			while (true){
				Console.ResetColor();
				Console.Clear();
				draw();
				ConsoleKey c;
				switch (focus_status){
					case 1:
						c = day.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 2;
						continue;
					case 2:
						c = month.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 3;
						if (c == ConsoleKey.LeftArrow) focus_status = 1;
						continue;
					case 3:
						c = year.focus();
						if (c == ConsoleKey.RightArrow && !has_time) focus_status = 7;
						else if (c == ConsoleKey.RightArrow) focus_status = 4;
						if (c == ConsoleKey.LeftArrow) focus_status = 2;
						continue;
					case 4:
						c = hr.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 5;
						if (c == ConsoleKey.LeftArrow) focus_status = 3;
						continue;
					case 5:
						c = min.focus();
						if (c == ConsoleKey.RightArrow && has_second ) focus_status = 6;
						if (c == ConsoleKey.RightArrow && !has_second) focus_status = 7;
						if (c == ConsoleKey.LeftArrow) focus_status = 4;
						continue;
					case 6:
						c = sec.focus();
						if (c == ConsoleKey.RightArrow) focus_status = 7;
						if (c == ConsoleKey.LeftArrow) focus_status = 5;
						continue;
					case 7:
						c = save.focus();
						if (c == ConsoleKey.LeftArrow && !has_time) focus_status = 3;
						else if (c == ConsoleKey.LeftArrow && !has_second) focus_status = 5;
						else if (c == ConsoleKey.LeftArrow) focus_status = 6;
						else if (c == ConsoleKey.Enter) {
							if (Datetime.velidate_complete_datetime(Year, Month, Day, Hour, Minute, Second))
								return ConsoleKey.Enter;
							else{
								this.height=7;
								Label err = new Label(this, "err", 2, 4, 41, 1, ConsoleColor.White, "Invalid datetime format");
								err.backgroundColor = ConsoleColor.Red;
							}
						};
						continue;
				}
			}
		}
	}
}