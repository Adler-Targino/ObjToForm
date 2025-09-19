using Microsoft.AspNetCore.Html;
using ObjToForm.DataTypes.Attributes;
using ObjToForm.DataTypes.Structs;
using ObjToForm.Interfaces;
using ObjToForm.Utils;

namespace ObjToForm.Services
{
    internal class ObjToBootstrap : IObjectConvertService
    {
        private CustomAttributes custAttr;
        public IHtmlContent ConvertToForm(Type obj, string prefix, bool modelBinding)
        {
            var properties = ObjectUtils.GetPropertyList(obj, prefix, modelBinding);

            string result = "";
            foreach (var prop in properties)
            {
                custAttr = new CustomAttributes(prop.CustomAttributes);
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

            result += "<button type='submit' class='btn btn-primary'>Submit</button>";

            return new HtmlString(result);
        }

        private void AddNumberInput(ref string s, PropertyData prop)
        {
            s +=
            $"<div class='form-group my-3'>" +
            $"<label for='{prop.PropertyName}'>{custAttr.CustomName ?? prop.PropertyName}</label>" +
            $"<input type='number' id='{prop.PropertyName}' step='any' name='{prop.PropertyName}' class='form-control'>" +
            $"</div>";            
        }

        private void AddTextInput(ref string s, PropertyData prop)
        {
            s +=
            $"<div class='form-group my-3'>" +
            $"<label for='{prop.PropertyName}'>{custAttr.CustomName ?? prop.PropertyName}</label>" +
            $"<input type='text' id='{prop.PropertyName}' step='any' name='{prop.PropertyName}' class='form-control'>" +
            $"</div>";
        }

        private void AddCharInput(ref string s, PropertyData prop)
        {
            s +=
            $"<div class='form-group my-3'>" +
            $"<label for='{prop.PropertyName}'>{custAttr.CustomName ?? prop.PropertyName}</label>" +
            $"<input type='text' id='{prop.PropertyName}' step='any' name='{prop.PropertyName}' class='form-control' maxlength='1' size='1'>" +
            $"</div>";
        }

        private void AddCheckInput(ref string s, PropertyData prop)
        {
            s +=
            $"<div class='form-check my-3'>" +
            $"<label for='{prop.PropertyName}' class='form-check-label'>{custAttr.CustomName ?? prop.PropertyName}</label>" +
            $"<input type='checkbox' id='{prop.PropertyName}' value='true' name='{prop.PropertyName}' class='form-check-input'>" +
            $"</div>";
        }

        private void AddDateInput(ref string s, PropertyData prop)
        {
            s +=
            $"<div class='form-group my-3'>" +
            $"<label for='{prop.PropertyName}' class='form-check-label'>{custAttr.CustomName ?? prop.PropertyName}</label>" +
            $"<input type='date' id='{prop.PropertyName}' value='true' name='{prop.PropertyName}' class='form-control'>" +
            $"</div>";
        }

        private void AddSelect(ref string s, PropertyData prop)
        {
            s +=
            $"<div class='form-group my-3'>" +
            $"<label for='{prop.PropertyName}'>{custAttr.CustomName ?? prop.PropertyName}</label>" +
            string.Join("",
                    Enum.GetNames(prop.PropertyType)
                        .Select(v => $"<option value='{v}'>{v}</option>"))
                        .Insert(0, $"<select id='{prop.PropertyName}' name='{prop.PropertyName}' class='form-select'>") +
                        "</select>" +
            $"</div>";            
        }
    }
}
