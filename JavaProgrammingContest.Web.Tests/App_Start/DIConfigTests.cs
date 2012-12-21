using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.App_Start
{
    [TestFixture]
    class DIConfigTests
    {
        [Test]
        public void CheckObjectErrors()
        {
            DIConfig.RegisterTypes();
            Assert.IsTrue(true);
        }
    }
}
