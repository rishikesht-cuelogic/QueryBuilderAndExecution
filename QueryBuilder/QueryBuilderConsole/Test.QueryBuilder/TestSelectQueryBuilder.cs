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
            var expectedOutput = "select * from city";
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


        [TestMethod]
        public void SelectAllColumnsFromMultipleTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTables("country", "state", "city");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from country,state,city";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectQueryWithAliasColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("Student");
            query.SelectColumn("FirstName [name]");
            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select firstname [name] from student";
            Assert.AreEqual(expectedOutput, output);
        }


        [TestMethod]
        public void SelectQueryWithExists()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("State");
            var subQuery = new SelectQueryBuilder();
            subQuery.SelectFromTable("City");
            query.AddExist(subQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from state where exists (select * from city)";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectQueryWithNotExists()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("State");
            var subQuery = new SelectQueryBuilder();
            subQuery.SelectFromTable("City");
            query.AddExist(subQuery,true);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from state where not exists (select * from city)";
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

        
        #endregion

    }
}
