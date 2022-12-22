# Project - Genetic Algorithms

## Name
Genetic Algorithm and Robby the robot

## Description
The goal of this project is to produce the best solution for robby the robot to collect empty soda cans that lie scattered around his square grid world, by following instructions encoded in an array of 243 genome enumerations. A console application takes the input from the user and produces the instructions or as we like to call it a possible solution using Genetic Algorithms principles. We visualized the solution using Monogame.

In a genetic algorithm (GA), the desired output is a solution to some problem….
The input to the GA has two parts: a population of candidate programs, and a fitness function that takes a candidate program and assigns to it a fitness value that measures how well that program works on the desired task.
Here is the recipe for the GA. 
Repeat the following steps for some number of generations: 
1. Generate an initial population of candidate solutions. The simplest way to create the initial population is just to generate a bunch of random programs (strings), called “individuals.” 
2. Calculate the fitness of each individual in the current population. 
3. Select some number of the individuals with highest fitness to be the parents of the next generation. 
4. Pair up the selected parents. Each pair produces offspring by recombining parts of the parents, with some chance of random mutations, and the offspring enter the new population. The selected parents continue creating offspring until the new population is full (i.e., has the same number of individuals as the initial population). The new population now becomes the current population. 

Robby’s job is to clean up his world by collecting the empty soda cans. Robby’s world consists of 100 squares (sites) laid out in a 10 × 10 grid. Let’s imagine that there is a wall around the boundary of the entire grid. Various sites have been littered with soda cans (but with no more than one can per site). Robby isn’t very intelligent, he has no memory of his past moves, and his eyesight isn’t that great. From wherever he is, he can see the contents of one adjacent site in the north, south, east, and west directions, as well as the contents of the site he occupies. A site can be empty, contain a can, or be a wall. 


## Installation
clone the repository and open it on any IDE . Note: we used VScode.

## Usage
here is a sample execution:
https://www.loom.com/share/6f185bfb31454989a933ea6017ee301e


## Authors
Dany Makhoul
Harout Dabbaghian
Bastian Fernandez Cortez

