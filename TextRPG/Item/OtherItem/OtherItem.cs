using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class OtherItem : Item, IMultiple
    {
        private int quantity;
        protected OtherItem(ItemID id, int quantity = 1)
            : base(id)
        {
            this.quantity = quantity;
        }
        public void AddQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public int GetQuantity()
        {
            return quantity;
        }
    }
}
