using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SceneTitle : Scene
    {
        public override void Init()
        {
            playerPos = null;
        }

        public override void Release()
        {

        }
        public override void Enter()
        {

        }

        public override void Exit()
        {

        }


        public override void Render()
        {
            Console.ForegroundColor = ConsoleColor.White;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. 게임 시작");
            sb.AppendLine("2. 게임 종료");
            sb.Append("메뉴를 선택하세요: ");
            Console.Write(sb.ToString());
        }

        public override void Update()
        {
            int input = InputInt(1, 2);
            switch (input)
            {
                case 1:
                    EventManager.Instance.ReserveChangeScene(GroupScene.Map1);
                    break;
                case 2:
                    EventManager.Instance.ReserveChangeScene(GroupScene.GameOver);
                    break;
            }
        }
    }
}
