using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Monster
    {
        public Position pos;

        public string Name {  get; private set; }
        public int CurHP { get; protected set; }
        public int MaxHP { get; private set; }
        public int CurMP { get; private set; }
        public int MaxMP { get; private set; }

        public int Level { get; private set; }

        public int Damage { get; private set; }
        public int Reward {  get; private set; }

        public Monster(Position pos, string name, int maxHP, int maxMP, int level, int damage, int reward)
        {
            Name = name;
            this.pos = pos;
            CurHP = MaxHP = maxHP;
            CurMP = MaxMP = maxMP;
            Level = level;
            Damage = damage;
            Reward = reward;
        }
        public virtual void TakeDamage(int damage)
        {
            CurHP -= damage;
            Console.WriteLine($"{Name}이 {damage}의 피해를 받습니다");
            Thread.Sleep(1000);
            if (CurHP <= 0)
            {
                CurHP = 0;
                Console.WriteLine($"{Name}이 쓰러집니다");
            }
        }
        public abstract char GetIcon();
        public abstract void MoveAction(int[,] map);

        protected void Move(Direction dir, int[,] map)
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
