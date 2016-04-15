using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace PortableDataAccessLayer
{
    public class DataAccessLayer : IDataAccess
    {
        private DBHelper _dbHelper = null;
        public DataAccessLayer(Connection objConnection)
        {
            Configuration.ConnectionString = objConnection.ConnectionString;
            Configuration.ProviderName = objConnection.ProviderName;
            _dbHelper = new DBHelper();
        }
        public object GetDataFromDatabase(string sqlSelectCommond, string[] TableNames = null, ProcReturnType ProcReturnType = ProcReturnType.JSON)
        {
            if (!string.IsNullOrEmpty(sqlSelectCommond.Trim()))
            {
                try
                {
                    DataSet _Ds = _dbHelper.ExecuteDataSet(sqlSelectCommond);
                    if (_Ds != null)
                    {
                        if (TableNames != null)
                        {
                            if (TableNames.Length.Equals(_Ds.Tables.Count))
                            {
                                short TableCounter = 0;

                                foreach (DataTable table in _Ds.Tables)
                                {
                                    table.TableName = TableNames[TableCounter++];
                                }
                            }
                        }
                        if (ProcReturnType.DataSet == ProcReturnType)
                            return _Ds;
                        if (ProcReturnType.JSON == ProcReturnType)
                        {
                            return JsonConvert.SerializeObject(new ReturnData
                            {
                                Status = Status.Success,
                                Message = StatusMessage.InvalidInput,
                                Data = _Ds
                            });

                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ProcReturnType.DataSet == ProcReturnType)
                    {
                        throw ex;
                    }
                    if (ProcReturnType.JSON == ProcReturnType)
                    {
                        return JsonConvert.SerializeObject(new ReturnData
                        {
                            Status = Status.Failure,
                            Message = ex.Message,
                            Data = null
                        });

                    }
                }
            }
            return JsonConvert.SerializeObject(new ReturnData
            {
                Status = Status.Failure,
                Message = StatusMessage.InvalidInput,
                Data = null
            });
        }

        public int SaveDataToDatabase(string sqlInsertCommond)
        {
            int retValue = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            if (string.IsNullOrEmpty(sqlInsertCommond.Trim()))
            {
                IDbTransaction transaction = _dbHelper.BeginTransaction();


                try
                {
                    IDataReader objScalar = _dbHelper.ExecuteDataReader(sqlInsertCommond, paramCollection, transaction, CommandType.Text);

                    if (objScalar != null)
                    {
                        retValue = objScalar.RecordsAffected;
                        objScalar.Close();
                        objScalar.Dispose();
                    }

                    _dbHelper.CommitTransaction(transaction);
                }
                catch (Exception err)
                {
                    _dbHelper.RollbackTransaction(transaction);
                    throw err;
                }

            }
            return retValue;
        }
    }
}
