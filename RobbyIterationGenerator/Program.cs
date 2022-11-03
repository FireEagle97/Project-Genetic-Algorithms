using System;
using GeneticAlgorithm;

namespace RobbyIterationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Chromosome chromosome = new Chromosome(243,5);
            Chromosome chromosome1 = new Chromosome(243,4);
            IChromosome[] chromList = chromosome.Reproduce(chromosome1,0.06);
            Console.WriteLine("parent1");
            for(var j=0; j< chromosome.Length;j++){
                Console.Write(chromosome[j]);
                if(j % 50 == 5){
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("parent2");
            for(var j=0; j< chromosome1.Length;j++){
                Console.Write(chromosome1[j]);
                if(j % 50 == 5){
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("child1");
            Console.WriteLine(chromList[0].Length);
            for(var j =0; j< chromList[0].Length;j++){
                Console.Write(chromList[0][j]);
                
                if(j % 50 == 5){
                    Console.WriteLine();
                }
            }
            // Console.WriteLine();
            // var count =0;
            // for(var j =0; j< chromList[1].Length;j++){
            //     Console.Write(chromList[1][j]);
            //     if(chromosome[j]!= chromList[1][j]){
            //         count++;
            //     }
                
            //     if(j % 50 == 0){
            //         Console.WriteLine();
            //     }
            // }
            // Console.WriteLine("the count"+count);
                
                    
        }

        
    }
}
