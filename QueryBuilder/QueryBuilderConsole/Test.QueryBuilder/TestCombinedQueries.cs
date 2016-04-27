using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestCombinedQueries
    {
        [TestMethod]
        public void GenerateMonthlySpendQuery()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Spend");
            query.AddAggregate(Aggregate.Sum, "Spend.AmountInUSD");
            query.AddJoin(JoinType.InnerJoin, "Location", "LocationId", "Spend", "LocationId");
            query.AddJoin(JoinType.InnerJoin, "Media", "MediaId", "Spend", "MediaId");
            query.AddJoin(JoinType.InnerJoin, "DateTimeDimension", "DateTimeId", "Spend", "DateTimeId");
            query.AddWhere("Location.LocationName", Comparison.Equals, "ARGENTINA");
            query.AddWhere("Media.MediaName", Comparison.Equals, "RADIO");
            query.AddWhere("DateTimeDimension.MonthInNumber", Comparison.Equals, 1);
            query.AddWhere("DateTimeDimension.Year", Comparison.Equals, 2012);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select sum(spend.amountinusd) from spend inner join location on spend.locationid = location.locationid inner join media on spend.mediaid = media.mediaid inner join datetimedimension on spend.datetimeid = datetimedimension.datetimeid where ((location.locationname = 'argentina') and (media.medianame = 'radio') and (datetimedimension.monthinnumber = 1) and (datetimedimension.year = 2012))";
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
