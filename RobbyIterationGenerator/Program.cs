using System;
using System.Threading.Tasks;
using RobbyTheRobot;

namespace RobbyIterationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //UI
            Console.WriteLine("Please input the number of generations:");
            String numberOfGenerations = Console.ReadLine();
            Console.WriteLine("Please input the population size:");
            String populationSize = Console.ReadLine();
            Console.WriteLine("Please input the number of trials:");
            String numberOfTrials = Console.ReadLine();
            Console.WriteLine("Please input the folder path:");
            String folderPath = Console.ReadLine();


            //Robby instance creation        
            IRobbyTheRobot robby = Robby.CreateRobby(Convert.ToInt32(numberOfGenerations), Convert.ToInt32(populationSize), Convert.ToInt32(numberOfTrials));
            
            //Use a Task to run the code
            Task task = Task.Run(() => {
                robby.GeneratePossibleSolutions(folderPath);
            });
                

            //Something tos stop the execution
            Console.WriteLine("Press x if you wish to stop the execution of the program");
                //Add task stopping method here

            //Temporary
            task.Wait();
        }

        
    }
}
