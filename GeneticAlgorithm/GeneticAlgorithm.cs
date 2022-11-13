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
            }
            else
            {
                _currentGeneration = GenerateNextGeneration();
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
            var newGeneration = new Generation(this,FitnessCalculation, _seed);
            var elites = SelectElites();
            //copy the elites chromosomes
            for (var k =0; k < elites.Length;k++){
                newGeneration.ChromosomesArray[k] = elites[k];
            }
            for (var i =elites.Length; i < PopulationSize; i++){
                    var parent1 = currentGeneration.SelectParent();
                    var parent2 = currentGeneration.SelectParent();
                    var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    //add the reproduced children to the ChildChromosomes
                    for(var j =0; j < childrenGeneration.Length; j++) {
                        newGeneration.ChromosomesArray[i] = childrenGeneration[j];
                        i++;
                    }
                }
            return newGeneration;

        }
        /// <summary>
        /// This method returns the elite Chromosomes based in a sorted array of CHromosomesArray
        /// </summary>
        /// <returns></returns>
        public IChromosome[] SelectElites()
        {
            //Assuming that currentGeneration is sorted
            var eliteCount = (int) Math.Round(_eliteRate * _populationSize);
            var currentGeneration = _currentGeneration as Generation;
            var elites = new IChromosome[eliteCount];
            for (var i = 0; i < eliteCount; i++)
            {
                elites[i] = currentGeneration.ChromosomesArray[i];
            }
            return elites;
        }
    }
}
