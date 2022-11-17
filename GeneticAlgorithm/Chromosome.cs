using System;
namespace GeneticAlgorithm
{

    internal class Chromosome : IChromosome
    {
        private int[] _genes;
        private double _fitness;
        private int _lengthOfGene;
        private Random _rnd;

        //One that takes the number of genes, the length of a gene, and a potential seed
        public Chromosome(int numberOfGenes, int lengthOfGene, int ?seed = null)
        {
             _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
            _lengthOfGene = lengthOfGene;
            _genes = new int[numberOfGenes];
            for (int i = 0; i < numberOfGenes; i++)
            {
                _genes[i] = _rnd.Next(0, lengthOfGene);
            }
        }        
        
        /// <summary>
        /// Constructor that performs a deep copy of the chromosome based on an array of genes
        /// </summary>
        public Chromosome(Chromosome other)
        {
            _genes = new int[other.NumOfGenes];
            for (int i = 0; i < other.NumOfGenes; i++)
            {
                _genes[i] = other[i];
            }
            _fitness = other.Fitness;
            _lengthOfGene = other._lengthOfGene;
            _rnd = other._rnd;
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

         //lengthOfGene
        public int LengthOfGene => _lengthOfGene;
         

        public int NumOfGenes{
            get{
                return _genes.Length;
            }
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
            var numChangedGenes = Math.Round(child.Length*mutationRate);
            for (int i = 0; i < numChangedGenes; i++)
            {
                var changedIndex = _rnd.Next(0,(int)child.Length);
                child[changedIndex] = _rnd.Next(child.LengthOfGene);
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
            // Random rnd = new Random();
            Chromosome[] children = new Chromosome[2];
            //print this genes and spouse genes
            // Console.WriteLine("this genes");
            // foreach (var item in this.Genes)
            // {
            //     Console.Write(item);
            // }
            // Console.WriteLine();
            // Console.WriteLine("spouse genes");
            // foreach (var item in spouse.Genes)
            // {
            //     Console.Write(item);
            // }

            var child1 = new Chromosome(this);
            var child2 = new Chromosome((Chromosome)spouse);     
            int[] parent1Genes = this.Genes;
            //print the parent1Genes
            // Console.WriteLine("Parent1Genes: ");
            // for (int i = 0; i < parent1Genes.Length; i++)
            // {
            //     Console.Write(parent1Genes[i] + " ");
            // }
            int[] parent2Genes = spouse.Genes; 
            //print the parent2Genes
            // Console.WriteLine("Parent2Genes: ");
            // for (int i = 0; i < parent2Genes.Length; i++)
            // {
            //     Console.Write(parent2Genes[i] + " ");
            // }
            int point1 = _rnd.Next(0, NumOfGenes);
            int point2 = _rnd.Next(0, NumOfGenes);
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
            //print the children genes 
            // Console.WriteLine("Children genes: ");
            // for (int i = 0; i < children.Length; i++)
            // {
            //     for (int j = 0; j < children[i].NumOfGenes; j++)
            //     {
            //         Console.Write(children[i][j] + " ");
            //     }
            //     Console.WriteLine();
            // }

            return children;
        }

        /// <summary>
        /// compares the fitness of the current Chromosome to the fitness of the provided Chromosome
        /// </summary>
        /// <param name="other">The Chromosome to compare to</param>
        /// <returns></returns>
        
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