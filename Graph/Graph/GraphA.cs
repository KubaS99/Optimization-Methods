using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class GraphA
    {
        public string sourceName;
        public bool isDirected;
        public int[,] adjency;
        public int count;

        public GraphA(int[,] adj, bool isDirected,string sourceName)
        {
            adjency = adj;
            this.isDirected = isDirected;
            count = adj.GetLength(0)*2;
            this.sourceName = sourceName;
        }
        public GraphA(int a)
        {
            isDirected = false;
            adjency = new int[a,a];
            count = a;

        }
        public GraphA(string fileName)
        {
            sourceName = fileName;
            int amount = getVertexCount(fileName);
            adjency = new int[amount,amount];
            count = amount;
            ReadFromFile(fileName);
        }
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
            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(fileName))
            {
                if (counter == 0 || counter == 2)
                {
                    counter++;
                }
                else if (counter == 1)
                {
                    isDirected = (line == "true" ? true : false);
                    counter++;
                }
                else
                {
                    string[] tmp = line.Split(" ");
                    int[] data = { Convert.ToInt32(tmp[0]) - 1, Convert.ToInt32(tmp[1]) - 1, Convert.ToInt32(tmp[2]) };

                    adjency[data[0], data[1]] = data[2];
                    if(!isDirected)
                    {
                        adjency[data[1], data[0]] = data[2];
                    }
                }
            }
        }
        public void Print()
        {
            Console.Write("    ");
            for(int i = 0; i < count; i++)
            {
                Console.Write(string.Format("{0,3}",i+ " "));
            }
            Console.WriteLine();
            Console.WriteLine();
            for(int i = 0; i < count; i++)
            {
                Console.Write(i + "   ");
                for(int j = 0; j < count; j++)
                {
                    Console.Write(string.Format("{0,3}", adjency[i, j] + " "));
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

    }
}
