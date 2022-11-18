using System;
using System.Diagnostics;
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

            Console.WriteLine("How many generations do you want to run? (recmmended 1000)");
            int numberOfGenerations = Convert.ToInt32(Console.ReadLine());
            TakeUserInput(ref numberOfGenerations);


            Console.WriteLine("What is the population size? (recommended 200)");
            //if user input is a letter, it will throw an exception
            int populationSize = Convert.ToInt32(Console.ReadLine());
            TakeUserInput(ref populationSize);


            Console.WriteLine("How many trials do you want to run? (recommended 40)");
            int numberOfTrials = Convert.ToInt32(Console.ReadLine());
            TakeUserInput(ref numberOfTrials);



            //Create a new instance of Robby (has seed for testing)
            IRobbyTheRobot robby = Robby.CreateRobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials);

            //prompts the user where to save the text file
            Console.WriteLine("Where do you want to save the text file?");
            string path = Console.ReadLine();
            Console.WriteLine("Press x if you wish to stop the execution of the program");


            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //Start the thread
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Task task = Task.Run(() =>
            {
                Console.WriteLine("Task started");
                robby.GeneratePossibleSolutions(path);
            }, token);


            //Read for input that equals 'x'
            char key = Console.ReadKey().KeyChar;
            if (key == 'x')
            {
                tokenSource.Cancel();
                Console.WriteLine("Task cancellation requested");
            }
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            task.Wait();
            watch.Stop();
            Console.WriteLine("Generation took {0} milliseconds", watch.ElapsedMilliseconds);
        }

        public static void TakeUserInput(ref int input)
        {
            bool valid = false;
            while (!valid)
            {

                if (input > 0)
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                    input = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

    }
}

