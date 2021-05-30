namespace SecretGarden.OrderSystem.InterfaceLib{
	class Color{
		public enum color_codes{
			BLACK,
			RED,
			GREEN,
			YELLOW,
			BLUE,
			MAGENTA,
			CYAN,
			WHITE,
			RESET
		}
		public enum color_types{
			NORMAL,
			BRIGHT
		}
		public color_codes color_code;
		public color_types color_type;
		public Color(color_codes color_code, color_types color_type){
			this.color_code = color_code;
			this.color_type = color_type;
		}
	}
}