namespace GeneticAlgorithm{
    
    internal class Generation : IGenerationDetails
    {
        //public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);

        private IChromosome[] _chromosomeArray;



        // One that takes the IGeneticAlgorithm, FitnessEventHandler, and a potential seed
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            var random = seed.HasValue ? new Random(seed.Value) : new Random();
            _chromosomeArray = new IChromosome[geneticAlgorithm.PopulationSize];
            for (var i = 0; i < geneticAlgorithm.PopulationSize; i++)
            {
                _chromosomeArray[i] = new Chromosome(geneticAlgorithm, random);
            }
        }
        //  Performs a deep copy the generation based on an array of IChromosomes
        public Generation(IChromosome[] chromosomeArray)
        {
            _chromosomeArray = new IChromosome[chromosomeArray.Length];
            for (var i = 0; i < chromosomeArray.Length; i++)
            {
                _chromosomeArray[i] = chromosomeArray[i].Clone();
            }
        }
        public IChromosome this[int index] { get{
            return _chromosomeArray[index];
            }
        }

        public double AverageFitness { get; }

        public double MaxFitness { get; }

        public long NumberOfChromosomes { get; }

        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessEventHandler, int? seed = null){
            //deep copy?
        }

        public void EvaluateFitnessOfPopulation()
        {
            foreach(IChromosome element in _chromosomeArray){
                GeneticAlgorithm.FitnessCalculation(element, this);
            }
        }

        public IChromosome SelectParent()
        {
            IChromosome chosenChromosome = _chromosomeArray[0];
            return chosenChromosome;
        }
    }
}