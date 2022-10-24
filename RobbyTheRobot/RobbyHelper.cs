using System;
using GeneticAlgorithm;

namespace RobbyTheRobot
{
    class RobbyHelper
    {
        
        /// <summary>
        /// Moves robby and scores him based on the list of possible moves.
        /// </summary>
        /// <param name="moves">The list of moves</param>
        /// <param name="grid">The test grid to move robby on</param>
        /// <param name="rng">A random number generator</param>
        /// <param name="x">A start x position that is updated to his subsequent position</param>
        /// <param name="y">A start y position that is updated to his subsequent position</param>
        /// <returns>The score</returns>
        public static double ScoreForAllele(int[] moves, ContentsOfGrid[,] grid, Random rng, ref int x, ref int y)
        {
            DirectionOfGridContents direction = RobbyHelper.LookAround(x, y, grid);
            //find the gene
            int gene = RobbyHelper.FindGeneIndex(direction);
            //find the move
            PossibleMoves move = (PossibleMoves)moves[gene];
            bool done;
            do
            {
                done = true;
                switch (move)
                {
                    case PossibleMoves.North://move north
                        if (direction.N == ContentsOfGrid.Wall)
                            return -5;
                        y -= 1;
                        break;
                    case PossibleMoves.South://move south
                        if (direction.S == ContentsOfGrid.Wall)
                            return -5;
                        y += 1;
                        break;
                    case PossibleMoves.East: //move east
                        if (direction.E == ContentsOfGrid.Wall)
                            return -5;
                        x += 1;
                        break;
                    case PossibleMoves.West: //move west
                        if (direction.W == ContentsOfGrid.Wall)
                            return -5;
                        x -= 1;
                        break;
                    case PossibleMoves.Nothing: //do nothong
                        break;
                    case PossibleMoves.PickUp: //pick up can
                        if (grid[x, y] == ContentsOfGrid.Can) //there is a can
                        {
                            grid[x, y] = ContentsOfGrid.Empty;
                            return +10;
                        }
                        else
                            return -1; //penalty for picking up nothing
                    case PossibleMoves.Random: //random move
                        done = false;
                        int num = rng.Next(0, Enum.GetNames(typeof(PossibleMoves)).Length);
                        move = (PossibleMoves)num;
                        break;
                }
            }
            while (!done);
            return 0;
        }

        /// <summary>
        /// Used to fill up a DirectionsContent struct based on Robby's position in the 
        /// grid and what is immediately adjacent to him.
        /// </summary>
        /// <param name="x">Robby's x coordinates</param>
        /// <param name="y">Robby's y coordinates</param>
        /// <param name="grid">The test grid where Robby is</param>
        /// <returns>What Robby sees in all directions plus current</returns>
        private static DirectionOfGridContents LookAround(int x, int y, ContentsOfGrid[,] grid)
        {
            //what do you see?
            DirectionOfGridContents direction = new DirectionOfGridContents();
            //where are the walls?
            if (y == 0)
                direction.N = ContentsOfGrid.Wall; //wall
            else
                direction.N = grid[x, y - 1];

            if (y == grid.GetLength(1) - 1)
                direction.S = ContentsOfGrid.Wall;
            else
                direction.S = grid[x, y + 1];

            if (x == grid.GetLength(0) - 1)
                direction.E = ContentsOfGrid.Wall;
            else
                direction.E = grid[x + 1, y];

            if (x == 0)
                direction.W = ContentsOfGrid.Wall;
            else
                direction.W = grid[x - 1, y];

            direction.Current = grid[x, y];

            return direction;
        }

        /// <summary>
        /// Provides the index of the gene for the given set of grid directions
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static int FindGeneIndex(DirectionOfGridContents dir)
        {
            int gene = 0;
            gene += getIndexForDirection(dir.N, 4);
            gene += getIndexForDirection(dir.S, 3);
            gene += getIndexForDirection(dir.E, 2);
            gene += getIndexForDirection(dir.W, 1);
            gene += getIndexForDirection(dir.Current, 0);
            return gene;
        }
        /// <summary>
        /// Used to build up the index of the gene in the Chromosome
        /// </summary>
        /// <param name="content">Content in a given direction</param>
        /// <param name="power">Exponent of 10</param>
        /// <returns>Partial calculation of the gene's index</returns>
        private static int getIndexForDirection(ContentsOfGrid content, int power)
        {
            if (content == ContentsOfGrid.Empty)
                return 0;
            if (content == ContentsOfGrid.Can)
                return (int)(Math.Pow(3, power));
            //Wall
            return (int)(2 * Math.Pow(3, power));
        }
    }
}