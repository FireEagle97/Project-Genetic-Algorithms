namespace GeneticAlgorithm{
    
    internal class Generation : IGenerationDetails
    {
        //public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);

        private IChromosome[] _chromosomeArray;
        private IGeneticAlgorithm _geneticAlgorithm;
        private FitnessEventHandler _fitnessFunction;
        private int? _seed;


        // One that takes the IGeneticAlgorithm, FitnessEventHandler, and a potential seed

        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessFunction, int? seed)
        {
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessFunction = fitnessFunction;
            _seed = seed;    
        }

        //constructor that performs a deep copy of the generation based on an array of IChromosomes
        public Generation(IGenerationDetails generation)
        {
            _chromosomeArray = new IChromosome[generation.NumberOfChromosomes];
            for (int i = 0; i < generation.NumberOfChromosomes; i++)
            {
                //TODO: Implement deep copy of IChromosome
                _chromosomeArray[i] = generation[i];
                // _chromosomeArray[i] = new Chromosome(generation[i]);
            }
        }
      
        public IChromosome this[int index] { get{
            return _chromosomeArray[index];
            }
        }

        public double AverageFitness { get; }

        public double MaxFitness { get; }

        public long NumberOfChromosomes { get; }


        /// <summary>
        /// Computes the fitness of all the Chromosomes in the generation. 
        /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
        /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
        /// the average of the number of trials is used to compute the final fitness of the Chromosome.
        /// </summary>

        public void EvaluateFitnessOfPopulation()
        {
            foreach(IChromosome element in _chromosomeArray){
                double fitness = 0;
                for(int i = 0; i < _geneticAlgorithm.NumberOfTrials; i++){
                    fitness += _fitnessFunction(element, this);
                }
                //unimplemented
                // element.Fitness = fitness / _geneticAlgorithm.NumberOfTrials;
            }
        }

        /// <summary>
        /// Randomly selects a parent by comparing its fitness to others in the population
        /// </summary>
        /// <returns></returns>
        public IChromosome SelectParent()
        {
            IChromosome chosenChromosome = _chromosomeArray[0];
            return chosenChromosome;
        }
    }
}