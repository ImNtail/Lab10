using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab10
{
    public class First
    {
        /*
        Реализовать в виде отдельных функций алгоритмы сортировки элементов
        массива (четные номера вариантов – по возрастанию, нечетные номера –
        по убыванию): слиянием, пирамидальная, быстрая. Каждую функцию
        вызвать 3 раза для разных входных данных: 1) массив из 100 000 элементов
        типа int, сгенерированный случайным образом; 2) тот же массив,
        отсортированный в порядке возрастания элементов; 3) тот же массив,
        отсортированный в порядке убывания элементов. Вывести на консоль и
        сравнить время работы всех алгоритмов в каждом случае ( «секунды :
        миллисекунды» ). Вывести количество сравнений и перестановок элементов
        для каждого метода сортировки во всех трех случаях. Результаты
        сортировки программно записать в файл sorted.dat. Программно проверить,
        что данные были действительно отсортированы.
        */
        static TimeSpan workTime;
        static ulong countOfTranspositions = 0, countOfComparisons = 0;
        static DateTime startTime, endTime;
        const int length = 100000;
        static int cntOfCalls = 0;
        public static void Execute()
        {
            Random rand = new Random();
            int[] originalArray = new int[length], originalSortedArray = new int[length], originalReversSortedArray = new int[length], array = new int[length], sortedArray = new int[length], reverseSortedArray = new int[length], arrayForCheck = new int[length];
            for (int i = 0; i < length; i++)
            {
                originalArray[i] = rand.Next(-999, 999);
                array[i] = originalArray[i];
                originalSortedArray[i] = array[i];
                originalReversSortedArray[i] = array[i];
            }
            Console.WriteLine("First array is created\n");
            mergeSort(originalSortedArray, 0, originalSortedArray.Length - 1);
            Array.Reverse(originalSortedArray);
            Console.WriteLine("Sorted array is created\n");
            mergeSort(originalReversSortedArray, 0, originalReversSortedArray.Length - 1);
            using (StreamReader readArray = new StreamReader(new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sorted.dat"), FileMode.OpenOrCreate)))
            {
                try
                {
                    try
                    {
                        bool sorted = true;
                        Console.WriteLine("Reading array from file...");
                        for (int i = 0; i < arrayForCheck.Length; i++)
                            arrayForCheck[i] = int.Parse(readArray.ReadLine());
                        Console.WriteLine("Checking array...");
                        for (int i = 0; i < arrayForCheck.Length - 1; i++)
                            if (arrayForCheck[i] < arrayForCheck[i+1])
                            {
                                sorted = false;
                                break;
                            }
                        if (sorted)
                            Console.WriteLine("Array is sorted\n");
                        else
                            Console.WriteLine("Array is not sorted\n");
                    }
                    catch (ArgumentNullException)
                    {
                        bool isEmpty = true;
                        foreach (int number in arrayForCheck)
                        {
                            if (number != 0)
                                isEmpty = false;
                        }
                        if (isEmpty)
                            Console.WriteLine("File is empty!\n");
                        else
                            Console.WriteLine("Array is not full\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Array is broken\n");
                }
            }
            using (StreamWriter writeArray = new StreamWriter(new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sorted.dat"), FileMode.Create)))
            {
                Console.WriteLine("Writing array to file...");
                foreach (int number in originalReversSortedArray)
                {
                    writeArray.Write(number + "\n");
                }
                Console.WriteLine("Array is written\n");
            }
            Console.WriteLine("Reverse array is created\n");
            int select = 0;
            while (select != 4)
            {
                Console.WriteLine("Choose array:\n1 - random array\n2 - ascending sorted array\n3 - descending sorted array\nWrite 4 if you want to quit\n");
                select = int.Parse(Console.ReadLine());
                cntOfCalls = 0;
                switch (select)
                {
                    case 1:
                        Console.WriteLine("Random array is chosen\n");
                        SortingCall(array, originalArray);
                        SortingCall(array, originalArray);
                        SortingCall(array, originalArray);
                        break;
                    case 2:
                        Array.Copy(originalSortedArray, sortedArray, length);
                        Console.WriteLine("Ascending sorted array is chosen\n");
                        SortingCall(sortedArray, originalSortedArray);
                        SortingCall(sortedArray, originalSortedArray);
                        SortingCall(sortedArray, originalSortedArray);
                        break;
                    case 3:
                        Array.Copy(originalReversSortedArray, reverseSortedArray, length);
                        Console.WriteLine("Descending sorted array is chosen\n");
                        SortingCall(reverseSortedArray, originalReversSortedArray);
                        SortingCall(reverseSortedArray, originalReversSortedArray);
                        SortingCall(reverseSortedArray, originalReversSortedArray);
                        break;
                }
            }
        }
        static void SortingCall(int[] array, int[] originalArray)
        {
            if (cntOfCalls == 3) cntOfCalls = 0;
            startTime = DateTime.Now;
            if (cntOfCalls == 0)
            {
                mergeSort(array, 0, array.Length - 1);
                Console.WriteLine("Merge sort is done at {0}\nCount of transpositions: {1}\nCount of comparisons: {2}\n", workTime, countOfTranspositions, countOfComparisons);
            }
            else if (cntOfCalls == 1)
            {
                pyramidalSort(array, 0, array.Length - 1);
                Console.WriteLine("Pyramidal sort is done at {0}\nCount of transpositions: {1}\nCount of comparisons: {2}\n", workTime, countOfTranspositions, countOfComparisons);
            }
            if (cntOfCalls == 2)
            {
                quickSort(array, 0, array.Length - 1);
                Console.WriteLine("Quick sort is done at {0}\nCount of transpositions: {1}\nCount of comparisons: {2}\n", workTime, countOfTranspositions, countOfComparisons);
            }
            endTime = DateTime.Now;
            workTime = endTime - startTime;
            countOfComparisons = 0;
            countOfTranspositions = 0;
            Array.Copy(originalArray, array, length);
            cntOfCalls++;
        }
        static void mergeSort(int[] array, int left, int right)
        {
            if (right <= left)
                return;
            int mid = (left + right) / 2;

            mergeSort(array, left, mid);
            mergeSort(array, mid + 1, right);
            merge(array, left, mid, right);
        }
        static void merge(int[] array, int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];

            int i = left, j = mid + 1, k = 0;

            for (k = 0; k < temp.Length; k++)
            {
                if (i > mid)
                    temp[k] = array[j++];
                else if (j > right)
                    temp[k] = array[i++];
                else
                    temp[k] = (array[i] > array[j]) ? array[i++] : array[j++];
                countOfTranspositions++;
                countOfComparisons += 3;
            }

            k = 0;
            i = left;
            while (k < temp.Length && i <= right)
                array[i++] = temp[k++];
        }
        static void pyramidalSort(int[] array, int left, int right)
        {
            int N = right - left + 1;
            for (int i = right; i >= left; i--)
                Heapify(array, i, N);

            while (N > 0)
            {
                Swap(ref array[left], ref array[N - 1]);
                countOfTranspositions++;
                Heapify(array, left, --N);
            }
        }
        static void Heapify(int[] array, int i, int N)
        {
            while (2 * i + 1 < N)
            {
                int k = 2 * i + 1;

                if (2 * i + 2 < N && array[2 * i + 2] <= array[k])
                {
                    countOfComparisons++;
                    k = 2 * i + 2;
                }
                if (array[i] > array[k])
                {
                    Swap(ref array[i], ref array[k]);
                    countOfTranspositions++;
                    countOfComparisons++;
                    i = k;
                }
                else
                    break;
            }
        }
        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        static void quickSort(int[] array, int left, int right)
        {
            if (right <= left)
                return;

            int p = Partition(array, left, right);

            quickSort(array, left, p - 1);
            quickSort(array, p + 1, right);
        }
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];

            int i = left - 1, j = right;

            while (i < j)
            {
                while (array[++i] > pivot) countOfComparisons++;
                while (array[--j] < pivot)
                {
                    countOfComparisons++;
                    if (j == left)
                        break;
                }

                if (i < j)
                {
                    Swap(ref array[i], ref array[j]);
                    countOfTranspositions++;
                    countOfComparisons++;
                }
                else
                    break;
            }

            Swap(ref array[i], ref array[right]);
            countOfTranspositions++;

            return i;
        }
    }
}