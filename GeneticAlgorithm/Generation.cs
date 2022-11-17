using System;
namespace GeneticAlgorithm{
    
    internal class Generation : IGenerationDetails
    {
        private IChromosome[] _chromosomeArray;
        private FitnessEventHandler _fitnessFunction;
        private IGeneticAlgorithm _geneticAlgorithm;
        private Random _rnd;


        // One that takes the IGeneticAlgorithm, FitnessEventHandler, and a potential seed
        public Generation(IGeneticAlgorithm geneticAlgorithm, FitnessEventHandler fitnessFunction, int? seed = null)
        {
            _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
            _geneticAlgorithm = geneticAlgorithm;
            _fitnessFunction = fitnessFunction;
            _chromosomeArray = new IChromosome[geneticAlgorithm.PopulationSize];
            for (int i = 0; i < geneticAlgorithm.PopulationSize; i++)
            {
                _chromosomeArray[i] = new Chromosome(geneticAlgorithm.NumberOfGenes, geneticAlgorithm.LengthOfGene, seed);
            }          
        }
            
  
        public Generation(IGeneration generation)
        {
            _chromosomeArray = new IChromosome[generation.NumberOfChromosomes];
            for (int i = 0; i < generation.NumberOfChromosomes; i++)
            {
                _chromosomeArray[i] = new Chromosome((Chromosome)generation[i]);
            }
        }
       /// <summary>
        /// Retrieves the IChromosome from the generation
        /// </summary>
        /// <value>The selected IChromosome</value>
        public IChromosome this[int index] { 
            get{
                IChromosome chromosome = new Chromosome((Chromosome)_chromosomeArray[index]);
                return chromosome;
            }
        }
    

        /// <summary>
        /// The average fitness across all Chromosomes
        /// </summary>
        /// <value>The average fitness</value>
        
        public double AverageFitness { get; set; }

        /// <summary>
        /// The maximum fitness across all Chromosomes 
        /// </summary>
        /// <value>The maximum fitness</value>
        
        public double MaxFitness { get; set;}

         /// <summary>
        /// Returns the number of Chromosomes in the generation
        /// </summary>
        /// <value>The number of Chromosomes in the generation</value>
        public long NumberOfChromosomes => _chromosomeArray.Length;


        /// <summary>
        /// Computes the fitness of all the Chromosomes in the generation. 
        /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
        /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
        /// the average of the number of trials is used to compute the final fitness of the Chromosome.
        /// </summary>
        public void EvaluateFitnessOfPopulation()
        {
            double totalFitness = 0;
            double maxFitness = 0;
            for (int i = 0; i < NumberOfChromosomes; i++)
            {
                double fitness = 0;
                for (int j = 0; j < _geneticAlgorithm.NumberOfTrials; j++)
                {
                    fitness += _fitnessFunction(_chromosomeArray[i], this);
                }
                fitness /= _geneticAlgorithm.NumberOfTrials;
                Chromosome ch = _chromosomeArray[i] as Chromosome;
                ch.Fitness = fitness;
                totalFitness += fitness;
                if (fitness > maxFitness)
                {
                    maxFitness = fitness;
                }
            }
            AverageFitness = totalFitness / NumberOfChromosomes;
            MaxFitness = maxFitness;
            //sort the chromosomes by fitness
            Array.Sort(_chromosomeArray, (x, y) => y.Fitness.CompareTo(x.Fitness));

            //print the highest fitness chromosome
            // Console.WriteLine("Highest fitness: " + _chromosomeArray[0].Fitness);
            //loop to print the fitness of each chromosome
            // for (int i = 0; i < NumberOfChromosomes; i++)
            // {
            //     Console.WriteLine("Chromosome {0} fitness: {1}", i, _chromosomeArray[i].Fitness);
            // }
        }


        /// <summary>
        /// Randomly selects a parent by comparing its fitness to others in the population
        /// use compare to method of IChromosome
        /// </summary>
        /// <returns></returns>}
           public IChromosome SelectParent()
        {
            //create a elite array using the elite rate 
            int eliteArraySize = (int)(_geneticAlgorithm.EliteRate * NumberOfChromosomes);

            Random rand = new Random();
            //print the Fitenss of _ChromosomesArray[0] to check if it is the highest fitness
            //get a random chromosne from _chromosomeArray[i] where i is between 0 and eliteArraySize
            int randomIndex = rand.Next(0, eliteArraySize);
            
                        // Console.WriteLine("Fitness of _ChromosomesArray[0]: " + _chromosomeArray[randomIndex].Fitness);
                // int compare = _chromosomeArray[randomIndex].CompareTo(_chromosomeArray[0]);
            return _chromosomeArray[randomIndex];


            // Console.WriteLine("Elite Array Size: {0}", eliteArraySize);
            // IChromosome[] eliteArray = new IChromosome[eliteArraySize];

            //   for (var i = 0; i < eliteArraySize; i++)
            // {
            //     eliteArray[i] = _chromosomeArray[i];
            // }
            //Print the elite array fitness
            // for (int i = 0; i < eliteArraySize; i++)
            // {
            //     Console.WriteLine("Elite Array Chromosome {0} fitness: {1}", i, eliteArray[i].Fitness);
            // }
            //print the chromosomes of the elite array
            // for (int i = 0; i < eliteArraySize; i++)
            // {
            //     Console.WriteLine("Elite Chromosome {0} fitness: {1}", i, eliteArray[i].Fitness);
            // }
            // Console.WriteLine("elite array size: " + eliteArray.Length);



            // int random1 = _rnd.Next(0, eliteArraySize);
            // int random2 = _rnd.Next(0, eliteArraySize);
            // // var randIndex1 = _rnd.Next(0, this.ChromosomesArray.Length);
            // // var randIndex2 = _rnd.Next(0,this.ChromosomesArray.Length);
            // //get two random chromosomes

            // IChromosome randomChromosome1 = eliteArray[random1];
            // IChromosome randomChromosome2 = eliteArray[random2];
            //compare the two chromosomes
            // Console.WriteLine("random chromosome 1 fitness: " + randomChromosome1.Fitness);
            // Console.WriteLine("random chromosome 2 fitness: " + randomChromosome2.Fitness);
            // int compare = randomChromosome1.CompareTo(randomChromosome2);
            // // Console.WriteLine("compare: " + compare);



            // // var randChromosome1 = this.ChromosomesArray[randIndex1];
            // // var randChromosome2 = this.ChromosomesArray[randIndex2];
            // //compare the two chromosomes
            // // var compare = randChromosome1.CompareTo(randChromosome2);
            // //if the first chromosome is greater than the second, return the first chromosome
            // if (compare == 1)
            // {
            //     return randomChromosome1;
            // }
            // //if the second chromosome is greater than the first, return the second chromosome
            // else if (compare == -1)
            // {
            //     return randomChromosome2;
            // }
            // //if the chromosomes are equal, return the first chromosome
            // else
            // {
            //     return randomChromosome1;
            // }

        }
        //     if (this.ChromosomesArray[randIndex1].Fitness.CompareTo(this.ChromosomesArray[randIndex2].Fitness) > 0){
        //         return this.ChromosomesArray[randIndex1];
        //     }else {
        //         return this.ChromosomesArray[randIndex2];
        //     }
        // }

        /// <summary>
        /// returns chromosome array
        /// </summary>
        /// <returns></returns>
        public IChromosome[] ChromosomesArray
        {
            get{
                return _chromosomeArray;
            }
            // set{
            //     _chromosomeArray = value;
            // }

        }

        }
    }
