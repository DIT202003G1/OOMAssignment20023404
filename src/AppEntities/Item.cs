using System.Collections.Generic;

using SecretGarden.OrderSystem.Database.Tables.Item;
using SecretGarden.OrderSystem.Database;
using SecretGarden.OrderSystem.Exceptions;

namespace SecretGarden.OrderSystem.AppEntity{
	class Item{
		int item_id;
		string item_name;
		double item_price;
		protected Item(int item_id, string item_name, double item_price){
			this.item_id = item_id;
			this.item_name = item_name;
			this.item_price = item_price;
		}
		public static Item[] fetch_items(){
			List<Item> items = new List<Item>();
			List<ItemRecord> records = DBWrapper.Instance.item_table.get_records();
			foreach (ItemRecord i in records){
				
				if (i.itemType == ItemType.Others) items.Add(
						new Item(
							i.primaryKey[0],
							i.itemName,
							i.Price
						)
					);
				else items.Add(
					new Cake(
						i.primaryKey[0],
						i.itemName,
						i.Price,
						(int) i.cakeSize
					)
				);

			}
			return items.ToArray();
		}
		public static Item fetch_item(int id){
			if (!DBWrapper.Instance.item_table.exists(new int[] {id}))
				throw new ItemException(ItemException.exception_type.ITEM_NOT_FOUND);
			ItemRecord record = DBWrapper.Instance.item_table.retrieve(new int[] {id});
			if (record.itemType == ItemType.Others) return new Item(
				record.primaryKey[0],
				record.itemName,
				record.Price
			);
			else return new Cake(
				record.primaryKey[0],
				record.itemName,
				record.Price,
				(int) record.cakeSize
			);
		}
		public int Id{get=>item_id;}
		public string Name{get=>item_name;}
		public double Price{get=>item_price;}
		virtual public ItemType get_item_type(){
			return ItemType.Others;
		}
	}
}