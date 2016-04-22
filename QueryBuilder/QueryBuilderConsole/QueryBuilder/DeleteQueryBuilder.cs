using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    /// <summary>
    /// It is used to create DELETE query.
    /// </summary>
    public class DeleteQueryBuilder:QueryBuilder
    {
        #region Properties
        protected SelectQueryBuilder selectQueryBuilder { get; set; }
        #endregion

        #region Constructor
        //public DeleteQueryBuilder() { }
        public DeleteQueryBuilder(string tableName)
        {
            this.tableName = tableName;
        }
        #endregion

        #region Public
        /// <summary>
        /// It builds the query and returns string
        /// </summary>
        /// <returns></returns>
        public override string BuildQuery()
        {
            try
            {
                var query = Constants.Delete+" "+Constants.From+" " + tableName;

                // Output where statement
                if (whereStatement != null && whereStatement.ClauseLevels > 0)
                {
                    query += " "+Constants.Where+" " + whereStatement.BuildWhereStatement();
                }

                return query;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public WhereClause AddWhere(string field, Comparison @operator, int level, SelectQueryBuilder selectQueryBuilder)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, selectQueryBuilder);
            whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        #endregion  

    }
}
