using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class LeatherGlove : Equipment
    {
        public LeatherGlove()
            : base(ItemID.LETHER_GLOVE) { }

        public override LeatherGlove Clone()
        {
            return new LeatherGlove();
        }

        protected override void EquipEffect()
        {
            Console.WriteLine("방어력이 10 증가했다");
            PlayerStat.Instance.AddDefence(10);
        }

        protected override void UnEquipEffect()
        {
            Console.WriteLine("방어력이 원래대로 돌아갔다");
            PlayerStat.Instance.MinusDefence(10);
        }
    }
}
