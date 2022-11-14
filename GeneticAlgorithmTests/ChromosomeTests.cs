using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests
{
[TestClass]
public class ChromosomeTests
{

    //Test the constructor
    [TestMethod]
    public void TestConstructor()
    {
        //Arrange
        int numberOfGenes = 10;
        int seed = 1;
        //Act
        Chromosome chromosome = new Chromosome(numberOfGenes, seed);
        //Assert
        Assert.AreEqual(numberOfGenes, chromosome.Length);
        
        Assert.AreEqual(10, chromosome.Length);
    }

    //Test the Reproduce method Length
    [TestMethod]
    public void TestReproduce()
    {
        //Arrange
        int numberOfGenes = 10;
        int seed = 1;
        Chromosome chromosome = new Chromosome(numberOfGenes, seed);
        Chromosome chromosome1 = new Chromosome(numberOfGenes, seed);
        //Act
        IChromosome[] chromList = chromosome.Reproduce(chromosome1, 1);
        //Assert
        Assert.AreEqual(chromosome.Length, chromList[0].Length);
        Assert.AreEqual(chromosome1.Length, chromList[1].Length);
        Assert.AreNotEqual(chromosome, chromList[0]);
        Assert.AreNotEqual(chromosome1, chromList[1]);
    }

    //Test the CompareTo method
    [TestMethod]
    public void TestCompareTo()
    {
        //Arrange
        int numberOfGenes = 10;
        int seed = 1;
        Chromosome chromosome = new Chromosome(numberOfGenes, seed);
        Chromosome chromosome1 = new Chromosome(numberOfGenes, seed);

        //Act
        int result = chromosome.CompareTo(chromosome1);
        //Assert
        Assert.AreEqual(0, result);
    }



    [TestMethod]

    public void reproduceTest(){
        //Arrange
        int numberOfGenes = 254;
        int seed = 1;
        Chromosome chromosome = new Chromosome(numberOfGenes, 7, seed);
        Chromosome chromosome1 = new Chromosome(numberOfGenes, 7, seed);
        IChromosome[] chromList = chromosome.Reproduce(chromosome1, 1);
        var count1 = 0;
        var count2 = 0;
        //Act
        for(var i =0; i<chromList[0].Length;i++){
            if(chromList[0][i] != chromosome[i]){
                count1++;
            }
            if(chromList[1][i] != chromosome1[i]){
                count2++;
            }
        }
        //Assert
        Assert.AreEqual(count2,count1);
    }
}
}
