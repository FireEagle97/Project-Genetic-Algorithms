namespace GeneticAlgorithm{
    
    internal class Generation : IGenerationDetails
    {
        private IChromosome[] _chromosomeArray;
        public IChromosome this[int index] { get{
            return _chromosomeArray[index];
            }
        }

        public double AverageFitness { get; }

        public double MaxFitness { get; }

        public long NumberOfChromosomes { get; }

        // One that takes the IGeneticAlgorithm, FitnessEventHandler, and a potential seed
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            _chromosomeArray = new IChromosome[geneticAlgorithm.PopulationSize];
            for (var i = 0; i < geneticAlgorithm.PopulationSize; i++)
            {
                _chromosomeArray[i] = new Chromosome(geneticAlgorithm.NumberOfGenes, geneticAlgorithm.LengthOfGene, seed);
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

        public void EvaluateFitnessOfPopulation()
        {
            fitnessCalculation?.Invoke();
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