using System;
using QueryBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder.Enums;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestUnionSelectQueryBuilder
    {
        [TestMethod]
        public void UnionOfTwoTable()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            query.AddUnion(Union.Union, stateQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city union select * from state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionOfTwoTableOnSingleColumn()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumn("Name");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumn("Name");
            query.AddUnion(Union.Union, stateQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select name from city union select name from state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionOfThreeTableOnSingleColumn()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumn("CityName");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumn("StateName");

            var countryQuery = new SelectQueryBuilder();
            countryQuery.SelectFromTable("Country");
            countryQuery.SelectColumn("CountryName");

            query.AddUnion(Union.Union, stateQuery);
            query.AddUnion(Union.Union, countryQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select cityname from city union select statename from state union select countryname from country";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionOfTwoTableOnTwoColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumns("Id","CityName");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumns("Id","StateName");
            query.AddUnion(Union.Union, stateQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select id,cityname from city union select id,statename from state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionOfThreeTableOnTwoColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumns("id","CityName");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumns("id","StateName");

            var countryQuery = new SelectQueryBuilder();
            countryQuery.SelectFromTable("Country");
            countryQuery.SelectColumns("id","CountryName");

            query.AddUnion(Union.Union, stateQuery);
            query.AddUnion(Union.Union, countryQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select id,cityname from city union select id,statename from state union select id,countryname from country";
            Assert.AreEqual(expectedOutput, output);
        }


        #region Union All
        [TestMethod]
        public void UnionAllOfTwoTable()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            query.AddUnion(Union.UnionAll, stateQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city union all select * from state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionAllOfTwoTableOnSingleColumn()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumn("Name");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumn("Name");
            query.AddUnion(Union.UnionAll, stateQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select name from city union all select name from state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionAllOfThreeTableOnSingleColumn()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumn("CityName");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumn("StateName");

            var countryQuery = new SelectQueryBuilder();
            countryQuery.SelectFromTable("Country");
            countryQuery.SelectColumn("CountryName");

            query.AddUnion(Union.UnionAll, stateQuery);
            query.AddUnion(Union.UnionAll, countryQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select cityname from city union all select statename from state union all select countryname from country";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionAllOfTwoTableOnTwoColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumns("Id", "CityName");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumns("Id", "StateName");
            query.AddUnion(Union.UnionAll, stateQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select id,cityname from city union all select id,statename from state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void UnionAllOfThreeTableOnTwoColumns()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.SelectColumns("id", "CityName");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            stateQuery.SelectColumns("id", "StateName");

            var countryQuery = new SelectQueryBuilder();
            countryQuery.SelectFromTable("Country");
            countryQuery.SelectColumns("id", "CountryName");

            query.AddUnion(Union.UnionAll, stateQuery);
            query.AddUnion(Union.UnionAll, countryQuery);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select id,cityname from city union all select id,statename from state union all select id,countryname from country";
            Assert.AreEqual(expectedOutput, output);
        }
        #endregion

        #region Exceptions
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnionByPassingNullForSelectQueryBuilder()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            var stateQuery = new SelectQueryBuilder();
            stateQuery.SelectFromTable("State");
            query.AddUnion(Union.Union, null);
        }
        #endregion

    }
}
