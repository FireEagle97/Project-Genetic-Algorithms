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
            NumberOfGenerations = numberOfGenerations;
            PopulationSize = populationSize;
            NumberOfTrials = numberOfTrials;
            //Instructions stipulate that the size of the grid is 100
            GridSize = 100;
            //and Robby can do 200 possible actions
            NumberOfActions = 200;
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

        public void GeneratePossibleSolutions(string folderPath)
        {
            for(int i = 0; i < NumberOfTrials; i++){
                //Create the grid
                ContentsOfGrid[,] grid = GenerateRandomTestGrid();
                for(int j = 0; j < NumberOfGenerations; j++){
                    //Scores for all chromosomes
                    int[] arrayOfScores = new int[PopulationSize];
                    for(int k = 0; k < PopulationSize; k++){
                        //Variable to hold the score
                        int score = 0;
                        for(int l = 0; l < NumberOfActions; l++){
                            //Ends the scoring if all cans are found. I don't know if this is nesessary
                            bool endScoringCheck = true;
                            foreach(var content in grid){
                                if(content == ContentsOfGrid.Can){
                                    endScoringCheck = false;
                                }
                            }
                            if(endScoringCheck){
                                //Breaks this loop, ending the scoring
                                break;
                            }
                            score += RobbyHelper.ScoreForAllele(, grid, , , );
                        }
                        arrayOfScores[k] = score;
                    }
                    //Save certain candidates if on gen 1, 20, 100, 200, 500, 1000
                    if(j == 0 || j == 19 || j == 99 || j == 119 || j == 499 || j == 999){
                        //Put data in file or prepare it in a comma separated list like so:
                        //max score, number of moves to display, all moves

                        //Loop to find max score in arrayOfScores
                        //number of moves to display ???
                        //all moves is all genes of a Chromosome
                    }
                    //Create next gen???
                }
                //Reset the gen for next trial?
                NumberOfTestGrids += 1; //Unsure if it goes here
            }   
            //Write/Save to file 😖
        }

    }
}
