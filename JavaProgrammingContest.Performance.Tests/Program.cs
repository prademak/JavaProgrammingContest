using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Compiler.Java;
using JavaProgrammingContest.Process.Compiler.Java.Helpers;
using JavaProgrammingContest.Process.Compiler.Model;

namespace JavaProgrammingContest.Performance.Tests{
    public class Program{
        private const int NumThreads = 25;
        private static readonly Random Random = new Random();
        private static readonly List<CompilerResult> Results = new List<CompilerResult>();
        private static readonly Stopwatch Stopwatch = new Stopwatch();

        private static void Main(string[] args){
            Stopwatch.Start();
            for (var i = 0; i < NumThreads; i++)
                new Thread(Compile).Start();
            Console.ReadLine();
        }

        private static void Compile(){
            var compiler = new JavaCompiler{
                FilePathCreator = new TestFilePathCreator(),
                CompilerProcess = new JavaCompilerProcess(new SettingsReader())
            };

            var result = compiler.CompileFromPlainText(new Participant{Id = Random.Next(1, 10000)}, 
@"// Sample class
class Solution {
	public static void main(String[] args){
		System.out.println(""Hello World!"");
	}
}
");

            Save(result);
        }

        private static void Save(CompilerResult result){
            Results.Add(result);

            if (Results.Count == NumThreads){
                Stopwatch.Stop();

                var minTime = Results.Min(c => c.CompilationTime);
                var maxTime = Results.Max(c => c.CompilationTime);
                var average = Results.Average(c => c.CompilationTime);

                Console.WriteLine("{0} compile requests has been asynchronously executed.", NumThreads);
                Console.WriteLine("Statistics:");
                Console.WriteLine("Average Compile Time : {0}", average);
                Console.WriteLine("Minimum Compile Time : {0}", minTime);
                Console.WriteLine("Maximum Compile Time : {0}", maxTime);
                Console.WriteLine("Total running time : {0}", Stopwatch.ElapsedMilliseconds);
            }
        }
    }
}