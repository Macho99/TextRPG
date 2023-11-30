using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SlimeMucus : OtherItem
    {
        public SlimeMucus(int quantity = 1) : base(ItemID.SLIME_MUCUS, quantity) { }
        public override SlimeMucus Clone()
        {
            return new SlimeMucus(GetQuantity());
        }
    }
}
