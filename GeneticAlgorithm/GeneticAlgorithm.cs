using System;
namespace GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {
        private IGeneration _currentGeneration;
        private readonly int _populationSize;
        private readonly int _numberOfGenes;
        private readonly int _lengthOfGene;
        private readonly double _mutationRate;
        private readonly double _eliteRate;
        private readonly int _numberOfTrials;
        private readonly FitnessEventHandler _fitnessCalculation;
        private readonly int? _seed;
        private long _generationCount;


        //Contains a constructor that takes the population size, number of genes, length of genes, mutation rate, elite rate, number of trials, the fitness function, and a potential seed
        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            _populationSize = populationSize;
            _numberOfGenes = numberOfGenes;
            _lengthOfGene = lengthOfGene;
            _mutationRate = mutationRate;
            _eliteRate = eliteRate;
            _numberOfTrials = numberOfTrials;
            _fitnessCalculation = fitnessCalculation;
            _seed = seed;
        }

        //implements the interface
        public int PopulationSize => _populationSize;
        public int NumberOfGenes => _numberOfGenes;
        public int LengthOfGene => _lengthOfGene;
        public double MutationRate => _mutationRate;
        public double EliteRate => _eliteRate;
        public int NumberOfTrials => _numberOfTrials;
        public long GenerationCount => _generationCount;
        public IGeneration CurrentGeneration => _currentGeneration;
        public FitnessEventHandler FitnessCalculation => _fitnessCalculation;

        /// <summary>
        /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
        /// If a generation has already been created, it will provide the next generation.
        /// </summary>
        /// <returns>The current generation</returns>  

        public IGeneration GenerateGeneration()
        {
            if (_currentGeneration == null)
            {
                _currentGeneration = new Generation(new GeneticAlgorithm(_populationSize, _numberOfGenes, _lengthOfGene, _mutationRate, _eliteRate, _numberOfTrials, _fitnessCalculation, _seed), _fitnessCalculation, _seed);
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();
                
            }
            else
            {
                _currentGeneration = GenerateNextGeneration();
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();
            }
            _generationCount++;
            return _currentGeneration;
        }

        /// <summary>
        ///This method must create the next set of Chromosomes through reproduction
        /// The elite rate should be used to select only a subset of the best Chromosomes based on fitness - call SelectElites
        /// A new Generation should be created based on the resulting child Chromosomes
        /// </summary>
        /// <returns></returns>
        private IGeneration GenerateNextGeneration()
        {
            var currentGeneration = _currentGeneration as Generation;
            //create a new empty generation
        
            //print currentGeneration the ten genes in the current generation
            Console.WriteLine("Current Generation");
             int[] genes1 = currentGeneration.ChromosomesArray[0].Genes;
            int[] genes2 = currentGeneration.ChromosomesArray[1].Genes;
            int[] genes3 = currentGeneration.ChromosomesArray[2].Genes;
            int[] genes4 = currentGeneration.ChromosomesArray[3].Genes;
            int[] genes5 = currentGeneration.ChromosomesArray[4].Genes;
            int[] genes6 = currentGeneration.ChromosomesArray[5].Genes;
            int[] genes7 = currentGeneration.ChromosomesArray[6].Genes;
            int[] genes8 = currentGeneration.ChromosomesArray[7].Genes;
            int[] genes9 = currentGeneration.ChromosomesArray[8].Genes;
            int[] genes10 = currentGeneration.ChromosomesArray[9].Genes;

            

        
            // for (int i = 0; i < genes1.Length; i++)
            // {
            //     Console.Write(genes1[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes2.Length; i++)
            // {
            //     Console.Write(genes2[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes3.Length; i++)
            // {
            //     Console.Write(genes3[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes4.Length; i++)
            // {
            //     Console.Write(genes4[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes5.Length; i++)
            // {
            //     Console.Write(genes5[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes6.Length; i++)
            // {
            //     Console.Write(genes6[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes7.Length; i++)
            // {
            //     Console.Write(genes7[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes8.Length; i++)
            // {
            //     Console.Write(genes8[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genes9.Length; i++)
            // {
            //     Console.Write(genes9[i] + " ");
            // }
            //String with array values
            // String arrayValues = "";
            // foreach(int gene in topGenes){
            //     arrayValues += gene.ToString();
            // }
    
    //create a empty new generation
            // var newGeneration = new Generation(this,FitnessCalculation, _seed);
            //          int[] genesnew1 = newGeneration.ChromosomesArray[0].Genes;
            // int[] genesnew2 = newGeneration.ChromosomesArray[1].Genes;
            // Console.WriteLine("New Generation before changes ...........................");
            //     for (int i = 0; i < genesnew1.Length; i++)
            // {
            //     Console.Write(genes1[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genesnew2.Length; i++)
            // {
            //     Console.Write(genes2[i] + " ");
            // }


            //start to populate after the elites
            for (var i =0; i < PopulationSize; i++){
                    // var parent1 = currentGeneration.SelectParent();
                    // var parent2 = currentGeneration.SelectParent();
                    //call selectRightParent which returns two parents
                     var parent1 = currentGeneration.SelectParent();
                     var parent2 = currentGeneration.SelectParent();

                     //print the parents
                        // Console.WriteLine("Parent 1");
                        // int[] genesParent1 = parent1.Genes;
                        // for (int j = 0; j < genesParent1.Length; j++)
                        // {
                        //     Console.Write(genesParent1[j] + " ");
                        // }
                        // Console.WriteLine();
                        // Console.WriteLine("Parent 2");
                        // int[] genesParent2 = parent2.Genes;
                        // for (int j = 0; j < genesParent2.Length; j++)
                        // {
                        //     Console.Write(genesParent2[j] + " ");
                        // }


                    //check if parent 1 and 2 are the same parent
                    // while (parent1 == parent2){
                    //     parent2 = currentGeneration.SelectParent();
                    // }
                    //create a child from the parents 

                      var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                      //print the children generation
                        // Console.WriteLine("Children Generation........................");
                        int[] genes34 = childrenGeneration[0].Genes;
                        int[] genes44 = childrenGeneration[1].Genes;
                        // for (int j = 0; j < genes34.Length; j++)
                        // {
                        //     Console.Write(genes34[j] + " ");
                        // }
                        // Console.WriteLine();
                        // for (int j = 0; j < genes44.Length; j++)
                        // {
                        //     Console.Write(genes44[j] + " ");
                        // }
                    //add the child to the new generation
                    

                        // var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    //swap the currentgeneration chromosmesArray with the new the children generation
                    // currentGeneration.ChromosomesArray = childrenGeneration;
                    
                        //curenetGeneration should have the new children generation
                        // currentGeneration.ChromosomesArray = childrenGeneration;
                        (_currentGeneration as Generation)[i]= childrenGeneration[0];
                        (_currentGeneration as Generation)[i+1]= childrenGeneration[1];
                        i++;  
                    
                    //if parent 1 and 2 are the same parent, call select parent again                    
                    // var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    // currentGeneration.ChromosomesArray[i] = childrenGeneration[0];
                    // currentGeneration.ChromosomesArray[i+1] = childrenGeneration[1];
                    // i++;    
                }
    //    Console.WriteLine("Generation: " + currentGeneration);
       //print the first two values of the generation genes
    //    Console.WriteLine("Next Generation...........................................");
    //       Console.WriteLine("Current Generation");
    
    //         for (int i = 0; i < genes1.Length; i++)
    //         {
    //             Console.Write(genes1[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes2.Length; i++)
    //         {
    //             Console.Write(genes2[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes3.Length; i++)
    //         {
    //             Console.Write(genes3[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes4.Length; i++)
    //         {
    //             Console.Write(genes4[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes5.Length; i++)
    //         {
    //             Console.Write(genes5[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes6.Length; i++)
    //         {
    //             Console.Write(genes6[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes7.Length; i++)
    //         {
    //             Console.Write(genes7[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes8.Length; i++)
    //         {
    //             Console.Write(genes8[i] + " ");
    //         }
    //         Console.WriteLine();
    //         for (int i = 0; i < genes9.Length; i++)
    //         {
    //             Console.Write(genes9[i] + " ");
    //         }

   
            return currentGeneration;
        }

        // private IChromosome[] SelectRightParent()
        // {
        //     var currentGeneration = _currentGeneration as Generation;
        //     var parent1 = currentGeneration.SelectParent();
        //     var parent2 = currentGeneration.SelectParent();
        //     while (parent1 == parent2){
        //         parent2 = currentGeneration.SelectParent();
        //     }
        //     return new IChromosome[]{parent1, parent2};
        // }
    }
}
