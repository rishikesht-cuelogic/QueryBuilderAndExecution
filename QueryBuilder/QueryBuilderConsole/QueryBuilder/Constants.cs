namespace QueryBuilder
{
    internal static class Constants
    {
        public static string Select = "SELECT";
        public static string Distinct = "DISTINCT";
        public static string Top = "TOP";
        public static string Percent = "PERCENT";
        public static string From = " FROM";
        public static string InnerJoin = "INNER JOIN";
        public static string OuterJoin = "OUTER JOIN";
        public static string LeftJoin = "LEFT JOIN";
        public static string RightJoin = "RIGHT JOIN";
        public static string On = "ON";
        public static string Where = "WHERE";
        public static string GroupBy = "GROUP BY";
        public static string Having = "HAVING";
        public static string OrderBy = "ORDER BY";
        public static string Ascending = "ASC";
        public static string Descending = "DESC";
        public static string And = "AND";
        public static string Or = "OR";
        public static string Like = "LIKE";
        public static string Not = "NOT";
        public static string In = "IN";
        public static string EqualTo = "=";
        public static string NotEqualTo = "<>";
        public static string GreaterThan = ">";
        public static string LessThan = "<";
        public static string GreaterThanEqualTo = ">=";
        public static string LessThanEqualTo = "<=";
        public static string IsNull = "IS NULL";
        public static string Null = "NULL";
        public static string Delete = "DELETE";
        public static string Insert = "INSERT";
        public static string Into = "INTO";
        public static string Values = "Values";
        public static string Update = "Update";
        public static string Set = "SET";
        public static string Exists = "EXISTS";

        #region Aggregate Functions
        public static string Avg = "AVG";
        public static string Count = "COUNT";
        public static string Sum = "SUM";
        public static string Min = "MIN";
        public static string Max = "MAX";
        #endregion  
    }
}
