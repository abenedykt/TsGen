using System;
using System.Reflection;

namespace AB.TsGen.Extensions
{
    public static class PopertyInfoExtensions
    {
        public static string AsTypeScriptType(this PropertyInfo property)
        {
            if (property.PropertyType == typeof(string))
            {
                return "string";
            }
            if (property.PropertyType == typeof(int))
            {
                return "number";
            }
            if (property.PropertyType == typeof(DateTime))
            {
                return "string";
            }
            if (property.PropertyType.IsClass)
            {
                return property.PropertyType.Name;
            }
            return "any";
        }
    }
}