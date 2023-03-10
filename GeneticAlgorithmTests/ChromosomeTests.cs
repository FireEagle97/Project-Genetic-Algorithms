using System;
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

    //Test the Reproduce method Length
    [TestMethod]
    public void testReproduce()
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

    //Test the copy constructor 
    [TestMethod]
    public void testCopyConstructor()
    {
        //Arrange
        int numberOfGenes = 10;
        int lengthOfGene = 7;
        int seed = 1;
        Chromosome chromosome = new Chromosome(numberOfGenes, lengthOfGene, seed);
        //Act
        Chromosome chromosome1 = new Chromosome(chromosome);
        //Assert
        Assert.AreEqual(chromosome.Length, chromosome1.Length);
        Assert.AreEqual(chromosome.LengthOfGene, chromosome1.LengthOfGene);
        for (int i = 0; i < chromosome.Length; i++)
        {
            Assert.AreEqual(chromosome[i], chromosome1[i]);
        }
    }
    
    [TestMethod]

    public void reproduceMutateTest(){
        //Arrange
        int numberOfGenes = 243;
        int seed = 1;
        double mutationRate = 0.01;
        Chromosome chromosome = new Chromosome(numberOfGenes, 7, seed);
        Chromosome chromosome1 = new Chromosome(numberOfGenes, 7, seed);
        //Assert
        var numChangedGenes = Math.Round(chromosome.Length * mutationRate);
        Chromosome chromList = chromosome.mutate(chromosome1, mutationRate);
        var count = 0;
        //Act
        //check if there is the same number of different genes
        for (int i = 0; i < chromosome.Length; i++)
        {
            if (chromosome[i] != chromList[i])
            {
                count++;
            }
        }
        Assert.AreEqual(numChangedGenes, count);

    }

    //test the reproduce crossover method
    [TestMethod]
    public void reproduceCrossoverTest(){
        //Arrange
        int numberOfGenes = 254;
        Chromosome chromosome = new Chromosome(numberOfGenes, 7);
        Chromosome chromosome1 = new Chromosome(numberOfGenes, 7);
        var count1 = 0;
        var count2 = 0;
        //Act
        IChromosome[] chromList = chromosome.Reproduce(chromosome1, 0);
        //check if there is the same number of different genes
    
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
