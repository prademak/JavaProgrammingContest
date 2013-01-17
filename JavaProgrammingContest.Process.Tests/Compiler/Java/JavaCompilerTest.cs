using System;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Process.Compiler.Java;
using NUnit.Framework;

namespace JavaProgrammingContest.Process.Tests.Compiler.Java
{
    [TestFixture]
    public class JavaCompilerTests
    {
        private JavaCompiler jc; 
        [SetUp]
        public void SetUp()
        {
            jc = new JavaCompiler();
        }

        [TestCase]
        [ExpectedException(typeof(NullReferenceException ))]
        public void CreateFilePathGivesNullReferenceException()
        {
            
        }
    }
}