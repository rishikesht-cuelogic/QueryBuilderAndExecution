using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    /// <summary>
    /// It is used to create INSERT query.
    /// </summary>
    public class InsertQueryBuilder : IQueryBuilder
    {
        #region Properties
        protected string tableName { get; set; }
        protected Dictionary<string, string> columnValues;
        protected List<string> listValues { get; set; }
        protected List<string> selectedColumns = new List<string>();
        protected SelectQueryBuilder selectQueryBuilder { get; set; }
        #endregion

        #region C'tor
        public InsertQueryBuilder(string tableName)
        {
            columnValues = new Dictionary<string, string>();
            this.tableName = tableName;
        }
        /// <summary>
        /// It 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        public InsertQueryBuilder(string tableName,params string[] values)
        {
            listValues = new List<string>();
            foreach (var item in values)
            {
                listValues.Add(item);
            }
            columnValues = new Dictionary<string, string>();
            this.tableName = tableName;
        }
        #endregion

        #region Private
        private string GetColumns()
        {
            if (selectQueryBuilder != null)
                return Utility.ConvertArrayToString(selectedColumns,true);

            return Utility.ConvertArrayToString(columnValues.Select(t => t.Key).ToList(),true);
        }
        private string GetValues()
        {
            return Utility.ConvertArrayToStringWithWrapSingleQuote(columnValues.Select(t => t.Value).ToList(),true);
        }
        #endregion

        #region Public     
        /// <summary>
        /// It is used to value to column
        /// </summary>
        /// <param name="columnName">It is column name</param>
        /// <param name="value">It is a primitive datatype value which will be assigned</param>
        public void SetColumnValue(string columnName, object value)
        {
            try
            {
                if (value == null)
                    throw new NullReferenceException("value should not be null");

                if (!value.GetType().IsPrimitive && value.GetType().Name!="String")
                    throw new ArgumentException("value should be primitive datatype");

                columnValues.Add(columnName, value.ToString());
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        /// <summary>
        /// It builds the query and returns string
        /// </summary>
        /// <returns></returns>
        public string BuildQuery()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tableName))
                    throw new NullReferenceException("tableName should not be null or empty");

                var query = Constants.Insert+" "+Constants.Into+" " + tableName + "";
                if (listValues != null && listValues.Count > 0)
                {
                    return query + " "+Constants.Values + Utility.ConvertArrayToStringWithWrapSingleQuote(listValues, true);
                }
                query = query + GetColumns();
                if (selectQueryBuilder != null)
                {
                    return query + " " + selectQueryBuilder.BuildQuery();
                }
                query = query + " "+ Constants.Values;
                query = query + GetValues();
                return query;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        /// <summary>
        /// It is used to insert bunch of data using select query.
        /// </summary>
        /// <param name="selectQueryBuilder">It is object of select query builder</param>
        /// <param name="columns">It is array of column names for which you have to insert data</param>
        public void SetSelectQuery(SelectQueryBuilder selectQueryBuilder, params string[] columns)
        {
            foreach(var item in columns)
            {
                selectedColumns.Add(item);
            }
            this.selectQueryBuilder = selectQueryBuilder;
        }
        #endregion     

    }
}
