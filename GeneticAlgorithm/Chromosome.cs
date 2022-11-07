using System;
namespace GeneticAlgorithm
{

    public class Chromosome : IChromosome
    {
        private int[] _genes;
        private double _fitness;
        private Random _rnd;

        //One that takes the number of genes, the length of a gene, and a potential seed
        public Chromosome(int numberOfGenes, int seed)
        {
            _rnd = new Random(seed);
            // _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
            NumOfGenes = numberOfGenes;
            _genes = new int[NumOfGenes];
            for (int i = 0; i < NumOfGenes; i++)
            {
                _genes[i] = _rnd.Next(0, 6);
            }
            PotSeed = seed;
            

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

        
        // int[] Genes { get; }
        public int[] Genes => _genes;

        public int NumOfGenes{
            get;
        }
        public int PotSeed{
            get;
        }


        /// <summary>
        /// Returns the current gene at the provided position
        /// </summary>
        /// <value></value>

        public int this[int index]
        {
            //use Genes
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
        /// muation function
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

        //this is the better way test both
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {
            Chromosome[] children = new Chromosome[2];
        //     children[0] = new Chromosome(this);
        //     children[1] = new Chromosome((Chromosome)spouse);
        //     int splitPoint = _rnd.Next(0, _genes.Length);
        //     for (int i = splitPoint; i < _genes.Length; i++)
        //     {
        //         children[0][i] = spouse[i];
        //         children[1][i] = this[i];
        //         // children[0].Genes[i] = spouse[i];
        //         // children[1].Genes[i] = this[i];
        //     }
        //     // children[0] = mutate(children[0], mutationProb);
        //     // children[1] = mutate(children[1], mutationProb);
        //     return children;
        // // }

        //test with this too
            // Chromosome child1 = new Chromosome(this.NumOfGenes, this.PotSeed);
            // Chromosome child2 = new Chromosome(this.NumOfGenes, this.PotSeed);
            // int[] parent1Genes = this.Genes;
            // int[] parent2Genes = spouse.Genes;
            // int crossoverPoint = _rnd.Next(0, this.NumOfGenes);
            // for(var i = 0; i < crossoverPoint; i++){
            //     child1.Genes[i] = parent1Genes[i];
            //     child2.Genes[i] = parent2Genes[i];
            // }
            // for(var i = crossoverPoint; i < this.NumOfGenes; i++){
            //     child1.Genes[i] = parent2Genes[i];
            //     child2.Genes[i] = parent1Genes[i];
            // }
            // children[0] = mutate(child1, mutationProb);
            // children[1] = mutate(child2, mutationProb);
            // // children[0] = child1;
            // // children[1] = child2;
            // return children;
            var lowerBound = _rnd.Next(0,121);
            var upperBound = _rnd.Next(121,243);
            Chromosome[] childChromosomes = new Chromosome[2]; 
            Chromosome child1 = new Chromosome(243,5);
            Chromosome child2 = new Chromosome(243,4);
            for(var i=0; i<spouse.Length;i++){
                if(i >= lowerBound && i <= upperBound){
                    child1[i]=this.Genes[i];
                }
                child1[i]= spouse[i];
            }
            for(var j=0; j<this.Genes.Length;j++){
                if(j>= lowerBound && j<= upperBound){
                    child2[j]= spouse[j];
                }
                child2[j] = this.Genes[j];
            }
        
            childChromosomes[0]= this.mutate(child1,mutationProb);
            childChromosomes[1]=this.mutate(child2,mutationProb);
            return childChromosomes;


        }
        //Chromosomes should be compared based on their Fitness
        //Test both these functions
        // public int CompareTo(IChromosome other)
        // {
        //     if (other == null)
        //     {
        //         return 1;
        //     }
        //     if (this.Fitness > other.Fitness)
        //     {
        //         return 1;
        //     }
        //     else if (this.Fitness < other.Fitness)
        //     {
        //         return -1;
        //     }
        //     else
        //     {
        //         return 0;
        //     }
        // }
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