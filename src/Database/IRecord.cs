namespace SecretGarden.OrderSystem.Database{
	interface IRecord{
		void remove_record();
		int[] primaryKey{get;}
		string sqlTupleDefaultPk{get;}
		string sqlTuple{get;}
	}
}