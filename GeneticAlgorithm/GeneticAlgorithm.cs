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

        // Implements a private method call GenerateNextGeneration
        //combines the elite chromosomes and reproduced chromosomes in one generation
        // TODO: Implement GenerateNextGeneration
        private IGeneration GenerateNextGeneration()
        {
            Random rnd = new Random();
            var eliteCount = (int) Math.Round(_eliteRate * _populationSize);
            var currentGeneration = _currentGeneration as Generation;
            var eliteChromosomes = currentGeneration.getEliteChromosomes(eliteCount);
            var childChromosomes = new IChromosome[_populationSize - eliteCount];
            Chromosome[] newGenerationChromosomes = new Chromosome[PopulationSize];
            var newGeneration = new Generation(this,FitnessCalculation, _seed);
            for (var i =0; i > newGeneration.ChromosomesArray.Length; i++){
                 //add elite chromosomes to the New Generation chromsomes Array
                for(var k =0; k < eliteChromosomes.Length;k++){
                    newGenerationChromosomes[i] = eliteChromosomes[k] as Chromosome;
                    i++;
                }
                //add the rest of chromosomes to the new generation chromosomes Array
                for (var j = 0; i < childChromosomes.Length; j++)
                {
                    var parent1 = _currentGeneration[rnd.Next(0,(int)_currentGeneration.NumberOfChromosomes)];
                    var parent2 = _currentGeneration[rnd.Next(0,(int)_currentGeneration.NumberOfChromosomes)];
                    var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    //add the produced children to the ChildChromosomes
                    newGeneration.ChromosomesArray[i] = childrenGeneration[j];
                    i++;
                }

            }
            newGeneration.ChromosomesArray = newGenerationChromosomes;
            return newGeneration;
        }








    }
}
