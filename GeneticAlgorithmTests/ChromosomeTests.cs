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
}
}
