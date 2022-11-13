using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;

namespace RobbyTheRobotTests
{
    [TestClass]
    public class RobbyTheRobotTests
    {
        [TestMethod]
        public void GenerateRandomTestGridTest()
        {
            RobbyTheRobot.RobbyTheRobot robby = new RobbyTheRobot.RobbyTheRobot(1, 1, 1);
            ContentsOfGrid[,] grid = robby.GenerateRandomTestGrid();
            int canCounter = 0;
            foreach(ContentsOfGrid element in grid){
                if(element == ContentsOfGrid.Can){
                    canCounter++;
                }
            }
            Assert.IsTrue(canCounter == 50);
        }

        [TestMethod]
        public void GeneratePossibleSolutionsTest()
        {
            RobbyTheRobot.RobbyTheRobot robby = new RobbyTheRobot.RobbyTheRobot(20, 4, 2);
            robby.GeneratePossibleSolutions("Test_runs");
        }
    }
}
