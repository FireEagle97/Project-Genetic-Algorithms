using System;
using System.Collections.Generic;

namespace RobbyTheRobot
{
    //delegate for notifying when a file has been written
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions {get; set;}
        public int NumberOfTestGrids {get; set;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}
        public double MutationRate {get;}
        public event WriteFileHandler FileWritten;
        public double EliteRate {get;}
        //Unsure if should exist
        public int PopulationSize {get;}
        //Unsure if should exist
        public int NumberOfTrials {get;}

        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null){
            //Instructions stipulate that the size of the grid is 100
            GridSize = 100;
            //and Robby can do 200 possible actions
            NumberOfActions = 200;
            //Arbitrary value according to the slides
            NumberOfTestGrids = 100;
            //Arbitrary as well
            MutationRate = 0.5;
            //Maybe arbitrary
            EliteRate = 0.5;
            NumberOfGenerations = numberOfGenerations;

            NumberOfTrials = numberOfTrials;
            PopulationSize = populationSize;
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            //Checks if grid can be divided into equal rows
            if (Math.Sqrt(GridSize) % 1 == 0){
                int rowSize = Convert.ToInt32(Math.Sqrt(GridSize));

                //Create new grid
                ContentsOfGrid[,] grid = new ContentsOfGrid[rowSize, rowSize];
                Random random = new Random();
                int gridHalf = GridSize/2;
                List<int> canCheckerList = new List<int>();

                //Populate it with cans
                for(int i = 0; i < gridHalf; i++){
                    int canNumber = random.Next(0, GridSize);

                    //If the can number is already filled, generate a new can number
                    while(canCheckerList.Contains(canNumber)){
                        canNumber = random.Next(0, GridSize);
                    }
                    //int to use to determine the position in the outer array
                    int canPositionOuter;
                    //int to use to determine the position in the inner array
                    int canPositionInner = canNumber;
                    
                    //Determine outer can position
                    canPositionOuter = Convert.ToInt32(Math.Floor((double)canNumber/rowSize));
                    //Determine inner can position
                    while(canPositionInner > rowSize-1){
                        canPositionInner -= rowSize-1;
                    }

                    //Insert can in grid
                    grid[canPositionOuter, canPositionInner] = ContentsOfGrid.Can;
                    //update canCheckerArray
                    canCheckerList.Add(canNumber);
                }

                //Fill everything else with .Empty (An Enum's default value is apparently 0)
                for(int i = 0; i < GridSize; i++){
                    for(int j = 0; j < rowSize; j++){
                        if(grid[i,j] == 0){
                            grid[i,j] = ContentsOfGrid.Empty;
                        }
                    }
                }

            return grid;
            } else {
                throw new ArgumentException("Gridsize must be a squared number");
            }
        }

        public void GeneratePossibleSolutions(string folderPath){
            //Create GeneticAlgorithm. What is the length of a gene?
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(PopulationSize, 243, 1, MutationRate, EliteRate, NumberOfTrials, ComputeFitness);

            for(int j = 0; j < NumberOfGenerations; j++){
                //Create the generation
                Generation generation = geneticAlgorithm.GenerateGeneration();

                for(int k = 0; k < PopulationSize; k++){
                  generation[k].Fitness = ComputeFitness(generation[k], generation);
                }

                //Save the top candidate on generations 1, 20, 100, 200, 500, 1000
                if(j == 0 || j == 19 || j == 99 || j == 119 || j == 499 || j == 999){
                    //Find the top candidate

                    //Put data in file in a comma separated list like so:
                    //max score, number of moves to display, all moves

                    //Chromosome.Fitness
                    //Chromosome.actionNumber
                    //Chromosome`s Genes[]

                    //Write/Save to file 😖
                }
            }
        }   

        public double ComputeFitness(IChromosome chromosome, IGeneration generation){
            //Variable to hold the score
            double score = 0;
            //number of actions it took for Robby to finish
            int actionsTaken = 0;
            //x and y initial positions
            int x = 0;
            int y = 0;

            //Create the grid
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            //Random number generator needed in ScoreForAllele
            Random random = new Random();
            
            for(int l = 0; l < NumberOfActions; l++){
                //Add move to the score
                score += RobbyHelper.ScoreForAllele(chromosome.Genes, grid, random, x, y);

                //Adds the amount of actions taken
                actionsTaken += 1;

                //Ends the scoring if all cans are found.
                bool endScoringCheck = false;
                foreach(var content in grid){
                    if(content == ContentsOfGrid.Can){
                        endScoringCheck = true;
                        break;
                    }
                }
                //If all cans are picked up
                if(endScoringCheck){
                    Console.WriteLine("No more cans were found! Breaking the loop!");
                    //Save actionsTaken in Chromosome`s actionNumber
                    chromosome.actionNumber = actionsTaken;
                    //Saves the score
                    return score; 
                }
                //If loop is about to end
                if(l == NumberOfActions-1){
                    //Save actionsTaken in Chromosome`s actionNumber
                    chromosome.actionNumber = actionsTaken;
                    //Saves the score
                    return score;
                } 
            }
            throw new ApplicationException("An end condition was never reached in ComputeFitness");
        }
    }
}
