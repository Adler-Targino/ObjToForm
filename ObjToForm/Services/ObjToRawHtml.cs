using Microsoft.AspNetCore.Html;
using ObjToForm.Interfaces;
using ObjToForm.Utils;

namespace ObjToForm.Services
{
    internal class ObjToRawHtml : IObjectConvertService
    {
        public IHtmlContent ConvertToForm(Type obj)
        {
            var attrDict = ObjectUtils.GetAttributesDictionary(obj);

            string result = "";
            foreach (var attr in attrDict)
            {
                string attrLabel = $"<label for='{attr.Key}'>{attr.Key}</label><br>";
                string attrInput = attr.Value switch
                {
                    Type t when t == typeof(int) || t == typeof(long) || t == typeof(float) || t == typeof(double) =>
                        $"<input type='number' id='{attr.Key}' step='any' name='{attr.Key}'><br>",
                    Type t when t == typeof(string) =>
                        $"<input type='text' id='{attr.Key}' name='{attr.Key}'><br>",
                    Type t when t == typeof(char) =>
                        $"<input type='text' id='{attr.Key}' name='{attr.Key}' maxlength='1' size='1'><br>",
                    Type t when t == typeof(bool) =>
                        $"<input type='checkbox' id='{attr.Key}' value='true' name='{attr.Key}'><br>",
                    Type t when t == typeof(DateTime) =>
                        $"<input type='date' id='{attr.Key}' name='{attr.Key}'><br>",
                    Type t when t.IsEnum =>
                        string.Join("",
                               Enum.GetNames(t)
                                   .Select(v => $"<option value='{v}'>{v}</option>"))
                                   .Insert(0, $"<select id='{attr.Key}' name='{attr.Key}'>") + 
                                   "</select><br>",

                    _ => $"<input type='text' id='{attr.Key}' name='{attr.Key}'><br>"
                };

                result += attrLabel;
                result += attrInput;
            }

            result += "<br><input type='submit' value='Submit'>";

            return new HtmlString(result);
        }
    }
}
