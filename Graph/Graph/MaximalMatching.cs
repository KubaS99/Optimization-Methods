using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    class Connection
    {
        public int a;
        public int b;
        public Connection(int a, int b)
        {
            this.a = a; this.b = b;
        }
        public void PrintConnection()
        {
            Console.WriteLine(a + " -> " + b);
        }
    }
    static class MaximalMatching
    {
        private static int findFreeNeighbour(GraphL g, int currentV)
        {
            bool freeExist = false;
            var neighbours = g.vertices[currentV].edges.Keys.ToList();
            Random r = new Random();
            for (int i = 0; i < neighbours.Count; i++)
            {
                if(g.vertices[neighbours[i]].isVisited == false)
                    freeExist = true;
            }
            if (freeExist)
            {
                int tmp = r.Next(0, neighbours.Count);
                int next = neighbours[tmp];
                while(g.vertices[next].isVisited != false)
                {
                    tmp = r.Next(0, neighbours.Count);
                    next = neighbours[tmp];
                }
                return next;
            }
            else
                return -1;
        }
        private static bool Acceptance(double delta, double T)
        {
            Random r = new Random();
            double p = Math.Exp(-delta / T);
            if (p > r.NextDouble())
                return true;
            return false;
        }
        private static (List<Connection>,List<Connection>) findSolution(GraphL g)
        {
            bool matchingMode = true;
            List<Connection> matched = new List<Connection>();
            List<Connection> nonMatched = new List<Connection>();
            Random r = new Random();
            int current = r.Next(0, g.count);
            g.vertices[current].isVisited = true;
            int tmp;
            while(true)
            {
                tmp = findFreeNeighbour(g, current);
                if(tmp == -1)
                {
                    return (matched, nonMatched);
                }
                else
                {
                    if (matchingMode == true)
                        matched.Add(new Connection(current, tmp));
                    else
                        nonMatched.Add(new Connection(current, tmp));
                    matchingMode = !matchingMode;
                    current = tmp;
                    g.vertices[tmp].isVisited = true;
                }                
            }
        }
        private static int VerCount((List<Connection>,List<Connection>) solution)
        {
            return solution.Item1.Count + solution.Item2.Count;
        }
        public static (List<Connection>, List<Connection>) FindMaximumMatch(GraphL g)
        {
           
            double T = 1;
            double iter = 10000;
            double delta;

            var currentSolution = findSolution(g);
            
            var bestSolution = currentSolution;            
            while (iter > 0)
            {
                g.ResetVisits();
                var candidateSolution = findSolution(g);
                if(candidateSolution.Item1.Count > currentSolution.Item1.Count)
                {
                    currentSolution = candidateSolution;
                    if((candidateSolution.Item1.Count > bestSolution.Item1.Count) && (VerCount(candidateSolution) > VerCount(bestSolution)))
                    {
                        bestSolution = candidateSolution;
                    }
                }               
                else
                {
                    delta = currentSolution.Item1.Count - candidateSolution.Item1.Count;
                    if (Acceptance(delta, T))
                    {
                        currentSolution = candidateSolution;
                    }
                }
                T *= 0.90;
                iter--;
            }

            return bestSolution;
        }

        public static (GraphL v1, GraphL v2) ToBipartite(GraphL g)
        {
            bool error = false;
            bool change = false;
            bool assumed;//false -> 1; true -> 2
            //int assumed;
            List<Vertex> first = new List<Vertex>();
            List<int> help1 = new List<int>();
            List<Vertex> second = new List<Vertex>();
            List<int> help2 = new List<int>();
            List<int> help3 = new List<int>();
            foreach (var v in g.vertices)
            {
                var key = v.edges.Take(1).Select(d => d.Key).First();
                if (help1.Contains(key))
                    assumed = true;
                else
                    assumed = false;
                foreach (int n in v.edges.Keys)
                {
                    if (assumed)
                    {
                        if (help2.Contains(n))
                            error = true;
                    }
                    if (!assumed)
                    {
                        if (help1.Contains(n))
                            error = true;
                    }
                }
                if (error)
                    throw new Exception("Graph is not binomial");
                if (assumed)
                {
                    help2.Add(v.id);
                    second.Add(v);
                }
                else
                {
                    help1.Add(v.id);
                    first.Add(v);
                }

            }
            var v1 = new GraphL(first, false);
            var v2 = new GraphL(second, false);
            return (v1, v2);
        }
        //static void findSolution(GraphL g)
        //{
        //    Random r = new Random();
        //    int start = r.Next(g.vertices.Count);
        //    int current;
        //    g.vertices[start].isVisited = true;

        //}
        public static void FindAssociation(GraphL g)
        {
            Random r = new Random();
        }
        public static void Test(GraphL g)
        {
            var res = findSolution(g);
            Console.WriteLine("Matched: ");
            foreach (var conn in res.Item1)
                conn.PrintConnection();
            Console.WriteLine("Non matched: ");
            foreach (var conn in res.Item2)
                conn.PrintConnection();
        }
    }
    
}



//public static void ToBinomial(GraphL g)
//{
//    bool flag = false;
//    bool change = false;
//    List<Vertex> first = new List<Vertex>();
//    List<int> help1 = new List<int>();
//    List<Vertex> second = new List<Vertex>();
//    List<int> help2 = new List<int>();
//    List<int> help3 = new List<int>();
//    foreach (var v in g.vertices)
//    {
//        change = false;
//        flag = false;
//        var key = v.edges.Take(1).Select(d => d.Key).First();
//        if (help1.Contains(key))
//            flag = true;

//        foreach (int n in v.edges.Keys)
//        {
//            if (help1.Contains(n))
//                help3.Add(1);
//            else if (help2.Contains(n))
//                help3.Add(2);
//            else
//                help3.Add(3);
//        }
//        if (help3[0] == 2)
//            flag = true;
//        foreach (int i in help3)
//        {
//            if (i == 2)
//            {
//                if (!flag)
//                {
//                    flag = !flag;
//                    change = true;
//                }
//            }
//        }
//        if (flag && !change)
//        {
//            first.Add(v);
//            help1.Add(v.id);
//        }
//        else if (!flag && !change)
//        {
//            second.Add(v);
//            help2.Add(v.id);
//        }
//    }
//    foreach (var i in help1)
//        Console.Write(i + " ");
//    Console.WriteLine();
//    foreach (var i in help2)
//        Console.Write(i + " ");
//}