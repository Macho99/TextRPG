using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class PlayerPos
    {
        public Position pos;

        public PlayerPos()
        {

            pos = new Position(1, 1);
        }

        public void Move(Direction dir, int[,] map)
        {
            Position prevPos = pos;
            switch (dir)
            {
                case Direction.Up:
                    pos.y--;
                    break;
                case Direction.Down:
                    pos.y++;
                    break;
                case Direction.Left:
                    pos.x--;
                    break;
                case Direction.Right:
                    pos.x++;
                    break;
                default:
                    break;
            }

            if (map[pos.y, pos.x] == 1)
            {
                pos = prevPos;
            }
        }
    }
}
