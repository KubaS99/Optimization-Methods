using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    static class EulerCycle
    {
        public static bool CheckConditions(GraphL graph)
        {
            if (!graph.IsConnected())
            {
                Console.WriteLine("Graf niespójny!");
                return false;
            }
            if (graph.isDirected)
            {
                if (!graph.InOutEqual())
                {
                    Console.WriteLine("Ilość wejść != ilość wyjść");
                    return false;
                }
            }
            else
            {
                foreach(var ver in graph.vertices)
                {
                    if(ver.Degree()%2==1)
                    {
                        Console.WriteLine("Nieparzysty stopień!");
                        return false;
                    }
                }               
            }
            return true;
        }
        public static void FindCycle(GraphL graph)
        {
            List<int> cycle = new List<int>();
            Vertex v;
            Vertex w;
            if (!CheckConditions(graph))
            {
                Console.WriteLine("Brak cyklu Eulera!");
                return;
            }
            Stack<Vertex> stack = new Stack<Vertex>();
            stack.Push(graph.vertices[0]);
            while(stack.Count!=0)
            {
                v = stack.Peek();
                if(v.edges.Keys.Count==0)
                {
                    stack.Pop();
                    cycle.Add(v.id);
                }
                else
                {
                    var key = v.edges.Take(1).Select(d => d.Key).First();
                    w = graph.vertices[key];
                    stack.Push(w);
                    graph.RemoveEdge(v.id, key);
                }
            }
            //foreach(var ver in cycle)
            //{
            //    //Console.Write(ver+ " -> ");
            //}
            for(int i=0; i<cycle.Count; i++)
            {
                Console.Write(cycle[i]);
                if (i != cycle.Count - 1)
                    Console.Write(" -> ");
                else 
                    Console.Write('\n');
            }
        }

    }
}
