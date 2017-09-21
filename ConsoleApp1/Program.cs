using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static int charToNum(char pismeno)
        {           
            return pismeno-65;
        }
        public static void Render(int delkaPole, int[,]  plocha)
        {
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
                }
                Console.Write("\n");
            }
        }
        enum State
        {
            Prazdno, Lod, Hit, Miss
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
            string[,] lodeInfo = new string[5,4];
            
            plocha[0, 0] = (int)State.Lod;

            Render(delkaPole, plocha);
            
        }
    }
}
