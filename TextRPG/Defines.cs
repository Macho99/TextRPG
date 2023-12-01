using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum ItemID
    {
        RED_POTION,
        SLIME_MUCUS,
        LETHER_GLOVE,
        LONG_SWORD
    }
    public enum Direction
    {
        Up, Down, Left, Right, None
    }

    public enum GroupScene
    {
        Title, Map1, Map2, Battle, Inventory, GameOver, Prev, Size
    };
}
