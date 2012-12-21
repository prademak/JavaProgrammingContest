using JavaProgrammingContest.Web.App_Start;
using NUnit.Framework;

namespace JavaProgrammingContest.Web.Tests.App_Start
{
    [TestFixture]
    class ConfigTests
    {
        [Test]
        public void CheckDIConfig()
        {
            DIConfig.RegisterTypes();
            Assert.IsTrue(true);
        }

        [Test]
        public void CheckMapperConfig()
        {
            MapperConfig.Configure();
            Assert.IsTrue(true);
        }
    }
}
