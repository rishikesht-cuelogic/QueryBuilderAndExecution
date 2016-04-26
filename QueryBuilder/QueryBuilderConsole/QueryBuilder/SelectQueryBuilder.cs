using System;
using System.Collections.Generic;
using QueryBuilder.Enums;

namespace QueryBuilder
{
    /// <summary>
    /// It is used to create SELECT query.
    /// </summary>
    public class SelectQueryBuilder : IQueryBuilder
    {
        protected bool distinct = false;
        protected TopClause topClause = new TopClause(100, TopUnit.Percent);
        protected List<string> selectedColumns = new List<string>();	// array of string
        protected List<string> selectedTables = new List<string>();	// array of string
        protected List<JoinClause> joins = new List<JoinClause>();	// array of JoinClause
        protected WhereStatement whereStatement = new WhereStatement();
        protected List<OrderByClause> orderByStatement = new List<OrderByClause>();	// array of OrderByClause
        protected List<string> groupByColumns = new List<string>();		// array of string
        protected WhereStatement havingStatement = new WhereStatement();
        protected SelectQueryBuilder existSubQuery;
        protected string existQueryText=Constants.Exists;
        protected IdbRelationship dbRelationship;
        protected List<AggregateFunction> aggregateFunctions = new List<AggregateFunction>();

        internal WhereStatement WhereStatement
        {
            get { return whereStatement; }
            set { whereStatement = value; }
        }

        public SelectQueryBuilder()
        {
        }
        /// <summary>
        /// Parameterized constructor which accepts IdbRelationship 
        /// </summary>
        /// <param name="dbRelationship">Auto detection of foreign key and primary key</param>
        public SelectQueryBuilder(IdbRelationship dbRelationship)
        {
            this.dbRelationship = dbRelationship;
        }
        /// <summary>
        /// Get or Set Distinct
        /// </summary>
        public bool Distinct
        {
            get { return distinct; }
            set { distinct = value; }
        }
        /// <summary>
        /// Get or Set TopRecords
        /// </summary>
        public int TopRecords
        {
            get { return topClause.Quantity; }
            set
            {
                topClause.Quantity = value;
                topClause.Unit = TopUnit.Records;
            }
        }
        /// <summary>
        /// Get or Set Top Cluase
        /// </summary>
        public TopClause TopClause
        {
            get { return topClause; }
            set { topClause = value; }
        }

