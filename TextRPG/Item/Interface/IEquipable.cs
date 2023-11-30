using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public interface IEquipable
    {
        bool Equip();
        bool UnEquip();
    }
}
