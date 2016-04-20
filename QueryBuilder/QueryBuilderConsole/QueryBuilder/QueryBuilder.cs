using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public abstract class QueryBuilder: IQueryBuilder
    {
        #region Properties and Fields
        protected string _tableName { get; set; }
        protected WhereStatement _whereStatement = new WhereStatement();
        internal WhereStatement WhereStatement
        {
            get { return _whereStatement; }
            set { _whereStatement = value; }
        }
        #endregion

        #region Public
        /// <summary>
        /// It is sets the table name on which insert/update operation will occur
        /// </summary>
        /// <param name="tableName"></param>
        public void SetTableName(string tableName)
        {
            this._tableName = tableName;
        }
        public void AddWhere(WhereClause clause) {
            AddWhere(clause, 1);
        }
        public void AddWhere(WhereClause clause, int level)
        {
            _whereStatement.Add(clause, level);
        }
        /// <summary>
        /// It add where condition.
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is filter value</param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue) {
            return AddWhere(field, @operator, compareValue, 1);
        }
        public WhereClause AddWhere(Enum field, Comparison @operator, object compareValue) {
            return AddWhere(field.ToString(), @operator, compareValue, 1);
        }
        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is filter value</param>
        /// <param name="level">It is level of where clause</param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        public WhereClause AddWhere(string field, Comparison @operator, SelectQueryBuilder selectQueryBuilder)
        {
            return AddWhere(field, @operator, selectQueryBuilder, 1);
        }
        public WhereClause AddWhere(string field, Comparison @operator, SelectQueryBuilder selectQueryBuilder, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, selectQueryBuilder);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        #endregion

        #region Abstract Methods
        public abstract string BuildQuery();
        #endregion

    }
}
