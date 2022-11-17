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
        Chromosome chrom;
        public IGeneration GenerateGeneration()
        {
            if (_currentGeneration == null)
            {
                _currentGeneration = new Generation(new GeneticAlgorithm(_populationSize, _numberOfGenes, _lengthOfGene, _mutationRate, _eliteRate, _numberOfTrials, _fitnessCalculation, _seed), _fitnessCalculation, _seed);
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();
                // chrom = _currentGeneration[0] as Chromosome;
                
            }
            else
            {
                _currentGeneration = GenerateNextGeneration();
                // for(int j = 0; j < _currentGeneration[0].Length; j++){
                //         Console.WriteLine((_currentGeneration[0])[j]+ " " + chrom[j]);
                //     }
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
            var nextGeneration = new Generation(_currentGeneration);
    

            //start to populate after the elites
            for (var i =0; i < PopulationSize; i++){
                    var parent1 = nextGeneration.SelectParent();
                    var parent2 = nextGeneration.SelectParent();
                    //if parent1 and parent2 fitness are the same, then select a new parent2
                        //while (parent1.Fitness == parent2.Fitness){
                            //parent2 = nextGeneration.SelectParent();
                        //}
                    var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    //add the reproduced children to the ChildChromosomes
                    //     if (i == PopulationSize -1){
                    //     break;
                    // }
                    for(int j = 0; j < _currentGeneration[0].Length; j++){
                        Console.WriteLine(parent1[j]+ " " + (childrenGeneration[0])[j]);
                    }

                    nextGeneration[i] = childrenGeneration[0];
                    nextGeneration[i+1] = childrenGeneration[1];
                    i++;    
                }
       
            return nextGeneration;
        }
    }
}