        /// <summary>
        /// Return array of selected columns. If columns are not selected, It returns single element array with value *
        /// </summary>
        public string[] SelectedColumns
        {
            get
            {
                if (selectedColumns.Count > 0)
                    return selectedColumns.ToArray();
                else
                    return new string[1] { "*" };
            }
        }
        /// <summary>
        /// Returns array of selected tables
        /// </summary>
        public string[] SelectedTables
        {
            get { return selectedTables.ToArray(); }
        }
        /// <summary>
        /// It selects all columns
        /// </summary>
        public void SelectAllColumns()
        {
            selectedColumns.Clear();
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
            selectedColumns.Clear();
            selectedColumns.Add(column);
        }
        /// <summary>
        /// It selects multiple columns
        /// </summary>
        /// <param name="columns">It is array of selectable column's names</param>
        public void SelectColumns(params string[] columns)
        {
            selectedColumns.Clear();
            foreach (string column in columns)
            {
                selectedColumns.Add(column);
            }
        }
        /// <summary>
        /// It selects table
        /// </summary>
        /// <param name="table">It is table name</param>
        public void SelectFromTable(string table)
        {
            selectedTables.Clear();
            selectedTables.Add(table);
        }
        /// <summary>
        /// It selects multiple tables
        /// </summary>
        /// <param name="tables">It is array of tables names</param>
        public void SelectFromTables(params string[] tables)
        {
            selectedTables.Clear();
            foreach (string Table in tables)
            {
                selectedTables.Add(Table);
            }
        }
        /// <summary>
        /// Add new join using JoinClause
        /// </summary>
        /// <param name="newJoin">It is join clause</param>
        public void AddJoin(JoinClause newJoin)
        {
            joins.Add(newJoin);
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
        public void AddJoin(JoinType join, string toTableName, string toColumnName, string fromTableName, string fromColumnName, Comparison @operator = Comparison.Equals)
        {
            JoinClause NewJoin = new JoinClause(join, toTableName, toColumnName, @operator, fromTableName, fromColumnName);
            joins.Add(NewJoin);
        }
        /// <summary>
        ///  Add new join using two table names. It will auto detect referenced column name and primary key column name
        /// </summary>
        /// <param name="join">It is Type is join</param>
        /// <param name="toTableName">It Reference table name i.e. Parent table name</param>
        /// <param name="fromTableName">It is child table name</param>
        /// <param name="operator">It is comparison operator. Default is equality</param>
        public void AddJoin(JoinType join, string toTableName, string fromTableName, Comparison @operator = Comparison.Equals)
        {
            if (dbRelationship == null)
                throw new NullReferenceException("dbRelationship is not initialize. Please use parameterized constructor of SelectQueryBuilder to initialize dbRelationship");

            var relation = dbRelationship.GetRelationInfo(fromTableName, toTableName);
            JoinClause NewJoin = new JoinClause(join, toTableName, relation.ToColumnName, @operator, fromTableName, relation.FromColumnName);
            joins.Add(NewJoin);
        }
        /// <summary>
        /// Get or Set WhereStatement
        /// </summary>
        public WhereStatement Where
        {
            get { return whereStatement; }
            set { whereStatement = value; }
        }
        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="clause">It is object where clause</param>
        public void AddWhere(WhereClause clause)
        {
            AddWhere(clause, 1);
        }
        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="clause">It is object of where clause</param>
        /// <param name="level">It is level. It applies AND operation for all where clause in same level and applies OR operation for different level </param>
        public void AddWhere(WhereClause clause, int level)
        {
            whereStatement.Add(clause, level);
        }
        /// <summary>
        /// It adds where condition.
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is filter value</param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue)
        {
            return AddWhere(field, @operator, compareValue, 1);
        }
        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is filter value</param>
        /// <returns></returns>
        public WhereClause AddWhere(Enum field, Comparison @operator, object compareValue)
        {
            return AddWhere(field.ToString(), @operator, compareValue, 1);
        }

        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is filter value</param>
        /// <param name="level">It is level. It applies AND operation for all where clause in same level and applies OR operation for different level </param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="selectQueryBuilder">It is used for subquery</param>
        public WhereClause AddWhere(string field, Comparison @operator, SelectQueryBuilder selectQueryBuilderl)
        {
            return AddWhere(field, @operator, selectQueryBuilderl, 1);
        }

        /// <summary>
        /// It adds where condition
        /// </summary>
        /// <param name="field">It is column name which you want to filter</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="selectQueryBuilder">It is used for subquery</param>
        /// <param name="level">It is level. It applies AND operation for all where clause in same level and applies OR operation for different level </param>
        /// <returns></returns>
        public WhereClause AddWhere(string field, Comparison @operator, SelectQueryBuilder selectQueryBuilder, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, selectQueryBuilder);
            whereStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }

