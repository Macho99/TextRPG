using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Player
    {
        private static Player instance;
        public char icon = 'P';

        public int CurHP { get; private set; }
        public int MaxHP { get; private set; }
        public int CurMP { get; private set; }
        public int MaxMP { get; private set; }

        public int CurExp { get; private set; }
        public int Level { get; private set; }

        public int Damage { get; private set; }
        public Monster Target { get; set; }

        private const int MAX_LEVEL = 100;
        private int[] expTable;

        public void TakeDamage(int damage)
        {
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
        public void GainExp(int exp)
        {

        }
        private Player()
        {
            CurHP = MaxHP = 100;
            CurMP = MaxMP = 100;
            CurExp = 0;
            Level = 1;
            Damage = 10;

            expTable = new int[MAX_LEVEL];
            expTable[0] = 10;
            for (int i=1;i<MAX_LEVEL; i++)
            {
                expTable[i] = (int)(expTable[i] * 1.5f);
            }
        }
        
        public static Player Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }
    }
}
