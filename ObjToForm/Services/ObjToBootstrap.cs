using Microsoft.AspNetCore.Html;
using ObjToForm.DataTypes.Objects;
using ObjToForm.Interfaces;
using ObjToForm.Utils;
using System.Text;

namespace ObjToForm.Services
{
    internal class ObjToBootstrap : IObjectConvertService
    {
        private CustomAttributes custAttr;
        public IHtmlContent ConvertToForm(Type obj, string prefix, bool modelBinding)
        {
            var properties = ObjectUtils.GetPropertyList(obj, prefix, modelBinding);

            var result = new StringBuilder();
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

            result.Append("<button type='submit' class='btn btn-primary'>Submit</button>");

            return new HtmlString(result.ToString());
        }

        private void AddNumberInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='number'");
            custAttr.HtmlAttributes.Add("step='any'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, "form-control"));
            s.Append("</div>");            
        }

        private void AddTextInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='text'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, "form-control"));
            s.Append("</div>");
        }

        private void AddCharInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='text'");
            custAttr.HtmlAttributes.Add("maxlength='1'");
            custAttr.HtmlAttributes.Add("size='1'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, "form-control"));
            s.Append("</div>");
        }

        private void AddCheckInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='checkbox'");
            custAttr.HtmlAttributes.Add("value='true'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-check my-3"));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, "form-check-input"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName, "form-check-label"));
            s.Append("</div>");            
        }

        private void AddDateInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='date'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, "form-control"));
            s.Append("</div>");
        }

        private void AddSelect(ref StringBuilder s, PropertyData prop)
        {
            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildSelect(custAttr, prop, "form-select"));
            s.Append("</div>");        
        }
    }
}
