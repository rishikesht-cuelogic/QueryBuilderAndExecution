namespace QueryBuilder
{
    internal static class Constants
    {
        #region Select
        public static string Select = "SELECT";
        public static string Distinct = "DISTINCT";
        public static string Top = "TOP";
        public static string Percent = "PERCENT";
        public static string From = " FROM";
        #endregion

        #region Insert
        public static string Insert = "INSERT";
        public static string Into = "INTO";
        public static string Values = "Values";

        #endregion

        #region Update
        public static string Update = "Update";
        public static string Set = "SET";
        #endregion

        #region Delete
        public static string Delete = "DELETE";       
        #endregion

        #region Joins
        public static string InnerJoin = "INNER JOIN";
        public static string FullJoin = "FULL JOIN";
        public static string LeftJoin = "LEFT JOIN";
        public static string RightJoin = "RIGHT JOIN";
        public static string CrossJoin = "CROSS JOIN";
        public static string On = "ON";
        #endregion

        #region Order By
        public static string OrderBy = "ORDER BY";
        public static string Ascending = "ASC";
        public static string Descending = "DESC";
        #endregion

        #region Where

        #region Logical Operators
        public static string And = "AND";
        public static string Or = "OR";
        #endregion

        #region Conditional Operators
        public static string EqualTo = "=";
        public static string NotEqualTo = "<>";
        public static string GreaterThan = ">";
        public static string LessThan = "<";
        public static string GreaterThanEqualTo = ">=";
        public static string LessThanEqualTo = "<=";
        #endregion

        public static string Like = "LIKE";
        public static string Not = "NOT";
        public static string In = "IN";
        public static string IsNull = "IS NULL";
        public static string Null = "NULL";
        public static string Where = "WHERE";
        public static string Exists = "EXISTS";
        #endregion

        #region Aggregate Functions
        public static string Avg = "AVG";
        public static string Count = "COUNT";
        public static string Sum = "SUM";
        public static string Min = "MIN";
        public static string Max = "MAX";
        #endregion

        #region Group By
        public static string GroupBy = "GROUP BY";
        public static string Having = "HAVING";
        #endregion
    }
}
