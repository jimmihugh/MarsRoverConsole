using MarsRover;
using NUnit.Framework;

using MarsRoverConsoleLibrary;

namespace MarsRoverTests
{
    class MockMarsRoverCommands : MarsRoverCommands
    {
        public bool leftCalled = false;
        public bool rightCalled = false;
        public bool travelCalled = false;
        public bool submitCalled = false;
        public void LeftCommand()
        {
            leftCalled = true;
        }
        public void RightCommand()
        {
            rightCalled = true;
        }
        public void TravelCommand(int distance)
        {
            travelCalled = true;
        }
        public void SubmitCurrentCommands()
        {
            submitCalled = true;
        }
    }

    class ConsolePresentTests
    {
        MockMarsRoverCommands marsRoverCommandsMock;
        ConsolePresenter consolePresenter;
        [SetUp]
        public void Setup()
        {
            marsRoverCommandsMock = new MockMarsRoverCommands();
            consolePresenter = new();
            consolePresenter.InjectMarsRover(marsRoverCommandsMock);
        }

        [Test]
        public void SingleValidInputProducesNoOutput()
        {
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("left"), "Unexpected output");
        }
        [Test]
        public void SingleInValidInputProducesOutput()
        {
            StringAssert.AreNotEqualIgnoringCase("", consolePresenter.Input("dsgfsfd"), "Expected error");
        }
        [Test]
        public void LeftCommandCalled()
        {
            consolePresenter.Input("Left");
            Assert.That(marsRoverCommandsMock.leftCalled, Is.EqualTo(true));
        }
        [Test]
        public void RightCommandCalled()
        {
            consolePresenter.Input("Right");
            Assert.That(marsRoverCommandsMock.rightCalled, Is.EqualTo(true));
        }
        [Test]
        public void TravelCommandCalled()
        {
            consolePresenter.Input("50m");
            Assert.That(marsRoverCommandsMock.travelCalled, Is.EqualTo(true));
        }
        [Test]
        public void SubmitCommandCalled()
        {
            consolePresenter.Input("Left");
            consolePresenter.Input("");
            Assert.That(marsRoverCommandsMock.submitCalled, Is.EqualTo(true));
        }
    }
}
