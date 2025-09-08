using Microsoft.VisualBasic.FileIO;
using ObjToForm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjToForm.Utils
{
    public static class ObjectUtils
    {
        public static Dictionary<string, Type> GetAttributesDictionary(Type obj, string prefix = "", bool ModelBinding = false)
        {
            var dict = new Dictionary<string, Type>();

            string typeName =  ModelBinding ? $"{prefix}." : "";

            foreach (var property in obj.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                string name = string.IsNullOrEmpty(prefix) ? $"{typeName}{property.Name}" : $"{prefix}.{property.Name}";
                Type propertyType = property.PropertyType;

                if (propertyType.IsPrimitive || propertyType == typeof(string) || propertyType.IsValueType)
                {
                    dict[name] = propertyType;
                }
                else if (propertyType.IsClass)
                {
                    foreach (var sub in GetAttributesDictionary(propertyType, name))
                    {
                        dict[sub.Key] = sub.Value;
                    }
                }
            }

            return dict;
        }
    }
}
