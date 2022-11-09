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
            return grid;
            } else {
                throw new ArgumentException("Gridsize must be a squared number");
            }
        }

        public void GeneratePossibleSolutions(string folderPath){
            //Create GA? What is the length of a gene? What is FitnessHandler doing here
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(PopulationSize, 243, 1, MutationRate, EliteRate, NumberOfTrials,);

            //Create the grid
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            //Random number generator needed in ScoreForAllele
            Random random = new Random();

            for(int j = 0; j < NumberOfGenerations; j++){
                //Create the generation
                Generation generation = geneticAlgorithm.GenerateGeneration();
                //List of int[] to hold all arrays of moves
                List<String> listOfStringOfNumberOfActions = new List<String>();

                for(int k = 0; k < PopulationSize; k++){
                    //Variable to hold the score
                    int score = 0;
                    //Get the Chromosome
                    Chromosome chromosome = generation[k];
                    //array of actions Robby performed to finish for a single chromosome
                    String StringOfNumberOfActions = "";
                    //x and y initial positions
                    int x = 0;
                    int y = 0;
                    
                    for(int l = 0; l < NumberOfActions; l++){
                        //Store gene 
                        StringOfNumberOfActions += //gene here
                        //Add move to the score
                        score += RobbyHelper.ScoreForAllele(chromosome.Genes, grid, random, x, y);

                        //Ends the scoring if all cans are found.
                        bool endScoringCheck = true;
                        foreach(var content in grid){
                            if(content == ContentsOfGrid.Can){
                                endScoringCheck = false;
                                break;
                            }
                        }
                        //If all cans are picked up
                        if(endScoringCheck){
                            Console.WriteLine("No more cans were found! Breaking the loop!");
                            //Adds the moves done during the run to the list
                            listOfStringOfNumberOfActions.Add(StringOfNumberOfActions);
                            //Saves the score
                            chromosome.Fitness = score; 
                            break;
                        }
                        //If loop is about to end
                        if(l == NumberOfActions-1){
                            //Adds the moves done during the run to the list
                            listOfStringOfNumberOfActions.Add(StringOfNumberOfActions);
                            //Saves the score
                            chromosome.Fitness = score;
                        } 
                    }
                }

                //Save the top candidate on generations 1, 20, 100, 200, 500, 1000
                if(j == 0 || j == 19 || j == 99 || j == 119 || j == 499 || j == 999){
                    //Find the top candidate

                    //Put data in file or prepare it in a comma separated list like so:
                    //max score, number of moves to display, all moves

                    //Loop to find max score in arrayOfScores
                    //calculate number of actions by counting all moves 
                    //all moves it took

                    //Write/Save to file 😖
                }
            }
        }   
    }
}
