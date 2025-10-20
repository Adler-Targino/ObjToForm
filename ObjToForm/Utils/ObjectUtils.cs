using ObjToForm.DataTypes.Objects;
using System.Reflection;

namespace ObjToForm.Utils
{
    public static class ObjectUtils
    {
        public static List<PropertyData> GetPropertyList(object obj, string prefix = "", bool ModelBinding = false)
        {
            Type objType = obj.GetType();
            var propertiesList = new List<PropertyData>();

            string typeName =  ModelBinding ? $"{prefix}." : "";

            foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                PropertyData propertyData = new PropertyData();

                string name = string.IsNullOrEmpty(prefix) ? $"{typeName}{property.Name}" : $"{prefix}.{property.Name}";
                Type propertyType = property.PropertyType;
                object? value = property.GetValue(obj);

                if (propertyType.IsPrimitive || propertyType == typeof(string) || propertyType.IsValueType)
                {
                    propertyData.PropertyName = name;
                    propertyData.PropertyType = propertyType;
                    propertyData.CustomAttributes = property.GetCustomAttributes(false);
                    propertyData.PropertyValue = value;
                    propertiesList.Add(propertyData);
                }
                else if (propertyType.IsClass)
                {
                    var subObj = value ?? Activator.CreateInstance(propertyType);
                    foreach (var subProperty in GetPropertyList(subObj, name, ModelBinding))
                    {
                        propertiesList.Add(subProperty);
                    }
                }
            }

            return propertiesList;
        }
    }
}
