using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    /// <summary>
    /// It provides services which gives relationship between two tables.
    /// </summary>
    public interface IdbRelationship
    {
        Relation GetRelationInfo(string fromTableName, string toTableName);
    }
}
