using System;
using Xunit;
using _360Widgets;

namespace _360WidgetsTests
{
    public class UnitTest1
    {
        [Fact]
        public void EmptyInputTest()
        {
            //Arrange
            string InputString = "";

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("", output);
        }
        [Fact]
        public void CorrectInputTest()
        {
            //Arrange
            string[] lines = System.IO.File.ReadAllLines("../../../input.txt");
            string InputString = string.Join("\n", lines); 

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("{\"temp-1\":\"UltraPrecise\",\"temp-2\":\"UltraPrecise\",\"hum-1\":\"Keep\",\"hum-2\":\"Discard\",\"mon-1\":\"Keep\",\"mon-2\":\"Keep\"}", output);
        }
        [Fact]
        public void FileHaveJustThermometer()
        {
            //Arrange
            string[] lines = System.IO.File.ReadAllLines("../../../JustThermometer.txt");
            string InputString = string.Join("\n", lines);

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("{\"temp-1\":\"UltraPrecise\",\"temp-2\":\"UltraPrecise\"}", output);
        }
        [Fact]
        public void FileHaveJustHumidity()
        {
            //Arrange
            string[] lines = System.IO.File.ReadAllLines("../../../JustHumidity.txt");
            string InputString = string.Join("\n", lines);

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("{\"hum-1\":\"Keep\",\"hum-2\":\"Discard\"}", output);
        }
        [Fact]
        public void FileHaveJustMonoxide()
        {
            //Arrange
            string[] lines = System.IO.File.ReadAllLines("../../../JustMonoxide.txt");
            string InputString = string.Join("\n", lines);

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("{\"mon-1\":\"Keep\",\"mon-2\":\"Keep\"}", output);
        }
        [Fact]
        public void WithoutReference()
        {
            //Arrange
            string[] lines = System.IO.File.ReadAllLines("../../../WithoutReference.txt");
            string InputString = string.Join("\n", lines);

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("", output);
        }
        [Fact]
        public void LargeInputTest() {
            //Arrange
            string[] lines = System.IO.File.ReadAllLines("../../../LargeInput.txt");
            string InputString = string.Join("\n", lines);

            //Act
            string output = _360Widgets.ProcessLogFiles.EvaluateLogFile(InputString);

            //Assert
            Assert.Equal("{\"temp-1\":\"UltraPrecise\",\"temp-2\":\"UltraPrecise\"}", output);
        }
    }
}
