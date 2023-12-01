using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SceneInventory : Scene
    {
        Inventory inventory;
        public override void Init()
        {
            playerPos = null;
            inventory = Inventory.Instance;
        }

        public override void Release()
        {
            inventory = null;
        }

        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("인벤토리로 진입합니다..");
            Thread.Sleep(500);
        }

        public override void Exit()
        {
            Console.WriteLine("이전으로 돌아갑니다");
            Thread.Sleep(500);
        }

        public override void Render()
        {
            Console.Clear();
            inventory.PrintItems();

            PrintPlayerStat(60, 2);
        }

        public override void Update()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. 아이템 사용하기");
            sb.AppendLine("2. 아이템 이름순 정렬하기");
            sb.AppendLine("3. 아이템 종류순 정렬하기");
            sb.AppendLine("4. 돌아가기\n");
            sb.Append("명령을 입력해주세요: ");
            int input = InputInt(1, 4, sb.ToString());
            switch(input) {
                case 1:
                    UseItem();
                    break;
                case 2:
                    inventory.SortName();
                    break;
                case 3:
                    inventory.SortType();
                    break;
                case 4:
                    EventManager.Instance.ReserveChangeScene(GroupScene.Prev);
                    break;
            }
        }
        private void UseItem()
        {
            int input = InputInt(Math.Min(1, inventory.ItemCount()), inventory.ItemCount(), "사용하기 원하는 아이템을 고르세요: ");
            inventory.UseItem(input - 1);
            Thread.Sleep(1000);
        }
    }
}