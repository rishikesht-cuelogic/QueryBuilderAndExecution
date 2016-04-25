

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestDistinctSelectQueryBuilder
    {
        [TestMethod]
        public void SelectDistinctOnSingleColumn()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.SelectColumns("firstName");
            query.Distinct = true;

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select distinct firstname from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectDistinctOnMultipleColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.SelectColumns("firstName", "lastName", "email");
            query.Distinct = true;

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select distinct firstname,lastname,email from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectDistinctOnAllColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.Distinct = true;

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select distinct * from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectQueryWithNoDistinct()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.Distinct = false;

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student";
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
