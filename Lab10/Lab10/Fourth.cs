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
            int[,] distances = {
                { -1, 50, 75, -1, -1, -1, -1, -1 },
                { 50, -1, -1, -1, -1, 130, 35, -1},
				{ 75, -1, -1, 60, -1, 55, -1, 140},
				{ -1, -1, 60, -1, 95, -1, -1, -1},
				{ -1, -1, -1, 95, -1, 40, -1, -1},
				{ -1, 130, 55, -1, 40, -1, -1, -1},
				{ -1, 35, -1, -1, -1, -1, -1, 140},
				{ -1, -1, 140, -1, -1, -1, 140, -1} };
            int city = 0, limit = 200;
            while (city < 1 || city > 8)
            {
                Console.Write("Enter the number of city (from 1 to " + distances.GetLength(0) + "): ");
                city = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    Console.Write("{0, -3} ", distances[i, j]);
                Console.WriteLine("\n");
            }
            Graph g = new Graph(distances, 8);
            Dictionary<string, int> distancesDictionary = new Dictionary<string, int>();
            Console.WriteLine("\nPaths:\n");
            for (int j = 0; j < 8; j++)
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
