using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public enum MarsRoverDirection
    {
        South, West, North, East
    }


    class MarsRoverLogic
    {
        public class LeftCommand : MarsRoverCommand
        {
            readonly MarsRoverLogic logic;
            public LeftCommand(MarsRoverLogic logic)
            {
                this.logic = logic;
            }
            public void Execute()
            {
                logic.RotateRover(-1);
            }
        }
        public class RightCommand : MarsRoverCommand
        {
            readonly MarsRoverLogic logic;
            public RightCommand(MarsRoverLogic logic)
            {
                this.logic = logic;
            }
            public void Execute()
            {
                logic.RotateRover(1);
            }
        }
        public class TravelCommand : MarsRoverCommand
        {
            readonly int distance;
            readonly MarsRoverLogic logic;
            public TravelCommand(MarsRoverLogic logic, int distance)
            {
                this.logic = logic;
                this.distance = distance;
            }
            public void Execute()
            {
                logic.Travel(distance);
            }
        }

        const int WidthOfMars = 100;
        const int HeightOfMars = 100;

        LocationReporter location;

        MarsRoverDirection roverDirection;
        int x = 0;
        int y = 0;

        bool haltDueToOutOfBounds = false;

        public MarsRoverLogic(LocationReporter locationReporter)
        {
            this.location = locationReporter;
        }

        public void SubmitCommands(Queue<MarsRoverCommand> commandQueue)
        {
            while (commandQueue.Count > 0)
            {
                commandQueue.Dequeue().Execute();
                if (haltDueToOutOfBounds)
                {
                    commandQueue.Clear();
                    haltDueToOutOfBounds = false;
                    break;
                }
            }
            ReportLocation ();
        }

        void RotateRover(int directionOfRotation)
        {
            if (Math.Abs(directionOfRotation) > 1)
                throw new ArgumentException ("Cannot rotate more than once at a time", nameof(directionOfRotation));

            //Shameless hack for rotating enumeration without conditional statements
            roverDirection = (MarsRoverDirection)((((int)roverDirection + 4) + directionOfRotation) % 4);
        }

        void Travel(int distance)
        {
            switch (roverDirection)
            {
                case MarsRoverDirection.South:
                    TravelVertical(distance);
                    break;
                case MarsRoverDirection.West:
                    TravelHorizontal(-distance);
                    break;
                case MarsRoverDirection.North:
                    TravelVertical(-distance);
                    break;
                case MarsRoverDirection.East:
                    TravelHorizontal(distance);
                    break;
            }
        }
        void TravelHorizontal(int distance)
        {
            x += distance;
            if (x < 0)
            {
                x = 0;
                haltDueToOutOfBounds = true;
            }
            else if (x >= WidthOfMars)
            {
                x = WidthOfMars - 1;
                haltDueToOutOfBounds = true;
            }
        }
        void TravelVertical(int distance)
        {
            y += distance;
            if (y < 0)
            {
                y = 0;
                haltDueToOutOfBounds = true;
            }
            else if (y >= HeightOfMars)
            {
                y = HeightOfMars - 1;
                haltDueToOutOfBounds = true;
            }
        }

        void ReportLocation()
        {
            location.Report(ConvertCoordsToTile(), roverDirection);
        }
        int ConvertCoordsToTile()
        {
            return y * WidthOfMars + x + 1;
        }
    }
}
