using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public class DeleteQueryBuilder:IQueryBuilder
    {
        protected WhereStatement _whereStatement = new WhereStatement();
        public string BuildQuery()
        {
            var query = "DELETE ";

            return query;
        }
    }
}
