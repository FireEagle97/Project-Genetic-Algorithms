using System;
namespace GeneticAlgorithm
{

    internal class Chromosome : IChromosome
    {
        private int[] _genes;
        private double _fitness;
        

        private Random _rnd;

        //One that takes the number of genes, the length of a gene, and a potential seed
        public Chromosome(int numberOfGenes, int ?seed = null)
        {
            //initialise seed
             _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
            NumOfGenes = numberOfGenes;
            _genes = new int[NumOfGenes];
            for (int i = 0; i < NumOfGenes; i++)
            {
                _genes[i] = _rnd.Next(0, 6);
            }            

        }
        //Performs a deep copy of the Chromosome
        public Chromosome(Chromosome other)
        {
            _genes = new int[other._genes.Length];
            for (int i = 0; i < other._genes.Length; i++)
            {
                _genes[i] = other._genes[i];
            }
            _fitness = other._fitness;
        }

        /// <summary>
        /// The fitness score of the IChromosome
        /// </summary>
        /// <value>A value representing the fitness of the IChromosome</value>
        public double Fitness {
            get{
                return _fitness;
            }
            set{
                _fitness = value;
            }
        } 

        /// <summary>
        /// The length of the genes
        /// </summary>
        
        public long Length => _genes.Length;

         public int[] Genes => _genes;

        public int NumOfGenes{
            get;
        }
    


        /// <summary>
        /// Returns the current gene at the provided position
        /// </summary>
        /// <value></value>
        public int this[int index]
        {
            get
            {
                return Genes[index];
            }
            set
            {
                Genes[index] = value;
            }
        }


        /// <summary>
        /// changing them to random values according to the mutation rate.
        /// </summary>

        private Chromosome mutate(Chromosome child, double mutationRate)
        {
            var numChangedGenes = Math.Round(child.Length%mutationRate);
            for (int i = 0; i < numChangedGenes; i++)
            {
                var changedIndex = _rnd.Next(0,(int)child.Length);
                child[changedIndex] = _rnd.Next(0,6);
            }
            return child;
        }


        /// <summary>
        /// Uses a crossover function to create two offspring, then iterates through the
        /// two child Chromosomes genes, changing them to random values according to the mutation rate.
        /// </summary>
        /// <param name="spouse">The Chromosome to reproduce with</param>
        /// <param name="mutationProb">The rate of mutation</param>
        /// <returns></returns>
     
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            Chromosome[] children = new Chromosome[2];
            Chromosome child1 = new Chromosome(this.NumOfGenes);
            Chromosome child2 = new Chromosome(this.NumOfGenes);
            int[] parent1Genes = this.Genes;
            int[] parent2Genes = spouse.Genes;
            int point1 = _rnd.Next(0, this.NumOfGenes);
            int point2 = _rnd.Next(0, this.NumOfGenes);
            if (point1 > point2)
            {
                int temp = point1;
                point1 = point2;
                point2 = temp;
            }

            for (int i = 0; i < this.NumOfGenes; i++)
            {
                if (i < point1 || i > point2)
                {
                    child1[i] = parent1Genes[i];
                    child2[i] = parent2Genes[i];
                }
                else
                {
                    child1[i] = parent2Genes[i];
                    child2[i] = parent1Genes[i];
                }
            }
   
            // mutate the children
            children[0] = mutate(child1, mutationProb);
            children[1] = mutate(child2, mutationProb);
            return children;
        }

    
        public int CompareTo(IChromosome obj)
        {
            if (obj == null)
            {
                return 1;
            }
            Chromosome otherChromosome = obj as Chromosome;
            if (otherChromosome != null)
                return this.Fitness.CompareTo(otherChromosome.Fitness);
            else
                throw new ArgumentException("object is not a Chromosome");

        }

    }
}