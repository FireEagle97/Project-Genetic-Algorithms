namespace GeneticAlgorithm
{
    public interface IGeneticAlgorithm
    {
        int PopulationSize { get; }
        int NumberOfGenes { get; }
        int LengthOfGene { get; }
        double MutationRate { get; }
        double EliteRate { get; }

        /// <summary>
        /// The number of times the fitness function should be called when computing the result
        /// </summary>
        int NumberOfTrials {get;}

        /// <summary>
        /// The current number of generations generated since the start of the algorithm
        /// </summary>
        long GenerationCount {get;}

        /// <summary>
        /// Returns the current generation
        /// </summary>
        IGeneration CurrentGeneration {get;}

        /// <summary>
        /// The delegate of the fitness method to be called
        /// </summary>
        /// <value></value>
        FitnessEventHandler FitnessCalculation {get;}

        /// <summary>
        /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
        /// If a generation has already been created, it will provide the next generation.
        /// </summary>
        /// <returns>The current generation</returns>
        IGeneration GenerateGeneration();

    }
}