
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestTopClauseSelectQueryBuilder
    {
        [TestMethod]
        public void SelectTopRecords()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopRecords = 100;

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select top 100 * from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectTopMaximumRecords()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopRecords = int.MaxValue;

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select top "+ int.MaxValue + " * from student";
            Assert.AreEqual(expectedOutput, output);
        }      

        [TestMethod]
        public void SelectTopWithValidPercentage()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopClause = new TopClause { Unit = TopUnit.Percent, Quantity = 99 };

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select top 99 percent * from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectTopWith100Percentage()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopClause = new TopClause { Unit = TopUnit.Percent, Quantity = 100 };

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student";
            Assert.AreEqual(expectedOutput, output);
        }

        #region Exceptions
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SelectTopWithInvalidPercentage()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopClause = new TopClause { Unit = TopUnit.Percent, Quantity = 101 };

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SelectTopWithNegativePercentage()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopClause = new TopClause { Unit = TopUnit.Percent, Quantity = -10 };

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SelectTopWithNegativeQuanity()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.TopRecords = -1;

            //Act
            var output = query.BuildQuery().ToLower();
        }
        #endregion  

    }
}
