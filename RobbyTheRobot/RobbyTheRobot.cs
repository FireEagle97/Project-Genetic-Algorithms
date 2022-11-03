using System;
using System.Collections.Generic;

namespace RobbyTheRobot
{
    //delegate for notifying when a file has been written
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions {get;}

        public int NumberOfTestGrids {get;}

        public int GridSize {get;}

        public int NumberOfGenerations {get;}

        public double MutationRate {get;}
        public WriteFileHandler WriteFile {get;}
        public double EliteRate {get;}
        //Unsure if should exist
        public int PopulationSize {get;}
        //Unsure if should exist
        public int NumberOfTrials {get;}
        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null){
            NumberOfGenerations = numberOfGenerations;
            PopulationSize = populationSize;
            NumberOfTrials = numberOfTrials;
            //set GridSize
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
                List<int> canCheckerList = new List<int>;

                //Populate it with cans
                for(int i = 0; i < gridHalf; i++){
                    int canNumber = random.Next(0, GridSize);

                    //If the can number is already filled, generate a new can number
                    while(canCheckerList.Contains(canNumber)){
                        canNumber = random.Next(0, GridSize);
                    }
                    //int to use to determine the position in the outer array
                    int canPositionOuter = canNumber;
                    //int to use to determine the position in the inner array
                    int canPositionInner = canNumber;
                    
                    //Determine outer can position
                    //Determine inner can position
                    while(canPositionInner > rowSize){
                        canPositionInner -= rowSize;
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
            throw new NotImplementedException();
        }

        
    }
}
