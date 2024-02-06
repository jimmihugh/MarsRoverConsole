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
            Console.WriteLine("Leave Command Blank to submit currently queued commands.");

            bool running = true;
            while (running)
            {
                try
                {
                    Console.Write("Command: ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "quit" || input.ToLower() == "exit")
                    {
                        running = false;
                    }
                    else
                    {
                        string output = consolePresenter.Input(input);
                        if (output != "")
                        {
                            foreach (string outputLine in output.Split(Environment.NewLine))
                            {
                                Console.WriteLine(outputLine);
                            }
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
