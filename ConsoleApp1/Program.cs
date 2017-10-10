using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        //public static string[,] lodeInfo1 = new string[4, 4];
        //public static string[,] lodeInfo2 = new string[4, 4];
        public static int delkaPole = 10;
        public static int[,] plocha = new int[delkaPole, delkaPole];
        public static int[,] plocha2 = new int[delkaPole, delkaPole];
        public static int[,] hits = new int[2, 2];
        //0,0 = first player hit count
        //1,1 = second player miss count
        public static bool player = false; //true = player2, false = player1
        public static int phase = 0; //0 = placing warships, 1=playing
        public static int shipCtrX = 0;
        public static int shipCtrY = 0;

        enum State
        {
            Prazdno, Lod, Hit, Miss
        }

        public static int charToNum(char pismeno)
        {           
            return pismeno-65;
        }

        public static void Render(int[,]  plocha, int ctrX, int ctrY, int phase, bool player)
        {
            Console.Clear();
            //!String.IsNullOrEmpty(lodeInfo[i, a])
            
            Console.Write("Now is playing: ");
            if (player)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("PLAYER 2");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("PLAYER 1");
            }
            Console.ResetColor();
            Console.Write("\nPhase: ");
            switch (phase)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Place your warships!");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Battle!");
                    break;
                default:
                    break;               
            }
            Console.ResetColor();
            Console.Write("\n\n");
           
            //Console.WriteLine("X: " + ctrX + " Y: " + ctrY);
            for (int i = 0; i < delkaPole + 1; i++)
            {
                Console.Write(Convert.ToChar(i + 64) + " ");
            }
            Console.Write("\n");
            for (int i = 0; i < delkaPole; i++)
            {
                Console.Write(i + " ");
                for (int a = 0; a < delkaPole; a++)
                {
                    //color of selection
                    if(ctrY == i && ctrX == a)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    //different symbols
                    if (phase == 0  && ctrY == i && ctrX == a && shipCtrX == 0) 
                    {
                        Console.Write("■ ");
                    }
                    else if (phase == 0 && ctrY == i && (ctrX == a || ctrX+1 == a) && shipCtrX == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("■ ");
                    }
                    else if (phase == 0 && ctrY == i && (ctrX == a || ctrX + 1 == a || ctrX + 2 == a) && shipCtrX == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("■ ");
                    }
                    else if (phase == 0 && ctrY == i && (ctrX == a || ctrX + 1 == a || ctrX + 2 == a || ctrX + 3 == a) && shipCtrX == 3)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("■ ");
                    }
                    else if ( ( phase == 1 && ctrY == i && ctrX == a ) && ( plocha[i, a] == (int)State.Hit || plocha[i, a] == (int)State.Lod || plocha[i, a] == (int)State.Prazdno ) )
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }
                    else if ((phase == 1 && ctrY == i && ctrX == a) && plocha[i, a] == (int)State.Miss)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("0 ");
                    }
                    else if (plocha[i, a] == (int)State.Prazdno || (plocha[i, a] == (int)State.Lod && phase == 1 ) )
                    {
                        Console.Write("~ ");
                    }
                    else if ((plocha[i, a] == (int)State.Lod) && phase == 0)
                    {
                        Console.Write("■ ");
                    }
                    else if (plocha[i, a] == (int)State.Hit)
                    {
                        Console.Write("X ");
                    }
                    else if (plocha[i, a] == (int)State.Miss)
                    {
                        Console.Write("0 ");
                    }

                    Console.ResetColor();
                }
                Console.Write("\n");
            }
        }
        public static void Selection(int ctrY, int ctrX, int[,] plocha)
        {
            //hits
            if (phase == 1 && plocha[ctrX, ctrY] == (int)State.Lod)
            {
                plocha[ctrX, ctrY] = (int)State.Hit;
                if (player)
                {
                    hits[1, 0]++;

                }
                else
                {
                    hits[0, 0]++;
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;               
                Console.WriteLine("You hit enemy warship!");
                Console.ResetColor();
                Thread.Sleep(500);
                player = !player;
            }
            else if (phase == 1 && plocha[ctrX, ctrY] == (int)State.Prazdno)
            {
                plocha[ctrX, ctrY] = (int)State.Miss;
                if (player)
                {
                    hits[1, 1]++;
                    
                }
                else
                {
                    hits[0, 1]++;
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You missed enemy warship!");
                Console.ResetColor();
                Thread.Sleep(500);
                player = !player;
            }
            else if (phase == 0 && shipCtrX == 0)
            {
                //lodeInfo1[shipCtrX, shipCtrY] = ""+(char)ctrX + ctrY+"";
                plocha[ctrX, ctrY] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY>=4)
                {
                    shipCtrX++;
                    shipCtrY = 0;
                }
            }
            else if (phase == 0 && shipCtrX == 1 && ctrY < 8)
            {
                //lodeInfo1[shipCtrX, shipCtrY] = "" + (char)ctrX + ctrY + "*" + (char)ctrX+1 + ctrY;
                plocha[ctrX, ctrY] = (int)State.Lod;
                plocha[ctrX, ctrY+1] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY >= 2)
                {
                    shipCtrX++;
                    shipCtrY = 0;
                }
            }
            else if (phase == 0 && shipCtrX == 2 && ctrY < 7)
            {
                //lodeInfo1[shipCtrX, shipCtrY] = "" + (char)ctrX + ctrY + "*" + (char)ctrX + 1 + ctrY + "*" + (char)ctrX + 2 + ctrY;
                plocha[ctrX, ctrY] = (int)State.Lod;
                plocha[ctrX, ctrY+1] = (int)State.Lod;
                plocha[ctrX, ctrY+2] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY >= 2)
                {
                    shipCtrX++;
                    shipCtrY = 0;
                }
            }
            else if (phase == 0 && shipCtrX == 3 && ctrX <= 6)
            {
                //lodeInfo1[shipCtrX, shipCtrY] = "" + (char)ctrX + ctrY + "*" + (char)ctrX + 1 + ctrY + "*" + (char)ctrX + 2 + ctrY + "*" + (char)ctrX + 3 + ctrY;
                plocha[ctrX, ctrY] = (int)State.Lod;
                plocha[ctrX, ctrY+1] = (int)State.Lod;
                plocha[ctrX, ctrY+2] = (int)State.Lod;
                plocha[ctrX, ctrY+3] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY >= 1)
                {
                    shipCtrX = 0;
                    shipCtrY = 0;
                    if (!player)
                    {
                        player = !player;
                    }
                    else
                    {
                        phase++;
                        player = !player;
                    }
                    
                }
            }

            
        }
        public static int[,] FillPlocha(int [,] plocha)
        {
            for (int i = 0; i < delkaPole; i++)
            {
                for (int a = 0; a < delkaPole; a++)
                {
                    plocha[i, a] = (int)State.Prazdno;
                }
            }
            return plocha;
        }
        static void Main(string[] args)
        {
            State[] stav = new State[3];

            //základní vyplnění pole prázdnem
            plocha = FillPlocha(plocha);
            plocha2 = FillPlocha(plocha2);

            int ctrX = 0;
            int ctrY = 0;

            //selection loop
            while (true)
            {
                if (player)
                {
                    if(phase == 0)
                    {
                        Render(plocha2, ctrX, ctrY, phase, player);
                    }
                    else
                    {                        
                        Render(plocha, ctrX, ctrY, phase, player);
                    }
                }
                else
                {
                    if (phase == 0)
                    {
                        Render(plocha, ctrX, ctrY, phase, player);
                    }
                    else
                    {
                        Render(plocha2, ctrX, ctrY, phase, player);
                    }
                }

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                            
                if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
                {
                    ctrY--;
                    if (ctrY < 0)
                    {
                        ctrY = delkaPole-1;
                    }
                }
                else if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
                {
                    ctrY++;
                    if (ctrY > delkaPole - 1)
                    {
                        ctrY = 0;
                    }
                }
                else if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
                {
                    ctrX++;
                    if (ctrX > delkaPole - 1)
                    {
                        ctrX = 0;
                    }
                }
                else if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
                {
                    ctrX--;
                    if (ctrX < 0)
                    {
                        ctrX = delkaPole-1;
                    }
                    
                }
                else if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    if (player)
                    {
                        if (phase == 0)
                        {
                            Selection(ctrX, ctrY, plocha2);
                            Render(plocha2, ctrX, ctrY, phase, player);
                        }
                        else
                        {
                            Selection(ctrX, ctrY, plocha);
                            Render(plocha, ctrX, ctrY, phase, !player);
                        }
                        
                        //Render(plocha2, ctrX, ctrY, phase, !player);
                        if (phase > 0)
                        {
                            Console.Write("\n\n");
                            Console.WriteLine("3");
                            Thread.Sleep(500);
                            Console.WriteLine("2");
                            Thread.Sleep(500);
                            Console.WriteLine("1");
                            Thread.Sleep(500);
                        }
                        
                    }
                    else
                    {
                        if (phase == 0)
                        {
                            Selection(ctrX, ctrY, plocha);
                            Render(plocha, ctrX, ctrY, phase, player);
                        }
                        else
                        {
                            Selection(ctrX, ctrY, plocha2);
                            Render(plocha2, ctrX, ctrY, phase, !player);
                        }
                        
                        if (phase > 0)
                        {
                            Console.Write("\n\n");
                            Thread.Sleep(1000);
                            Console.WriteLine("3");
                            Thread.Sleep(500);
                            Console.WriteLine("2");
                            Thread.Sleep(500);
                            Console.WriteLine("1");
                            Thread.Sleep(500);
                        }
                        
                    }
                    
                    //break;
                }

            }
            
            
        }
    }
}
