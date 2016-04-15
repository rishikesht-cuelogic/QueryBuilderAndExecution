using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{

    public class MSSQLRelationship : IdbRelationship
    {
        public Relation GetRelationInfo(string fromTableName, string toTableName)
        {
            Relation relation = new Relation();
            Server myServer = new Server(@"TEST-PC");
            if (myServer.ConnectionContext.IsOpen)
                myServer.ConnectionContext.Disconnect();

            #region Authentication
            //Using windows authentication
            //myServer.ConnectionContext.LoginSecure = true;
            //myServer.ConnectionContext.Connect();

            //Using SQL Server authentication
            myServer.ConnectionContext.LoginSecure = false;
            myServer.ConnectionContext.Login = "sa";
            myServer.ConnectionContext.Password = "pa$$word";
            Database db = myServer.Databases["kantarPractice"];
            #endregion

            Table tb = db.Tables[fromTableName];
            if (tb == null)
                return null;

            foreach (ForeignKey f in tb.ForeignKeys)
            {
                if (f != null && f.Columns.Count > 0)
                {
                    var referencedColumnName = f.Columns[0].Name;
                    var referencedTableName = f.ReferencedTable;
                    if (referencedTableName == toTableName)
                    {
                        var table = db.Tables[referencedTableName];
                        if (table != null)
                        {
                            var totalColumns = table.Columns.Count;
                            for (var i = 0; i < totalColumns; i++)
                            {
                                if (table.Columns[i].InPrimaryKey)
                                {
                                    var primaryKeyName = table.Columns[i].Name;
                                    relation.FromTableName = fromTableName;
                                    relation.ToTableName = toTableName;
                                    relation.FromColumnName = referencedColumnName;
                                    relation.ToColumnName = primaryKeyName;
                                    return relation;
                                }
                            }
                        }
                    }
                }
            }


            return null;
        }

    }
}
