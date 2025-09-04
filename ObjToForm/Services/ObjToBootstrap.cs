using Microsoft.AspNetCore.Html;
using ObjToForm.Interfaces;
using ObjToForm.Utils;

namespace ObjToForm.Services
{
    internal class ObjToBootstrap : IObjectConvertService
    {
        public IHtmlContent ConvertToForm(Type obj)
        {
            var attrDict = ObjectUtils.GetAttributesDictionary(obj);

            string result = "";
            foreach (var attr in attrDict)
            {
                string div = attr.Value switch
                {
                    Type t when t == typeof(int) || t == typeof(long) || t == typeof(float) || t == typeof(double) =>
                        "<div class='form-group my-3'>" +
                        $"<label for='{attr.Key}'>{attr.Key}</label>" +
                        $"<input type='number' id='{attr.Key}' name='{attr.Key}' class='form-control'>" +
                        "</div>",
                    Type t when t == typeof(string) =>
                        "<div class='form-group my-3'>" +
                        $"<label for='{attr.Key}'>{attr.Key}</label>" +
                        $"<input type='text' id='{attr.Key}' name='{attr.Key}' class='form-control'>" +
                        "</div>",
                    Type t when t == typeof(char) =>
                        "<div class='form-group my-3'>" +
                        $"<label for='{attr.Key}'>{attr.Key}</label>" +
                        $"<input type='text' id='{attr.Key}' name='{attr.Key}' class='form-control' maxlength='1' size='1'>" +
                        "</div>",
                    Type t when t == typeof(bool) =>
                        "<div class='form-check my-3'>" +
                        $"<label for='{attr.Key}' class='form-check-label'>{attr.Key}</label>" +
                        $"<input type='checkbox' id='{attr.Key}' name='{attr.Key}' class='form-check-input'>" +
                        "</div>",
                    Type t when t == typeof(DateTime) =>
                        "<div class='form-group my-3'>" +
                        $"<label for='{attr.Key}'>{attr.Key}</label>" +
                        $"<input type='date' id='{attr.Key}' name='{attr.Key}' class='form-control'>" +
                        "</div>",
                    Type t when t.IsEnum =>
                        "<div class='form-group my-3'>" +
                        $"<label for='{attr.Key}'>{attr.Key}</label>" +
                        string.Join("",
                               Enum.GetNames(t)
                                   .Select(v => $"<option value='{v}'>{v}</option>"))
                                   .Insert(0, $"<select id='{attr.Key}' name='{attr.Key}' class='form-select'>") + 
                                   "</select>" +
                        "</div>",

                _ =>"<div class='form-group my-3'>" +
                        $"<label for='{attr.Key}'>{attr.Key}</label>" + 
                        $"<input type='text' id='{attr.Key}' name='{attr.Key}'>" +
                        "</div>"
                };

                result += div;
            }

            result += "<button type='submit' class='btn btn-primary'>Submit</button>";

            return new HtmlString(result);
        }
    }
}
