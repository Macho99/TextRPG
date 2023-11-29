using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public struct Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static bool operator==(Position left, Position right)
        {
            if(left.y == right.y)
            {
                if(left.x == right.x)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool operator!=(Position left, Position right)
        {
            if(left == right)
            {
                return false;
            }
            return true;
        }
    }
    public enum Direction
    {
        Up, Down, Left, Right, None
    }

    public enum GroupScene
    {
        Title, Map, Battle, GameOver, Prev, Size
    };
}
