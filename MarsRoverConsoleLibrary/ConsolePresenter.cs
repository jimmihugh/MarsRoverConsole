using System;
using MarsRover;

namespace MarsRoverConsoleLibrary
{
    public class ConsolePresenter : LocationReporter
    {
        MarsRoverCommands marsRoverCommands;

        string locationReport = "";
        string directionReport = "";
        public void InjectMarsRover(MarsRoverCommands marsRoverCommands)
        {
            this.marsRoverCommands = marsRoverCommands;
        }
        public string Input(string input)
        {
            switch (input.ToLower())
            {
                case "left":
                    marsRoverCommands.LeftCommand();
                    break;
                case "right":
                    marsRoverCommands.RightCommand();
                    break;
                case "":
                    marsRoverCommands.SubmitCurrentCommands();
                    break;
                default:
                    bool travelCommandSubmitted = TestIfTravelCommandAndSubmit(input);
                    if (travelCommandSubmitted == false)
                    {
                        return "Command not recognised.";
                    }
                    break;
            }
            return CheckIfLocationUpdatedAndReturn();
        }
        bool TestIfTravelCommandAndSubmit(string input)
        {
            if (input.EndsWith("m"))
            {
                string travelDistance = input.Substring(0, input.LastIndexOf('m'));
                bool commandIsNumber = int.TryParse(travelDistance, out int distance);
                if (commandIsNumber)
                    marsRoverCommands.TravelCommand(distance);
                else
                    return false;
                return true;
            }
            return false;
        }

        void LocationReporter.Report(int location, MarsRoverDirection direction)
        {
            locationReport = location.ToString();
            directionReport = ConvertRoverDirectionToString(direction);
        }

        string CheckIfLocationUpdatedAndReturn()
        {
            if (locationReport == "")
                return "";
            string location = "position " + locationReport + " " + directionReport;
            locationReport = "";
            return location;
        }

        string ConvertRoverDirectionToString(MarsRoverDirection direction)
        {
            switch (direction)
            {
                case MarsRoverDirection.South:
                    return "south";
                case MarsRoverDirection.West:
                    return "west";
                case MarsRoverDirection.North:
                    return "north";
                case MarsRoverDirection.East:
                    return "east";
            }
            throw new ArgumentOutOfRangeException("Unexpected Direction", nameof(direction));
        }

    }
}
