
using QueryBuilder;
using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilderConsole
{
    public enum Columns
    {
        Name,
        Id
    }
    class Program
    {
        static void Main(string[] args)
        {

            //SelectQueryBuilder query = new SelectQueryBuilder(new MSSQLRelationship());
            

            SelectQueryBuilder query = new SelectQueryBuilder();

            // Start with first table
            query.SelectFromTable("City");

            // Add Joins
            query.AddJoin(JoinType.InnerJoin,
                          "State", "StateId",
                          "City", "StateId");

            //query.AddJoin(JoinType.InnerJoin, "Country", "Id", Comparison.Equals, "State", "CountryId");
            query.AddJoin(JoinType.InnerJoin, "Country", "State");
            query.AddJoin(new JoinClause(JoinType.InnerJoin, "Country", "CountryId", Comparison.Equals, "State", "StateId"));

            // Select specific columns
            query.SelectColumns("City.CityName", "state.StateName", "Country.CountryName");

            var countryFilter = new WhereClause("Country.Countryname", Comparison.Equals, "India");
            countryFilter.AddClause(LogicOperator.Or, Comparison.Equals, "Pakistan");
            var stateFilter = new WhereClause("State.StateName", Comparison.Equals, "Maharashtra");
            query.AddWhere(countryFilter);
            query.AddWhere(stateFilter);

            var query2 = new SelectQueryBuilder();
            query2.SelectFromTable("City");
            TopClause topClause = new TopClause(100, TopUnit.Percent);
            query2.TopClause = topClause;
            var text = query2.BuildQuery();

            Console.WriteLine(query.BuildQuery());
            Console.ReadKey(true);
        }

    }
}
