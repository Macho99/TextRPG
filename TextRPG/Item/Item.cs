using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Item : IComparable<Item>
    {
        private ItemID id;
        protected Item(ItemID id)
        {
            this.id = id;
        }
        public ItemID ID { get { return id; } }
        public string Name { get { return Data.Instance.dictItemName[id]; } }
        public string Desc { get { return Data.Instance.dictItemDesc[id]; } }
        public abstract Item Clone();

        public int CompareTo(Item? other)
        {
            if (id < other.id)
            {
                return 1;
            }
            else if (id > other.id)
            {
                return -1;
            }
            return 0;
        }
    }
}