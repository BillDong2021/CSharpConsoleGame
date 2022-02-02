using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightChess
{
    class Program
    {
    

        static void Main(string[] args)
        {
            Game.GenerateHearder();
            Player player1 = new Player();
            Player player2 = new Player();
            Console.WriteLine("请输入玩家A的姓名");
            player1.Name = Console.ReadLine();
            Console.WriteLine("请输入玩家B的姓名");
            player2.Name = Console.ReadLine();
            Console.Clear();
            Game.GenerateHearder();
            Console.WriteLine("{0}的飞机用A表示，{1}的飞机用B表示", player1.Name, player2.Name);
            Console.WriteLine("游戏正式开始，请按任意键继续");
            Console.ReadKey(true);
            Game.Play(ref player1, ref player2);
            if (player1.Position>player2.Position)
            {
                Console.WriteLine("游戏结束！玩家{0}胜利！", player1.Name);
            }
            else
                Console.WriteLine("游戏结束！玩家{0}胜利！", player1.Name);
            Console.ReadKey(true);
        }
    }
    class Game
    {
        static int mapLength = 100;//用静态字段模拟全局变量（因为该字段会被多个方法反复使用，而且值不会改变，也不会暴露给外界）
        static int[] mapArray = Map.GenerateMapArray(mapLength); //静态字段在声明时就应初始化。mapArray是不变的，但是playerPosition是会变的
        public static void GenerateHearder()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------欢迎来到飞行棋游戏，版本1.0---------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--------------------------------------------");
        }
        public static void DrawMap(int[] playerPosition)
        {
            Console.WriteLine("图例:□普通格子，◎幸运轮盘，☆地雷，▲暂停，卐时空隧道");
            int longerLine = 30;
            int shorterLine = 5;
            //画第一横行 
            for (int i = 0; i < longerLine; i++)
            {
                Map.DrawCell(playerPosition, mapArray, i);
            }
            Console.WriteLine();
            //画第一竖列
            for (int i = longerLine; i < longerLine + shorterLine; i++)
            {
                for (int j = 0; j < longerLine-1; j++)
                {
                    Console.Write("　");
                }
                Map.DrawCell(playerPosition, mapArray, i);
                Console.WriteLine();
            }
            //画第二横行
            for (int i = 2 * longerLine+shorterLine - 1; i >= longerLine + shorterLine ; i--)
            {
                Map.DrawCell(playerPosition, mapArray, i);
            }
            Console.WriteLine();
            //画第二竖行
            for (int i = 2 * longerLine + shorterLine ; i < 2 * longerLine +2 * shorterLine; i ++)
            {
                Map.DrawCell(playerPosition, mapArray, i);
                for (int j = 0; j < longerLine - 1; j++)
                {
                    Console.Write("　");
                }
                Console.WriteLine();
            }
            //画第三横行
            for (int i = 2 * longerLine + 2 * shorterLine; i < mapLength; i++)
            {
                Map.DrawCell(playerPosition, mapArray, i);
            }
        }

        public static void Play(ref Player player1,ref Player player2)
        {
            int[] playerPosition = { player1.Position, player2.Position };
            bool flag1 = true;
            bool flag2 = true;
            while (player1.Position!=99 && player2.Position!=99)
            {
                if (flag1)
                {
                    Console.Clear();
                    Game.GenerateHearder();
                    playerPosition[0] = player1.Position;
                    playerPosition[1] = player2.Position;
                    Game.DrawMap(playerPosition);
                    Console.WriteLine();
                    Game.GoAhead(ref player1);
                    Game.CheckPosition(ref player1, ref player2, ref flag1);
                }
                else
                {
                    flag1 = true;
                }
                if (player1.Position == 99)
                {
                    break;
                }    

                if (flag2)
                {
                    Console.Clear();
                    Game.GenerateHearder();
                    playerPosition[0] = player1.Position;
                    playerPosition[1] = player2.Position;
                    Game.DrawMap(playerPosition);
                    Console.WriteLine();
                    Game.GoAhead(ref player2);
                    Game.CheckPosition(ref player2, ref player1, ref flag2);
                }
                else
                {
                    flag2 = true; 
                }
                
            }
            
        }
        public static void GoAhead(ref Player player)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("请玩家{0}按任意键掷骰子", player.Name);
            Console.ReadKey(true);
            Random rd = new Random();
            int randNum = rd.Next(1, 6);
            player.Position = player.Position + randNum;
            Console.WriteLine("玩家{0}掷出{1}，前进至第{2}格", player.Name, randNum, player.Position+1);
        }
        public static void CheckPosition(ref Player player1, ref Player player2, ref bool flag)
        {
            //player1表示当前回合操作的玩家，player2表示另一个玩家
            switch (mapArray[player1.Position])
            {
                case 1://幸运轮盘，请选择 1--交换位置 2--轰炸对方
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("玩家{0}踩到了幸运轮盘◎，请选择 1--交换位置 2--轰炸对方", player1.Name);
                    string input = Console.ReadLine();
                    while (input != "1" && input != "2")
                    {
                        Console.WriteLine("输入错误，请重新输入");
                        input = Console.ReadLine();
                    }
                    if (input == "1")
                    {
                        Console.WriteLine("玩家{0}选择与玩家{1}交换位置",player1.Name, player2.Name);
                        int temp = player2.Position;
                        player2.Position = player1.Position;
                        player1.Position = temp;
                        Console.WriteLine("交换成功，请按任意键继续游戏");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.WriteLine("玩家{0}选择轰炸对方，玩家{1}后退4格", player1.Name, player2.Name);
                        player2.Position = player2.Position - 4;
                        Console.WriteLine("轰炸成功，请按任意键继续游戏");
                        Console.ReadKey(true);
                    }
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("玩家{0}踩到了地雷☆，后退6格", player1.Name);
                    player1.Position = player1.Position - 6;
                    Console.WriteLine("请按任意键继续游戏");
                    Console.ReadKey(true);
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("玩家{0}踩到了暂停▲，暂停一回合", player1.Name);
                    flag = false;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("玩家{0}踩到了时空隧道，前往下一个时空隧道所在地", player1.Name);
                    int index = Array.IndexOf(mapArray, 4, player1.Position+1);
                    if (index != -1)
                    {
                        player1.Position = index;
                        Console.WriteLine("玩家{0}已到达下一个时空隧道所在位置", player1.Name);
                    }
                    else
                        Console.WriteLine("玩家{0}已到达最后一个时空隧道，位置不变", player1.Name);
                    Console.WriteLine("请按任意键继续游戏");
                    Console.ReadKey(true);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("玩家{0}踩到了普通格子□，安全", player1.Name);
                    Console.WriteLine("请按任意键继续游戏");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
    class Map
    {
        public static int[] GenerateMapArray(int mapLength)
        {
            int[] mapArray = new int[mapLength];
            List<int> tempList = new List<int>();
            //生成幸运轮盘
            int luckyTurnNumber = 5;
            int[] luckyTurn = Map.GenerateSpecialCell(ref tempList, luckyTurnNumber);
            for (int i = 0; i < luckyTurnNumber; i++)
            {
                mapArray[luckyTurn[i]] = 1;
            }

            //生成地雷
            int landMineNumber = 5;
            int[] landMine = Map.GenerateSpecialCell(ref tempList, landMineNumber);
            for (int i = 0; i < landMineNumber; i++)
            {
                mapArray[landMine[i]] = 2;
            }

            //生成暂停
            int pauseNumber = 5;
            int[] pause = Map.GenerateSpecialCell(ref tempList, pauseNumber);
            for (int i = 0; i < pauseNumber; i++)
            {
                mapArray[pause[i]] = 3;
            }

            //生成时空隧道
            int timeTunnelNumber = 5;
            int[] timeTunnel = Map.GenerateSpecialCell(ref tempList, timeTunnelNumber);
            for (int i = 0; i < timeTunnelNumber; i++)
            {
                mapArray[timeTunnel[i]] = 4;
            }
            return mapArray;
        }
        public static int[] GenerateSpecialCell(ref List<int> tempList, int length)
        {
            int[] index = new int[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int randNum = rd.Next(1, 100);
                while (tempList.Contains(randNum))
                {
                    randNum = rd.Next(1, 100);
                }
                tempList.Add(randNum);
                index[i] = randNum;
            }
            return index;
        }
        public static void DrawSpecialCell(int i)
        {
            switch (i)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("◎");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("☆");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("▲");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("卐");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("□");
                    break;
            }
        }
        public static void DrawCell(int[] playerPosition, int[] mapArray, int i)
        {
            if (playerPosition[0] == playerPosition[1] && playerPosition[0] == i)
            {
                Console.Write("<>");
            }
            else if (playerPosition[0] == i)
            {
                Console.Write("Ａ");
            }
            else if (playerPosition[1] == i)
            {
                Console.Write("Ｂ");
            }
            else
            {
                Map.DrawSpecialCell(mapArray[i]);
            }
        }
    }
    class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                if (value.Length != 0)
                {
                    name = value;
                }
                else
                    throw new Exception("玩家姓名不能为空");
            }
        }

        private int position = 0;
        public int Position
        {
            get { return position; }
            set 
            {
                if (value >= 0 && value <= 99)
                {
                    position = value;
                }
                else if (value>99)
                {
                    position = 99;
                }
                    else
                {
                    position = 0;
                }
            }
        }
    }
}