        /// <summary>
        /// It is used to sort the data
        /// </summary>
        /// <param name="clause">Clause have field on which you want to sort it and sort type. i.e. Ascending or Descending. Default is Ascending sort</param>
        public void AddOrderBy(OrderByClause clause)
        {
            orderByStatement.Add(clause);
        }
        /// <summary>
        /// It is used to sort the data
        /// </summary>
        /// <param name="field">It is a column name of type enum on which you want to sort it</param>
        /// <param name="order">It is type of sort i.e. Ascending or Descending. Default is Ascending sort</param>
        public void AddOrderBy(Enum field, Sorting order = Sorting.Ascending) { this.AddOrderBy(field.ToString(), order); }
        /// <summary>
        /// It is used to sort the data
        /// </summary>
        /// <param name="field">It is column name on which you want to sort data</param>
        /// <param name="order">It is type of sort i.e. Ascending or Descending. Default is Ascending sort</param>
        public void AddOrderBy(string field, Sorting order = Sorting.Ascending)
        {
            OrderByClause NewOrderByClause = new OrderByClause(field, order);
            orderByStatement.Add(NewOrderByClause);
        }
        /// <summary>
        /// It is used to Group by clause
        /// </summary>
        /// <param name="columns">It is array of column names by which you want to group it</param>
        public void GroupBy(params string[] columns)
        {
            foreach (string Column in columns)
            {
                groupByColumns.Add(Column);
            }
        }
        /// <summary>
        /// It is property of Having by clause
        /// </summary>
        public WhereStatement Having
        {
            get { return havingStatement; }
            set { havingStatement = value; }
        }
        /// <summary>
        /// It is used to having by clause which is used in Group By clause
        /// </summary>
        /// <param name="clause">It is having clause which is similar to where clause. Default level is 1. </param>
        public void AddHaving(WhereClause clause) { AddHaving(clause, 1); }
        /// <summary>
        /// It is used to having by clause which is used in Group By clause
        /// </summary>
        /// <param name="clause">It is having clause which is similar to where clause.</param>
        /// <param name="level">It is having by level </param>
        public void AddHaving(WhereClause clause, int level)
        {
            havingStatement.Add(clause, level);
        }
        /// <summary>
        /// It is used to having by clause which is used in Group By clause
        /// </summary>
        /// <param name="field">It is a column name on which you want to filter group</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is value used for filter the group</param>
        /// <returns>It returns WhereClause which is same as Having clause</returns>
        public WhereClause AddHaving(string field, Comparison @operator, object compareValue) { return AddHaving(field, @operator, compareValue, 1); }
        /// <summary>
        /// It is used to having by clause which is used in Group By clause
        /// </summary>
        /// <param name="field">It is a column name of type enum on which you want to filter group</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is value used for filter the group</param>
        /// <returns>It returns WhereClause which is same as Having clause</returns>
        public WhereClause AddHaving(Enum field, Comparison @operator, object compareValue) { return AddHaving(field.ToString(), @operator, compareValue, 1); }
        /// <summary>
        /// It is used to having by clause which is used in Group By clause
        /// </summary>
        /// <param name="field">It is a column name of type enum on which you want to filter group</param>
        /// <param name="operator">It is comparison operator</param>
        /// <param name="compareValue">It is value used for filter the group</param>
        /// <param name="level">It is level of having by clause</param>
        /// <returns>It returns WhereClause which is same as Having clause</returns>
        public WhereClause AddHaving(string field, Comparison @operator, object compareValue, int level)
        {
            WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
            havingStatement.Add(NewWhereClause, level);
            return NewWhereClause;
        }
        /// <summary>
        /// It is used to add EXISTS or NOT EXISTS clause. Default is EXISTS Clause
        /// </summary>
        /// <param name="subQuery">It is a subquery of EXISTS or NOT EXISTS clause</param>
        /// <param name="notExist">For NOT EXISTS Clause, Set this as true</param>
        public void AddExist(SelectQueryBuilder subQuery, bool notExist = false)
        {
            existSubQuery = subQuery;
            if (notExist)
                existQueryText = Constants.Not + " " + existQueryText;
        }

        public void AddAggregate(Aggregate aggregate,string columnName)
        {
            Validate.ColumnName(columnName);
            aggregateFunctions.Add(new AggregateFunction(aggregate, columnName));            
        }

        /// <summary>
        /// It builds the query and returns string
        /// </summary>
        /// <returns></returns>
        public string BuildQuery()
        {
            return GetQuery();
        }

        

