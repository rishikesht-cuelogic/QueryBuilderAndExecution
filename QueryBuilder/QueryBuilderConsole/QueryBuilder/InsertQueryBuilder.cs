using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public class InsertQueryBuilder : IQueryBuilder
    {
        #region Properties
        protected string _tableName { get; set; }
        protected Dictionary<string, string> _columnValues;
        protected List<string> _selectedColumns = new List<string>();
        protected SelectQueryBuilder _selectQueryBuilder { get; set; }
        #endregion

        #region C'tor
        public InsertQueryBuilder() {
            _columnValues = new Dictionary<string, string>();
        }
        public InsertQueryBuilder(string tableName)
        {
            _columnValues = new Dictionary<string, string>();
            this._tableName = tableName;
        }
        #endregion

        #region Private
        private string GetColumns()
        {
            if (_selectQueryBuilder != null)
                return Utility.ConvertArrayToString(_selectedColumns,true);

            return Utility.ConvertArrayToString(_columnValues.Select(t => t.Key).ToList(),true);
        }
        private string GetValues()
        {
            return Utility.ConvertArrayToString(_columnValues.Select(t => t.Value.ToString()).ToList(),true);
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
        /// <summary>
        /// It is used to value to column
        /// </summary>
        /// <param name="columnName">It is column name</param>
        /// <param name="value">It is value which will be assigned</param>
        public void SetColumnValue(string columnName, object value)
        {
            _columnValues.Add(columnName, value.ToString());
        }
        /// <summary>
        /// It builds the query and returns string
        /// </summary>
        /// <returns></returns>
        public string BuildQuery()
        {
            var query = "INSERT INTO " + _tableName+" ";
            query = query + GetColumns();
            if (_selectQueryBuilder != null)
            {
                return query + " " + _selectQueryBuilder.BuildQuery();
            }
            query = query + " VALUES ";
            query = query + GetValues();
            return query;
        }
        /// <summary>
        /// It is used to insert bunch of data using select query.
        /// </summary>
        /// <param name="selectQueryBuilder">It is object of select query builder</param>
        /// <param name="columns">It is array of column names for which you have to insert data</param>
        public void SetSelectQuery(SelectQueryBuilder selectQueryBuilder, string[] columns)
        {
            foreach(var item in columns)
            {
                _selectedColumns.Add(item);
            }
            _selectQueryBuilder = selectQueryBuilder;
        }
        #endregion     

    }
}
