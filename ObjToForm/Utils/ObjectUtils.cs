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

            PropertyData parentData = new PropertyData();
            parentData.PropertyName = objType.Name;
            parentData.PropertyType = objType;
            parentData.CustomAttributes = objType.GetCustomAttributes(false);
            propertiesList.Add(parentData);

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
                else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
                {
                    Type? enumerableType = null;
                    if (propertyType.IsArray)
                        enumerableType = propertyType.GetElementType();
                    else if (propertyType.IsGenericType)
                        enumerableType = propertyType.GetGenericArguments().FirstOrDefault();

                    if (enumerableType != null)
                    {
                        var enumerable = value as System.Collections.IEnumerable;
                        int index = 0;

                        if (enumerable != null)
                        {
                            foreach (var item in enumerable)
                            {
                                foreach (var subProperty in GetPropertyList(item, $"{name}[{index}]", ModelBinding))
                                {
                                    propertiesList.Add(subProperty);
                                }
                                index++;
                            }
                        }
                        else
                        {
                            if (enumerableType.IsPrimitive || enumerableType == typeof(string) || enumerableType.IsValueType)
                            {
                                propertyData.PropertyName = $"{name}[0]";
                                propertyData.PropertyType = enumerableType;
                                propertyData.CustomAttributes = property.GetCustomAttributes(false);
                                propertiesList.Add(propertyData);
                            }
                            else
                            {
                                var activatedItem = Activator.CreateInstance(enumerableType!);

                                foreach (var subProperty in GetPropertyList(activatedItem!, $"{name}[0]", ModelBinding))
                                {
                                    propertiesList.Add(subProperty);
                                }
                            }
                        }
                    }

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
