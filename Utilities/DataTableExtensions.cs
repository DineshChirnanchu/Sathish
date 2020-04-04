using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SampleApp.Models;
using SampleApp.Validations;

namespace SampleApp.Utilities
{
    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = (typeof(T).GenericTypeArguments.FirstOrDefault()).GetProperties().ToList();
            IList<T> result = new List<T>();
            bool validationFlag = false;
            bool resCountMatch = false;
            bool resNamesMactch = false;
            bool resTypeMactch = false;

            foreach (DataRow row in table.Rows)
            {
                if (!validationFlag)
                {
                    var vri = new ValidateResultInfo();
                    resCountMatch = vri.IsFiledCountMatched(row, properties);
                    resNamesMactch = vri.IsFiledNamesMatched(row, properties);
                    resTypeMactch = vri.IsFiledTypesMatched(row, properties);
                    validationFlag = true;
                }

                if (resCountMatch && resNamesMactch && resTypeMactch)
                {
                    var item = CreateItemFromRow<T>(row, properties);
                    result.Add(item);
                }

            }
            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            dynamic item = Activator.CreateInstance(typeof(T).GenericTypeArguments.FirstOrDefault());
            //T item = new T();
            foreach (var property in properties)
            {
                property.SetValue(item, row[property.Name], null);
            }
            return item;
        }

        //private static object CreateItemFromRow(DataRow row, IList<PropertyInfo> properties, Type t)
        //{
        //    object item = Activator.CreateInstance(t);
        //    foreach (var property in properties)
        //    {
        //        property.SetValue(item, row[property.Name], null);
        //    }
        //    return item;
        //}

    //    if (typeof(T).GetGenericTypeDefinition() == typeof(List<>))
    //        {
    //            ty = typeof(T).GetType().GenericTypeArguments.FirstOrDefault();
    //            properties = ty.GetProperties().ToList();
    //}

    //public static IList<object> ToList(DataTable table, Type t)
    //{
    //    IList<PropertyInfo> properties = t.GetProperties().ToList();
    //    IList<object> result = new List<object>();
    //    bool validationFlag = false;
    //    bool resCountMatch = false;
    //    bool resNamesMactch = false;
    //    bool resTypeMactch = false;

    //    foreach (DataRow row in table.Rows)
    //    {
    //        if (!validationFlag)
    //        {
    //            var vri = new ValidateResultInfo();
    //            resCountMatch = vri.IsFiledCountMatched(row, properties);
    //            resNamesMactch = vri.IsFiledNamesMatched(row, properties);
    //            resTypeMactch = vri.IsFiledTypesMatched(row, properties);
    //            validationFlag = true;
    //        }

    //        if (resCountMatch && resNamesMactch && resTypeMactch)
    //        {
    //            var item = CreateItemFromRow(row, properties, t);
    //            result.Add(item);
    //        }

    //    }
    //    return result;
    //}
}
}
