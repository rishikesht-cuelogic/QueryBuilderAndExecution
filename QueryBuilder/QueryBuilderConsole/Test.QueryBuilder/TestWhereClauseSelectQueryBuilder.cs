
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;

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
            query.AddWhere("firstName", Comparison.Equals, "test");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student  where  firstname = 'test'";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectWithMultipleWhereClauseAndAND()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.Equals, "test");
            query.AddWhere("lastName", Comparison.Equals, "test");

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student  where  ((firstname = 'test') and (lastname = 'test'))";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void SelectWithMultipleWhereClauseAndOR()
        {
            //Assign
            var query = new SelectQueryBuilder();
            query.SelectFromTable("student");
            query.AddWhere("firstName", Comparison.Equals, "test");
            query.AddWhere("lastName", Comparison.Equals, "test",2);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "select * from student  where  firstname = 'test'  or lastname = 'test'";
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
