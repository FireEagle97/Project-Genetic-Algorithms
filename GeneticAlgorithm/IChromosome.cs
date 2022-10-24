using System;

namespace GeneticAlgorithm
{
    public interface IChromosome :IComparable<IChromosome>
    {
        /// <summary>
        /// The fitness score of the IChromosome
        /// </summary>
        /// <value>A value representing the fitness of the IChromosome</value>
        double Fitness {get;}

         int[] Genes { get; }

        /// <summary>
        /// Uses a crossover function to create two offspring, then iterates through the
        /// two child Chromosomes genes, changing them to random values according to the mutation rate.
        /// </summary>
        /// <param name="spouse">The Chromosome to reproduce with</param>
        /// <param name="mutationProb">The rate of mutation</param>
        /// <returns></returns>
        IChromosome[] Reproduce (IChromosome spouse, double mutationProb);

        /// <summary>
        /// Returns the current gene at the provided position
        /// </summary>
        /// <value></value>
        int this[int index] {get;}

        /// <summary>
        /// The length of the genes
        /// </summary>
        long Length {get;}

    }
}