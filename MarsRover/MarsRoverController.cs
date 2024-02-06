using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{

    public class MarsRoverController : MarsRoverCommands
    {
        const int MaxCommands = 5;
        readonly Queue <MarsRoverCommand> commandQueue = new (MaxCommands);
        readonly MarsRoverLogic marsRoverLogic;
        public MarsRoverController(LocationReporter locationReporter)
        {
            marsRoverLogic = new MarsRoverLogic(locationReporter);
        }
        public void LeftCommand()
        {
            commandQueue.Enqueue(new MarsRoverLogic.LeftCommand (marsRoverLogic));
            RunCommandsIfQueueFull();
        }
        public void RightCommand()
        {
            commandQueue.Enqueue(new MarsRoverLogic.RightCommand(marsRoverLogic));
            RunCommandsIfQueueFull();
        }
        public void TravelCommand(int distance)
        {
            commandQueue.Enqueue(new MarsRoverLogic.TravelCommand(marsRoverLogic, distance));
            RunCommandsIfQueueFull();
        }
        public void SubmitCurrentCommands()
        {
            RunCommands();
        }
        void RunCommandsIfQueueFull()
        {
            if (commandQueue.Count == MaxCommands)
            {
                RunCommands();
            }
        }
        void RunCommands()
        {
            marsRoverLogic.SubmitCommands(commandQueue);
        }
    }
}
