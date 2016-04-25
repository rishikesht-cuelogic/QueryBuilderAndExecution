using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestDeleteQueryBuilder
    {
        [TestMethod]
        public void DeleteAllRecords()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "delete from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void DeleteConditionalQueryByParameterisedConstructor()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student","Id",Comparison.Equals,2);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "delete from student where id = 2";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void DeleteConditionalQueryWithStringComparisonValue()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student", "Id", Comparison.Equals, "2");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "delete from student where id = '2'";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void DeleteQueryWithINClause()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student", "Id", Comparison.In, new int[] { 1,2,3});

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "delete from student where id in ('1','2','3')";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void DeleteConditionalQueryWithDatetimeComparisonValue()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student", "createddate", Comparison.GreaterThan, DateTime.Now.Date);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "delete from student where createddate > '2016-04-25 12:00:00'";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void DeleteQueryUsingSubQuery()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student");
            var selectQuery = new SelectQueryBuilder();
            selectQuery.SelectFromTable("Student");
            selectQuery.SelectColumn("Id");
            query.AddWhere("Id", Comparison.In, selectQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "delete from student where id in (select id from student)";
            Assert.AreEqual(expectedOutput, output);
        }

        #region Exceptions
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteQueryWithTableNameNull()
        {
            //Assign
            var query = new DeleteQueryBuilder(null);

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteQueryWithBlankTableName()
        {
            //Assign
            var query = new DeleteQueryBuilder("");

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteQueryWithWhitespacesTableName()
        {
            //Assign
            var query = new DeleteQueryBuilder("     ");

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteConditionQueryWithNullColumnName()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student",null,Comparison.Equals,2);

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteConditionQueryWithEmptyColumnName()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student", "", Comparison.Equals, 2);

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteConditionQueryWithWhitespacesColumnName()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student", "  ", Comparison.Equals, 2);

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteConditionQueryWithObjectComparisonValue()
        {
            //Assign
            var query = new DeleteQueryBuilder("Student", "id", Comparison.Equals,new { Id=2});

            //Act
            var output = query.BuildQuery().ToLower();
        }
        #endregion


    }
}
