﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GeneticAlgorithm;

namespace RobbyTheRobot
{
    //delegate for notifying when a file has been written
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        private const int GRID_SIZE = 100;
        private const int NUMBER_OF_ACTIONS = 200;
        private const int NUMBER_OF_TEST_GRIDS = 100;
        private const double MUTATION_RATE = 0.01;
        private const double ELITE_RATE = 0.05;
        public int NumberOfActions {get; set;}
        public int NumberOfTestGrids {get; set;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}
        public double MutationRate {get;}
        public static event WriteFileHandler FileWritten;
        public double EliteRate {get;}
        //Unsure if should exist
        public int PopulationSize {get;}
        //Unsure if should exist
        public int NumberOfTrials {get;}
        //For the Seed
        public Random RandomObject {get;}
        private int? _seed;
 
        public RobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null){
            //Instructions stipulate that the size of the grid is 100

            _seed = seed;
            GridSize = GRID_SIZE;
            //and Robby can do 200 possible actions
            NumberOfActions = NUMBER_OF_ACTIONS;
            //Arbitrary value according to the slides
            NumberOfTestGrids = NUMBER_OF_TEST_GRIDS;
            //Arbitrary as well
            MutationRate = MUTATION_RATE;
            //Maybe arbitrary
            EliteRate = ELITE_RATE;
            //This is 200, but its in the constructor so we don't set it here
            NumberOfGenerations = numberOfGenerations;
            //Random object
            
            RandomObject = seed.HasValue ? new Random(seed.Value) : new Random();

            NumberOfTrials = numberOfTrials;
            PopulationSize = populationSize;
            FileWritten = printInfo;

            //Set the seed
            _seed = seed;
        }

        public static void printInfo(String s){
            Console.WriteLine(s);
        }  

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            //Checks if grid can be divided into equal rows
            if (Math.Sqrt(GridSize) % 1 == 0){
                int rowSize = Convert.ToInt32(Math.Sqrt(GridSize));

                //Create new grid
                ContentsOfGrid[,] grid = new ContentsOfGrid[rowSize, rowSize];
                int gridHalf = GridSize/2;
                List<int> canCheckerList = new List<int>();

                //Populate it with cans
                for(int i = 0; i < gridHalf; i++){
                    int canNumber = RandomObject.Next(0, GridSize);

                    //If the can number is already filled, generate a new can number
                    while(canCheckerList.Contains(canNumber)){
                        canNumber = RandomObject.Next(0, GridSize);
                    }
                    //int to use to determine the position in the outer array
                    int canPositionOuter;
                    //int to use to determine the position in the inner array
                    int canPositionInner = canNumber;
                    
                    //Determine outer can position
                    canPositionOuter = Convert.ToInt32(Math.Floor((double)canNumber/rowSize));
                    //Determine inner can position
                    while(canPositionInner > rowSize-1){
                        canPositionInner -= rowSize;
                    }

                    //Insert can in grid
                    grid[canPositionOuter, canPositionInner] = ContentsOfGrid.Can;
                    //update canCheckerArray
                    canCheckerList.Add(canNumber);
                }

                //Fill everything else with .Empty
                for(int i = 0; i < rowSize; i++){
                    for(int j = 0; j < rowSize; j++){
                        if(grid[i,j] != ContentsOfGrid.Can){
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
            //Create GeneticAlgorithm
            IGeneticAlgorithm geneticAlgorithm = GeneticLib.CreateGeneticAlgorithm(PopulationSize, 243, 7, MutationRate, EliteRate, NumberOfTrials, ComputeFitness, _seed);

            //List of Chromosomes to write to files
            List<IChromosome> list = new List<IChromosome>();

            for(int i = 0; i < NumberOfGenerations; i++){
                //Create the generation + Evaluate their Fitness and sort the array
                geneticAlgorithm.GenerateGeneration();

                //Save the top candidate on generations 1, 20, 100, 200, 500, 1000
                if(i == 0 || i == 19 || i == 99 || i == 199 || i == 499 || i == 999){
                    //Create a variable to hold the top chromosome (already sorted in EvaluateFitnessOfPopulation())
                    IChromosome topCandidate = geneticAlgorithm.CurrentGeneration[0];
                    //Invoke handler
                    FileWritten?.Invoke("Top candidate for generation "+(i+1)+" added to list");
                    //Add topCandidate to the list
                    list.Add(topCandidate);
                }
            }
            //Write to file
            WriteToFile(folderPath, list);
        }   

        public double ComputeFitness(IChromosome chromosome, IGeneration generation){
            //Variable to hold the score
            double score = 0;
            //x and y initial positions
            int x = 0;
            int y = 0;

            //Create the grid
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            
            for(int i = 0; i < NumberOfActions; i++){
                //Add move to the score
                score += RobbyHelper.ScoreForAllele(chromosome.Genes, grid, RandomObject, ref x, ref y);
            }
            return score;
        }

        //Method to write to file
        public static void WriteToFile(string folderPath, List<IChromosome> list){
            
            int[] numberArray = {1, 20, 100, 200, 500, 1000};
            List<int> numberList = new List<int>();
            for(int i = 0 ; i < list.Count; i++){
                numberList.Add(numberArray[i]);
            }

            //Changes the filename for each generation
            for(int j = 0; j < numberList.Count; j++){
            string fileName = $"{folderPath}Top_Candidate{numberList[j]}.txt";

            //Put data in file in a comma separated list like so:
            //max score, number of moves to display, all moves
            

                //Chromosome.Fitness
                double topFitness = list[j].Fitness;
                //Arbitrary amount of moves to show
                int numberOfMoves = NUMBER_OF_ACTIONS;
                //Chromosome's Genes[]
                int[] topGenes = list[j].Genes;
                //String with array values
                String arrayValues = "";
                foreach(int gene in topGenes){
                    arrayValues += gene.ToString();
                }

                //Have a string to hold top a top candidate's data for each generation
                string topCandidateString = $"{topFitness}, {numberOfMoves}, {arrayValues}";
                
                // Write file using StreamWriter  
                using (StreamWriter writer = File.CreateText(fileName))  
                {  
                    writer.WriteLine(topCandidateString);  
                }
            
            FileWritten?.Invoke("File written");
            Console.WriteLine();
            // Read a file  
            string readText = File.ReadAllText(fileName);  
            Console.WriteLine(readText);  
            }
            FileWritten?.Invoke("Press any key to end the program");
        }
    }
}
