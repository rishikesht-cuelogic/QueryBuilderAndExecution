using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public class UpdateQueryBuilder : QueryBuilder
    {
        #region Properties and fields        
        protected Dictionary<string, string> _columnValues;
        #endregion

        #region C'tor
        public UpdateQueryBuilder()
        {
            _columnValues = new Dictionary<string, string>();
        }
        public UpdateQueryBuilder(string tableName)
        {
            _columnValues = new Dictionary<string, string>();
            this._tableName = tableName;
        }
        #endregion

        #region 
        private string GetSettersInString()
        {
            var text = "";
            foreach (var item in _columnValues)
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
            var query = "UPDATE " + _tableName + " SET ";
            query = query + GetSettersInString()+" WHERE ";
            query = query + _whereStatement.BuildWhereStatement();
            return query;
        }

      
        /// <summary>
        /// It is used to value to column
        /// </summary>
        /// <param name="columnName">It is column name</param>
        /// <param name="value">It is value which will be assigned</param>
        public void SetColumnValue(string columnName, object value)
        {
            _columnValues.Add(columnName, value.ToString());
        }

        #endregion




    }
}
