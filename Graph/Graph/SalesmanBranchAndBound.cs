using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public static class SalesmanBranchAndBound
    {
        static int[] finalPath;     
        static bool[] visited;

        static int finalCost = int.MaxValue;


        static int firstMin(int[,] adj, int index)
        {
            int res = int.MaxValue;
            for (int i = 0; i < adj.GetLength(0); i++)
                if (adj[index,i] < res && index != i)
                    res = adj[index,i];
            return res;
        }


        static int secondMin(int[,] adj, int index)
        {
            int lowest = int.MaxValue, res = int.MaxValue;
            for (int i = 0; i < adj.GetLength(0); i++)
            {           
                if (adj[index, i] <= lowest && index!=i)
                {
                    res = lowest;
                    lowest = adj[index, i];
                }
                else if ((adj[index, i] <= res && adj[index, i] != lowest) && index!=i)
                {
                    res = adj[index, i];
                }
            }
            return res;
        }
        static void FindNextCity(int[,] adj, int bound, int cost, int depth, int[] currentPath)
        {
            if (depth == adj.GetLength(0))
            {
                if (adj[currentPath[depth - 1],currentPath[0]] != 0)
                {                    
                    int res = cost + adj[currentPath[depth - 1],currentPath[0]];
                   
                    if (res < finalCost)
                    {
                        for(int i=0;i<currentPath.Length;i++)
                        {
                            finalPath[i] = currentPath[i];
                        }
                        finalCost = res;
                    }
                }
                return;
            }

            
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                if (adj[currentPath[depth - 1],i] != 0 && visited[i] == false)
                {
                    int tmp = bound;
                    cost += adj[currentPath[depth - 1],i];

                    if (depth == 1)
                    {
                        bound -= ((firstMin(adj, currentPath[depth - 1]) + firstMin(adj, i)) / 2);
                    }
                    else
                    {
                        bound -= ((secondMin(adj, currentPath[depth - 1]) + firstMin(adj, i)) / 2);
                    }

                    
                    if (bound + cost < finalCost)
                    {
                        currentPath[depth] = i;
                        visited[i] = true;

                        FindNextCity(adj, bound, cost, depth + 1, currentPath);
                    }

                    cost -= adj[currentPath[depth - 1],i];
                    bound = tmp;

                    for (int j = 0; j < visited.Length; j++)
                    {
                        visited[j] = false;
                    }
                    for (int j = 0; j <= depth - 1; j++)
                    {
                        visited[currentPath[j]] = true;
                    }
                }
            }
        }

        public static void FindSolution(GraphA g)
        {
            finalPath = new int[g.adjency.GetLength(0) + 1];
            visited = new bool[g.adjency.GetLength(0)];
            g.Print();

            int[] currentPath = new int[g.adjency.GetLength(0) + 1];
            int bound = 0;          
            
            for(int i=0;i<visited.Length; i++)
            {
                visited[i] = false;
            }
            for(int i=0;i<finalPath.Length;i++)
            {
                finalPath[i] = -1;
            }
            for (int i = 0; i < g.adjency.GetLength(0); i++)
            {
                bound += (firstMin(g.adjency, i) + secondMin(g.adjency, i));
            }
            bound /= 2;

            visited[0] = true;
            currentPath[0] = 0;

            FindNextCity(g.adjency, bound, 0, 1, currentPath);

            Console.Write("Path: ");
            for (int i = 0; i <= g.adjency.GetLength(0); i++)
            {
                Console.Write(finalPath[i] + " -> ");
            }
            Console.WriteLine();
            Console.WriteLine("Cost: " + finalCost);
        }
    }
}
