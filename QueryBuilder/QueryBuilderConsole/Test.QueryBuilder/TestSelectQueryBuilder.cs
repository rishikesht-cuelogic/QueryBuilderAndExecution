using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestSelectQueryBuilder
    {
        [TestMethod]
        public void SimpleSelectQuery()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select city.* from city";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectSpecificColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumns("id", "name");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select id,name from city";
            Assert.AreEqual(expectedOutput, output);
        }

     

        #region Exception Test
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SelectQueryWithNotPassingTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();

            //Act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SelectQuery()
        {
            //Assign
            var query = new SelectQueryBuilder();

            //Act
            var output = query.BuildQuery().ToLower();
        }
        #endregion

    }
}
