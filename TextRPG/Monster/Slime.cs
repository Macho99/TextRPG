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
        private int turnCnt = 0;
        private const int moveTurn = 3;
        //매 업데이트마다 랜덤 방향으로 이동
        private Random random;
        public Slime(int y, int x) : base(new Position(y,x), "슬라임", 20, 10, 1, 5, 10)
        {
            random = new Random();
        }
        public override char GetIcon()
        {
            return icon;
        }
        public override void MoveAction(int[,] map)
        {
            turnCnt += random.Next(0, 2);
            if(turnCnt < moveTurn)
            {
                return;
            }
            turnCnt -= moveTurn;
            int randNum = random.Next(0, (int)Direction.None);
            Direction dir = (Direction)randNum;
            Move(dir, map);
        }

        public override void TakeDamage(int damage)
        {
            double randNum = random.NextDouble();
            if(randNum < 0.5)
            {
                Console.WriteLine($"{Name}이 미끄러워 공격이 빗나갑니다...");
                damage = (int) (damage * 0.5);
            }
            base.TakeDamage(damage);
        }
    }
}
