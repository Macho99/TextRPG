using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Equipment : Item, IEquipable
    {
        public bool IsEquip { get; protected set; }
        public Equipment(ItemID id)
            : base(id)
        {
            IsEquip = false;
        }

        //성공하면 true
        public virtual bool Equip()
        {
            if (!IsEquip)
            {
                Console.WriteLine("{0}을 착용합니다.", Name);
                IsEquip = true;
                return true;
            }
            else
            {
                Console.WriteLine("{0}을 이미 착용중입니다.", Name);
                return false;
            }
        }

        //성공하면 true
        public virtual bool UnEquip()
        {
            if (IsEquip)
            {
                Console.WriteLine("{0}을 착용해제합니다.", Name);
                IsEquip = false;
                return true;
            }
            else
            {
                Console.WriteLine("{0}을 착용하고있지 않습니다!",Name);
                return false;
            }
        }
    }
}
