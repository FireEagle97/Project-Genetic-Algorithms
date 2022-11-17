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
            _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessFunction = fitnessFunction;
            _chromosomeArray = new IChromosome[geneticAlgorithm.PopulationSize];
            for (int i = 0; i < geneticAlgorithm.PopulationSize; i++)
            {
                _chromosomeArray[i] = new Chromosome(geneticAlgorithm.NumberOfGenes, geneticAlgorithm.LengthOfGene, seed);
            }          
        }
            
  
        public Generation(IGeneration generation)
        {
            var generationGeneration = generation as Generation;
            _geneticAlgorithm = generationGeneration._geneticAlgorithm;
            _fitnessFunction = generationGeneration._fitnessFunction;
            _rnd = generationGeneration._rnd;
            _chromosomeArray = new IChromosome[generation.NumberOfChromosomes];
            for (int i = 0; i < generation.NumberOfChromosomes; i++)
            {
                _chromosomeArray[i] = new Chromosome((Chromosome)generation[i]);
            }
        }
       /// <summary>
        /// Retrieves the IChromosome from the generation
        /// </summary>
        /// <value>The selected IChromosome</value>
        public IChromosome this[int index] { 
            get{
                IChromosome chromosome = new Chromosome((Chromosome)_chromosomeArray[index]);
                return chromosome;
            }
            set{
                _chromosomeArray[index] = value;
            }
        }
    

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
            Console.WriteLine("Average Fitness: " + AverageFitness);
            MaxFitness = maxFitness;
            Array.Sort(_chromosomeArray, (x, y) => y.Fitness.CompareTo(x.Fitness));  
        }


        /// <summary>
        /// Randomly selects a parent by comparing its fitness to others in the population
        /// use compare to method of IChromosome
        /// </summary>
        /// <returns></returns>
           public IChromosome SelectParent(){          
            //calculate elite number
            int eliteNumber = (int)Math.Round(_geneticAlgorithm.EliteRate * NumberOfChromosomes);  

            int randomIndex1 = _rnd.Next(0, eliteNumber);
            int randomIndex2 = _rnd.Next(0, eliteNumber);
    
            if (_chromosomeArray[randomIndex1].CompareTo(_chromosomeArray[randomIndex2]) > 0)
            {
                // Console.WriteLine("first is greater");
                return _chromosomeArray[randomIndex1];
            }
            else
            {
                return _chromosomeArray[randomIndex2];
            }            
        }


        /// <summary>
        /// returns chromosome array
        /// </summary>
        /// <returns></returns>
        public IChromosome[] ChromosomesArray
        {
            get{
                return _chromosomeArray;
            }
            
        }

        }
    }
