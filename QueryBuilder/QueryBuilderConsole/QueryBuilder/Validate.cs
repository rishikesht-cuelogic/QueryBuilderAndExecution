using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    internal static class Validate
    {
        private static void CheckNull(string name,string message)
        {
            if (name == null)
                throw new ArgumentNullException(message);
        }

        private static void CheckEmptyOrWhiteSpace(string name, string message)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(message);
        }

        public static bool TableName(string tableName)
        {
            CheckNull(tableName, "Table name must be not null");
            CheckEmptyOrWhiteSpace(tableName, "Table name must be non empty");

            return true;
        }

        public static bool ColumnName(string columnName)
        {
            CheckNull(columnName, "Column name name must be not null");
            CheckEmptyOrWhiteSpace(columnName, "Column name must be non empty");

            return true;
        }

        public static bool SqlValue(object value)
        {
            if (!Utility.IsValidSqlValue(value))
                throw new ArgumentException("Invalid value");

            return true;
        }
    }
}
