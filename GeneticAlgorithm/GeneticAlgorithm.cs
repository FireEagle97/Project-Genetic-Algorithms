using System;
namespace GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {
        private IGeneration _currentGeneration;
        private readonly int _populationSize;
        private readonly int _numberOfGenes;
        private readonly int _lengthOfGene;
        private readonly double _mutationRate;
        private readonly double _eliteRate;
        private readonly int _numberOfTrials;
        private readonly FitnessEventHandler _fitnessCalculation;
        private readonly int? _seed;
        private long _generationCount;


        //Contains a constructor that takes the population size, number of genes, length of genes, mutation rate, elite rate, number of trials, the fitness function, and a potential seed
        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            _populationSize = populationSize;
            _numberOfGenes = numberOfGenes;
            _lengthOfGene = lengthOfGene;
            _mutationRate = mutationRate;
            _eliteRate = eliteRate;
            _numberOfTrials = numberOfTrials;
            _fitnessCalculation = fitnessCalculation;
            _seed = seed;
        }

        //implements the interface
        public int PopulationSize => _populationSize;
        public int NumberOfGenes => _numberOfGenes;
        public int LengthOfGene => _lengthOfGene;
        public double MutationRate => _mutationRate;
        public double EliteRate => _eliteRate;
        public int NumberOfTrials => _numberOfTrials;
        public long GenerationCount => _generationCount;
        public IGeneration CurrentGeneration => _currentGeneration;
        public FitnessEventHandler FitnessCalculation => _fitnessCalculation;

        /// <summary>
        /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
        /// If a generation has already been created, it will provide the next generation.
        /// </summary>
        /// <returns>The current generation</returns>  

        public IGeneration GenerateGeneration()
        {
            if (_currentGeneration == null)
            {
                _currentGeneration = new Generation(new GeneticAlgorithm(_populationSize, _numberOfGenes, _lengthOfGene, _mutationRate, _eliteRate, _numberOfTrials, _fitnessCalculation, _seed), _fitnessCalculation, _seed);
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();
            }
            else
            {
                _currentGeneration = GenerateNextGeneration();
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();
            }
            _generationCount++;
            return _currentGeneration;
        }

        /// <summary>
        ///This method must create the next set of Chromosomes through reproduction
        /// The elite rate should be used to select only a subset of the best Chromosomes based on fitness - call SelectElites
        /// A new Generation should be created based on the resulting child Chromosomes
        /// </summary>
        /// <returns></returns>

             
        // private IGeneration GenerateNextGeneration()
        // {
        //     var currentGeneration = _currentGeneration as Generation;
        //     var newGeneration = new Generation(this,FitnessCalculation, _seed);
        //     var elites = SelectElites();
        //     //copy the elites chromosomes
        //     for (var k =0; k < elites.Length;k++){
        //         newGeneration.ChromosomesArray[k] = elites[k];
        //     }
        //     for (var i =elites.Length; i < PopulationSize; i++){
        //             var parent1 = currentGeneration.SelectParent();
        //             var parent2 = currentGeneration.SelectParent();
        //             var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
        //             //add the reproduced children to the ChildChromosomes
        //             newGeneration.ChromosomesArray[i] = childrenGeneration[0];
        //             newGeneration.ChromosomesArray[i+1] = childrenGeneration[1];
        //             i++;    
        //             // for(var j =0; j < childrenGeneration.Length; j++) {
        //             //     newGeneration.ChromosomesArray[i] = childrenGeneration[j];
        //             //     i++;
        //             // }
        //         }
        //     return newGeneration;

        // }

        // /// <summary>
        // /// This method returns the elite Chromosomes based in a sorted array of CHromosomesArray
        // /// </summary>
        // /// <returns></returns>
        // public IChromosome[] SelectElites()
        // {
        //     //Assuming that currentGeneration is sorted
        //     var eliteCount = (int) Math.Round(_eliteRate * _populationSize);
        //     var elites = new IChromosome[eliteCount];
        //     for (var i = 0; i < eliteCount; i++)
        //     {
        //         Console.WriteLine(i);
        //         Console.WriteLine(_currentGeneration[i]);
        //         elites[i] = _currentGeneration[i];
        //     }
        //     return elites;
        // }

          
        private IGeneration GenerateNextGeneration()
        {
            var currentGeneration = _currentGeneration as Generation;
            var newGeneration = new Generation(this,FitnessCalculation, _seed);
            var elites = SelectElites();
            //print the reference of the elites
            
            // for (var i =0; i < elites.Length;i++){
            //     Console.WriteLine(elites[i]);
            // }
            //copy the elites chromosomes
            for (var k =0; k < elites.Length;k++){
                newGeneration.ChromosomesArray[k] = elites[k];
            }
            for (var i =elites.Length; i < PopulationSize; i++){
                    var parent1 = currentGeneration.SelectParent();
                    var parent2 = currentGeneration.SelectParent();
                    var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    //add the reproduced children to the ChildChromosomes
                    newGeneration.ChromosomesArray[i] = childrenGeneration[0];
                    if(i == PopulationSize-1){ break;}
                    newGeneration.ChromosomesArray[i+1] = childrenGeneration[1];
                    
                    i++;    
               
                }
                //print the new generation
                // for (var i =0; i < newGeneration.ChromosomesArray.Length;i++){
                //     Console.WriteLine(newGeneration.ChromosomesArray[i]);
                // }
            return newGeneration;
        }
        //     return newGeneration;
        //     var newGeneration = new Generation(this,FitnessCalculation, _seed);
        //     var elitesGeneration = new Generation(this,FitnessCalculation, _seed);
        //     var elites = SelectElites();
        //     // elitesGeneration.ChromosomesArray = elites;
        //     // var elites = //new genertation a chromosome array of 
        //     //copy the elites chromosomes
        //     for (var k =0; k < elitesGeneration.ChromosomesArray.Length;k++){
        //         newGeneration.ChromosomesArray[k] = elitesGeneration.ChromosomesArray[k];
        //     }
        //     for (var i =elites.Length; i < PopulationSize; i++){
        //             var parent1 = elitesGeneration.SelectParent();
        //             var parent2 = elitesGeneration.SelectParent();
        //             var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
        //             //add the reproduced children to the ChildChromosomes
        //             newGeneration.ChromosomesArray[i] = childrenGeneration[0];
        //             newGeneration.ChromosomesArray[i+1] = childrenGeneration[1];
        //             i++;    
        //         }
        //         //call EvaluateFitnessOfPopulation
        //         newGeneration.EvaluateFitnessOfPopulation();
        //     return newGeneration;

        // }
        /// <summary>
        /// This method returns the elite Chromosomes based in a sorted array of CHromosomesArray
        /// </summary>
        /// <returns></returns>
        public IChromosome[] SelectElites()
        {
            //Assuming that currentGeneration is sorted
            var eliteCount = (int) Math.Round(_eliteRate * _populationSize);
            var elites = new IChromosome[eliteCount];
            for (var i = 0; i < eliteCount; i++)
            {
                // Console.WriteLine(i);
                // Console.WriteLine(_currentGeneration[i]);
                elites[i] = _currentGeneration[i];
            }
            return elites;
        }

    }
}
