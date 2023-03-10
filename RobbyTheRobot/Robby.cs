namespace RobbyTheRobot
{
    public static class Robby
    {
        //return RobbyTheRobot.RobbyTheRobot
        public static IRobbyTheRobot CreateRobbyTheRobot(int numberOfGenerations, int populationSize, int numberOfTrials, int? seed = null)
        {
            return new RobbyTheRobot(numberOfGenerations, populationSize, numberOfTrials, seed);
        }
    }
}