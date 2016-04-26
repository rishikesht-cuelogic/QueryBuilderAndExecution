using QueryBuilder.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    internal static class SqlUtility
    {
        public static string FormatSQLValue(object someValue, bool INClause = false)
        {
            if (INClause)
            {
                if (!someValue.GetType().IsArray)
                    throw new ArgumentException("IN clause need array of values");
                if (someValue == null)
                    throw new ArgumentNullException("Values must not be null");

                var text = " (";

                IEnumerable array = someValue as IEnumerable;

                foreach (object item in array)
                {
                    if (!Utility.IsValidSqlValue(item))
                        throw new ArgumentException("Values must be primitive datatype");

                    text = text + "'" + item.ToString() + "',";
                }
                if (text == " (")
                    throw new ArgumentException("IN clause need non empty array of values");

                text = text.TrimEnd(',');
                return text + ")";
            }

            string FormattedValue = "";
            //				string StringType = Type.GetType("string").Name;
            //				string DateTimeType = Type.GetType("DateTime").Name;

            if (someValue == null)
            {
                FormattedValue = Constants.Null;
            }
            else
            {
                switch (someValue.GetType().Name)
                {
                    case "String": FormattedValue = "'" + ((string)someValue).Replace("'", "''") + "'"; break;
                    case "DateTime": FormattedValue = "'" + ((DateTime)someValue).ToString("yyyy/MM/dd hh:mm:ss") + "'"; break;
                    case "DBNull": FormattedValue = Constants.Null; break;
                    case "Boolean": FormattedValue = (bool)someValue ? "1" : "0"; break;
                    case "SqlLiteral": FormattedValue = ((SqlLiteral)someValue).Value; break;
                    default: FormattedValue = someValue.ToString(); break;
                }
            }
            return FormattedValue;
        }

        public static string GetAggregateFunction(Aggregate aggregate)
        {
            switch (aggregate)
            {
                case Aggregate.Avg: return Constants.Avg;
                case Aggregate.Sum: return Constants.Sum;
                case Aggregate.Min: return Constants.Min;
                case Aggregate.Max: return Constants.Max;
                case Aggregate.Count: return Constants.Count;
            }
            return string.Empty;
        }
        public static string GetQuery(List<AggregateFunction> aggregateFunctions)
        {
            if (aggregateFunctions == null)
                return string.Empty;

            var query = string.Empty;
            foreach(var item in aggregateFunctions)
            {
                query = query + item.CreateAggreateFunction()+",";              
            }
            query = query.Trim(',');
            return query;
        }
    }
}
