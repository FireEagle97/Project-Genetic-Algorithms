using System;

namespace RobbyTheRobot
{
    public class RobbyTheRobot : IRobbyTheRobot
    {
        public int NumberOfActions {get;}

        public int NumberOfTestGrids {get;}

        public int GridSize {get;}

        public int NumberOfGenerations {get;}

        public double MutationRate {get;}

        public double EliteRate {get;}

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            int gridSize = GridSize*GridSize;
            int gridHalf = GridSize/2;
            ContentsOfGrid[,] grid = new ContentsOfGrid[GridSize, GridSize];
            Random random = new Random();
            int canNumber = random.Next();
            return grid;
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new NotImplementedException();
        }

        
    }
}
