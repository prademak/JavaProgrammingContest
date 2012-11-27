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
 

        [SetUp]
        public void Init()
        {
            _compiler = new Compiler();
            _runner = new Runner();
           
        }

        [Test]
        public void TestIfCompilerWorks()
        {
            Assert.IsNotNull(_compiler);
          
        }

       
    }
}
