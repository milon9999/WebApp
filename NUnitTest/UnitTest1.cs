using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using WebApp.Models;

namespace NUnitTest
{
    public class Tests
    {
        //For tests you need to put a test file to the folder which path is in config appsettings.test.json -> ImportPath
        private MockRateRepository _mock;
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            _mock = new MockRateRepository(config);
        }

        [Test]
        public void TestRate()
        {
            var selectRateList = _mock.GetSelectRateList();

            foreach (var rate in selectRateList)
            {
                Assert.IsNotNull(rate.Value);
                Assert.IsNotNull(rate.Text);
            }
            Assert.Pass();
        }

        [Test]
        public void TestDate()
        {
            var selectRateList = _mock.GetAllHistoryDates();
            foreach (var date in selectRateList)
            {
                Assert.IsNotNull(date.Value);
                Assert.IsNotNull(date.Text);
            }
            Assert.Pass();
        }
    }
}