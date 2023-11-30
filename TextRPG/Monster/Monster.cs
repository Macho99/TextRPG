using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Monster
    {
        public Point pos;
        protected Random random = new Random();

        public string Name {  get; private set; }
        public int CurHP { get; protected set; }
        public int MaxHP { get; private set; }
        public int CurMP { get; private set; }
        public int MaxMP { get; private set; }

        public int Level { get; private set; }

        public int Damage { get; private set; }
        public int Defence {  get; private set; }
        public int Reward {  get; private set; }

        protected abstract List<(ItemID, float)> getDropTable();
        public List<Item> DropItem(float chanceMultiply = 1.0f)
        {
            List<(ItemID, float)> dropTable = getDropTable();
            List<Item> result = new List<Item>();
            foreach(var item in dropTable)
            {
                float chance = (float)random.NextDouble() * chanceMultiply;
                if(chance > item.Item2)
                {
                    result.Add(Data.Instance.dictItem[item.Item1].Clone());
                }
            }
            return result;
        }
        public Monster(Point pos, string name, int maxHP, int maxMP, int level, int damage, int defence, int reward)
        {
            Name = name;
            this.pos = pos;
            CurHP = MaxHP = maxHP;
            CurMP = MaxMP = maxMP;
            Level = level;
            Damage = damage;
            Defence = defence;
            Reward = reward;
        }
        public virtual void TakeDamage(int damage)
        {
            damage = (int)(damage * (100 / (float) (100 + Defence))); //롤 방어력 공식 적용
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
            Point prevPos = pos;
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
