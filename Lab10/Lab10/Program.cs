using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class Program
    {
        static void Main(string[] args)
        {
            int select = 0;
            while (select < 1 || select > 4)
            {
                Console.Write("Choose the task from 1 to 4: ");
                select = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            switch (select)
            {
                case 1:
                    Console.WriteLine("The first task: \n");
                    First.Execute();
                    break;
                case 2:
                    Console.WriteLine("The second task: \n");
                    Second.Execute();
                    break;
                case 3:
                    Console.WriteLine("The third task: \n");
                    Third.Execute();
                    break;
                case 4:
                    Console.WriteLine("The fourth task: \n");
                    Fourth.Execute();
                    break;
                default:
                    break;
            }
        }
    }
}