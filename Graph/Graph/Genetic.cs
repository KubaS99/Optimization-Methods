using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public static class Genetic
    {
        static void PrintChromosome(int[] ch)
        {
            for (int i = 0; i < ch.Length; i++)
            {
                Console.Write(ch[i] + " -> ");
            }
        }
        static void FixSwaps(ref Dictionary<int,int> swap1,ref Dictionary<int,int> swap2)
        {
            var intersects = (swap1.Keys.Intersect(swap2.Keys)).ToList();
            var dist1 = (swap1.Keys.Except(swap2.Keys)).ToList();
            var dist2 = (swap2.Keys.Except(swap1.Keys)).ToList();

            var newSwap1 = new Dictionary<int,int>();
            var newSwap2 = new Dictionary<int,int>();
            foreach(var n in intersects)
            {
                newSwap1[n] = n;
                newSwap2[n] = n;
            }
           
            for(int i = 0; i < dist1.Count; i++)
            {
                newSwap1[dist1[i]] = dist2[i];
                newSwap2[dist2[i]] = dist1[i];
            }
            swap1 = newSwap1;
            swap2 = newSwap2;          
        }
        static bool Contains(int[] ch, int val)
        {
            for (int i = 0; i < ch.Length; i++)
                if (ch[i] == val)
                    return true;
            return false;
        }
        static bool InRange(int start,int stop,int val)
        {
            if(val >= start && val <=stop)
                return true;
            return false;
        }
        public static (int[] out1, int[] out2) CrossingOver(int[] ch1, int[] ch2)
        {
            Random r = new Random();
            Dictionary<int, int> swaps1 = new Dictionary<int, int>();
            Dictionary<int, int> swaps2 = new Dictionary<int, int>();
            
            int start = r.Next(0, ch1.Length);
            int stop = r.Next(start, ch1.Length);
            int[] out1 = new int[ch1.Length];
            int[] out2 = new int[ch2.Length];

            for(int i = 0; i < out1.Length; i++)
            {
                out1[i] = -1;
                out2[i] = -1;
            }
            for(int i = start;i<=stop;i++)
            {
                out1[i] = ch1[i];
                out2[i] = ch2[i];
                swaps1[ch1[i]] = ch2[i];
                swaps2[ch2[i]] = ch1[i];
            }
            FixSwaps(ref swaps1,ref swaps2);
            for(int i=0;i<ch1.Length;i++)
            {
                if (!(Contains(out1, ch2[i])) && !(InRange(start, stop, i)))
                {
                    out1[i] = ch2[i];
                }
                else if(Contains(out1,ch2[i]) && !(InRange(start, stop, i)))
                {
                    out1[i] = swaps1[ch2[i]];
                }

                if (!(Contains(out2, ch1[i])) && !(InRange(start, stop, i)))
                {
                    out2[i] = ch1[i];
                }
                else if (Contains(out2, ch1[i]) && !(InRange(start, stop, i)))
                {
                    out2[i] = swaps2[ch1[i]];
                }
            }
            return (out1, out2);
            //PrintChromosome(out1);
            //PrintChromosome(out2);

        }
        static void testFixSwaps(ref Dictionary<int, int> swap1, ref Dictionary<int, int> swap2)
        {
            var intersects = (swap1.Keys.Intersect(swap2.Keys)).ToList();
            var dist1 = (swap1.Keys.Except(swap2.Keys)).ToList();
            var dist2 = (swap2.Keys.Except(swap1.Keys)).ToList();

            var newSwap1 = new Dictionary<int, int>();
            var newSwap2 = new Dictionary<int, int>();
            foreach (var n in intersects)
            {
                newSwap1[n] = n;
                newSwap2[n] = n;
            }

            for (int i = 0; i < dist1.Count; i++)
            {
                newSwap1[dist1[i]] = dist2[i];
                newSwap2[dist2[i]] = dist1[i];
            }
            swap1 = newSwap1;
            swap2 = newSwap2;
            Console.WriteLine("swap1:");
            foreach(var k in swap1.Keys)
            {
                Console.WriteLine(k+" : "+swap1[k]);
            }
            Console.WriteLine("swap2:");
            foreach (var k in swap2.Keys)
            {
                Console.WriteLine(k + " : " + swap2[k]);
            }
        }
        public static (int[] out1, int[] out2) testCrossingOver()
        {
            Random r = new Random();
            Dictionary<int, int> swaps1 = new Dictionary<int, int>();
            Dictionary<int, int> swaps2 = new Dictionary<int, int>();
            int[] ch1 = { 0, 4, 1, 2, 3, 6, 5 };
            int[] ch2 = { 4, 6, 1, 3, 0, 5, 2 };
            //int[] ch1 = { 1, 5, 2, 3, 4, 7, 6 };
            //int[] ch2 = { 5, 7, 2, 4, 1, 6, 3 };
            //int start = r.Next(0, ch1.Length-1);
            //int stop = r.Next(start, ch1.Length-1);
            int start = 1;
            int stop = 1;
            //int start = 2;
            //int stop = 4;
            int[] out1 = new int[ch1.Length];
            int[] out2 = new int[ch2.Length];

            for (int i = 0; i < out1.Length; i++)
            {
                out1[i] = -1;
                out2[i] = -1;
            }
            for (int i = start; i <= stop; i++)
            {
                out1[i] = ch1[i];
                out2[i] = ch2[i];
                swaps1[ch1[i]] = ch2[i];
                swaps2[ch2[i]] = ch1[i];
            }
            testFixSwaps(ref swaps1, ref swaps2);
            for (int i = 0; i < ch1.Length; i++)
            {
                if (!(Contains(out1, ch2[i])) && !(InRange(start, stop, i)))
                {
                    out1[i] = ch2[i];
                }
                else if (Contains(out1, ch2[i]) && !(InRange(start, stop, i)))
                {
                    out1[i] = swaps1[ch2[i]];
                }

                if (!(Contains(out2, ch1[i])) && !(InRange(start, stop, i)))
                {
                    out2[i] = ch1[i];
                }
                else if (Contains(out2, ch1[i]) && !(InRange(start, stop, i)))
                {
                    out2[i] = swaps2[ch1[i]];
                }
            }
            PrintChromosome(out1);
            PrintChromosome(out2);
            return (out1, out2);
           

        }
        private static void MutatePopulation(ref List<int[]> population, double mutationChance)
        {
            Random r = new Random();
            double score;
            for(int i=0; i<population.Count;i++)
            {
                score = r.NextDouble();
                if(score <= mutationChance && i!=0)
                {
                    population[i] = Mutate(population[i]);
                }
            }
        }
        private static int[] Mutate(int[] ch)
        {
            Random r = new Random();
            int first = r.Next(0, ch.Length);
            int second = first;
            while(first==second)
                second=r.Next(0, ch.Length);
            int tmp = ch[first];
            ch[first] = ch[second];
            ch[second] = tmp;

            return ch;
        }


        private static int[] RandomChromosome(int[] baseChromosome)
        {
            var tmp = baseChromosome.OrderBy(a => Guid.NewGuid()).ToArray();
            return tmp;
        }


        private static int[] RandomChromosome2(int[] baseChromosome)
        {
            Random r = new Random();
            int[] result = new int[baseChromosome.Length];
            int n = baseChromosome.Length;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                int tmp = baseChromosome[k];
                baseChromosome[k] = baseChromosome[n];
                baseChromosome[n] = tmp;
            }
            return baseChromosome;
        }


        private static List<int[]> GeneratePopulation(int populationSize, int chromosomeLength)
        {
            List<int[]> population = new List<int[]>();
            int[] baseChromosome = new int[chromosomeLength];
            for (int i = 0; i < chromosomeLength; i++)
                baseChromosome[i] = i;
            for(int i=0;i<populationSize; i++)
                population.Add(RandomChromosome(baseChromosome));
            return population;
            
        }


        private static int FitFunction(int[] ch,GraphL g)
        {
            int result = 0;
            for(int i=0;i<ch.Length-1;i++)
            {
                result+= g.GetCost(ch[i],ch[i+1]);
            }
            result += g.GetCost(ch[ch.Length-1],ch[0]);
            return result;
        }



        private static (List<int[]>, int[]) GenerateNewPopulation(List<int[]> currentPopulation, double crossingPercantage, GraphL g)
        {
            List<int[]> newPopulation = new List<int[]>();
            int populationSize = currentPopulation.Count;
            List<int> scores = new List<int> ();
            int[] elite = new int[currentPopulation[0].Length];
            foreach (var ch in currentPopulation)
                scores.Add(FitFunction(ch,g));
            int min = int.MaxValue;
            int index = -1;
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i] < min)
                {
                    min = scores[i];
                    index = i;
                }
            }
            for(int i=0;i<elite.Length;i++)
            {
                elite[i] = currentPopulation[index][i];
            }

            newPopulation.Add(elite);

            var orderedZip = scores.Zip(currentPopulation, (x, y) => new { x, y })
                                  .OrderBy(pair => pair.x)
                                  .ToList();
            scores = orderedZip.Select(pair => pair.x).ToList();
            currentPopulation = orderedZip.Select(pair => pair.y).ToList();

            int count = (int)(currentPopulation.Count * crossingPercantage);
            List<int[]> picked = new List<int[]>();
            for(int i = 0; i<count; i++)
            {
                picked.Add(currentPopulation[i]);
            }
            Random r = new Random();
            while(newPopulation.Count <= populationSize)
            {
                int firstCh = r.Next(0, count);
                int secondCh = r.Next(0, count);
                if(firstCh != secondCh)
                {
                    var newChromosomes = CrossingOver(picked[firstCh], picked[secondCh]);
                    newPopulation.Add(newChromosomes.out1);
                    newPopulation.Add(newChromosomes.out2);
                }
            }
            while (newPopulation.Count > populationSize)
            {
                newPopulation.RemoveAt(newPopulation.Count - 1);
            }
            return (newPopulation,elite);
        }


        public static void FindSolution(GraphL g, int populationSize, double mutationChance,double crossingPercentage, int maxIter)
        {
            List<int[]> population = GeneratePopulation(populationSize, g.count);
            int[] elite= population[0];

            for(int i = 0; i < maxIter; i++)
            {
                Console.WriteLine("\t\tGeneration: " + (i + 1));
                var newChromosomes = GenerateNewPopulation(population, crossingPercentage, g);
                population = newChromosomes.Item1;
                MutatePopulation(ref population, mutationChance);
                elite = newChromosomes.Item2;
                Console.WriteLine("Current elite: ");
                PrintChromosome(elite);
                Console.Write(elite[0] + "\n");
                Console.WriteLine("Cost: " + FitFunction(elite, g));
                Console.WriteLine("-------------------------------------------------\n");
            }
            int min = FitFunction(elite,g);
            for(int i = 0; i<populationSize;i++)
            {
                if (FitFunction(population[i],g) < min)
                {
                    for(int j=0;j<elite.Length;j++)
                    {
                        elite[j] = population[i][j];
                    }
                }
            }
            Console.WriteLine("Best found solution: ");
            PrintChromosome(elite);
            Console.Write(elite[0]+"\n");
            Console.WriteLine("Cost: " + FitFunction(elite, g));
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
