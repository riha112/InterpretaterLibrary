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
            Compiler.Run(lines);
            for(var i =0; i < Output.OutputLines.Count; i++)
                Console.WriteLine($" {i:D2} |  {Output.OutputLines[i]}");

            //  compiler.Execute(lines);
            //var output = compiler.CodeOutput.Split('\n');
            //foreach (var line in output)
            //{
            //    Console.WriteLine(line);
            //}
        }
    }
}
