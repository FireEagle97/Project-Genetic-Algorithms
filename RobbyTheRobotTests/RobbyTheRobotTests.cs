using System;
using System.IO;
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
            //Count 50 cans
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
            //Delete all entries in Test_Runs
            System.IO.DirectoryInfo di = new DirectoryInfo("../../../Test_Runs/");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            RobbyTheRobot.RobbyTheRobot robby = new RobbyTheRobot.RobbyTheRobot(1000, 200, 1);
            robby.GeneratePossibleSolutions("../../../Test_Runs/");
            //Check if file exists
            bool fileCheck = File.Exists("../../../Test_Runs/Top_Candidate1.txt");
            Assert.IsTrue(fileCheck);
        }
    }
}
