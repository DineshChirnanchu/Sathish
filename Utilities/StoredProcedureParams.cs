using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace SampleApp.Utilities
{
    public class StoredProcedureParams
    {
        public string Name { get; set; }
        public List<SqlParameter> Params { get; set; }

    }
}
