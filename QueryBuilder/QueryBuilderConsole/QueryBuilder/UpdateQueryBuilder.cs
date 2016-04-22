using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    /// <summary>
    /// It is used to create UPDATE query.
    /// </summary>
    public class UpdateQueryBuilder : QueryBuilder
    {
        #region Properties and fields        
        protected Dictionary<string, string> columnValues;
        #endregion

        #region C'tor
        //public UpdateQueryBuilder()
        //{
        //    columnValues = new Dictionary<string, string>();
        //}
        public UpdateQueryBuilder(string tableName)
        {
            columnValues = new Dictionary<string, string>();
            this.tableName = tableName;
        }
        #endregion

        #region 
        private string GetSettersInString()
        {
            var text = "";
            foreach (var item in columnValues)
            {
                text = text + item.Key + " = '" + item.Value + "',";
            }
            text = text.TrimEnd(',');
            return text;
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
                var query = "UPDATE " + tableName + " SET ";
                query = query + GetSettersInString() + " WHERE ";
                query = query + whereStatement.BuildWhereStatement();
                return query;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

      
        /// <summary>
        /// It is used to value to column
        /// </summary>
        /// <param name="columnName">It is column name</param>
        /// <param name="value">It is value which will be assigned</param>
        public void SetColumnValue(string columnName, object value)
        {
            columnValues.Add(columnName, value.ToString());
        }

        #endregion




    }
}
