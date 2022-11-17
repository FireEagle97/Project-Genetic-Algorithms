using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests{


    [TestClass]
    public class GeneticTests{

        [TestMethod]
        //test GeneticAlgorithm constructor
        public void TestConstructor(){
            //Arrange
            int populationSize = 10;
            int numberOfGenes = 10;
            int lengthOfGene = 6;
            double mutationRate = 0.1;
            double eliteRate = 0.1;
            int numberOfTrials = 10;
            FitnessEventHandler fitnessEventHandler = null;
            //Act
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
        
            //Assert
            Assert.AreEqual(populationSize, geneticAlgorithm.PopulationSize);
            Assert.AreEqual(numberOfGenes, geneticAlgorithm.NumberOfGenes);
            Assert.AreEqual(lengthOfGene, geneticAlgorithm.LengthOfGene);
            Assert.AreEqual(mutationRate, geneticAlgorithm.MutationRate);
            Assert.AreEqual(eliteRate, geneticAlgorithm.EliteRate);
            Assert.AreEqual(numberOfTrials, geneticAlgorithm.NumberOfTrials);
            Assert.AreEqual(fitnessEventHandler, geneticAlgorithm.FitnessCalculation);
        }

        [TestMethod]
        //test GenerateGeneration method
        public void TestGenerateGeneration(){
            //Arrange
            int populationSize = 10;
            int numberOfGenes = 10;
            int lengthOfGene = 6;
            double mutationRate = 0.1;
            double eliteRate = 0.1;
            int numberOfTrials = 10;
            FitnessEventHandler fitnessEventHandler = null;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
            //Act
            Generation generation = (Generation)geneticAlgorithm.GenerateGeneration();
            //Assert
            Assert.AreEqual(generation, geneticAlgorithm.CurrentGeneration);
        }
        // public void TestGenerateGeneration(){
        //     //Arrange
        //     int populationSize = 10;
        //     int numberOfGenes = 10;
        //     int lengthOfGene = 6;
        //     double mutationRate = 0.1;
        //     double eliteRate = 0.1;
        //     int numberOfTrials = 10;
        //     FitnessEventHandler fitnessEventHandler = null;
        //     GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
        //     //Act
        //     IGeneration generation = geneticAlgorithm.GenerateGeneration();
        //     //Assert
        //     Assert.AreEqual(generation, geneticAlgorithm.CurrentGeneration);
        // }

 
    }
}