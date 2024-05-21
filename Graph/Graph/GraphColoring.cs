using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public static class GraphColoring
    {
        static List<int> GetBiggestGroup(GraphL g)
        {
            List<int> best = new List<int>();
            List<int> tmp;
            foreach (var v in g.vertices)
            {
                if (v.isVisited == false)
                {
                    tmp = FindGroup(g, v);
                    if (tmp.Count > best.Count)
                        best = tmp;
                }
            }
            foreach (int n in best)
            {
                g.vertices[n].isVisited = true;
            }            
            return best;
        }
        static List<int> FindGroup(GraphL g, Vertex ver)
        {
            List<int> bannedNeighbours = new List<int>();
            List<int> result = new List<int>();

            result.Add(ver.id);
            //bannedNeighbours.Add(ver.id);
            foreach (var n in ver.edges.Keys)
                bannedNeighbours.Add(n);

            foreach (var v in g.vertices)
            {                
                if (v.id != ver.id)
                {
                    if (!(bannedNeighbours.Contains(v.id)) && v.isVisited == false)
                    {
                        result.Add(v.id);
                        foreach (var n in v.edges.Keys)
                            bannedNeighbours.Add(n);
                    }
                }
            }
            return result;
        }
        public static List<List<int>> FindSolution(GraphL g)
        {

            List<List<int>> results = new List<List<int>>();
            while (g.HasNonVisited())
            {
                results.Add(GetBiggestGroup(g));
            }

            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine("Color " + i);
                foreach (var r in results[i])
                {
                    Console.Write(r + " ");
                }
                Console.WriteLine("\n");
            }
            g.ResetVisits();
            return results;
        }
    }
}
