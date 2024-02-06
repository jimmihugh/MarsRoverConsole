using NUnit.Framework;
using MarsRoverConsoleLibrary;
using MarsRover;

namespace MarsRoverTests
{
    public class Tests
    {
        ConsolePresenter consolePresenter;
        [SetUp]
        public void Setup()
        {
            consolePresenter = new();
            consolePresenter.InjectMarsRover(new MarsRoverController(consolePresenter));
        }

        [Test]
        public void ExampleTest()
        {
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("50m"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("Left"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("23m"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("Left"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("position 4624 north", consolePresenter.Input("4m"), "Expect output: position 4624 north");
        }

        [Test]
        public void AcceptanceTest2()
        {
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("Left"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("42m"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("Right"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("", consolePresenter.Input("74m"), "Unexpected output");
            StringAssert.AreEqualIgnoringCase("position 7443 south", consolePresenter.Input(""), "Expect output: position 7443 south");
        }
    }
}