using System;
using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests
{
    [TestClass]
    public class GeneticTests
    {

          private double computeFitness(IChromosome chromosome, IGeneration generation)
        {
            return 3;
        }
        [TestMethod]
        //test GeneticAlgorithm constructor
        public void TestConstructor()
        {
            //Arrange
            int populationSize = 10;
            int numberOfGenes = 10;
            int lengthOfGene = 6;
            double mutationRate = 0.1;
            double eliteRate = 0.1;
            int numberOfTrials = 10;
            FitnessEventHandler? fitnessEventHandler = computeFitness;
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
        public void TestGenerateGeneration()
        {
            //Arrange
            int populationSize = 10;
            int numberOfGenes = 10;
            int lengthOfGene = 6;
            double mutationRate = 0.1;
            double eliteRate = 0.1;
            int numberOfTrials = 10;
            FitnessEventHandler? fitnessEventHandler = computeFitness;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
            //Act
            IGeneration? generation = geneticAlgorithm.GenerateGeneration();
            //Assert
            Assert.AreEqual(generation?.NumberOfChromosomes, populationSize);

        }


        //test GenerateNextGeneration method
        [TestMethod]
        public void TestGenerateNextGeneration()
        {
            //Arrange
            int populationSize = 10;
            int numberOfGenes = 10;
            int lengthOfGene = 6;
            double mutationRate = 0.1;
            double eliteRate = 0.1;
            int numberOfTrials = 10;
            FitnessEventHandler? fitnessEventHandler = computeFitness;
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm1 = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
            //Act
            IGeneration? generation = geneticAlgorithm.GenerateGeneration();
            IGeneration? nextGeneration = geneticAlgorithm1.GenerateGeneration();
            //Assert
            Assert.AreEqual(generation?.NumberOfChromosomes, nextGeneration?.NumberOfChromosomes);
        }

        //test selectElite method
        [TestMethod]
        public void TestSelectElite()
        {
            //Arrange
            int populationSize = 100;
            int numberOfGenes = 10;
            int lengthOfGene = 6;
            double mutationRate = 0.05;
            double eliteRate = 0.1;
            int numberOfTrials = 1;
            FitnessEventHandler? fitnessEventHandler = computeFitness;
            var eliteCount = (int)Math.Round(eliteRate * populationSize);
            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler);
            IGeneration? generation = geneticAlgorithm.GenerateGeneration();
            //Act
            IChromosome[] elite = geneticAlgorithm.SelectElites();
            //Assert
            Assert.AreEqual(elite.Length, eliteCount);
        }

    }
}
