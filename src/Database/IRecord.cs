using System;

namespace SecretGarden.OrderSystem.Database{
	interface IRecord{
		void remove_record();
		DBTable tableWrapper{get;}
		int primaryKey{get;}
		string sqlTulpe{get;}
	}
}