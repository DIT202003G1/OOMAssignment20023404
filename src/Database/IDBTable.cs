using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Database{
	interface IDBTable<T>{
		List<T> get_records();
		bool exists(int pk_id);
		void new_record(T record);
		T retrave(int pk_id);
		void update(T value);
	}
}