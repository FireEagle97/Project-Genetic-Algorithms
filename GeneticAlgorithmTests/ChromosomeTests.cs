using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests
{
[TestClass]
public class ChromosomeTests
{
    [TestMethod]
    public void compareToTest()
    {
        Chromosome chromosome1 = new Chromosome(243,4);
        Chromosome chromosome2 = new Chromosome(243,3);
        chromosome1.Fitness = 2;
        chromosome2.Fitness = 2 ;
        Assert.AreEqual(chromosome1.CompareTo(chromosome2), 0);
    }
    [TestMethod]
    public void reproduceTest(){
        Chromosome chromosome1 = new Chromosome(243,4);
        Chromosome chromosome2 = new Chromosome(243,4);
        IChromosome[] chromList = chromosome1.Reproduce(chromosome2,6);
        var count1 = 0;
        var count2 = 0;
        for(var j =0; j< chromList[0].Length;j++){
    
            if(chromList[0][j] != chromosome1[j]){
                count1++;
            }

        }
        for(var j =0; j< chromList[0].Length;j++){
    
            if(chromList[0][j] != chromosome2[j]){
                count2++;
            }

        }
        Assert.AreEqual(count1, count2);
    }
}
}
