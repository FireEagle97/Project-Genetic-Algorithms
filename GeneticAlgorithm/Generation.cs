using System;
namespace GeneticAlgorithm{
    
    internal class Generation : IGenerationDetails
    {
        private IChromosome[] _chromosomeArray;
        private FitnessEventHandler _fitnessFunction;
        private IGeneticAlgorithm _geneticAlgorithm;

        private Random _rnd;


        // One that takes the IGeneticAlgorithm, FitnessEventHandler, and a potential seed
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessFunction, int? seed = null)
        {
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessFunction = fitnessFunction;
            _chromosomeArray = new IChromosome[geneticAlgorithm.PopulationSize];
            for (int i = 0; i < geneticAlgorithm.PopulationSize; i++)
            {
                _chromosomeArray[i] = new Chromosome(geneticAlgorithm.NumberOfGenes, geneticAlgorithm.LengthOfGene, seed);
            }          
        }
            
  

        //constructor that performs a deep copy of the generation based on an array of IChromosomes
        public Generation(IGenerationDetails other)
        {
            _chromosomeArray = new IChromosome[other.NumberOfChromosomes];
            for (int i = 0; i < other.NumberOfChromosomes; i++)
            {
                _chromosomeArray[i] = new Chromosome((Chromosome)other[i]);
            }
            
        }
      
       /// <summary>
        /// Retrieves the IChromosome from the generation
        /// </summary>
        /// <value>The selected IChromosome</value>
        public IChromosome this[int index] => _chromosomeArray[index];
    

        /// <summary>
        /// The average fitness across all Chromosomes
        /// </summary>
        /// <value>The average fitness</value>
        
        public double AverageFitness { get; set; }

        /// <summary>
        /// The maximum fitness across all Chromosomes 
        /// </summary>
        /// <value>The maximum fitness</value>
        
        public double MaxFitness { get; set;}

         /// <summary>
        /// Returns the number of Chromosomes in the generation
        /// </summary>
        /// <value>The number of Chromosomes in the generation</value>
        public long NumberOfChromosomes => _chromosomeArray.Length;


        /// <summary>
        /// Computes the fitness of all the Chromosomes in the generation. 
        /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
        /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
        /// the average of the number of trials is used to compute the final fitness of the Chromosome.
        /// </summary>
        public void EvaluateFitnessOfPopulation()
        {
            double totalFitness = 0;
            double maxFitness = 0;
            for (int i = 0; i < NumberOfChromosomes; i++)
            {
                double fitness = 0;
                for (int j = 0; j < _geneticAlgorithm.NumberOfTrials; j++)
                {
                    fitness += _fitnessFunction(_chromosomeArray[i], this);
                }
                fitness /= _geneticAlgorithm.NumberOfTrials;
                Chromosome ch = _chromosomeArray[i] as Chromosome;
                ch.Fitness = fitness;
                totalFitness += fitness;
                if (fitness > maxFitness)
                {
                    maxFitness = fitness;
                }
            }
            AverageFitness = totalFitness / NumberOfChromosomes;
            MaxFitness = maxFitness;
            //sort the chromosomes by fitness
            Array.Sort(_chromosomeArray, (x, y) => y.Fitness.CompareTo(x.Fitness));
        }

        /// <summary>
        /// sorts the array of ChromosomeArray by fitness - 
        /// </summary>
        //TODO use it or remove it after testing
        // public void SortByFitness()
        // {
        //     Array.Sort(_chromosomeArray, (x, y) => x.Fitness.CompareTo(y.Fitness));
        // }

        /// <summary>
        /// Randomly selects a parent by comparing its fitness to others in the population
        /// use compare to method of IChromosome
        /// </summary>
        /// <returns></returns>

        public IChromosome SelectParent()
        {
            double totalFitness = 0;
            for (int i = 0; i < NumberOfChromosomes; i++)
            {
                totalFitness += _chromosomeArray[i].Fitness;
            }
            double randomFitness = _rnd.NextDouble() * totalFitness;
            double fitnessSoFar = 0;
            for (int i = 0; i < NumberOfChromosomes; i++)
            {
                fitnessSoFar += _chromosomeArray[i].Fitness;
                if (fitnessSoFar >= randomFitness)
                {
                    return _chromosomeArray[i];
                }
            }
            return _chromosomeArray[NumberOfChromosomes - 1];
        }

        /// <summary>
        /// returns chromosome array
        /// </summary>
        /// <returns></returns>
        public IChromosome[] GetChromosomeArray()
        {
            return _chromosomeArray;
        }


        }
    }
