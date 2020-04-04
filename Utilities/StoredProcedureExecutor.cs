using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApp.Models.ResultsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SampleApp.Utilities
{
    public class StoredProcedureExecutor : DbContext
    {
        private DbContext dbContext;
        public StoredProcedureExecutor(DbContext context)
        {
            dbContext = context;
        }

        public T exceProcedure<T>(List<SqlParameter> _params, Type spName) where T : new()
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    command.CommandText = spName.Name;
                    command.CommandType = CommandType.StoredProcedure;
                    if (_params.Count > 0)
                        command.Parameters.AddRange(_params.ToArray());
                    dbContext.Database.OpenConnection();
                    var dataReader = command.ExecuteReader();

                    if (dataReader.RecordsAffected > 0)
                    {
                        // if recordsAffected = 0 means no rows 
                        // 1 means insert or update or delete performed
                        // -1 meansselect performed
                        return (T)Convert.ChangeType(dataReader.RecordsAffected, typeof(T));
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dataReader);
                        var results = dt.ToList<T>();
                        
                        return (T)Convert.ChangeType(results, typeof(T));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static Type ListOfWhat<T>()
        {
            return typeof(T);
        }
    }
}
