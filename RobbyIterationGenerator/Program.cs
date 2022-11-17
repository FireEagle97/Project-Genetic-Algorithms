using System;
using System.Threading;
using System.Threading.Tasks;
using GeneticAlgorithm;
using RobbyTheRobot;

namespace RobbyIterationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //when something is written 
            
            //Ask user the number of generations
            Console.WriteLine("How many generations do you want to run?");
            int numberOfGenerations = Convert.ToInt32(Console.ReadLine());

            //the number of population
            Console.WriteLine("How many population do you want to run?");
            int populationSize = Convert.ToInt32(Console.ReadLine());
            //the number of literations
            Console.WriteLine("How many Trails do you want to run?");
            int numberOfTrials = Convert.ToInt32(Console.ReadLine());

            //Create a new instance of Robby
            IRobbyTheRobot robby = Robby.CreateRobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, 1);

            //prompts the user where to save the text file
            Console.WriteLine("Where do you want to save the text file?");
            string path = Console.ReadLine();
            Console.WriteLine("Press x if you wish to stop the execution of the program");

            //an event is risen WriteFileHandler FileWritten is called
            //cast it as RobbyTheRobot
            
            IRobbyTheRobot.FileWritten += printMessage;
            

            Thread thr = new Thread(() => robby.GeneratePossibleSolutions(path));
            thr.Start();


        //when user enters x finish the thread
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "x")
                {
                    thr.Abort();
                    break;
                }
                else
                {
                    Console.WriteLine("Enter x to stop the program");
                }
            }
        
            
        }

        public static void printMessage(string message)
        {
            Console.WriteLine(message);
        }
        //method to subsribe to event writtenHandler


            //Create a new instance of the RobbyIterationGenerator


           //print out the progress of the solution generation using the appropriate event handler 

           //When the generation is complete the UI should report how long the generation process too            
                    
        }

        
    }

