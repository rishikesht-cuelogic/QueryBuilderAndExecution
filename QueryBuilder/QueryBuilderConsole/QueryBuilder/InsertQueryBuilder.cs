using QueryBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    public class InsertQueryBuilder : BaseInsertUpdateQueryBuilder, IQueryBuilder
    {
        #region C'tor
        public InsertQueryBuilder() { }
        public InsertQueryBuilder(string tableName)
        {
            this._tableName = tableName;
        }
        #endregion

        #region Public     

        public string BuildQuery()
        {
            var query = "INSERT INTO " + _tableName;
            query = query + GetColumns();
            if (_selectQueryBuilder != null)
            {
                return query + " " + _selectQueryBuilder.BuildQuery();
            }
            query = query + " VALUES ";
            query = query + GetValues();
            return query;
        }
        #endregion     

    }
}
