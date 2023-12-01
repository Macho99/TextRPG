using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Inventory
    {
        private static Inventory instance;
        List<Item> items;
        private Inventory()
        {
            items = new List<Item>();
            AddItem(Data.Instance.dictItem[ItemID.RED_POTION].Clone());
            AddItem(Data.Instance.dictItem[ItemID.RED_POTION].Clone());
            AddItem(Data.Instance.dictItem[ItemID.LONG_SWORD].Clone());
            AddItem(Data.Instance.dictItem[ItemID.RED_POTION].Clone());
            AddItem(Data.Instance.dictItem[ItemID.SLIME_MUCUS].Clone());
            AddItem(Data.Instance.dictItem[ItemID.LETHER_GLOVE].Clone());
            AddItem(Data.Instance.dictItem[ItemID.LETHER_GLOVE].Clone());
            AddItem(Data.Instance.dictItem[ItemID.LONG_SWORD].Clone());
            AddItem(Data.Instance.dictItem[ItemID.SLIME_MUCUS].Clone());
        }
        public static Inventory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory();
                }
                return instance;
            }
        }
        public void AddItem(Item item)
        {
            if (item is IMultiple multi)
            {
                Item? findItem = FindItem(item.ID);
                if (findItem != null)
                {
                    IMultiple findMulti = (IMultiple)findItem;
                    findMulti.AddQuantity(multi.GetQuantity());
                }
                else
                {
                    items.Add(item);
                }
            }
            else
            {
                items.Add(item);
            }
        }
        public void AddItem(IList<Item> items)
        {
            foreach(Item item in items)
            {
                AddItem(item);
            }
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
        public void PrintItems()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=========================================");
            for (int i = 0; i < items.Count; i++)
            {
                sb.Append($"|  {i + 1}. {items[i].Name}");
                if (items[i] is IMultiple multi)
                {
                    sb.Append($": {multi.GetQuantity()}");
                }
                else if (items[i] is Equipment equipment)
                {
                    if (equipment.IsEquip)
                    {
                        sb.Append("(착용중)");
                    }
                }
                sb.Append($"\n|    :{items[i].Desc}\n");
            }
            sb.AppendLine("=========================================");
            Console.WriteLine(sb.ToString());
        }
        private Item? FindItem(ItemID id)
        {
            foreach (Item item in items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }
        private Item GetItem(int idx)
        {
            if (idx < 0 || idx >= items.Count)
            {
                throw new IndexOutOfRangeException(nameof(idx));
            }
            return items[idx];
        }
        public void UseItem(int idx)
        {
            Item item = GetItem(idx);
            if(item is Equipment equipment)
            {
                if (equipment.IsEquip)
                {
                    equipment.UnEquip();
                }
                else
                {
                    equipment.Equip();
                }
            }
            else if(item is ConsumptionItem consump)
            {
                if (!consump.Use())
                {
                    RemoveItem(consump);
                }
            }
            else
            {
                Console.WriteLine($"{item.Name}은(는) 사용할 수 없는 아이템입니다!");
            }
        }
        public void SelectItem(int idx)
        {
            if(idx < 0 || idx >= items.Count)
            {
                Console.WriteLine("올바른 숫자를 입력하세요");
            }

            Item item = GetItem(idx);
            if(item is  IUseable useable) {
                if (!useable.Use())
                {
                    RemoveItem(item);
                }
            }
            else if(item is IEquipable equipable)
            {
                if(equipable.Equip())
                {

                }
                else 
                {
                    equipable.UnEquip();
                }
            }
        }
        public void SortType()
        {
            items.Sort();
        }
        public void SortName()
        {
            PriorityQueue<Item, string> pq =  new PriorityQueue<Item, string>();
            foreach( var item in items)
            {
                pq.Enqueue(item, item.Name );
            }
            items.Clear();
            while( pq.Count > 0 )
            {
                items.Add(pq.Dequeue());
            }
        }
        public int ItemCount()
        {
            return items.Count;
        }
    }
}