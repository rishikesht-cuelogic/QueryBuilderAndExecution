using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QueryBuilder
{
    internal static class Utility
    {
        public static string ConvertArrayToString(List<string> list,bool addBracket=false,char seperatedBy=',')
        {
            var text = string.Empty;
            if (list == null || list.Count() == 0)
                return text;

            if (addBracket)
                text = "(";
            foreach(var item in list)
            {
                text = text+item + seperatedBy;
            }
            text=text.TrimEnd(seperatedBy);
            if (addBracket)
                text = text + ")";
            return text;
        }

        public static string ConvertArrayToStringWithWrapSingleQuote(List<object> list, bool addBracket = false, char seperatedBy = ',')
        {
            var text = string.Empty;
            if (list == null || list.Count() == 0)
                return text;

            if (addBracket)
                text = "(";
            foreach (var item in list)
            {
                text = text+SqlUtility.FormatSQLValue(item)+seperatedBy;
            }
            text=text.TrimEnd(seperatedBy);
            if (addBracket)
                text = text + ")";
            return text;
        }

        public static bool IsPrimitive(object value)
        {
            return value.GetType().IsPrimitive;
        }        

        public static bool IsArray(object value)
        {
            return value.GetType().IsArray;
        }

        public static string RemoveMultipleSpace(string value)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(value, " ");
        }
        
    }
}
