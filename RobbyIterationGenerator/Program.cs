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
            Console.WriteLine("What is the population size?");
            int populationSize = Convert.ToInt32(Console.ReadLine());
            //the number of literations
            Console.WriteLine("How many trials do you want to run?");
            int numberOfTrials = Convert.ToInt32(Console.ReadLine());

            //Create a new instance of Robby (has seed for testing)
            IRobbyTheRobot robby = Robby.CreateRobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, 1);

            //prompts the user where to save the text file
            Console.WriteLine("Where do you want to save the text file?");
            string path = Console.ReadLine();
            Console.WriteLine("Press x if you wish to stop the execution of the program");

            //an event is risen WriteFileHandler FileWritten is called
            //cast it as RobbyTheRobot
            
            //Cancellation token
            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //Start the thread
            Task task = Task.Run(() => {
                Console.WriteLine("Task started");
                robby.GeneratePossibleSolutions(path);
            }, token);

            //Read for input that equals 'x'
            char key = Console.ReadKey().KeyChar;
            if(key == 'x'){
                tokenSource.Cancel();
                Console.WriteLine("Task cancellation requested");
            }
            if(token.IsCancellationRequested){
                //Throw an exception if we want to cancel the thread
                token.ThrowIfCancellationRequested();
            }

            task.Wait();
        }                     
    }
}

