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
            var query = new SelectQueryBuilder();
            query.SelectFromTable("City");
            var output = query.BuildQuery().ToLower();
            Assert.AreEqual("select city.* from city",output );
        }
    }
}
