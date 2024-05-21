using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Graph
{
    static class GraphPrinter
    {
        public static void PrintGraphL(GraphL g)
        {
            List<string> lines = new List<string>();
            lines.Add("import networkx as nx");
            lines.Add("import matplotlib.pyplot as plt");
            if (g.isDirected)
                lines.Add("G = nx.DiGraph()");
            else
                lines.Add("G = nx.Graph()");
            foreach (Vertex v in g.vertices)
            {
                foreach(int n in v.edges.Keys)
                {
                    lines.Add("G.add_edge(" + v.id + "," + n + ")");
                }
            }
            lines.Add("plt.title(\"" + g.sourceName + "\")");
            lines.Add("my_pos = nx.spring_layout(G, seed = 100)");
            lines.Add("nx.draw(G,pos = my_pos,with_labels=True)");
            lines.Add("plt.show()");
            File.WriteAllLines("Graph.py", lines);
            Console.WriteLine(AppContext.BaseDirectory);
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = System.AppContext.BaseDirectory;
            processStartInfo.FileName = "python";
            processStartInfo.Arguments = "Graph.py";
            Process proc = Process.Start(processStartInfo);
            Thread.Sleep(1000);
        }
        public static void PrintGraphA(GraphA g)
        {
            List<string> lines = new List<string>();
            lines.Add("import networkx as nx");
            lines.Add("import matplotlib.pyplot as plt");
            if (g.isDirected)
                lines.Add("G = nx.DiGraph()");
            else
                lines.Add("G = nx.Graph()");
            for(int i=0; i<g.adjency.GetLength(0); i++)
            {
                for(int j=0; j<g.adjency.GetLength(1); j++)
                {
                    if(g.adjency[i,j] > 0)
                    {
                        lines.Add("G.add_edge(" + i + "," + j + ")");
                    }
                }
            }
            lines.Add("plt.title(\"" + g.sourceName + "\")");
            lines.Add("nx.draw(G,with_labels=True)");
            lines.Add("plt.show()");
            File.WriteAllLines("Graph.py", lines);
            Console.WriteLine(AppContext.BaseDirectory);
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = System.AppContext.BaseDirectory;
            processStartInfo.FileName = "python";
            processStartInfo.Arguments = "Graph.py";
            Process proc = Process.Start(processStartInfo);
            Thread.Sleep(1000);
        }
        static void PrintGraphLWithColors(GraphL g, Dictionary<int,string> colors)
        {
            List<string> lines = new List<string>();
            lines.Add("import networkx as nx");
            lines.Add("import matplotlib.pyplot as plt");
            if (g.isDirected)
                lines.Add("G = nx.DiGraph()");
            else
                lines.Add("G = nx.Graph()");
            foreach (Vertex v in g.vertices)
            {
                foreach (int n in v.edges.Keys)
                {
                    lines.Add("G.add_edge(" + v.id + "," + n + ")");
                }
            }
            lines.Add("plt.title(\"" + g.sourceName + "\")");
            lines.Add("nx.draw(G,with_labels=True)");
            lines.Add("plt.show()");
            File.WriteAllLines("Graph.py", lines);
            Console.WriteLine(AppContext.BaseDirectory);
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = System.AppContext.BaseDirectory;
            processStartInfo.FileName = "python";
            processStartInfo.Arguments = "Graph.py";
            Process proc = Process.Start(processStartInfo);
            Thread.Sleep(1000);
        }
        public static void PrintBiPartite(GraphA g)
        {
            List<string> lines = new List<string>();
            lines.Add("import networkx as nx");
            lines.Add("import matplotlib.pyplot as plt");
            if (g.isDirected)
                lines.Add("G = nx.DiGraph()");
            else
                lines.Add("G = nx.Graph()");
            string tmp = "G.add_nodes_from([";
            for (int i = 0; i < g.adjency.GetLength(1); i++)
                tmp += i.ToString() + ',';
            tmp = tmp.Remove(tmp.Length - 1);
            tmp += "], bipartite=0)";
            lines.Add(tmp);
            tmp = "G.add_nodes_from([";
            for (int i = 0; i < g.adjency.GetLength(0); i++)
                tmp += (i + g.adjency.GetLength(1)).ToString() + ',';
            tmp = tmp.Remove(tmp.Length - 1);
            tmp += "], bipartite=1)";
            lines.Add(tmp);
            tmp = "G.add_edges_from([";
            for (int i = 0; i < g.adjency.GetLength(0); i++)
            {
                for (int j = 0; j < g.adjency.GetLength(1); j++)
                {
                    if (g.adjency[i, j] == 0)
                    {
                        tmp += '(' + i.ToString() + ',' + (j + g.adjency.GetLength(0)).ToString() + "),";
                    }
                }
            }
            tmp = tmp.Remove(tmp.Length - 1);
            tmp += "])";
            lines.Add(tmp);
            //lines.Add("top = nx.bipartite.sets(G)[0]");
            //lines.Add("pos = nx.bipartite_layout(G, top)");
            lines.Add("nx.draw(G, with_labels=True)");
            lines.Add("plt.show()");


            File.WriteAllLines("Graph.py", lines);
            Console.WriteLine(AppContext.BaseDirectory);
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = System.AppContext.BaseDirectory;
            processStartInfo.FileName = "python";
            processStartInfo.Arguments = "Graph.py";
            Process proc = Process.Start(processStartInfo);
            Thread.Sleep(1000);
        }

        public static void PrintColoredGraphL(List<List<int>> colors,GraphL g)
        {
            List<string> colorsList = new List<string>();
            colorsList.Add("red");
            colorsList.Add("green");
            colorsList.Add("blue");
            colorsList.Add("purple");            
            colorsList.Add("pink");
            colorsList.Add("white");
            colorsList.Add("orange");           
            colorsList.Add("cyan");           
            colorsList.Add("magenta");          
            colorsList.Add("silver");          
            colorsList.Add("maroon");
            colorsList.Add("brown");
            colorsList.Add("beige");
            colorsList.Add("tan");
            colorsList.Add("peach");
            colorsList.Add("lime");
            colorsList.Add("olive");
            colorsList.Add("turquoise");
            colorsList.Add("teal");
            colorsList.Add("navy blue");
            colorsList.Add("indigo");
            colorsList.Add("violet");

            List<string> lines = new List<string>();
            lines.Add("import networkx as nx");
            lines.Add("import matplotlib.pyplot as plt");
            if (g.isDirected)
                lines.Add("G = nx.DiGraph()");
            else
                lines.Add("G = nx.Graph()");
            
            
            foreach (Vertex v in g.vertices)
            {
                foreach (int n in v.edges.Keys)
                {
                    lines.Add("G.add_edge(" + v.id + "," + (n) + ")");
                }
            }
            lines.Add("plt.title(\"" + g.sourceName + "\")");
            lines.Add("pos = nx.spring_layout(G, seed = 100)");

            string tmp = "";
            for (int i = 0; i < colors.Count; i++)
            {
                tmp = "nx.draw_networkx_nodes(G, pos, nodelist=[";
                for (int j = 0; j < colors[i].Count; j++)
                {
                    tmp += colors[i][j] + ",";
                }
                tmp = tmp.Remove(tmp.Length - 1);
                tmp += "], node_color=\"tab:" + colorsList[i] + "\")";
                lines.Add(tmp);
                tmp = "";
            }
            tmp = "nx.draw_networkx_edges(G,pos,edgelist=[";
            foreach (Vertex v in g.vertices)
            {
                foreach (int n in v.edges.Keys)
                {
                    tmp += ("(" + v.id + "," + (n) + "),");
                }
            }
            tmp = tmp.Remove(tmp.Length - 1);
            tmp += "],)";
            lines.Add(tmp);

            lines.Add("labels = {}");
            for(int i=0;i<g.vertices.Count; i++)
            {
                lines.Add("labels[" + i + "] = '" + i + "'");
            }

            lines.Add("nx.draw_networkx_labels(G, pos, labels)");
            lines.Add("plt.show()");
            File.WriteAllLines("Graph.py", lines);
            Console.WriteLine(AppContext.BaseDirectory);
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = System.AppContext.BaseDirectory;
            processStartInfo.FileName = "python";
            processStartInfo.Arguments = "Graph.py";
            Process proc = Process.Start(processStartInfo);
            Thread.Sleep(1000);
        }
    }
}
