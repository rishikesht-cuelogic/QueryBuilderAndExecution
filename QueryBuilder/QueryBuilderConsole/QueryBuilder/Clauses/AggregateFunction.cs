using QueryBuilder.Enums;

namespace QueryBuilder
{
    /// <summary>
    /// Represents a TOP clause for SELECT statements
    /// </summary>
    public struct AggregateFunction
    {
        public string ColumnName;
        public Aggregate Aggregate;
        public AggregateFunction(Aggregate aggregate,string columnName)
        {
            ColumnName = columnName;
            Aggregate = aggregate;
        }
        public string CreateAggreateFunction()
        {
            return SqlUtility.GetAggregateFunction(Aggregate) + "(" + ColumnName + ")";
        }
    }
}
