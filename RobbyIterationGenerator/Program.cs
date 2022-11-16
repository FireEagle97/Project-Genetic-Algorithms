using System;
using GeneticAlgorithm;
using RobbyTheRobot;

namespace RobbyIterationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Robby instance    

            //Ask user the number of generations
            Console.WriteLine("How many generations do you want to run?");
            int numberOfGenerations = Convert.ToInt32(Console.ReadLine());

            //the number of population
            Console.WriteLine("How many population do you want to run?");
            int populationSize = Convert.ToInt32(Console.ReadLine());
            //the number of literations
            Console.WriteLine("How many literations do you want to run?");
            int numberOfTrials = Convert.ToInt32(Console.ReadLine());

            //Create a new instance of Robby
            IRobbyTheRobot robby = RobbyLib.CreateRobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials);

            //prompts the user where to save the text file
            Console.WriteLine("Where do you want to save the text file?");
            string path = Console.ReadLine();

            //Create a new instance of the RobbyIterationGenerator
            robby.GeneratePossibleSolutions(path);


           //print out the progress of the solution generation using the appropriate event handler 

           //When the generation is complete the UI should report how long the generation process too





                    
                    
        }

        
    }
}
