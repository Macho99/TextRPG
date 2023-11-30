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
            AddItem(Data.Instance.dictItem[ItemID.RED_POTION].Clone());
            AddItem(Data.Instance.dictItem[ItemID.RED_POTION].Clone());
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
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("{0}. {1}", i + 1, items[i].Name);
                if (items[i] is IMultiple multi)
                {
                    Console.Write(": {0}", multi.GetQuantity());
                }
                else if (items[i] is Equipment equipment)
                {
                    if (equipment.IsEquip)
                    {
                        Console.Write("(착용중)");
                    }
                }
                Console.Write("\n{0}\n", items[i].Desc);
                Console.WriteLine();
            }
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
        public int ItemCount()
        {
            return items.Count;
        }
    }
}