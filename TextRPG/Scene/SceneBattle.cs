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
            sb.AppendLine("2. 반격기");
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
                    action = CommandCounterAttack;
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
        private void Attack(int playerDamage, int monsterDamage)
        {
            if(monsterDamage > 0)
            {
                player.TakeDamage(monsterDamage);
            }
            if(playerDamage > 0)
            {
                monster.TakeDamage(playerDamage);
            }

            Thread.Sleep(2000);
            if(player.CurHP <= 0)
            {
                Thread.Sleep(2000);
                EventManager.Instance.ReserveChangeScene(GroupScene.GameOver);
                return;
            }
            else if (monster.CurHP <= 0)
            {
                GetReward();
            }
        }
        private int AttackRoulette()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.WriteLine("공격의 강도를 결정합니다!! 아무키나 누르세요");

            int count = 1;
            // 룰렛 돌리는 작업을 시작합니다.
            Task spinningTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    Console.Write("■■■■");
                    await Task.Delay(50); // 0.05초마다 증가
                    count += random.Next(1,3);
                    if(count > 10)
                    {
                        count = 1;
                        Console.Write($"\r                                        \r");
                    }
                }
            });

            // 아무 키를 누를 때까지 대기합니다.
            Console.ReadKey(true);
            cts.Cancel(); // 룰렛 돌리는 작업을 취소합니다.
            Console.WriteLine($"\n공격이 {count}만큼 강해집니다!");
            Thread.Sleep(200);
            return count;
        }
        private void CommandAttack()
        {
            Console.WriteLine("공격 개시!");
            int spin = AttackRoulette();
            Attack(player.Damage + spin, monster.Damage);
        }
        private void CommandCounterAttack()
        {
            Console.WriteLine("반격기 사용!");
            bool result = CounterAttackRoulette();
            if(result)
            {
                Console.WriteLine($"{monster.Name}에게 더 강한 공격을 선사합니다!");
                Attack(player.Damage * 2, 0);
            }
            else
            {
                Console.WriteLine($"{monster.Name}에게 빈틈을 보였습니다.");
                Attack(0, monster.Damage * 2);
            }
        }
        private bool CounterAttackRoulette()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.WriteLine("화면에 마름모가 가득 찼을 때 아무키나 누르세요!!");

            int count = random.Next(1, 19);
            int threshold = 5;
            // 룰렛 돌리는 작업을 시작합니다.
            Task spinningTask = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(100); // 0.1초마다 증가
                    count += random.Next(1, 4);
                    if (count <= threshold)
                    {
                        Console.Write("\r■■■■■■■■■■■■■■");
                    }
                    else if (count < 20)
                    {
                        Console.Write("\r◇◇◇◇◇◇◇◇◇◇◇◇◇◇");
                    }
                    else {
                        count = 1;
                    }
                }
            });
            // 아무 키를 누를 때까지 대기합니다.
            Console.ReadKey(true);
            cts.Cancel(); // 룰렛 돌리는 작업을 취소합니다.\
            Thread.Sleep(100);
            if(count <= threshold)
            {
                Console.Write("\r■■■■■■■■■■■■■■");
                Console.WriteLine("\n반격에 성공했습니다!!");
                return true;
            }
            else
            {
                Console.Write("\r◇◇◇◇◇◇◇◇◇◇◇◇◇◇");
                Console.WriteLine("\n반격에 실패했습니다....");
                return false;
            }
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
            EventManager.Instance.ReserveChangeScene(GroupScene.Prev);
        }
        private void CommandRun()
        {
            int randNum = random.Next(0, 10);
            if (randNum < 9 && monster.Level > player.Level)
            {
                Console.WriteLine("도망에 실패하였습니다..");
                Attack(0, monster.Damage);
                Thread.Sleep(1000);
                return;
            }

            Console.WriteLine("도망에 성공하였습니다!");
            EventManager.Instance.ReserveChangeScene(GroupScene.Prev);
            Thread.Sleep(1000);
        }
        private void CommandInven()
        {
            EventManager.Instance.ReserveChangeScene(GroupScene.Inventory);
        }
    }
}
