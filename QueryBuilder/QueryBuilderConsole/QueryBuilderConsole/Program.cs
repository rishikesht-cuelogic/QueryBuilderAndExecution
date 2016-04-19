
using PortableDataAccessLayer;
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
            query.AddWhere(stateFilter,2);

            var query2 = new SelectQueryBuilder();
            query2.SelectFromTable("City");
            query2.SelectColumn("Name");
           // var text = query2.BuildQuery();
            var cityFilter = new WhereClause("City.Name", Comparison.In, query2);
            query.AddWhere(cityFilter,2);

            var subQuery = new SelectQueryBuilder();
            subQuery.SelectFromTable("Country");
            var selectCountries = new SelectQueryBuilder();
            selectCountries.SelectFromTable("State");
            selectCountries.SelectColumn("CountryId");
            var filter = new WhereClause("Id", Comparison.In, selectCountries);
            subQuery.AddWhere(filter);
           // var temp = subQuery.BuildQuery();

            var deleteQuery = new DeleteQueryBuilder("State");
            var states = new SelectQueryBuilder();
            states.SelectFromTable("City");
            states.SelectColumn("StateId");
            deleteQuery.AddWhere("Id", Comparison.In, states);
            //var deleteText = deleteQuery.BuildQuery();
            
            

            InsertQueryBuilder insertQuery = new InsertQueryBuilder("Student");
            insertQuery.SetColumns(new string[] { "FirstName", "LastName", "Address", "Email", "IsActive" });
            //insertQuery.SetValues(new object[] { "Nilesh", "Patil", "Kolhapur", "nilesh@gmail.com", 1 });

            SelectQueryBuilder studentData = new SelectQueryBuilder();
            studentData.SelectFromTable("Student");
            studentData.AddWhere("Id", Comparison.Equals, 1);
            studentData.SelectColumns(new string[] { "FirstName", "LastName", "Address", "Email", "IsActive" });
            
            insertQuery.AddSelectQuery(studentData);
            var t = insertQuery.BuildQuery();

            Console.WriteLine(t);
            Console.ReadKey(true);


        }

    }
}
