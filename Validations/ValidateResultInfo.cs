using SampleApp.Models.ResultsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SampleApp.Validations
{
    public class ValidateResultInfo
    {
        public bool IsFiledCountMatched(DataRow dr, IList<PropertyInfo> pi)
        {
            if (Convert.ToInt32(dr.Table.Columns.Count) == pi.Count)
                return true;

            return false;
        }

        public bool IsFiledNamesMatched(DataRow dr, IList<PropertyInfo> pi)
        {
            bool res = false;
            foreach (var column in dr.Table.Columns)
            {
                foreach (PropertyInfo propInfo in pi)
                {
                    if (column.ToString() == propInfo.Name)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                }
            }
            return res;
        }

        public bool IsFiledTypesMatched(DataRow dr, IList<PropertyInfo> pi)
        {
            bool res = false;
            for(int i = 0; i < dr.Table.Columns.Count; i++)
            {
                foreach (PropertyInfo propInfo in pi)
                {
                    if (dr.Table.Columns[i].DataType == propInfo.PropertyType)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                }
            }
            return res;
        }
    }
}
