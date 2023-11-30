using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SceneBattle : Scene
    {
        private Random random;
        private Monster monster;
        private PlayerStat player;
        public override void Init()
        { 
            random = new Random();
            player = PlayerStat.Instance;
            playerPos = null;
            monster = null;
        }

        public override void Release()
        {

        }

        public override void Enter()
        {
            monster = player.Target;
            Console.Clear();
            Console.WriteLine($"{monster.Name}와 전투를 개시합니다!!");
            Thread.Sleep( 800 );
        }

        public override void Exit()
        {
        }


        public override void Render()
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("============전투 중============\n");
            sb.AppendLine($"{monster.Name}:   {monster.Level} 레벨");
            sb.AppendLine($"체력: {monster.CurHP} / {monster.MaxHP}");
            sb.AppendLine($"마나: {monster.CurMP} / {monster.MaxMP}");
            sb.AppendLine($"공격력: {monster.Damage}");

            Console.WriteLine(sb.ToString());
            PrintPlayerStat(20, 2);
        }

        public override void Update()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("선택하세요!!\n");
            sb.AppendLine("1. 공격");
            sb.AppendLine("2. 방어");
            sb.AppendLine("3. 도망가기");
            sb.AppendLine("4. 인벤토리 열기");
            int command = InputInt(1, 4, sb.ToString());

            Action action;
            switch(command)
            {
                case 1:
                    action = CommandAttack;
                    break;
                case 2:
                    action = CommandDefence;
                    break;
                case 3:
                    action = CommandRun;
                    break;
                case 4:
                    action = CommandInven;
                    break;
                default:
                    action = CommandRun;
                    break;
            }
            action();
        }
        private void CommandAttack()
        {
            Console.WriteLine("공격 개시!!");
            player.TakeDamage(monster.Damage);
            monster.TakeDamage(player.Damage);

            if(monster.CurHP <= 0)
            {
                GetReward();
            }
            Thread.Sleep(1000);
        }
        private void GetReward()
        {
            Console.Clear();
            Console.WriteLine("승리했습니다!!");
            player.GainExp(monster.Reward);

            List<Item> items = monster.DropItem();
            Inventory.Instance.AddItem(items);

            foreach(Item item in items)
            {
                Console.WriteLine($"획득! {item.Name}");
                Thread.Sleep(200);
            }
            Thread.Sleep(1000);
            Core.Instance.SceneChange(GroupScene.Prev);
        }
        private void CommandDefence()
        {

        }
        private void CommandRun()
        {
            int randNum = random.Next(0, 10);
            if (randNum < 5 && monster.Level > player.Level)
            {
                Console.WriteLine("도망에 실패하였습니다..");
                Thread.Sleep(1000);
                return;
            }

            Console.WriteLine("도망에 성공하였습니다!");
            Core.Instance.SceneChange(GroupScene.Prev);
            Thread.Sleep(1000);
        }
        private void CommandInven()
        {
            Core.Instance.SceneChange(GroupScene.Inventory);
        }
    }
}
