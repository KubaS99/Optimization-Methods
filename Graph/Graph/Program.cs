using System;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //!!!!                                                   !!!!
            //!!!!                                                   !!!!
            //!!!!                                                   !!!!
            //!!!!           DO POPRAWNEGO DZIAŁANIA KLASY           !!!!
            //!!!!      GRAPHPRINTER WYMAGANA JEST INSTALACJA        !!!!
            //!!!!         PYTHON ORAZ BIBLIOTEKI NETWORKX           !!!!
            //!!!!                                                   !!!!
            //!!!!                                                   !!!!
            //!!!!                                                   !!!!
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            //############################### CYKL EULERA ######################################

            //GraphL Euler = new GraphL("4.txt");
            //GraphPrinter.PrintGraphL(Euler);
            //EulerCycle.FindCycle(Euler);






            //############################ MAKSYMALNE SKOJARZENIE ##############################

            //GraphL MaximalMatch = new GraphL("4.txt");
            //var res =  MaximalMatching.FindMaximumMatch(MaximalMatch);
            //Console.WriteLine("Matched: ");
            //foreach (var conn in res.Item1)
            //    conn.PrintConnection();
            //GraphPrinter.PrintGraphL(MaximalMatch);






            //########################### ALGORYTM WĘGIERSKI ###################################

            //GraphA Hungarian = new GraphA("wegierski.txt");
            //HungarianAlgorithm.FindSolution(Hungarian);






            //########################### PODZIAŁ I OGRANICZENIA ###############################

            //GraphA BranchAndBound = new GraphA("salesman2.txt");
            //SalesmanBranchAndBound.FindSolution(BranchAndBound);






            //########################### KOLOROWANIE GRAFU ####################################

            //GraphL Coloring = new GraphL("4.txt");
            //var colors = GraphColoring.FindSolution(Coloring);
            //GraphPrinter.PrintColoredGraphL(colors, Coloring);

            //GraphL Coloring2 = new GraphL("salesman2.txt");
            //var colors = GraphColoring.FindSolution(Coloring2);
            //GraphPrinter.PrintColoredGraphL(colors, Coloring2);






            //########################### ALGORYTM GENETYCZNY ##################################

            //GraphL g = new GraphL("salesman3.txt");
            //Genetic.FindSolution(g, 4, 0.05, 0.5, 200);
        }
    }
}
