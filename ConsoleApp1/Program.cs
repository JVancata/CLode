using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static string[,] lodeInfo1 = new string[4, 4];
        public static string[,] lodeInfo2 = new string[4, 4];
        public static int delkaPole = 10;
        public static int[,] plocha = new int[delkaPole, delkaPole];
        public static bool player = false; //true = player2, false = player1 XD
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

        public static void Render(int[,]  plocha, int ctrX, int ctrY, int phase)
        {
            Console.Clear();
            //!String.IsNullOrEmpty(lodeInfo[i, a])
           
            Console.WriteLine("X: " + ctrX + " Y: " + ctrY);
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
                    else if (phase == 1 && ctrY == i && ctrX == a && !player)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }
                    else if (plocha[i, a] == (int)State.Prazdno)
                    {
                        Console.Write("~ ");
                    }
                    else if ((plocha[i, a] == (int)State.Lod))
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
        public static void Selection(int ctrY, int ctrX, int delka)
        {
            //hits
            if (phase == 1 && plocha[ctrX, ctrY] == (int)State.Lod)
            {
                plocha[ctrX, ctrY] = (int)State.Hit;
            }
            else if (phase == 1 && plocha[ctrX, ctrY] == (int)State.Prazdno)
            {
                plocha[ctrX, ctrY] = (int)State.Miss;
            }


            else if (phase == 0 && shipCtrX == 0 && !player)
            {
                lodeInfo1[shipCtrX, shipCtrY] = ""+(char)ctrX + ctrY+"";
                plocha[ctrX, ctrY] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY>=4)
                {
                    shipCtrX++;
                    shipCtrY = 0;
                }
            }
            else if (phase == 0 && shipCtrX == 1 && !player && ctrY < 8)
            {
                lodeInfo1[shipCtrX, shipCtrY] = "" + (char)ctrX + ctrY + "*" + (char)ctrX+1 + ctrY;
                plocha[ctrX, ctrY] = (int)State.Lod;
                plocha[ctrX, ctrY+1] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY >= 2)
                {
                    shipCtrX++;
                    shipCtrY = 0;
                }
            }
            else if (phase == 0 && shipCtrX == 2 && !player && ctrY < 7)
            {
                lodeInfo1[shipCtrX, shipCtrY] = "" + (char)ctrX + ctrY + "*" + (char)ctrX + 1 + ctrY + "*" + (char)ctrX + 2 + ctrY;
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
            else if (phase == 0 && shipCtrX == 3 && !player && ctrX <= 6)
            {
                lodeInfo1[shipCtrX, shipCtrY] = "" + (char)ctrX + ctrY + "*" + (char)ctrX + 1 + ctrY + "*" + (char)ctrX + 2 + ctrY + "*" + (char)ctrX + 3 + ctrY;
                plocha[ctrX, ctrY] = (int)State.Lod;
                plocha[ctrX, ctrY+1] = (int)State.Lod;
                plocha[ctrX, ctrY+2] = (int)State.Lod;
                plocha[ctrX, ctrY+3] = (int)State.Lod;
                shipCtrY++;
                if (shipCtrY >= 1)
                {
                    shipCtrX++;
                    shipCtrY = 0;
                    phase++;
                }
            }

            
        }
        
        static void Main(string[] args)
        {
            State[] stav = new State[3];
            
            
            

            //základní vyplnění pole prázdnem
            for (int i = 0; i < delkaPole; i++)
            {
                for (int a = 0; a < delkaPole; a++)
                {
                    plocha[i, a] = (int) State.Prazdno;
                }
            }

            int ctrX = 0;
            int ctrY = 0;
            
            

            //selection loop
            while (true)
            {
                Render(plocha, ctrX, ctrY, phase);
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                            
                if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
                {
                    ctrY--;
                    if (ctrY < 0)
                    {
                        ctrY = delkaPole-1;
                    }
                    
                    Render(plocha, ctrX, ctrY, phase);
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
                    Selection(ctrX, ctrY, delkaPole);
                    //break;
                }

            }
            
            
        }
    }
}
