namespace SecretGarden.OrderSystem.Database{
	class Record{
		public DBTable table_wrapper;
		public int index;
		public Record(DBTable table_wrapper, int index){
			this.table_wrapper = table_wrapper;
			this.index = index;
		}
	}
}