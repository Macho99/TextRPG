using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class SceneMapParent : Scene
    {
        protected int[,] map;
        protected PlayerStat player;
        protected List<Monster> monsters = new List<Monster>();
        protected bool cleared;

        protected abstract void SetMapAndPlayer();
        protected abstract void AddMonster();
        public override void Init()
        {
            playerPos = new Player();
            player = PlayerStat.Instance;
            cleared = false;
            SetMapAndPlayer();
            AddMonster();
        }

        public override void Release()
        {

        }

        public override void Enter()
        {
            foreach (Monster monster in monsters)
            {
                if(monster.CurHP <= 0)
                {
                    monsters.Remove(monster);
                    break;
                }
            }
            if(monsters.Count == 0)
            {
                cleared = true;
                OpenPotal();
            }
        }
        protected abstract void OpenPotal();

        public override void Exit()
        {

        }

        public override void Render()
        {
            PrintMap(map);

            Console.ForegroundColor = ConsoleColor.Green;
            foreach(Monster monster in monsters)
            {
                PrintObject(monster.pos, monster.GetIcon());
            }

            Console.ForegroundColor = ConsoleColor.Red;
            PrintObject(playerPos.pos, player.icon);

            Console.ForegroundColor = ConsoleColor.White;
            PrintPlayerStat(map.GetLength(1) * 2 + 1);
        }
        protected abstract void GoNextScene();
        public override void Update()
        {
            ProcessInput();
            if (cleared)
            {
                if(map[playerPos.pos.y, playerPos.pos.x] == 2)
                {
                    GoNextScene();
                }
            }
            else
            {
                if (DetectBattle() == true) return;

                foreach (Monster monster in monsters)
                {
                    monster.MoveAction(map);
                }
                DetectBattle();
            }
        }
        private void ProcessInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.I)
            {
                EventManager.Instance.ReserveChangeScene(GroupScene.Inventory);
                return;
            }

            Direction dir = KeyToDirection(keyInfo);

            playerPos.Move(dir, map);
        }
        private bool DetectBattle()
        {
            foreach (Monster monster in monsters)
            {
                if (monster.pos == playerPos.pos)
                {
                    player.Target = monster;
                    EventManager.Instance.ReserveChangeScene(GroupScene.Battle);
                    return true;
                }
            }
            return false;
        }
        public void PrintMap(in int[,] map)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    sb.Append(Data.Instance.mapCharArr[map[i, j]]).Append(' ');
                }
                sb.AppendLine();
            }
            sb.AppendLine();
            Console.WriteLine(sb.ToString());
        }
    }
}
