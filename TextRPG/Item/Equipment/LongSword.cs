using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class LongSword : Equipment
    {
        public LongSword()
            : base(ItemID.LONG_SWORD) { }

        public override LongSword Clone()
        {
            return new LongSword();
        }

        protected override void EquipEffect()
        {
            Console.WriteLine("공격력이 10 증가했다");
            PlayerStat.Instance.AddDamage(10);
        }

        protected override void UnEquipEffect()
        {
            Console.WriteLine("공격력이 원래대로 돌아갔다");
            PlayerStat.Instance.MinusDamage(10);
        }
    }
}