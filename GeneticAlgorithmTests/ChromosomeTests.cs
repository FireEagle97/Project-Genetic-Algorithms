using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests
{
[TestClass]
public class ChromosomeTests
{

    //Test the constructor
    [TestMethod]
    public void testConstructor()
    {
        //Arrange
        int numberOfGenes = 10;
        int seed = 1;
        int lengthOfGene = 7;
        //Act
        Chromosome chromosome = new Chromosome(numberOfGenes,lengthOfGene, seed);
        //Assert
        Assert.AreEqual(numberOfGenes, chromosome.Length);
        Assert.AreEqual(10, chromosome.Length);
        Assert.AreEqual(7, chromosome.LengthOfGene);
    }

   
    //Test the CompareTo method
    [TestMethod]
    public void testCompareTo()
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
    public void  copyConsturctorTest(){
        //Arrange
        int numberOfGenes = 10;
        int seed = 1;
        int lengthOfGene = 7;
        //Act
        Chromosome chromosome1 = new Chromosome(numberOfGenes, lengthOfGene, seed);
        Chromosome chromosome2 = new Chromosome(chromosome1);
        //Assert
        Assert.AreEqual(chromosome1.NumOfGenes,chromosome2.NumOfGenes);
        Assert.AreEqual(chromosome1.LengthOfGene,chromosome1.LengthOfGene);
        Assert.AreEqual(chromosome1.Fitness, chromosome1.Fitness);
        Assert.AreEqual(chromosome1.Length, chromosome1.Length);
        Assert.AreEqual(chromosome1.Rnd.Equals(chromosome2.Rnd), true);

    }


    //Test the Reproduce method Length 
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
