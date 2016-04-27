
using QueryBuilder.Enums;

namespace QueryBuilder
{
    /// <summary>
    /// Represents a JOIN clause to be used with SELECT statements
    /// </summary>
    public struct JoinClause
    {
        public JoinType JoinType;
        public string FromTable;
        public string FromColumn;
        public Comparison ComparisonOperator;
        public string ToTable;
        public string ToColumn;
        public JoinClause(
            JoinType join, 
            string toTableName, 
            string toColumnName,           
            string fromTableName, 
            string fromColumnName,
             Comparison @operator=Comparison.Equals
            )
        {
            Validate.TableName(toTableName);
            Validate.TableName(fromTableName);
            Validate.ColumnName(toColumnName);
            Validate.ColumnName(fromColumnName);

            JoinType = join;
            FromTable = fromTableName;
            FromColumn = fromColumnName;
            ComparisonOperator = @operator;
            ToTable = toTableName;
            ToColumn = toColumnName;
        }
    }

}
