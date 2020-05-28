using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class Second
    {
        /*
        Сгенерировать массив из 100 купюр произвольным образом (купюры могут
        быть номиналом 1, 2, 5, 10, 20, 50, 100 единиц). Отсортировать массив
        алгоритмом сортировки подсчетами и вывести на экран.
        */
        public static void Execute()
        {
            int[] values = { 1, 2, 5, 10, 20, 50, 100 };
            int[] bills = new int[100];
            Random rand = new Random();
            Console.WriteLine("Bills:");
            for (int i = 0; i < bills.Length; i++)
            {
                bills[i] = values[rand.Next(0, values.Length)];
                Console.Write(bills[i] + " ");
            }
            Console.WriteLine("\n\nSorted:");
            Sort(bills, 0, bills.Length -1);
            foreach (int i in bills)
                Console.Write(i + " ");
        }
        static void Sort(int[] array, int left, int right)
        {
            int min = 0, max = 0;

            for (int i = left; i <= right; i++)
                if (array[i] < min) min = array[i];
                else if (array[i] > max) max = array[i];

            int bn = max - min + 1;

            int[] buckets = new int[bn];

            for (int i = left; i <= right; i++)
                buckets[array[i] - min]++;

            int idx = 0;
            for (int i = min; i <= max; i++)
                for (int j = 0; j < buckets[i - min]; j++)
                    array[idx++] = i;
        }

    }
}
