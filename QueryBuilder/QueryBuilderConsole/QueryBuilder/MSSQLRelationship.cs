using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder
{
    /// <summary>
    /// It is used to auto detect relationship between two tables of SQL Server database.
    /// </summary>
    public class MSSQLRelationship : IdbRelationship
    {
        #region Properties and fields
        private Relation relation;
        private Server server;
        private Database db;
        #endregion

        #region C'tor
        /// <summary>
        /// This constructor connects database server by windows authentication
        /// </summary>
        /// <param name="serverName">This is database server name</param>
        /// <param name="databaseName">This is database name which needs to connect you</param>
        public MSSQLRelationship(string serverName, string databaseName)
        {
            server = new Server(serverName);//@"TEST-PC"
            server.ConnectionContext.LoginSecure = true;
            server.ConnectionContext.Connect();
            db = server.Databases[databaseName];//"kantarPractice"
            relation = new Relation();
        }

        /// <summary>
        /// This constructor connects database server by database server authentication
        /// </summary>
        /// <param name="serverName">This is database server name</param>
        /// <param name="userName">This is user name which needs to connect database</param>
        /// <param name="password">This is password which needs to connect database</param>
        /// <param name="databaseName">This is database name which needs to connect you</param>
        public MSSQLRelationship(string serverName, string databaseName, string userName, string password)
        {
            server = new Server(serverName);//@"TEST-PC"
            server.ConnectionContext.LoginSecure = false;
            server.ConnectionContext.Login = userName;//"sa";
            server.ConnectionContext.Password = password;//"pa$$word";
            Database db = server.Databases[databaseName];//"kantarPractice"
            relation = new Relation();
        }
        #endregion

        #region Public
        public Relation GetRelationInfo(string fromTableName, string toTableName)
        {
            if (server == null || db == null)
                throw new Exception("Server or Database is not found. Please check server name and database name.");

            if (server.ConnectionContext.IsOpen)
                server.ConnectionContext.Disconnect();

            #region Authentication
            //Using SQL Server authentication

            #endregion

            Table table = db.Tables[fromTableName];
            if (table == null)
                throw new Exception("Table not found.");

            foreach (ForeignKey f in table.ForeignKeys)
            {
                if (f != null && f.Columns.Count > 0)
                {
                    var referencedColumnName = f.Columns[0].Name;
                    var referencedTableName = f.ReferencedTable;
                    if (referencedTableName == toTableName)
                    {
                        var referencedTable = db.Tables[referencedTableName];
                        if (referencedTable != null)
                        {
                            var totalColumns = referencedTable.Columns.Count;
                            for (var i = 0; i < totalColumns; i++)
                            {
                                if (referencedTable.Columns[i].InPrimaryKey)
                                {
                                    var primaryKeyName = referencedTable.Columns[i].Name;
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

            throw new Exception("No relationship found");
        }
        #endregion
    }
}
