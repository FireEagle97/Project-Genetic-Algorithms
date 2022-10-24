using System;

namespace RobbyVisualizer
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new RobbyVisualizerGame())
                game.Run();
        }
    }
}
