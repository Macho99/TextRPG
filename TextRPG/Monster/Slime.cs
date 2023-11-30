using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Slime : Monster
    {
        private char icon = 'S';
        private int moveCnt = 0;
        private const int movePeriod = 3;
        private static List<(ItemID, float)> dropTable;
        //매 업데이트마다 랜덤 방향으로 이동
        static Slime()
        {
            dropTable = new List<(ItemID, float)>();
            dropTable.Add((ItemID.LETHER_GLOVE, 0.5f));
            dropTable.Add((ItemID.RED_POTION, 0.5f));
            dropTable.Add((ItemID.RED_POTION, 0.5f));
            dropTable.Add((ItemID.SLIME_MUCUS, 0.5f));
            dropTable.Add((ItemID.SLIME_MUCUS, 0.5f));
            dropTable.Add((ItemID.SLIME_MUCUS, 0.5f));
        }
        protected override List<(ItemID, float)> getDropTable()
        {
            return dropTable;
        }
        public Slime(int y, int x) : base(new Point(y,x), "슬라임", 20, 10, 1, 5, 50, 100)
        {
            random = new Random();
        }
        public override char GetIcon()
        {
            return icon;
        }
        public override void MoveAction(int[,] map)
        {
            moveCnt += random.Next(0, 2);
            if(moveCnt < movePeriod)
            {
                return;
            }
            moveCnt -= movePeriod;
            int randNum = random.Next(0, (int)Direction.None);
            Direction dir = (Direction)randNum;
            Move(dir, map);
        }

        public override void TakeDamage(int damage)
        {
            double randNum = random.NextDouble();
            if(randNum < 0.2)
            {
                Console.WriteLine($"{Name}이 미끄러워 공격이 빗나갑니다...");
                damage = (int) (damage * 0.5);
            }
            base.TakeDamage(damage);
        }
    }
}
