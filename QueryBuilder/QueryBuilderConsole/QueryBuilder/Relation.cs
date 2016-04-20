using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public class Relation
    {
        public string FromTableName { get; set; }
        public string ToTableName { get; set; }
        public string FromColumnName { get; set; }
        public string ToColumnName { get; set; }
    }
}
