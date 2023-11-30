using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public interface IMultiple
    {
        int GetQuantity();
        void AddQuantity(int quantity);
    }
}
