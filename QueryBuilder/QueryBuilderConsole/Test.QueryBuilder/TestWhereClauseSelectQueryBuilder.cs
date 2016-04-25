
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;
namespace Test.QueryBuilder
{
    [TestClass]
    public class TestWhereClauseSelectQueryBuilder
    {
        [TestMethod]
        public void SelectWithSingleWhereClause()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.Equals, "@test");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student where firstname = '@test'";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectWithNotEqualToCondition()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.NotEquals, "@test");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student where firstname <> '@test'";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectWithLikeClause()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.Like, "%test%");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student where firstname like '%test%'";
            Assert.AreEqual(expectedOutput, output);
        }

        #region IN Clause
        [TestMethod]
        public void SelectWithIN()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");

            query.AddWhere("id", Comparison.In, new int[] { 1, 2, 3 });

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student where id in ('1','2','3')";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectWithINWithSubquery()
        {
            //Assign
            var query = new SelectQueryBuilder();
            var subQuery = new SelectQueryBuilder();
            subQuery.SelectFromTable("state");
            subQuery.SelectColumn("countryid");
            query.SelectFromTable("country");
            query.AddWhere("id", Comparison.In, subQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from country where id in (select countryid from state)";
            Assert.AreEqual(expectedOutput, output);
        }
        #endregion

        [TestMethod]
        public void SelectWithMultipleConditionsWithAND()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.Equals, "test");
            query.AddWhere("lastName", Comparison.Equals, "test");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student where ((firstname = 'test') and (lastname = 'test'))";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectWithMultipleConditionsWithOR()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.Equals, "test");
            query.AddWhere("lastName", Comparison.Equals, "test", 2);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student where firstname = 'test' or lastname = 'test'";
            Assert.AreEqual(expectedOutput, output);
        }

        #region Exceptions
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectWithINWithInvalidObjectData()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");

            query.AddWhere("id", Comparison.In, new { });

            //Act
            var output = query.BuildQuery().ToLower();

        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SelectWithINWithNullValue()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");

            query.AddWhere("id", Comparison.In, null);

            //Act
            var output = query.BuildQuery().ToLower();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectWithINWithNonPrimitiveDatatypeValues()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");

            query.AddWhere("id", Comparison.In, new SelectQueryBuilder[] { });

            //Act
            var output = query.BuildQuery().ToLower();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectWithINWithEmptyArray()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");

            query.AddWhere("id", Comparison.In, new int[] { });

            //Act
            var output = query.BuildQuery().ToLower();
        }
        #endregion
    }
}
