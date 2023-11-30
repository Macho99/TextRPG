using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class RedPotion : ConsumptionItem
    {
        public RedPotion(int quantity = 1): base(ItemID.RED_POTION, quantity) { }
        public override bool Use()
        {
            Console.WriteLine("맛은 없지만 체력이 10 회복된 것 같다");
            PlayerStat.Instance.AddHP(10);
            return base.Use();
        }
        public override RedPotion Clone()
        {
            return new RedPotion(GetQuantity());
        }
    }
}
