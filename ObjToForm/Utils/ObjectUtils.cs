using ObjToForm.DataTypes.Structs;
using System.Reflection;

namespace ObjToForm.Utils
{
    public static class ObjectUtils
    {
        public static List<PropertyData> GetPropertyList(Type obj, string prefix = "", bool ModelBinding = false)
        {
            var propertiesList = new List<PropertyData>();

            string typeName =  ModelBinding ? $"{prefix}." : "";

            foreach (var property in obj.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                PropertyData propertyData = new PropertyData();

                string name = string.IsNullOrEmpty(prefix) ? $"{typeName}{property.Name}" : $"{prefix}.{property.Name}";
                Type propertyType = property.PropertyType;

                if (propertyType.IsPrimitive || propertyType == typeof(string) || propertyType.IsValueType)
                {
                    propertyData.PropertyName = name;
                    propertyData.PropertyType = propertyType;
                    propertyData.CustomAttributes = property.GetCustomAttributes(false);
                    propertiesList.Add(propertyData);
                }
                else if (propertyType.IsClass)
                {
                    foreach (var subProperty in GetPropertyList(propertyType, name))
                    {
                        propertiesList.Add(subProperty);
                    }
                }
            }

            return propertiesList;
        }
    }
}
