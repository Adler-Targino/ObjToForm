using Microsoft.AspNetCore.Html;
using ObjToForm.DataTypes.Structs;
using ObjToForm.Interfaces;
using ObjToForm.Utils;

namespace ObjToForm.Services
{
    internal class ObjToRawHtml : IObjectConvertService
    {
        private CustomAttributes custAttr;
        public IHtmlContent ConvertToForm(Type obj, string prefix, bool modelBinding)
        {
            var properties = ObjectUtils.GetPropertyList(obj, prefix, modelBinding);

            string result = "";
            foreach (var prop in properties)
            {
                custAttr = new CustomAttributes(prop.CustomAttributes);

                result += $"<label for='{prop.PropertyName}'>{custAttr.CustomName ?? prop.PropertyName}</label><br>";

                switch (prop.PropertyType)
                {
                    case var _ when prop.PropertyType == typeof(int)
                                 || prop.PropertyType == typeof(long)
                                 || prop.PropertyType == typeof(float)
                                 || prop.PropertyType == typeof(double):
                        AddNumberInput(ref result, prop);
                        break;

                    case var _ when prop.PropertyType == typeof(string):
                        AddTextInput(ref result, prop);
                        break;

                    case var _ when prop.PropertyType == typeof(char):
                        AddCharInput(ref result, prop);
                        break;

                    case var _ when prop.PropertyType == typeof(bool):
                        AddCheckInput(ref result, prop);
                        break;

                    case var _ when prop.PropertyType == typeof(DateTime):
                        AddDateInput(ref result, prop);
                        break;

                    case var _ when prop.PropertyType.IsEnum:
                        AddSelect(ref result, prop);
                        break;

                    default:
                        AddTextInput(ref result, prop);
                        break;
                };
            }

            result += "<br><input type='submit' value='Submit'>";

            return new HtmlString(result);
        }

        private void AddNumberInput(ref string s, PropertyData prop)
        {
            s += $"<input type='number' id='{prop.PropertyName}' step='any' name='{prop.PropertyName}'><br>";
        }

        private void AddTextInput(ref string s, PropertyData prop)
        {
            s += $"<input type='text' id='{prop.PropertyName}' name='{prop.PropertyName}'><br>";
        }

        private void AddCharInput(ref string s, PropertyData prop)
        {
            s += $"<input type='text' id='{prop.PropertyName}' name='{prop.PropertyName}' maxlength='1' size='1'><br>";
        }

        private void AddCheckInput(ref string s, PropertyData prop)
        {
            s += $"<input type='checkbox' id='{prop.PropertyName}' value='true' name='{prop.PropertyName}'><br>";
        }

        private void AddDateInput(ref string s, PropertyData prop)
        {
            s += $"<input type='date' id='{prop.PropertyName}' name='{prop.PropertyName}'><br>";
        }

        private void AddSelect(ref string s, PropertyData prop)
        {
            s += string.Join("", Enum.GetNames(prop.PropertyType)
                                     .Select(v => $"<option value='{v}'>{v}</option>"))
                                     .Insert(0, $"<select id='{prop.PropertyName}' name='{prop.PropertyName}'>") +
                                     "</select><br>";
        }
    }
}
