using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        enum State
        {
            Prazdno, Lod, Hit, Miss
        }
        public static int charToNum(char pismeno)
        {           
            return pismeno-65;
        }
        public static void Render(int delkaPole, int[,]  plocha, int ctrX, int ctrY)
        {
            Console.Clear();
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
                    if (plocha[i, a] == (int)State.Prazdno)
                    {
                        Console.Write("~ ");
                    }
                    else if (plocha[i, a] == (int)State.Lod)
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
        public static void Selection(int ctrX, int ctrY, int delka)
        {
            ctrX = ctrX % delka;

        }
        
        static void Main(string[] args)
        {
            State[] stav = new State[3];
            int delkaPole = 10;
            int[,] plocha = new int[delkaPole, delkaPole];
            //(int) State.Prazdno
            //základní vyplnění pole prázdnem
            for (int i = 0; i < delkaPole; i++)
            {
                for (int a = 0; a < delkaPole; a++)
                {
                    plocha[i, a] = (int) State.Prazdno;
                }
            }
            //Console.WriteLine(charToNum('A'));
            //string test = "A0";
            //Console.WriteLine(charToNum(test[0])+"-"+test[1]);
            string[,] lodeInfo = new string[4,4];
            lodeInfo[0, 1] = "A0";
            lodeInfo[0, 1] = "A1";
            //!String.IsNullOrEmpty(lodeInfo[i, a])
            /*for (int i = 0; i < lodeInfo.GetLength(0); i++)
            {
                for (int a = 0; a < lodeInfo.GetLength(1); a++)
                {
                    

                    if (!String.IsNullOrEmpty(lodeInfo[i, a]))
                    {
                        Console.WriteLine(lodeInfo[i, a]);
                    }
                    
                }
            }*/
            //plocha[0, 0] = (int)State.Lod;
            int ctrX = 0;
            int ctrY = 0;
            //int selected;
            while (true)
            {
                Render(delkaPole, plocha, ctrX, ctrY);
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                //Console.WriteLine(consoleKeyInfo.Key);
                //Console.ReadLine();                
                if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
                {
                    ctrY--;
                    if (ctrY < 0)
                    {
                        ctrY = delkaPole;
                    }
                    Render(delkaPole, plocha, ctrX, ctrY);
                }
                else if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
                {
                    ctrY++;
                }
                else if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
                {
                    ctrX++;
                }
                else if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
                {
                    ctrX--;
                    if (ctrX < 0 )
                    {
                        ctrX = delkaPole;
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
