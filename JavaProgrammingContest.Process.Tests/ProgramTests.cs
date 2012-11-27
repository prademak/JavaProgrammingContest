using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using JavaProgrammingContest.Process;
namespace JavaProgrammingContest.Process.Tests
{
    [TestFixture]
    public class CompilerTests
    {
        Compiler _compiler;
        Runner _runner;
        private const String ClassPath = @"C:\Users\vincent\Documents\";
        private const String JavaFile = @"Solution.java";

        [SetUp]
        public void Init()
        {
            _compiler = new Compiler();
            _runner = new Runner();
           
        }

        [Test]
        public void TestIfCompilerWorks()
        {
            CompileResult result = _compiler.Compile(ClassPath + JavaFile);
            Assert.IsFalse(result.CompileFailed);
            
        }

        [Test]
        public void TestIfFileNameGetsChanged()
        {
            CompileResult result = _compiler.Compile(ClassPath + JavaFile);
            Assert.AreEqual(@"C:\Users\vincent\Documents\Solution", result.ClassFile);

        }

        [Test]
        public void TestIfCompiledFileIsRunnable()
        {
            CompileResult result = _compiler.Compile(ClassPath + JavaFile);

            String[] input = { "2", "5", "1 1 1 2 2", "5", "2 1 3 1 2" };
            String[] rightOutput = {"0", "4"};
            RunResult runResult = _runner.RunCode(" -classpath " + ClassPath + " Solution", rightOutput, input);
            Assert.IsTrue(runResult.OutputIsRight);
        }
    }
}
