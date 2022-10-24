using System;
using GeneticAlgorithm;

namespace RobbyTheRobot
{   
    /// <summary>
    /// Possible dirction of the grid
    /// </summary>
    public struct DirectionOfGridContents
    {
        public ContentsOfGrid N { get; set; }
        public ContentsOfGrid S { get; set; }
        public ContentsOfGrid E { get; set; }
        public ContentsOfGrid W { get; set; }
        public ContentsOfGrid Current { get; set; }

    }

    /// <summary>
    /// Possible contents of each grid location
    /// </summary>
    public enum ContentsOfGrid
    {
        Empty,
        Can,
        Wall
    }

    /// <summary>
    /// Possible moves Robby can perform in the grid
    /// </summary>
    public enum PossibleMoves
    {
        North,
        South,
        East,
        West,
        Nothing,
        PickUp,
        Random
    }
    /// <summary>
    /// Represents Robby the Robot
    /// </summary>
    public interface IRobbyTheRobot
    {
        int NumberOfActions {get;}
        int NumberOfTestGrids {get;}
        int GridSize {get;}
        int NumberOfGenerations {get;}

        double MutationRate {get;}

        double EliteRate {get;}

        /// <summary>
        /// Used to generate a single test grid filled with cans in random locations. Half of 
        /// the grid (rounded down) will be filled with cans. Use the GridSize to determine the size of the grid
        /// </summary>
        /// <returns>Rectangular array of Contents filled with 50% Cans, and 50% Empty </returns>
        ContentsOfGrid[,] GenerateRandomTestGrid();

        /// <summary>
        /// Generates a series of possible solutions based on the generations and saves them to disk.
        /// The text files generated must contain a comma seperated list of the max score, number of moves to display in the gui and all the actions robby will take (i.e the genes in the Chromosome).
        /// The top candidate of the 1st, 20th, 100, 200, 500 and 1000th generation will be saved.
        /// </summary>
        /// <param name="folderPath">The path of the folder where the text files will be saved</param>
        void GeneratePossibleSolutions(string folderPath);

        /// <summary>
        /// An event raised when a file is written to disk
        /// </summary>
        //event TODOMYCUSTOMDELEGATE FileWritten;

    }

    ///TODO Add custom delegate
}