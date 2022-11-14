using System;
namespace RobbyTheRobot{

    public static class Robby{
        public static IRobbyTheRobot CreateRobby(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null){
        return new RobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, seed);
    }    
    }
}