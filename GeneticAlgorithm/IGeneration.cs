namespace GeneticAlgorithm
{
    public delegate double FitnessEventHandler(IChromosome chromosome, IGeneration generation);
    /// <summary>
    /// Public interface representing an IGeneration
    /// </summary>
    public interface IGeneration
    {
        /// <summary>
        /// The average fitness across all Chromosomes
        /// </summary>
        double AverageFitness { get; }
        /// <summary>
        /// The maximum fitness across all Chromosomes
        /// </summary>
        double MaxFitness { get; }

        /// <summary>
        /// Returns the number of Chromosomes in the generation
        /// </summary>
        long NumberOfChromosomes { get; }

        /// <summary>
        /// Retrieves the IChromosome from the generation
        /// </summary>
        /// <value>The selected IChromosome</value>
        IChromosome this[int index] { get; }


    }

    /// <summary>
    /// Internal interface that hides specific implementation details of Generation
    /// </summary>
    internal interface IGenerationDetails : IGeneration
    {
        /// <summary>
        /// Randomly selects a parent by comparing its fitness to others in the population
        /// </summary>
        /// <returns></returns>
        IChromosome SelectParent();

        /// <summary>
        /// Computes the fitness of all the Chromosomes in the generation. 
        /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
        /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
        /// the average of the number of trials is used to compute the final fitness of the Chromosome.
        /// </summary>
        void EvaluateFitnessOfPopulation();
    }
}