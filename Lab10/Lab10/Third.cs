using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class Third
    {
        /*
        Задан граф. Представить его в виде матрицы инцидентности и в виде
        связных списков. Программа должна позволять вводить номера вершин X и
        Y, после чего вывести путь от X к Y, найденный алгоритмом DFS, и все
        пути от X к Y, найденные алгоритмом BFS
        */
        //========================Graph with adj matrix============================
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
            public Stack<int> DFS(int startPos, int endPos)
            {
                Stack<int> dfsStack = new Stack<int>();

                int[] path = new int[vertices];
                int[] checkedVertices = new int[vertices];

                dfsStack.Push(startPos);
                checkedVertices[startPos] = 1;

                while (dfsStack.Count > 0)
                {
                    int i = dfsStack.Pop();

                    for (int j = vertices - 1; j >= 0; j--)
                        if (graph[i,j] == 1 && checkedVertices[j] == 0)
                        {
                            checkedVertices[j] = 1;
                            dfsStack.Push(j);
                            path[j] = i;

                            if (j == endPos)
                                return backChain(path, startPos, endPos);
                        }
                }
                return null;
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
                        if (graph[i, j] == 1 && checkedVertices[j] == 0)
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
        //===============================Graph with linked lists================================
        public class ListGraph
        {
            private int vertices = 0;

            private Dictionary<int, List<int>> graph = null;

            public ListGraph(Dictionary<int, List<int>> linkedLists, int vertNum)
            {
                graph = linkedLists;
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
            public Stack<int> ListDFS(int startPos, int endPos)
            {
                Stack<int> dfsStack = new Stack<int>();

                int[] path = new int[vertices];
                int[] checkedVertices = new int[vertices];

                dfsStack.Push(startPos);
                checkedVertices[startPos] = 1;

                while (dfsStack.Count > 0)
                {
                    int i = dfsStack.Pop();

                    for (int j = vertices - 1; j >= 0; j--)
                        if (graph[i + 1].Contains(j + 1) && checkedVertices[j] == 0)
                        {
                            checkedVertices[j] = 1;
                            dfsStack.Push(j);
                            path[j] = i;

                            if (j == endPos)
                                return backChain(path, startPos, endPos);
                        }
                }
                return null;
            }
            public Stack<int> ListBFS(int startPos, int endPos)
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
                        if (graph[i + 1].Contains(j + 1) && checkedVertices[j] == 0)
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
        //===========================================Main==============================================
        public static void Execute()
        {
            int x = 0, y = 0;
            while (x < 1 || x > 8)
            {
                Console.Write("Enter the first vertex: ");
                x = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            while (y < 1 || y > 8)
            {
                Console.Write("Enter the second vertex: ");
                y = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("\nIn the form of an incidence matrix:\n");
            int[,] matrix = {
                {0,1,1,0,0,0,0,0},
                {1,0,0,0,0,1,1,0},
                {1,0,0,1,0,1,0,1},
                {0,0,1,0,1,0,0,0},
                {0,0,0,1,0,1,0,0},
                {0,1,1,0,1,0,0,0},
                {0,1,0,0,0,0,0,1},
                {0,0,1,0,0,0,1,0} };
            Graph g = new Graph(matrix, 8);
            Stack<int> dfs = g.DFS(x - 1, y - 1);
            Console.WriteLine("DFS:");
            ShowPath(dfs);
            Stack<int> bfs = g.BFS(x - 1, y - 1);
            Console.WriteLine("BFS:");
            ShowPath(bfs);


            Console.WriteLine("\n\nIn the form of linked lists:\n");
            Dictionary<int, List<int>> lists = new Dictionary<int, List<int>>();
            lists[1] = new List<int> { 2, 3 };
            lists[2] = new List<int> { 1, 6, 7 };
            lists[3] = new List<int> { 1, 4, 6, 8 };
            lists[4] = new List<int> { 3, 5 };
            lists[5] = new List<int> { 4, 6 };
            lists[6] = new List<int> { 2, 3, 5 };
            lists[7] = new List<int> { 2, 8 };
            lists[8] = new List<int> { 3, 7 };
            ListGraph listGraph = new ListGraph(lists, 8);
            Stack<int> listDFS = listGraph.ListDFS(x - 1, y - 1);
            Console.WriteLine("DFS:");
            ShowPath(listDFS);
            Stack<int> listBFS = listGraph.ListBFS(x - 1, y - 1);
            Console.WriteLine("BFS:");
            ShowPath(listBFS);
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
