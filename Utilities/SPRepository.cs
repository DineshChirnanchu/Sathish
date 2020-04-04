using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SampleApp.Utilities
{
    public class SpRepository 
    {
        private StoredProcedureExecutor _spexecutor;
        public SpRepository(StoredProcedureExecutor spexecutor)
        {
            _spexecutor = spexecutor;
        }

        public T CreateStoreProcedureModel<T>(Object obj) where T : new()
        {
            string typename = obj.GetType().Name.ToString();
            if (typename != null)  //type should be varified on checking the list of models wihtin the assembly
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties().ToArray();
                var _params = new List<SqlParameter>();
                foreach (PropertyInfo propinfo in props)
                {
                    var param = new SqlParameter()
                    {
                        ParameterName = propinfo.Name,
                        SqlValue = propinfo.GetValue(obj)
                    };
                    _params.Add(param);
                }
              return (T)Convert.ChangeType(_spexecutor.exceProcedure<T>(_params, type), typeof(T));
            }
            else
            {
                return (T)Convert.ChangeType("no valid response", typeof(T));
            }
        }
    }
}
