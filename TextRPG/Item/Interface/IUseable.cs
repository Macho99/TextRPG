using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public interface IUseable
    {
        // 아이템을 더 이상 사용할 수 없으면 false 반환
        bool Use();
    }
}
