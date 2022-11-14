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
        Generation generation = (Generation)geneticAlgorithm.GenerateGeneration();
        generation.EvaluateFitnessOfPopulation();
        //Assert
        Assert.AreEqual(generation.AverageFitness, geneticAlgorithm.CurrentGeneration.AverageFitness);

    }
    //Test Generation Chromosome Array
    //to be revised
    [TestMethod]
    public void TestChromosomeArray()
    {
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
        Chromosome[] chromsomesArr = new Chromosome[geneticAlgorithm.PopulationSize];
        for (int i = 0; i < geneticAlgorithm.PopulationSize; i++)
            {
                chromsomesArr[i] = new Chromosome(geneticAlgorithm.NumberOfGenes, geneticAlgorithm.LengthOfGene, seed);
            }  
        //Act
        Generation generationTest = new Generation(geneticAlgorithm, fitnessEventHandler,seed);
        //Assert
        for(var i =0; i < chromsomesArr.Length;i++ ){
            Assert.AreEqual(chromsomesArr[i],generationTest.ChromosomesArray[i]);
        }
        // Assert.AreEqual<Chromosome[]>(chromsomesArr, generationTest.ChromosomesArray);
    }
    //Test Copy Constructor
    //need to test Chromosome Array?
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

}
}