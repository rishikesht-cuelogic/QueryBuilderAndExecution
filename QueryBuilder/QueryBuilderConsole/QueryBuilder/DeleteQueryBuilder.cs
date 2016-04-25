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
            Validate.TableName(tableName);
            this.tableName = tableName;
        }
        /// <summary>
        /// It is parametersized constructor which accepts Table Name, columnName, operator, and compare value
        /// </summary>
        /// <param name="tableName">It is table name from which records get deleted</param>
        /// <param name="columnName">It is a column name on which you have to apply condition</param>
        /// <param name="operator">It is a comparison operator</param>
        /// <param name="compareValue">It is a value which used to compare</param>
        public DeleteQueryBuilder(string tableName,string columnName,Comparison @operator,object compareValue)
        {
            Validate.TableName(tableName);

            this.tableName = tableName;
            AddWhere(columnName, @operator, compareValue);
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
                query = query.Trim();
                return Utility.RemoveMultipleSpace(query);
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
