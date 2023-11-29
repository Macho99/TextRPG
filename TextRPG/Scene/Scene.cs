using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class Scene
    {
        public abstract void Init();
        public abstract void Release();
        public abstract void Update();
        public abstract void Render();
        public abstract void Enter();
        public abstract void Exit();
        protected int InputInt(int min, int max, in string msg = null)
        {
            while (true)
            {
                int command;
                if(msg != null)
                {
                    Console.WriteLine(msg);
                }

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out command))
                {
                    Console.WriteLine($"올바른 숫자로 입력해주세요");
                    continue;
                }
                if (command < min && command > max)
                {
                    Console.WriteLine($"{min} ~ {max}사이로 입력해주세요");
                    continue;
                }

                return command;
            }
        }
        protected ConsoleKeyInfo InputKey(in string msg = null)
        {
            if(msg != null)
                Console.WriteLine(msg);
            return Console.ReadKey();
        }
        protected string InputString()
        {
            throw new NotImplementedException();
        }
        protected Direction InputDirection()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Direction dir;
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    dir = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    dir = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    dir = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    dir = Direction.Right;
                    break;
                default:
                    dir = Direction.None;
                    break;
            }
            return dir;
        }
        protected void PrintMap(in int[,] map)
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            for(int i =0;i<map.GetLength(0);i++)
            {
                for(int j=0;j<map.GetLength(1);j++)
                {
                    sb.Append(Data.Instance.mapCharArr[map[i,j]]).Append(' ');
                }
                sb.AppendLine();
            }
            sb.AppendLine();
            Console.WriteLine(sb.ToString());
        }
        protected void PrintObject(Position pos, char icon)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(pos.x * 2, pos.y);
            Console.Write(icon);
        }
        protected void PrintPlayerStat(int xPos, int yPos = 5)
        {
            Console.CursorVisible = false;
            Player p = Player.Instance;
            Console.SetCursorPosition(xPos, yPos++);
            Console.Write($"플레이어:   {p.Level} 레벨");
            Console.SetCursorPosition(xPos, yPos++);
            Console.Write($"체력: {p.CurHP} / {p.MaxHP}");
            Console.SetCursorPosition(xPos, yPos++);
            Console.Write($"마나: {p.CurMP} / {p.MaxMP}");
            Console.SetCursorPosition(xPos, yPos++);
            Console.Write($"공격력: {p.Damage}");
            Console.SetCursorPosition(xPos, yPos++);
            Console.Write($"경험치: {p.CurExp} / {p.GetLevelExp()}");
            Console.WriteLine();
        }
    }
}
