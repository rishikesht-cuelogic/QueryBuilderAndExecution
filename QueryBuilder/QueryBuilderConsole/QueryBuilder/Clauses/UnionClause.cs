
using QueryBuilder.Enums;

namespace QueryBuilder
{
    /// <summary>
    /// Represents a JOIN clause to be used with SELECT statements
    /// </summary>
    public struct UnionClause
    {
        public Union Union;
        SelectQueryBuilder SelectQueryBuilder;
        public UnionClause(Union union,SelectQueryBuilder selectQueryBuilder)
        {
            Union = union;
            SelectQueryBuilder = selectQueryBuilder;
        }
        public string CreateUnionClause()
        {
            var query = string.Empty;
            query = SqlUtility.GetUnion(Union)+" "+SelectQueryBuilder.BuildQuery();
            return query;
        }
    }

}
