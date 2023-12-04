using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Hunter : Monster
    {
        char icon = 'H';
        List<Point> path;
        int moveIdx;        //현재 이동중인 path index
        int targetIdx;      //목표 (플레이어까지의 최단거리 중간)
        int moveCnt;        
        const int movePeriod = 2;     //moveCnt가 movePeriod가 되면 이동
        const int detectDist = 15;
        Random rand;
        Point playerPos;
        static List<(ItemID, float)> dropTable;
        static Hunter()
        {
            dropTable = new List<(ItemID, float)>();
            dropTable.Add((ItemID.LETHER_GLOVE, 0.5f));
            dropTable.Add((ItemID.RED_POTION, 0.5f));
            dropTable.Add((ItemID.RED_POTION, 0.5f));
            dropTable.Add((ItemID.RED_POTION, 0.5f));
            dropTable.Add((ItemID.LONG_SWORD, 0.5f));
        }
        protected override List<(ItemID, float)> getDropTable()
        {
            return dropTable;
        }
        public Hunter(int y, int x) : base(new Point(y,x), "헌터", 10, 0, 5, 20, 100, 15)
        {
            moveIdx = 0;
            targetIdx = 0;
            moveCnt = 0;
            rand = new Random();
        }

        public override char GetIcon()
        {
            return icon;
        }
        private void HuntPlayer(int[,] map)
        {
            if (moveIdx >= targetIdx)
            {
                path = new List<Point>();
                AStar.PathFinding(map, pos, playerPos, out path);
                //Core.Instance.GetCurScene().PrintDebugMap(map, path);
                if (path == null)
                {
                    return;
                }
                else
                {
                    targetIdx = path.Count / 3 + 2;
                    moveIdx = 0;
                }
            }

            Direction dir;
            if (path[moveIdx].y < pos.y)
            {
                dir = Direction.Up;
            }
            else if (path[moveIdx].y > pos.y)
            {
                dir = Direction.Down;
            }
            else if (path[moveIdx].x < pos.x)
            {
                dir = Direction.Left;
            }
            else if (path[moveIdx].x > pos.x)
            {
                dir = Direction.Right;
            }
            else
            {
                dir = Direction.None;
            }
            Move(dir, map);
            moveIdx++;
        }
        public override void MoveAction(int[,] map)
        {
            moveCnt += rand.Next(1,2);

            if (moveCnt < movePeriod) return;

            moveCnt -= movePeriod;
            playerPos = Core.Instance.GetCurScene().GetPlayer().pos;

            if ((playerPos - pos).GetLength() < detectDist)
            {
                HuntPlayer(map);
            }
            else {
                Move((Direction)rand.Next((int)Direction.None), map);
            }
        }
    }
}