        /// <summary>
        /// Builds the select query
        /// </summary>
        /// <returns>Returns a string containing the query, or a DbCommand containing a command with parameters</returns>
        private string GetQuery()
        {
            if (selectedTables == null || selectedTables.Count == 0)
                throw new Exception("At least one table name must be specified");

            if (topClause.Quantity < 0)
                throw new IndexOutOfRangeException("value of top clause should be greater than 0");

            if ((topClause.Unit == TopUnit.Percent) && (topClause.Quantity > 100 || topClause.Quantity < 0))
                throw new IndexOutOfRangeException("percentage should be less than 100 and greater than 0");

            string Query = Constants.Select + " ";

            // Output Distinct
            if (distinct)
            {
                Query += Constants.Distinct + " ";
            }

            // Output Top clause; Skip If it is 100 percent;
            if (!(topClause.Quantity == 100 & topClause.Unit == TopUnit.Percent))
            {
                Query += Constants.Top + " " + topClause.Quantity;
                if (topClause.Unit == TopUnit.Percent)
                {
                    Query += " " + Constants.Percent;
                }
                Query += " ";
            }

            if(aggregateFunctions.Count != 0)
            {
                Query = Query+" " + SqlUtility.GetQuery(aggregateFunctions);
            }

            // Output column names
            if (selectedColumns.Count == 0 && aggregateFunctions.Count==0)
            {
                //if (selectedTables.Count == 1)
                //    Query += selectedTables[0] + "."; // By default only select * from the table that was selected. If there are any joins, it is the responsibility of the user to select the needed columns.

                Query += "*";
            }
            else
            {
                foreach (string ColumnName in selectedColumns)
                {
                    if (aggregateFunctions.Count != 0)
                    {
                        Query = Query + ",";
                    }

                    Query += ColumnName + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
                //Query += ' ';
            }
            // Output table names
            if (selectedTables.Count > 0)
            {
                Query += Constants.From + " ";
                foreach (string TableName in selectedTables)
                {
                    Query += TableName + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
                Query += ' ';
            }

            // Output joins
            if (joins.Count > 0)
            {
                foreach (JoinClause Clause in joins)
                {
                    string JoinString = "";
                    switch (Clause.JoinType)
                    {
                        case JoinType.InnerJoin: JoinString = Constants.InnerJoin; break;
                        case JoinType.OuterJoin: JoinString = Constants.OuterJoin; break;
                        case JoinType.LeftJoin: JoinString = Constants.LeftJoin; break;
                        case JoinType.RightJoin: JoinString = Constants.RightJoin; break;
                    }
                    JoinString += " " + Clause.ToTable + " " + Constants.On + " ";
                    JoinString += WhereStatement.CreateComparisonClause(Clause.FromTable + '.' + Clause.FromColumn, Clause.ComparisonOperator, new SqlLiteral(Clause.ToTable + '.' + Clause.ToColumn));
                    Query += JoinString + ' ';
                }
            }

            // Output where statement
            if (whereStatement.ClauseLevels > 0)
            {
                Query += " " + Constants.Where + " " + whereStatement.BuildWhereStatement();
            }

            // Output GroupBy statement
            if (groupByColumns.Count > 0)
            {
                Query += " " + Constants.GroupBy + " ";
                foreach (string Column in groupByColumns)
                {
                    Query += Column + ',';
                }
                Query = Query.TrimEnd(',');
                Query += ' ';
            }

            // Output having statement
            if (havingStatement.ClauseLevels > 0)
            {
                // Check if a Group By Clause was set
                if (groupByColumns.Count == 0)
                {
                    throw new Exception("Having statement was set without Group By");
                }
                Query += " " + Constants.Having + " " + havingStatement.BuildWhereStatement();
            }

            // Output OrderBy statement
            if (orderByStatement.Count > 0)
            {
                Query += " " + Constants.OrderBy + " ";
                foreach (OrderByClause Clause in orderByStatement)
                {
                    string OrderByClause = "";
                    switch (Clause.SortOrder)
                    {
                        case Sorting.Ascending:
                            OrderByClause = Clause.FieldName + " " + Constants.Ascending; break;
                        case Sorting.Descending:
                            OrderByClause = Clause.FieldName + " " + Constants.Descending; break;
                    }
                    Query += OrderByClause + ',';
                }
                Query = Query.TrimEnd(','); // Trim de last AND inserted by foreach loop
                Query += ' ';
            }

            if (existSubQuery != null)
            {
                Query += Constants.Where+" "+existQueryText+" ("+existSubQuery.BuildQuery()+")";
            }

            Query = Query.Trim();
            return Utility.RemoveMultipleSpace(Query);
        }

    }

}
