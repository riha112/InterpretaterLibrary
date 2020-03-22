using System;
using System.IO;
using System.Linq;
using OldInterpreter;
using RihaInterpreterLibrary;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new RihaCompiler();
            var lines = File.ReadAllLines(@"..\..\..\DemoCode.txt");

            //var code = string.Join('\n', lines);

            Output.AddedLineEvent += OnAddedPrintEvent;
            Output.AddedInLineEvent += OnAddedInLinePrintEvent;
            Console.Write("000 |  ");

            Compiler.Run(lines);
            //for (var i = 0; i < Output.OutputLines.Count; i++)
            //    Console.WriteLine($" {i:D2} |  {Output.OutputLines[i]}");

            //  compiler.Execute(lines);
            //var output = compiler.CodeOutput.Split('\n');
            //foreach (var line in output)
            //{
            //    Console.WriteLine(line);
            //}
        }

        private static void OnAddedPrintEvent(object? sender, string line)
        {
            Console.WriteLine($"{line}");
            Console.Write($"{Output.OutputLines.Count - 1:D3} |  ");
        }
        private static void OnAddedInLinePrintEvent(object? sender, string line)
        {
            Console.Write($"{line}");
        }
    }
}
