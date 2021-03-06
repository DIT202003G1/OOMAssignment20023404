using System.Collections.Generic;

namespace SecretGarden.OrderSystem.Database{
	interface IDBTable<T>{
		List<T> get_records();
		bool exists(int[] pk_id);
		void new_record(T record);
		void append_record(T record);
		T retrieve(int[] pk_id);
		void update(T value);
	}
}