using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder;
using QueryBuilder.Enums;
using System;

namespace Test.QueryBuilder
{
    [TestClass]
    public class TestUpdateQueryBuilder
    {
        [TestMethod]
        public void UpdateOneColumn()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("firstName", "nilesh");

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "update student set firstname = 'nilesh'";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void UpdateIntegerOneColumn()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("id", 1);

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "update student set id = 1";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void UpdateBooleanOneColumn()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("isActive", false);

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "update student set isactive = 0";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void UpdateTwoColumns()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("firstName", "nilesh");
            query.SetColumnValue("lastName", "patil");

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "update student set firstname = 'nilesh',lastname = 'patil'";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void UpdateOneColumnWithCondition()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("firstName", "nilesh");
            query.AddWhere("Id", Comparison.Equals, 1);

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "update student set firstname = 'nilesh' where id = 1";
            Assert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void UpdateTwoColumnsWithCondition()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("firstName", "nilesh");
            query.SetColumnValue("lastName", "patil");
            query.AddWhere("Id", Comparison.Equals, 1);

            //act
            var output = query.BuildQuery().ToLower();

            //Assert
            var expectedOutput = "update student set firstname = 'nilesh',lastname = 'patil' where id = 1";
            Assert.AreEqual(expectedOutput, output);
        }

        #region Exceptions
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateQueryWithPassingObjectAsValue()
        {
            //Assign
            var obj = new { FirstName = "ABC", lastName = "PQR" };
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("firstName", obj);

            //act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateQueryWithPassingNullAsValue()
        {
            //Assign
            UpdateQueryBuilder query = new UpdateQueryBuilder("Student");
            query.SetColumnValue("firstName", null);

            //act
            var output = query.BuildQuery().ToLower();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateQueryWithTableNameNull()
        {
            UpdateQueryBuilder query = new UpdateQueryBuilder(null);
            var output = query.BuildQuery();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateQueryWithTableNameEmpty()
        {
            UpdateQueryBuilder query = new UpdateQueryBuilder("      ");
            var output = query.BuildQuery();
        }
        #endregion  
    }
}
