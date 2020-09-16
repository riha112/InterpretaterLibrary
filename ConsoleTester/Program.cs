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

            // Loads code
            var lines = File.ReadAllLines(@"..\..\..\FizzBuzz.txt");

            // Subscribes to events
            Output.AddedLineEvent += OnAddedPrintEvent;
            Output.AddedInLineEvent += OnAddedInLinePrintEvent;
    
            Console.Write("000 |  ");
            
            // Runs code
            Compiler.Run(lines);

            Console.Write("DONE");
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
