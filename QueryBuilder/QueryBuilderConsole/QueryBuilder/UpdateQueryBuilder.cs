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
        protected Dictionary<string, object> columnValues;
        #endregion

        #region C'tor
        //public UpdateQueryBuilder()
        //{
        //    columnValues = new Dictionary<string, string>();
        //}
        public UpdateQueryBuilder(string tableName)
        {
            Validate.TableName(tableName);
            columnValues = new Dictionary<string, object>();
            this.tableName = tableName;
        }
        #endregion

        #region 
        private string GetSettersInString()
        {
            var text = "";
            foreach (var item in columnValues)
            {
                text = text + item.Key + " = " + SqlUtility.FormatSQLValue(item.Value)+ ",";
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
                var query = Constants.Update+" " + tableName + " "+Constants.Set+" ";
                query = query + GetSettersInString();
                if (whereStatement!=null && whereStatement.Count != 0)
                {
                    query = query + " " + Constants.Where + " ";
                    query = query + whereStatement.BuildWhereStatement();
                }
                query = query.Trim();
                return Utility.RemoveMultipleSpace(query);
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
            try
            {
                if (value == null)
                    throw new NullReferenceException("value should not be null");

                if (!SqlUtility.IsValidSqlValue(value))
                    throw new ArgumentException("value should be primitive datatype");

                columnValues.Add(columnName, value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion




    }
}
