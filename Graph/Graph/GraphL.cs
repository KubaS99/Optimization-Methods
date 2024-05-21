using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class GraphL
    {
        public string sourceName;
        public List<Vertex> vertices;
        public bool isDirected;
        public int count;
        public GraphL(List<Vertex> vertices, bool isDirected)
        {
            count = vertices.Count;
            this.vertices = vertices;
            this.isDirected = isDirected;
        }
        public GraphL(string fileName)
        {
            sourceName = fileName;
            ReadFromFile(fileName);
        }
        //public void PrintAllVertex

        private int getVertexCount(string fileName)
        {
            int max = 0;
            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(fileName))
            {
                
                if (counter == 0 || counter == 2)
                {
                    counter++;
                }
                else if (counter == 1)
                {
                    counter++;
                }
                else
                {
                    string[] tmp = line.Split(" ");
                    int[] data = { Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2]) };

                    if (data[0] > max)
                        max = data[0];
                    if (data[1] > max)
                        max = data[1];
                    counter++;
                }
            }
            return max;
        }
        public void ReadFromFile(string fileName)
        {
            vertices = new List<Vertex>();
            count = getVertexCount(fileName);
            for(int i=0;i<count;i++)
            {
                vertices.Add(new Vertex(i));
            }  
            
            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(fileName))
            {
                if (counter == 0 || counter == 2)
                {
                    counter++;
                }
                else if (counter == 1)
                {
                    isDirected = line == "true" ? true : false;
                    counter++;
                } 
                else
                {
                    string[] tmp = line.Split(" ");
                    int[] data = { Convert.ToInt32(tmp[0])-1, Convert.ToInt32(tmp[1])-1, Convert.ToInt32(tmp[2]) };
                    
                        if(vertices[data[0]].edges.ContainsKey(data[1]))
                        {
                            if(!isDirected)
                            {
                                if (vertices[data[1]].edges.ContainsKey(data[0]))
                                {

                                }
                                else
                                {
                                    vertices[data[1] ].AddEdge(data[0], data[2]);
                                }
                            }
                        }
                        else
                        {
                            vertices[data[0]].AddEdge(data[1], data[2]);
                            if (!isDirected)
                            {
                                if (vertices[data[1]].edges.ContainsKey(data[0]))
                                {

                                }
                                else
                                {
                                    vertices[data[1]].AddEdge(data[0], data[2]);
                                }
                            }

                        }                                           
                    counter++;
                }
            }
        }
        private void Recur(Vertex v)
        {            
            v.isVisited = true;
            foreach (var neighbour in v.edges.Keys)
            {
                if(vertices[neighbour].isVisited== false)
                    Recur(vertices[neighbour]);
            }

        }
        public bool IsConnected()
        {           
            int visitedCount = 0;
            Recur(vertices[0]);
            foreach(var vertex in vertices)
            {
                if (vertex.isVisited)
                {
                    visitedCount++;
                    vertex.isVisited = false;
                }
            }
            if (visitedCount == vertices.Count)
                return true;
            return false;
            //Stack <Vertex> stack = new Stack<Vertex>();
            //int verCount = 0;
            //stack.Push(vertices[0]);
            //vertices[0].isVisited = true;
            //while(stack.Count>0)
            //{
            //    v = stack.Pop();
            //    verCount++;
            //    for(int i=0;i<v.edges.Count;i++)
            //    {

            //    }
                
        }
        
        public int Degree()
        {
            int degree = 0;
            foreach(var vertex in vertices)
            {
                if(vertex.Degree()>degree)
                    degree = vertex.Degree();
            }
            return degree;
        }
        public bool InOutEqual()
        {
            List<int> outEdges = new List<int>();
            List<int> inEdges = new List<int>();
            int ins = 0;
            foreach(var vertex in vertices)
            {
                outEdges.Add(vertex.edges.Count);
            }
            foreach (var vertex in vertices)
            {
                foreach (var inVertex in vertices)
                {
                    if (inVertex.id != vertex.id)
                    {
                        if (inVertex.edges.ContainsKey(vertex.id))
                            ins++;
                    }
                }
                inEdges.Add(ins);
                ins = 0;
            }
            bool equals = true;
            for(int i=0;i<outEdges.Count;i++)
            {
                if(outEdges[i]!=inEdges[i])
                    equals = false;
            }
            return equals;
        }
        public void RemoveEdge(int v1,int v2)
        {
            vertices[v1].edges.Remove(v2);
            vertices[v2].edges.Remove(v1);
        }
        public void ResetVisits()
        {
            foreach(var vertex in vertices)
                vertex.isVisited = false;
        }
        public void Print()
        {
            foreach (var vertex in vertices)
                vertex.PrintNeighbors();
        }

        public bool HasNonVisited()
        {
            bool hasNonVisited = false;
            foreach(var v in vertices)
            {
                if(v.isVisited==false)
                    hasNonVisited = true;
            }
            return hasNonVisited;
        }
        public bool VisitsCheck()
        {
            bool hasVisited = false;
            foreach (var v in vertices)
            {
                if (v.isVisited == true)
                    hasVisited = true;
            }
            return !hasVisited;
        }

        public int GetCost(int v1, int v2)
        {
            return vertices[v1].edges[v2];
        }
       
    }
}
//Na tablicy i na liście
//DOdawanie wierzchołka/krawędzi
//Pobieranie wierzchołka/krawędzi
//Usuwanie -||-
//Liczenie stopnia

//Skierowanie
//Wczytywanie z pliku