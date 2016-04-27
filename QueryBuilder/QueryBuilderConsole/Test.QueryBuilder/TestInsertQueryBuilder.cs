using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QueryBuilder;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestInsertQueryBuilder
    {
        [TestMethod]
        public void SimpleInsertQuery()
        {
            //Assign
            InsertQueryBuilder query = new InsertQueryBuilder("Student");
            query.SetColumnValue("firstName", "firstName");
            query.SetColumnValue("lastName", "lastName");
            query.SetColumnValue("email", "email");
            query.SetColumnValue("address", "address");

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "insert into student(firstname,lastname,email,address) values('firstname','lastname','email','address')";
            Assert.AreEqual(expectedOutput, output);
        }

      
        [TestMethod]
        public void InsertQueryWithValuesOnly()
        {
            //Assign
            var values = new object[] { 1, "firstname", "lastname", "email", "address", true };
            InsertQueryBuilder query = new InsertQueryBuilder("Student",values);

            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "insert into student values(1,'firstname','lastname','email','address',1)";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void InsertQueryWithSubquery()
        {
            //Assign
            InsertQueryBuilder query = new InsertQueryBuilder("Student");
            SelectQueryBuilder selectQuery = new SelectQueryBuilder();
            selectQuery.SelectFromTable("Student");
            selectQuery.SelectColumns("Id+100", "FirstName", "LastName", "Address", "Email", "IsActive");
            query.SetSelectQuery(selectQuery);
            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "insert into student select id+100,firstname,lastname,address,email,isactive from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void InsertQueryWithSubqueryAndSelectColumn()
        {
            //Assign
            InsertQueryBuilder query = new InsertQueryBuilder("Student");
            SelectQueryBuilder selectQuery = new SelectQueryBuilder();
            selectQuery.SelectFromTable("Student");
            selectQuery.SelectColumns("FirstName", "LastName", "Address", "Email", "IsActive");
            query.SetSelectQuery(selectQuery, "FirstName", "LastName", "Address", "Email", "IsActive");
            //Act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "insert into student(firstname,lastname,address,email,isactive) select firstname,lastname,address,email,isactive from student";
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertQueryWithPassingObjectAsValue()
        {
            //Assign
            var obj = new { FirstName = "ABC", lastName = "PQR" };
            InsertQueryBuilder query = new InsertQueryBuilder("Student");
            query.SetColumnValue("firstName", obj);

            //act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertQueryWithPassingNullAsValue()
        {
            //Assign
            InsertQueryBuilder query = new InsertQueryBuilder("Student");
            query.SetColumnValue("firstName", null);

            //act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertQueryWithTableNameNull()
        {
            InsertQueryBuilder query = new InsertQueryBuilder(null);
            var output = query.BuildQuery();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertQueryWithTableNameEmpty()
        {
            InsertQueryBuilder query = new InsertQueryBuilder("      ");
            var output = query.BuildQuery();
        }
    }
}
