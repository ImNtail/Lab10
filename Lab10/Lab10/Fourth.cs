using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class Fourth
    {
        /*
        Заданы города и двустороняя система дорог между ними в виде матрицы A,
        где a[i,j] = L (длина пути из города i в город j) или a[i,j]=–1, если из города i
        в город j прямого пути нет. Найти все города, в которые из заданного города
        можно добраться по суммарному пути не длиннее 200 км.
        */
        public class Graph
        {
            private int vertices = 0;

            private int[,] graph = null;

            public Graph(int[,] adjacencyMatrix, int vertNum)
            {
                graph = adjacencyMatrix;
                vertices = vertNum;
            }
            public Stack<int> backChain(int[] p, int startPos, int endPos)
            {
                int pos = endPos;

                Stack<int> pathStack = new Stack<int>();
                pathStack.Push(pos);

                while (pos != startPos)
                {
                    pos = p[pos];
                    pathStack.Push(pos);
                }

                return pathStack;
            }
            public Stack<int> BFS(int startPos, int endPos)
            {
                Queue<int> q = new Queue<int>();

                int[] path = new int[vertices];
                int[] checkedVertices = new int[vertices];

                q.Enqueue(startPos);
                checkedVertices[startPos] = 1;

                while (q.Count > 0)
                {
                    int i = q.Dequeue();
                    for (int j = 0; j < vertices; j++)
                        if (graph[i, j] > -1 && checkedVertices[j] == 0)
                        {
                            checkedVertices[j] = 1;
                            q.Enqueue(j);
                            path[j] = i;

                            if (j == endPos)
                                return backChain(path, startPos, endPos);
                        }
                }
                return null;
            }
        }
        public static void Execute()
        {
            Random rand = new Random();
            int[,] distances = new int[10, 10];
            for (int i = 0; i < distances.GetLength(0); i++)
                for (int j = 0; j < distances.GetLength(1); j++)
                    distances[i, j] = -1;
            for (int i = 0; i < distances.GetLength(0); i++)
                distances[i, rand.Next(0, i)] = rand.Next(40, 120);
            for (int i = 0; i < distances.GetLength(0); i++)
                for (int j = 0; j < distances.GetLength(1); j++)
                    if (j > i)
                        distances[i, j] = distances[j, i];
            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (int j = 0; j < distances.GetLength(1); j++)
                    Console.Write("{0, -3} ", distances[i, j]);
                Console.WriteLine("\n");
            }
            Console.WriteLine();
            int city = 0, limit = 200;
            while (city < 1 || city > distances.GetLength(0))
            {
                Console.Write("Enter the number of city (from 1 to " + distances.GetLength(0) + "): ");
                city = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Graph g = new Graph(distances, distances.GetLength(0));
            Dictionary<string, int> distancesDictionary = new Dictionary<string, int>();
            Console.WriteLine("\nPaths:\n");
            for (int j = 0; j < distances.GetLength(0); j++)
                if (!distancesDictionary.ContainsKey("From " + city + " to " + j + 1) && city != j + 1)
                {
                    var stackBFS = g.BFS(city - 1, j);
                    ShowPath(stackBFS);
                    Console.WriteLine();
                    DictionaryAdd(stackBFS, distances, ref distancesDictionary, city);
                }
            Console.WriteLine("\nDistances:\n");
            foreach (var item in distancesDictionary)
                if (item.Value <= limit)
                    Console.WriteLine(item.Key + " : " + item.Value + "\n");
        }
        static void DictionaryAdd(Stack<int> stackBFS, int[,] distances, ref Dictionary<string, int> distancesDictionary, int city)
        {
            int prevNum = -1, sum = 0;
            try
            {
                foreach (var i in stackBFS)
                {
                    if (prevNum == -1)
                        prevNum = i;
                    else
                    {
                        sum += distances[prevNum, i];
                        prevNum = i;
                        distancesDictionary["From " + city + " to " + (i + 1)] = sum;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("\nNothing in stack"); }
        }
        static void ShowPath(Stack<int> stack)
        {
            try
            {
                int cnt = 0;
                foreach (int i in stack)
                {
                    Console.Write((cnt == 0) ? Convert.ToString(i + 1) : " -> " + (i + 1));
                    cnt++;
                }
            }
            catch (Exception ex) { Console.WriteLine("There is no such path"); }
            Console.WriteLine();
        }
    }
}
