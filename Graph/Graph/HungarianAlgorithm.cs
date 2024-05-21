using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public static class HungarianAlgorithm
    {
        static (int,int) CalculateCost(Dictionary<int,int> solution, int[,] origin)
        {
            int sequenceCost = 0;
            int parallelCost = 0;
            foreach(int i in solution.Keys)
            {
                sequenceCost+=origin[solution[i],i];
                if(origin[solution[i],i]>parallelCost)
                    parallelCost=origin[solution[i],i];
            }
            return (sequenceCost,parallelCost);
        }
        static int ColOptimal(int[,] adj, int[,] origin, int col)
        {
            int minimal = int.MaxValue;
            int index=0;
            for(int i = 0; i < adj.GetLength(0); i++)
            {
                if((adj[i,col] == 0) && (origin[i,col]<minimal))
                {
                    minimal = origin[i,col];
                    index = i;
                }
            }
            for (int i = 0; i < adj.GetLength(1); i++)
            {
                adj[index, i] = -1;
            }
            return index;
        }

        static bool ColSolCheck(int[,] adj, int col)
        {
            int zerosSum = 0;
            int index;
            bool oneOnly = true;
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                if (adj[i,col] == 0)
                {
                    zerosSum++;
                    index = i;
                }
                if (zerosSum > 1)
                {
                    oneOnly = false;
                }
            }
            return oneOnly;
        }
        static bool RowSolCheck(int[,] adj, int row)
        {
            int zerosSum = 0;
            int index;
            bool oneOnly = true;
            for(int i = 0; i < adj.GetLength(0); i++)
            {
                if(adj[row,i] == 0)
                {
                    zerosSum++;
                    index = i;
                }
                if(zerosSum>1)
                {
                    oneOnly = false;
                }
            }
            return oneOnly;
        }
        static bool TestRow(int[,] crossed, int index)
        {
            bool isCrossed = true;
            for (int i = 0; i < crossed.GetLength(1); i++)
            {
                if (crossed[index, i] == 0)
                    isCrossed = false;
            }
            return isCrossed;
        }
        static bool TestCol(int[,] crossed, int index)
        {
            bool isCrossed = true;
            for(int i = 0; i< crossed.GetLength(1); i++)
            {
                if(crossed[i, index] == 0)
                    isCrossed = false;
            }
            return isCrossed;
        }
        static void ExecuteMin(int[,] adj, int[,] crossed)
        {
            int min = int.MaxValue;
            for(int i = 0; i < adj.GetLength(0); i++)
            {
                for(int j = 0; j < adj.GetLength(1); j++)
                {
                    if((adj[i,j] < min) && crossed[i,j] == 0)
                        min = adj[i,j];
                }
            }

            for (int i = 0; i < adj.GetLength(0); i++)
            {
                for (int j = 0; j < adj.GetLength(1); j++)
                {
                    if (crossed[i, j] == 0)
                        adj[i, j] -= min;

                    if (crossed[i, j] == 2)
                    {
                        if (TestCol(crossed, j) && TestRow(crossed, i))
                        {
                            adj[i, j] += min;
                        }
                    }
                }
            }         
        }
        static void CleanMarks(int[,] crossed)
        {
            for (int i = 0; i < crossed.GetLength(0); i++)
            {
                for (int j = 0; j < crossed.GetLength(1); j++)
                {
                    crossed[i, j] = 0;
                }
            }
        }

        static void MarkRow(int[,] crossed, int index)
        {
            for (int i = 0; i < crossed.GetLength(0); i++)
                crossed[index, i] += 1;
        }

        static void MarkCol(int[,] crossed, int index)
        {
            for (int i = 0; i < crossed.GetLength(1); i++)
                crossed[i, index] += 1;
        }
        static string FindLine(int[,] adj, int[,] crossed)
        {
            Random r = new Random();
            bool freeZero=false;
            for(int i = 0; i < adj.GetLength(0); i++)
            {
                for (int j = 0; j < adj.GetLength(1); j++)
                {
                    if (adj[i, j] == 0 && crossed[i, j] == 0)
                        freeZero=true;
                }
            }
            if (!freeZero)
                return "None "+ -1;

            List<int> rows = new List<int>();
            List<int> cols = new List<int>();
            int nullCounter = 0;
            for(int i=0;i<adj.GetLength(0);i++)
            {
                for(int j=0;j<adj.GetLength(1);j++)
                {
                    if (adj[i, j] == 0 && crossed[i, j] == 0)
                        nullCounter++;
                }
                rows.Add(nullCounter);
                nullCounter = 0;
            }
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                for (int j = 0; j < adj.GetLength(1); j++)
                {
                    if (adj[j,i] == 0 && crossed[j,i] == 0)
                        nullCounter++;
                }
                cols.Add(nullCounter);
                nullCounter = 0;
            }
            int maxRows = -1;
            int maxCols = -1;
            int rowIndex = -1;
            int colIndex = -1;
            for(int i=0;i< rows.Count;i++)
            {
                if (rows[i] > maxRows)
                {
                    maxRows = rows[i];
                    rowIndex = i;
                }
                if (cols[i] > maxCols)
                {
                    maxCols = cols[i];
                    colIndex = i;
                }
            }
            if (maxRows > maxCols)
                return "Row " + rowIndex;
            else if (maxRows < maxCols)
                return "Col " + colIndex;
            else
            {
                int tmp = r.Next();
                if (tmp % 2 == 0)
                    return "Row " + rowIndex;
                else
                    return "Col " + colIndex;
            }

            return "None " + -1;
        }
        static void print(int[,] adj)
        {
            for(int i = 0; i < adj.GetLength(0); i++)
            {
                for( int j = 0; j < adj.GetLength(1); j++)
                {
                    //Console.Write(adj[i, j] + " ");
                    Console.Write(string.Format("{0,3}",adj[i, j] + " "));
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
        static int[,] GetQuarter(int [,] adj)
        {
            int[,] res;
            if(adj.GetLength(0)%2!=0)
            {
                res = new int[adj.GetLength(0)/2+1,adj.GetLength(1)/2+1];
            }
            else
            {
                res = new int[adj.GetLength(0) / 2, adj.GetLength(1) / 2];
            }
            for(int i=0; i<res.GetLength(0); i++)
            {
                for(int j=adj.GetLength(1)/2; j<adj.GetLength(1); j++)
                {
                    res[i,j % (adj.GetLength(1) / 2)] = adj[i,j];
                }
            }
            return res;
        }
        static void ColsMin(int[,] adj)
        {
            int[] foundVals = new int[adj.GetLength(1)];
            int tmp = int.MaxValue;
            for (int j = 0; j < adj.GetLength(0); j++)
            {
                for (int i = 0; i < adj.GetLength(1); i++)
                {
                    if (adj[i, j] < tmp)
                        tmp = adj[i, j];
                }
                foundVals[j] = tmp;
                tmp = int.MaxValue;
            }
            for (int j = 0; j < adj.GetLength(0); j++)
            {
                for (int i = 0; i < adj.GetLength(1); i++)
                {
                    adj[i, j] -= foundVals[j];
                }
            }
        }
        static void RowsMin(int[,] adj)
        {
            int[] foundVals = new int[adj.GetLength(0)];
            int tmp = int.MaxValue;
            for(int i= 0; i < adj.GetLength(0); i++)
            {
                for(int j= 0; j < adj.GetLength(1); j++)
                {
                    if(adj[i,j] < tmp)
                        tmp = adj[i,j];
                }
                foundVals[i] = tmp;
                tmp=int.MaxValue;
            }
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                for (int j = 0; j < adj.GetLength(1); j++)
                {
                    adj[i,j] -= foundVals[i];
                }
            }
        }

        public static void FindSolution(GraphA g)
        {
            //g.Print();
            int[,] adj = GetQuarter(g.adjency);
            int[,] marked = new int[adj.GetLength(0), adj.GetLength(1)];
            CleanMarks(marked);
            print(adj);
            RowsMin(adj);
            print(adj);
            ColsMin(adj);
            print(adj);
            int linesCounter = 0;
            string line = FindLine(adj, marked);

            while (linesCounter < adj.GetLength(0))
            {
                CleanMarks(marked);
                linesCounter = 0;
                line = FindLine(adj, marked);
                while (line.Split(" ")[0] != "None")
                {
                    
                    if (line.Split(" ")[0] == "Row")
                    {
                        MarkRow(marked, Convert.ToInt32(line.Split(" ")[1]));
                        linesCounter++;
                    }
                    else if(line.Split(" ")[0] == "Col")
                    {
                        MarkCol(marked, Convert.ToInt32(line.Split(" ")[1]));
                        linesCounter++;
                    }
                    line = FindLine(adj, marked);
                    string tmp = line.Split(" ")[0];
                }
                if(linesCounter != adj.GetLength(0))
                {                
                    ExecuteMin(adj, marked);
                    Console.WriteLine("Cross:");
                    print(marked);
                    
                }
                print(adj);
                
            }
            int[,] origin = GetQuarter(g.adjency);
            int[,] adjCopy = new int[adj.GetLength(0),adj.GetLength(1)];
            for(int i=0;i<adj.GetLength(0);i++)
            {
                for(int j=0;j<adj.GetLength(1);j++)
                {
                    adjCopy[i, j] = adj[i, j];
                }
            }

            Dictionary<int, int> solution = new Dictionary<int, int>();
            for (int i = 0; i < adj.GetLength(0); i++)
                solution[i] = -1;
            for (int i=0;i<adj.GetLength(0);i++)
            {
                if(ColSolCheck(adj,i))
                {
                    for(int j=0;j < adj.GetLength(1);j++)
                    {
                        if(adj[j,i] == 0)
                        {
                            solution[i] = j;
                            for(int k=0;k<adj.GetLength(1);k++)
                            {
                                adj[j, k] = -1;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                if(solution[i] == -1)
                    solution[i] = ColOptimal(adj,origin,i);
            }
            foreach (int i in solution.Keys)
                Console.WriteLine("Job: "+ (i+1) +" : Worker: "+(solution[i]+1));
            var tmpRes = CalculateCost(solution, origin);
            Console.WriteLine("Sequence cost: " + tmpRes.Item1);
            Console.WriteLine("Parallel cost: " + tmpRes.Item2);
        }
    }
}
