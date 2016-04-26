using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestAggregateSelectQueryBuilder
    {
        [TestMethod]
        public void TestAggregateFunctionCount()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Count, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select count(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestAggregateFunctionMin()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Min, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select min(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestAggregateFunctionMax()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Max, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select max(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestAggregateFunctionAvg()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Avg, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select avg(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestAggregateFunctionSum()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Sum, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select sum(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void TestAggregateFunctionSumAndCount()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Sum, "Id");
            query.AddAggregate(Aggregate.Count, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select sum(id),count(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void TestAggregateFunctionSumAndAvg()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Sum, "Id");
            query.AddAggregate(Aggregate.Avg, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select sum(id),avg(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void TestAggregateFunctionMinAndMax()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Min, "Id");
            query.AddAggregate(Aggregate.Max, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select min(id),max(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void TestAggregateFunctionAvgAndSumAndMinAndMax()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Avg, "Id");
            query.AddAggregate(Aggregate.Sum, "Id");
            query.AddAggregate(Aggregate.Min, "Id");
            query.AddAggregate(Aggregate.Max, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select avg(id),sum(id),min(id),max(id) from city";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestAggregateFunctionMaxWithSelectedAnotherColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumn("Name");
            query.AddAggregate(Aggregate.Max, "Id");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select max(id),name from city";
            Assert.AreEqual(expectedOutput, output);
        }
        #region Exceptions
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAggregateFunctionCountWithNullColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Count, null);

            //Act
            var output = query.BuildQuery().ToLower();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAggregateFunctionCountWithEmptylColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Count, "");

            //Act
            var output = query.BuildQuery().ToLower();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAggregateFunctionCountWithWhitespacesColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddAggregate(Aggregate.Count, "  ");

            //Act
            var output = query.BuildQuery().ToLower();

        }
        #endregion
    }
}
