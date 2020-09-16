using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using RihaInterpreterLibrary.Processor;
using Xunit;

namespace TestingRihaInterpreterLibrary.Processors
{
    public static class LabelProcessorTester
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        public static void ProcessTesterCountSuccess(int testListId, int expected)
        {
            var lines = GetTestLines(testListId);
            var processor = new LabelProcessor();
            
            processor.Process(lines);
            var actual = LabelProcessor.Labels.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void ProcessTesterException()
        {
            var lines = GetTestLines(3);
            var processor = new LabelProcessor();

            Assert.Throws<Exception>(() => processor.Process(lines));
        }

        private static List<string> GetTestLines(int id)
        {
            var lines = new List<string>[]
            {
                new List<string>()
                {
                    "set a as number",
                    "hello world: abcd",
                    "llabel: 10",
                    "<label: 15",
                    "text label: aaa"
                },
                null,
                new List<string>()
                {
                    "set a as b",
                    "print: hello",
                    "say: 1: 2: 3",
                    "label: abc",
                    "goto: abc",
                    "label2: abc2",
                    "return"
                },
                new List<string>()
                {
                    "label: abc",
                    "label: abc",
                },
            };
            return lines[id];
        }


    }
}
