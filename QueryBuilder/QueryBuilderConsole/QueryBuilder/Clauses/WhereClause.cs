using System.Collections.Generic;
using QueryBuilder.Enums;

namespace QueryBuilder
{
    /// <summary>
    /// Represents a WHERE clause on 1 database column, containing 1 or more comparisons on 
    /// that column, chained together by logic operators: eg (UserID=1 or UserID=2 or UserID>100)
    /// This can be achieved by doing this:
    /// WhereClause myWhereClause = new WhereClause("UserID", Comparison.Equals, 1);
    /// myWhereClause.AddClause(LogicOperator.Or, Comparison.Equals, 2);
    /// myWhereClause.AddClause(LogicOperator.Or, Comparison.GreaterThan, 100);
    /// </summary>
    public struct WhereClause
    {
        private string m_FieldName;
        private Comparison m_ComparisonOperator;
        private object m_Value;
        private SelectQueryBuilder m_SelectQueryBuilder;
        /// <summary>
        /// SubCluase used for logical operator i.e. AND or OR. It create multiple filters
        /// </summary>
        internal struct SubClause
        {
            public LogicOperator LogicOperator;
            public Comparison ComparisonOperator;
            public object Value;
            public SubClause(LogicOperator logic, Comparison compareOperator, object compareValue)
            {
                LogicOperator = logic;
                ComparisonOperator = compareOperator;
                Value = compareValue;
            }
        }
        internal List<SubClause> SubClauses;	// Array of SubClause

        /// <summary>
        /// Gets/sets the name of the database column this WHERE clause should operate on
        /// </summary>
        public string FieldName
        {
            get { return m_FieldName; }
            set { m_FieldName = value; }
        }

        /// <summary>
        /// Gets/sets the comparison method
        /// </summary>
        public Comparison ComparisonOperator
        {
            get { return m_ComparisonOperator; }
            set { m_ComparisonOperator = value; }
        }

        /// <summary>
        /// Gets/sets the value that was set for comparison
        /// </summary>
        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public SelectQueryBuilder SelectQueryBuilder
        {
            get { return m_SelectQueryBuilder; }
            set { m_SelectQueryBuilder = value; }
        }

        public WhereClause(string field, Comparison firstCompareOperator, SelectQueryBuilder selectQueryBuilder)
        {
            m_FieldName = field;
            m_ComparisonOperator = firstCompareOperator;
            SubClauses = new List<SubClause>();
            m_Value = null;
            m_SelectQueryBuilder = selectQueryBuilder;
        }
        public WhereClause(string field, Comparison firstCompareOperator, object firstCompareValue)
        {
            Validate.ColumnName(field);
            if (firstCompareOperator != Comparison.In)
                Validate.SqlValue(firstCompareValue);

            m_FieldName = field;
            m_ComparisonOperator = firstCompareOperator;
            m_Value = firstCompareValue;
            SubClauses = new List<SubClause>();
            m_SelectQueryBuilder = null;
        }
        public void AddClause(LogicOperator logic, Comparison compareOperator, object compareValue)
        {
            SubClause NewSubClause = new SubClause(logic, compareOperator, compareValue);
            SubClauses.Add(NewSubClause);
        }
    }

}
