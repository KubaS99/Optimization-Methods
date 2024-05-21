using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class Vertex
    {
        public int id;
        public Dictionary<int, int> edges;
        public bool isVisited = false;
        
        public Vertex(int id)
        {
            this.id = id;
            this.edges = new Dictionary<int, int>();
        }
        public void AddEdge(int v, int cost)
        {
            edges.Add(v, cost);
        }
        public void PrintNeighbors()
        {
            Console.Write("Vertex: " + id+ " neigbours: ");
            foreach (int v in edges.Keys)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

        }
        public int Degree()
        {
            return edges.Count;
        }
    }
}
