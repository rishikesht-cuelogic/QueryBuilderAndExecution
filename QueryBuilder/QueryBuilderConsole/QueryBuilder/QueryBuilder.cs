using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public abstract class QueryBuilder: IQueryBuilder
    {
        public abstract string BuildQuery();
    }
}
