using System;
using System.Collections.Generic;
using System.Data.Common;
using QueryBuilder.Enums;

namespace QueryBuilder
{
    public class SelectQueryBuilder : IQueryBuilder
    {
        protected bool _distinct = false;
        protected TopClause _topClause = new TopClause(100, TopUnit.Percent);
        protected List<string> _selectedColumns = new List<string>();	// array of string
        protected List<string> _selectedTables = new List<string>();	// array of string
        protected List<JoinClause> _joins = new List<JoinClause>();	// array of JoinClause
        protected WhereStatement _whereStatement = new WhereStatement();
        protected List<OrderByClause> _orderByStatement = new List<OrderByClause>();	// array of OrderByClause
        protected List<string> _groupByColumns = new List<string>();		// array of string
        protected WhereStatement _havingStatement = new WhereStatement();
        protected IdbRelationship _dbRelationship;

        internal WhereStatement WhereStatement
        {
            get { return _whereStatement; }
            set { _whereStatement = value; }
        }

        public SelectQueryBuilder()
        {
            _dbRelationship = new MSSQLRelationship();
        }
        /// <summary>
        /// Parameterized constructor which accepts IdbRelationship 
        /// </summary>
        /// <param name="dbRelationship">Auto detection of foreign key and primary key</param>
        public SelectQueryBuilder(IdbRelationship dbRelationship = null)
        {
            _dbRelationship = (dbRelationship != null) ? dbRelationship : new MSSQLRelationship();
        }

        public bool Distinct
        {
            get { return _distinct; }
            set { _distinct = value; }
        }

        public int TopRecords
        {
            get { return _topClause.Quantity; }
            set
            {
                _topClause.Quantity = value;
                _topClause.Unit = TopUnit.Records;
            }
        }
        public TopClause TopClause
        {
            get { return _topClause; }
            set { _topClause = value; }
        }

        /// <summary>
        /// Return array of selected columns. If columns are not selected, It returns single element array with value *
        /// </summary>
        public string[] SelectedColumns
        {
            get
            {
                if (_selectedColumns.Count > 0)
                    return _selectedColumns.ToArray();
                else
                    return new string[1] { "*" };
            }
        }
        /// <summary>
        /// Returns array of selected tables
        /// </summary>
        public string[] SelectedTables
        {
            get { return _selectedTables.ToArray(); }
        }
        /// <summary>
        /// It selects all columns
        /// </summary>
        public void SelectAllColumns()
        {
            _selectedColumns.Clear();
        }
        /// <summary>
        /// It adds aggregate function COUNT()
        /// </summary>
        public void SelectCount()
        {
            SelectColumn("count(1)");
        }
        /// <summary>
        /// Select single column
        /// </summary>
        /// <param name="column">It is column name which will be selected</param>
        public void SelectColumn(string column)
        {
            _selectedColumns.Clear();
            _selectedColumns.Add(column);
        }
        /// <summary>
        /// It selects multiple columns
        /// </summary>
        /// <param name="columns">It is array of selectable column's names</param>
        public void SelectColumns(params string[] columns)
        {
            _selectedColumns.Clear();
            foreach (string column in columns)
            {
                _selectedColumns.Add(column);
            }
        }
        /// <summary>
        /// It selects table
        /// </summary>
        /// <param name="table">It is table name</param>
        public void SelectFromTable(string table)
        {
            _selectedTables.Clear();
            _selectedTables.Add(table);
        }
        /// <summary>
        /// It selects multiple tables
        /// </summary>
        /// <param name="tables">It is array of tables names</param>
        public void SelectFromTables(params string[] tables)
        {
            _selectedTables.Clear();
            foreach (string Table in tables)
            {
                _selectedTables.Add(Table);
            }
        }
        /// <summary>
        /// Add new join using JoinClause
        /// </summary>
        /// <param name="newJoin">It is join clause</param>
        public void AddJoin(JoinClause newJoin)
        {
            _joins.Add(newJoin);
        }
        /// <summary>
        /// Add new join using all parameters
        /// </summary>
        /// <param name="join">It is Join type</param>
        /// <param name="toTableName">It Reference table name i.e. Parent table name</param>
        /// <param name="toColumnName">It is primary key column name of referenced table i.e. column name of primary key of parent table</param>
        /// <param name="fromTableName">It is child table name</param>
        /// <param name="fromColumnName">It is column name of foreign key in child table</param>
        /// /// <param name="operator">It is comparison operator. Default is equality</param>
        public void AddJoin(JoinType join, string toTableName, string toColumnName, string fromTableName, string fromColumnName, Comparison @operator=Comparison.Equals)
        {
            JoinClause NewJoin = new JoinClause(join, toTableName, toColumnName, @operator, fromTableName, fromColumnName);
            _joins.Add(NewJoin);
        }
        /// <summary>
        ///  Add new join using two table names. It will auto detect referenced column name and primary key column name
        /// </summary>
        /// <param name="join">It is Type is join</param>
        /// <param name="toTableName">It Reference table name i.e. Parent table name</param>
        /// <param name="fromTableName">It is child table name</param>
        /// <param name="operator">It is comparison operator. Default is equality</param>
        public void AddJoin(JoinType join, string toTableName, string fromTableName, Comparison @operator=Comparison.Equals)
        {
            var relation = _dbRelationship.GetRelationInfo(fromTableName, toTableName);
            JoinClause NewJoin = new JoinClause(join, toTableName, relation.ToColumnName, @operator, fromTableName, relation.FromColumnName);
            _joins.Add(NewJoin);
        }
        /// <summary>
        /// Property of WhereStatement. 
        /// </summary>
        public WhereStatement Where
        {
            get { return _whereStatement; }
            set { _whereStatement = value; }
        }

