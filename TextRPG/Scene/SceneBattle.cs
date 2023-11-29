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
        private Player player;
        public override void Init()
        { 
            random = new Random();
            player = Player.Instance;
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
            Thread.Sleep( 1500 );
        }

        public override void Exit()
        {
            Console.WriteLine("전투를 종료합니다");
            Thread.Sleep(1500);
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
            int command = InputInt(1, 3, sb.ToString());

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
                default:
                    action = CommandRun;
                    break;
            }
            action();
            InputKey("아무 키나 입력하세요.");
        }
        private void CommandAttack()
        {
            Console.WriteLine("공격 개시!!");
            monster.TakeDamage(player.Damage);
            player.TakeDamage(monster.Damage);

            if(monster.CurHP <= 0)
            {
                player.GainExp(monster.Reward);
                Core.Instance.SceneChange(GroupScene.Prev);
            }
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
                return;
            }

            Console.WriteLine("도망에 성공하였습니다!");
            Core.Instance.SceneChange(GroupScene.Prev);
        }
    }
}
