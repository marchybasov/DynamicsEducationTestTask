using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpTest.Tests
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25)),
                new WeekEnd(new DateTime(2017, 4, 29), new DateTime(2017, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }


        [TestMethod]
        public void TestStartDateOrEndDateOnWeekend()
        {
            DateTime startDate = new DateTime(2017, 4, 23);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
           {
                new WeekEnd(new DateTime(2017, 4, 21), new DateTime(2017, 4, 23)),
                new WeekEnd(new DateTime(2017, 4, 28), new DateTime(2017, 4, 28))

           };
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 29)));


        }

        //[TestMethod]
        //public void TestSwappedWeekendInput()
        //{
        //    DateTime startDate = new DateTime(2020, 8, 20);

        //    int count = 4
        //}
    }
}