        public void AddWhere(WhereClause clause) { AddWhere(clause, 1); }
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
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue) { return AddWhere(field, @operator, compareValue, 1); }
        public WhereClause AddWhere(Enum field, Comparison @operator, object compareValue) { return AddWhere(field.ToString(), @operator, compareValue, 1); }
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            _whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }

        public void AddOrderBy(OrderByClause clause)
        {
            _orderByStatement.Add(clause);
        }
        public void AddOrderBy(Enum field, Sorting order) { this.AddOrderBy(field.ToString(), order); }
        public void AddOrderBy(string field, Sorting order)
        {
            OrderByClause NewOrderByClause = new OrderByClause(field, order);
            _orderByStatement.Add(NewOrderByClause);
        }

        public void GroupBy(params string[] columns)
        {
            foreach (string Column in columns)
            {
                _groupByColumns.Add(Column);
            }
        }

        public WhereStatement Having
        {
            get { return _havingStatement; }
            set { _havingStatement = value; }
        }

        public void AddHaving(WhereClause clause) { AddHaving(clause, 1); }
        public void AddHaving(WhereClause clause, int level)
        {
            _havingStatement.Add(clause, level);
        }
        public WhereClause AddHaving(string field, Comparison @operator, object compareValue) { return AddHaving(field, @operator, compareValue, 1); }
        public WhereClause AddHaving(Enum field, Comparison @operator, object compareValue) { return AddHaving(field.ToString(), @operator, compareValue, 1); }
        public WhereClause AddHaving(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            _havingStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }

        public string BuildQuery()
        {
            return (string)this.GetQuery();
        }

        /// <summary>
        /// Builds the select query
        /// </summary>
        /// <returns>Returns a string containing the query, or a DbCommand containing a command with parameters</returns>
        private object GetQuery()
        {
            string Query = "SELECT ";

            // Output Distinct
            if (_distinct)
            {
                Query += "DISTINCT ";
            }

            // Output Top clause
            if (!(_topClause.Quantity == 100 & _topClause.Unit == TopUnit.Percent))
            {
                Query += "TOP " + _topClause.Quantity;
                if (_topClause.Unit == TopUnit.Percent)
                {
                    Query += " PERCENT";
                }
                Query += " ";
            }

            // Output column names
            if (_selectedColumns.Count == 0)
            {
                if (_selectedTables.Count == 1)
                    Query += _selectedTables[0] + "."; // By default only select * from the table that was selected. If there are any joins, it is the responsibility of the user to select the needed columns.

                Query += "*";
            }
            else
            {
                foreach (string ColumnName in _selectedColumns)
                {
                    Query += ColumnName + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
                Query += ' ';
            }
            // Output table names
            if (_selectedTables.Count > 0)
            {
                Query += " FROM ";
                foreach (string TableName in _selectedTables)
                {
                    Query += TableName + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
                Query += ' ';
            }

            // Output joins
            if (_joins.Count > 0)
            {
                foreach (JoinClause Clause in _joins)
                {
                    string JoinString = "";
                    switch (Clause.JoinType)
                    {
                        case JoinType.InnerJoin: JoinString = "INNER JOIN"; break;
                        case JoinType.OuterJoin: JoinString = "OUTER JOIN"; break;
                        case JoinType.LeftJoin: JoinString = "LEFT JOIN"; break;
                        case JoinType.RightJoin: JoinString = "RIGHT JOIN"; break;
                    }
                    JoinString += " " + Clause.ToTable + " ON ";
                    JoinString += WhereStatement.CreateComparisonClause(Clause.FromTable + '.' + Clause.FromColumn, Clause.ComparisonOperator, new SqlLiteral(Clause.ToTable + '.' + Clause.ToColumn));
                    Query += JoinString + ' ';
                }
            }

            // Output where statement
            if (_whereStatement.ClauseLevels > 0)
            {
                Query += " WHERE " + _whereStatement.BuildWhereStatement();
            }

            // Output GroupBy statement
            if (_groupByColumns.Count > 0)
            {
                Query += " GROUP BY ";
                foreach (string Column in _groupByColumns)
                {
                    Query += Column + ',';
                }
                Query = Query.TrimEnd(',');
                Query += ' ';
            }

            // Output having statement
            if (_havingStatement.ClauseLevels > 0)
            {
                // Check if a Group By Clause was set
                if (_groupByColumns.Count == 0)
                {
                    throw new Exception("Having statement was set without Group By");
                }
                Query += " HAVING " + _havingStatement.BuildWhereStatement();
            }

            // Output OrderBy statement
            if (_orderByStatement.Count > 0)
            {
                Query += " ORDER BY ";
                foreach (OrderByClause Clause in _orderByStatement)
                {
                    string OrderByClause = "";
                    switch (Clause.SortOrder)
                    {
                        case Sorting.Ascending:
                            OrderByClause = Clause.FieldName + " ASC"; break;
                        case Sorting.Descending:
                            OrderByClause = Clause.FieldName + " DESC"; break;
                    }
                    Query += OrderByClause + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last AND inserted by foreach loop
                Query += ' ';
            }
            return Query;
        }
    }

}
