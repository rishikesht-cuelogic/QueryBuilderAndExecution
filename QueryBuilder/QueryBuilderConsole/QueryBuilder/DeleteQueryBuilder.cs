using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public class DeleteQueryBuilder:QueryBuilder
    {
        #region Properties
        protected string _tableName { get; set; }
        protected SelectQueryBuilder _selectQueryBuilder { get; set; }
        #endregion

        #region Constructor
        public DeleteQueryBuilder() { }
        public DeleteQueryBuilder(string tableName)
        {
            this._tableName = tableName;
        }
        #endregion

        #region Public
        public override string BuildQuery()
        {
            var query = "DELETE FROM " + _tableName;

            // Output where statement
            if (_whereStatement.ClauseLevels > 0)
            {
                query += " WHERE " + _whereStatement.BuildWhereStatement();
            }

            return query;
        }

        public WhereClause AddWhere(string field, Comparison @operator, int level, SelectQueryBuilder selectQueryBuilder)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, selectQueryBuilder);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        #endregion  

    }
}
