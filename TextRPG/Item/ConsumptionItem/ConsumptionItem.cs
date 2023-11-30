using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class ConsumptionItem : Item, IMultiple, IUseable
    {
        private int quantity;
        public ConsumptionItem(ItemID id, int quantity)
            : base(id)
        {
            this.quantity = quantity;
        }

        public void AddQuantity(int quantity)
        {
            this.quantity += quantity;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public virtual bool Use()
        {
            if (quantity <= 0)
            {
                throw new Exception();
            }
            else if (quantity == 1)
            {
                Console.WriteLine("아이템을 모두 사용했다");
                return false;
            }
            else
            {
                quantity -= 1;
                return true;
            }
        }
    }
}