
using QueryBuilder;
using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilderConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            MSSQLRelationship sr = new MSSQLRelationship();
            var temp=sr.GetRelationInfo("State", "Country");

            SelectQueryBuilder query = new SelectQueryBuilder(new MSSQLRelationship());

            // Start with first table
            query.SelectFromTable("City");

            // Add Joins
            query.AddJoin(JoinType.InnerJoin,
                          "State", "StateId",
                          Comparison.Equals,
                          "City", "StateId");

            //query.AddJoin(JoinType.InnerJoin, "Country", "Id", Comparison.Equals, "State", "CountryId");
            query.AddJoin(JoinType.InnerJoin, "Country", Comparison.Equals, "State");
            query.AddJoin(new JoinClause(JoinType.InnerJoin, "Country", "CountryId", Comparison.Equals, "State", "StateId"));

            // Select specific columns
            query.SelectColumns("City.CityName", "state.StateName", "Country.CountryName");

            query.AddWhere("Country.Countryname", Comparison.Equals, "India");

            Console.WriteLine(query.BuildQuery());
            Console.ReadKey(true);
        }

    }
}
