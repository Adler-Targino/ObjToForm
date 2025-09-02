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
        public static Dictionary<string, Type> GetAttributesDictionary(Type obj, string prefix = "")
        {
            var dict = new Dictionary<string, Type>();

            if (obj.IsAnonymousType())
            {
                //Properties
                foreach (var prop in obj.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    string name = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
                    Type propType = prop.PropertyType;                    

                    if (propType.IsPrimitive || propType == typeof(string) || propType.IsValueType)
                    {
                        dict[name] = propType;
                    }
                    else if (propType.IsClass)
                    {
                        foreach (var sub in GetAttributesDictionary(propType, name))
                        {
                            dict[sub.Key] = sub.Value;
                        }
                    }
                }
            }
            else
            {
                // Fields
                foreach (var field in obj.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    string name = string.IsNullOrEmpty(prefix) ? field.Name : $"{prefix}.{field.Name}";
                    Type fieldType = field.FieldType;

                    if (fieldType.IsPrimitive || fieldType == typeof(string) || fieldType.IsValueType)
                    {
                        dict[name] = fieldType;
                    }
                    else if (fieldType.IsClass)
                    {
                        foreach (var sub in GetAttributesDictionary(fieldType, name))
                        {
                            dict[sub.Key] = sub.Value;
                        }
                    }
                }
            }

            return dict;
        }
    }
}
