using System;

namespace MarsRover
{
    interface MarsRoverCommand
    {
        public void Execute();
    }

    public interface MarsRoverCommands
    {
        public void LeftCommand();
        public void RightCommand();
        public void TravelCommand(int distance);
        public void SubmitCurrentCommands();

    }
}
