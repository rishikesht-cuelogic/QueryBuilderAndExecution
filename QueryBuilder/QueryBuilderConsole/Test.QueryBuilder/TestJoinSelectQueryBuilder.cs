using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestJoinSelectQueryBuilder
    {
        [TestMethod]
        public void InnerJoinOnTwoTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "City", "StateId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city inner join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void LeftJoinOnTwoTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.LeftJoin, "State", "Id", "City", "StateId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city left join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void RightJoinOnTwoTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.RightJoin, "State", "Id", "City", "StateId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city right join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void FullJoinOnTwoTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.FullJoin, "State", "Id", "City", "StateId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city full join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void InnerJoinOnTwoTablesByJoinClauseObject()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            JoinClause join = new JoinClause(JoinType.InnerJoin, "State", "Id", "City", "StateId");
            query.AddJoin(join);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city inner join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void LeftJoinOnTwoTablesByJoinClauseObject()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            JoinClause join = new JoinClause(JoinType.LeftJoin,"State","Id","City","StateId");
            query.AddJoin(join);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city left join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void RightJoinOnTwoTablesByJoinClauseObject()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            JoinClause join = new JoinClause(JoinType.RightJoin, "State", "Id", "City", "StateId");
            query.AddJoin(join);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city right join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void FullJoinOnTwoTablesByJoinClauseObject()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            JoinClause join = new JoinClause(JoinType.FullJoin, "State", "Id", "City", "StateId");
            query.AddJoin(join);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city full join state on city.stateid = state.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void InnerJoinOnThreeTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "City", "StateId");
            query.AddJoin(JoinType.InnerJoin, "Country", "Id", "State", "CountryId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city inner join state on city.stateid = state.id inner join country on state.countryid = country.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void LeftJoinOnLeftTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.LeftJoin, "State", "Id", "City", "StateId");
            query.AddJoin(JoinType.LeftJoin, "Country", "Id", "State", "CountryId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city left join state on city.stateid = state.id left join country on state.countryid = country.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void RightJoinOnThreeTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.RightJoin, "State", "Id", "City", "StateId");
            query.AddJoin(JoinType.RightJoin, "Country", "Id", "State", "CountryId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city right join state on city.stateid = state.id right join country on state.countryid = country.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void FullJoinOnThreeTables()
        {
            //Assign
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.FullJoin, "State", "Id", "City", "StateId");
            query.AddJoin(JoinType.FullJoin, "Country", "Id", "State", "CountryId");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city full join state on city.stateid = state.id full join country on state.countryid = country.id";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void CrossJoinOnTwoTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddCrossJoin("State");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city cross join state";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void CrossJoinOnThreeTables()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddCrossJoin("State");
            query.AddCrossJoin("Country");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from city cross join state cross join country";
            Assert.AreEqual(expectedOutput, output);
        }


        #region Exceptions

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CrossJoinOnTwoTablesWithEmptyTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddCrossJoin("");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CrossJoinOnTwoTablesWithWhitespacesTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddCrossJoin("  ");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CrossJoinOnTwoTablesWithNullTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddCrossJoin(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithWhitespaceToTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, " ", "Id", "City", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithEmptyToTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "", "Id", "City", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InnerJoinOnTwoTablesWithNullToTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, null, "Id", "City", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithWhitespaceFromTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithEmptyFromTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "  ", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InnerJoinOnTwoTablesWithNullFromTableName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", null, "StateId");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithWhitespaceToColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "  ", "City", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithEmptyToColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "", "City", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InnerJoinOnTwoTablesWithNullToColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", null, "City", "StateId");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithWhitespaceFromColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "City", "  ");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InnerJoinOnTwoTablesWithEmptyFromColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "City", "");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InnerJoinOnTwoTablesWithNullFromColumnName()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            query.AddJoin(JoinType.InnerJoin, "State", "Id", "City", null);
        }
        #endregion
    }
}
