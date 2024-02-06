using System;
using MarsRoverConsoleLibrary;
using MarsRover;

namespace MarsRoverConsole
{
    class Program
    {
       
        static void Main(string[] args)
        {
            ConsolePresenter consolePresenter = new();
            consolePresenter.InjectMarsRover(new MarsRoverController(consolePresenter));

            Console.WriteLine("Mars Rover Project");

            bool running = true;
            while (running)
            {
                try
                {
                    string input = Console.ReadLine();
                    string output = consolePresenter.Input(input);
                    if (output != "")
                    {
                        foreach (string outputLine in output.Split(Environment.NewLine))
                        {
                            Console.WriteLine(outputLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception has occured: " + ex.Message);
                    Console.WriteLine("Continue?");
                    string shouldContinue = Console.ReadLine();
                    if (shouldContinue.ToLower() != "yes" && shouldContinue.ToLower () != "y")
                    {
                        running = false;
                    }
                }
            }
        }
    }
}
