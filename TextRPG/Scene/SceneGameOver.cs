using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SceneGameOver : Scene
    {
        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Init()
        {

        }

        public override void Release()
        {

        }

        public override void Render()
        {
            Console.WriteLine("게임을 종료합니다....");
        }

        public override void Update()
        {
            Core.Instance.GameOver();
        }
    }
}
