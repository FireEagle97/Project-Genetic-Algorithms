using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests
{
[TestClass]
public class GenerationTests
{
    //Test Constructor
    [TestMethod]
    public void TestConstructor(){
         //Arrange
        int populationSize = 10;
        int numberOfGenes = 10;
        int lengthOfGene = 6;
        double mutationRate = 0.1;
        double eliteRate = 0.1;
        int numberOfTrials = 10;
        int seed = 4;
        FitnessEventHandler? fitnessEventHandler = null;
        GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler, seed);
        //Act
        Generation generation1 = new Generation(geneticAlgorithm, fitnessEventHandler,seed);
        Generation generation2 = new Generation(geneticAlgorithm, fitnessEventHandler,seed);
        //Assert
        Assert.AreEqual(generation1.AverageFitness,generation2.AverageFitness);
        Assert.AreEqual(generation1.NumberOfChromosomes, generation1.NumberOfChromosomes);
        Assert.AreEqual(generation1.MaxFitness, generation2.MaxFitness);
        Assert.AreEqual(generation1.ChromosomesArray.Length, generation2.ChromosomesArray.Length);
    }

    //Test EvaluateFitnessOfPopulation method
    [TestMethod]
    public void TestEvaluateFitnessOfPopulation()
    {
        //Arrange
        int populationSize = 15;
        int numberOfGenes = 10;
        int lengthOfGene = 6;
        double mutationRate = 0.1;
        double eliteRate = 0.1;
        int numberOfTrials = 1;
        int seed = 4;
        FitnessEventHandler? fitnessEventHandler = computeFitness;
        GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler, seed);
        //Act
        Generation? generation = geneticAlgorithm.GenerateGeneration() as Generation;
        generation?.EvaluateFitnessOfPopulation();
        //Assert
        Assert.AreEqual(geneticAlgorithm.CurrentGeneration.AverageFitness, 3);
        Assert.AreEqual(geneticAlgorithm.CurrentGeneration.MaxFitness, 3);

    }
    private double computeFitness(IChromosome chromosome, IGeneration generation){
        return 3;
    }

    //Test Copy Constructor
    [TestMethod]
    public void TestCopyConstructor(){
         //Arrange
        int populationSize = 10;
        int numberOfGenes = 10;
        int lengthOfGene = 6;
        double mutationRate = 0.1;
        double eliteRate = 0.1;
        int numberOfTrials = 10;
        int seed = 4;
        FitnessEventHandler? fitnessEventHandler = null;
        GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler, seed);
        //Act
        Generation generation1 = new Generation(geneticAlgorithm, fitnessEventHandler,seed);
        Generation generation2 = new Generation(generation1);
        //Assert
        Assert.AreEqual(generation1.AverageFitness,generation2.AverageFitness);
        Assert.AreEqual(generation1.NumberOfChromosomes, generation1.NumberOfChromosomes);
        Assert.AreEqual(generation1.MaxFitness, generation2.MaxFitness);
        Assert.AreEqual(generation1.ChromosomesArray.Length, generation2.ChromosomesArray.Length);
    }

    //test SelectParent method
    [TestMethod]
    public void testSelectParent(){
        //Arrange
        int populationSize = 2;
        int numberOfGenes = 10;
        int lengthOfGene = 6;
        double mutationRate = 0.1;
        double eliteRate = 0.5;
        int numberOfTrials = 10;
        int seed = 4;
        FitnessEventHandler? fitnessEventHandler = computeFitness;
        GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler, seed);
        //Act
        Generation generation = new Generation(geneticAlgorithm, fitnessEventHandler,seed);
        double Bestfitness = generation.ChromosomesArray[0].Fitness;
        IChromosome? parent = generation.SelectParent();
        double parentFitness = parent.Fitness;
        //Assert
        Assert.AreEqual(Bestfitness, parentFitness);
    }
    

}
}
