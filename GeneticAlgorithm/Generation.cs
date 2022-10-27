namespace GeneticAlgorithm{
    
    internal class Generation : IGenerationDetails
    {
        //public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);

        private IChromosome[] _chromosomeArray;

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