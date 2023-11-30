using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class PlayerStat
    {
        private static PlayerStat instance;
        public char icon = 'P';

        public int CurHP { get; private set; }
        public int MaxHP { get; private set; }
        public int CurMP { get; private set; }
        public int MaxMP { get; private set; }

        public int CurExp { get; private set; }
        private int totalExp;
        public int Level { get; private set; }

        public int Damage { get; private set; }
        public int Defence {  get; private set; }
        public Monster Target { get; set; }

        private const int MAX_LEVEL = 100;
        private int[] expTable;

        private PlayerStat()
        {
            CurHP = MaxHP = 100;
            CurMP = MaxMP = 100;
            CurExp = 0;
            totalExp = 0;
            Level = 1;
            Damage = 110;

            expTable = new int[MAX_LEVEL];
            expTable[1] = 10;
            for (int i = 2; i < MAX_LEVEL; i++)
            {
                expTable[i] = (int)(expTable[i - 1] * 1.5f);
            }
        }

        public void TakeDamage(int damage)
        {
            damage = (int)(damage * (100 / (float)(100 + Defence))); //롤 방어력 공식 적용
            CurHP -= damage;
            Console.WriteLine($"플레이어가 {damage}의 피해를 받습니다");

            if (CurHP <= 0)
            {
                CurHP = 0;
                Console.WriteLine("플레이어가 쓰러집니다....");
                Thread.Sleep(2000);
                Core.Instance.SceneChange(GroupScene.GameOver);
                return;
            }
        }
        public void AddDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException();
            }
            Damage += amount;
        }
        public void MinusDamage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException();
            }
            Damage -= amount;
        }
        public void AddDefence(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException();
            }
            Defence += amount;
        }
        public void MinusDefence(int amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException();
            }
            Defence -= amount;
        }
        public void AddHP(int amount)
        {
            CurHP += amount;
            if( CurHP > MaxHP )
            {
                CurHP = MaxHP;
            }
        }
        public void GainExp(int exp)
        {
            Thread.Sleep(200);
            totalExp += exp;
            if(exp > 0)
            {
                Console.WriteLine($"{exp}만큼의 경험치를 얻었습니다");
            }
            CurExp = totalExp - expTable[Level - 1];
            if (expTable[Level] <= totalExp)
            {
                Level++;
                Console.WriteLine($"플레이어의 레벨이 {Level}이 되었습니다!!");
                GainExp(0);
            }
            else
            {
                CurExp = totalExp - expTable[Level - 1];
            }
        }
        public int GetLevelExp()
        {
            return expTable[Level + 1] - expTable[Level];
        }
        public static PlayerStat Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PlayerStat();
                }
                return instance;
            }
        }
    }
}
