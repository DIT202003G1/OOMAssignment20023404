namespace SecretGarden.OrderSystem.AppEntity{
	class Cake : Item{
		private int size;
		public Cake(int id, string item_name, double item_price, int cake_size)
			:base(id, item_name, item_price){
				this.size = cake_size;
			}
		public int Size{get=>size;}
		public override ItemType get_item_type(){return ItemType.Cake;}
	}
}