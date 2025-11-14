using Microsoft.AspNetCore.Html;
using ObjToForm.DataTypes.Objects;
using ObjToForm.Extensions;
using ObjToForm.Interfaces;
using ObjToForm.Utils;
using System.Text;

namespace ObjToForm.Services
{
    internal class ObjToBootstrap : IObjectConvertService
    {
        private CustomAttributes custAttr;
        private bool enumerableFlag = false;
        private string enumerableGroupName = string.Empty;
        private int enumerableIndex = 0;
        public IHtmlContent ConvertToForm(object obj, string prefix, bool modelBinding)
        {
            var properties = ObjectUtils.GetPropertyList(obj, prefix, modelBinding);

            var result = new StringBuilder();
            foreach (var prop in properties)
            {
                if (enumerableFlag)
                {
                    if (prop.PropertyName.IndexOf($"{enumerableGroupName}[") == -1)
                    {
                        CloseEnumerableContainerDiv(ref result);
                    }
                    else
                    {
                        if(enumerableIndex != prop.PropertyName.GetEnumerableIndex())
                        {
                            result.Append(HtmlUtils.BuildDiv(custAttr, "col-12 text-end"));
                            result.Append(HtmlUtils.BuildButton($"Remove {enumerableGroupName}", "button", $"btn btn-danger remove-{enumerableGroupName}"));
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append(HtmlUtils.BuildDiv(custAttr, id: $"{enumerableGroupName}-group"));
                            enumerableIndex = prop.PropertyName.GetEnumerableIndex();
                        }
                    }                   
                }

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

                    case var _ when typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType):
                        enumerableFlag = true;
                        enumerableGroupName = prop.PropertyName;
                        OpenEnumerableContainerDiv(ref result);
                        break;

                    case var _ when prop.PropertyType.IsClass:
                        AddHeading(ref result, prop);
                        break;

                    default:
                        AddTextInput(ref result, prop);
                        break;                
                };
            }

            result.Append(HtmlUtils.BuildButton("Submit", defaultClasses: "btn btn-primary"));

            return new HtmlString(result.ToString());
        }

        private void AddNumberInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='number'");
            custAttr.HtmlAttributes.Add("step='any'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, prop.PropertyValue, "form-control"));
            s.Append("</div>");            
        }

        private void AddTextInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='text'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, prop.PropertyValue, "form-control"));
            s.Append("</div>");
        }

        private void AddCharInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='text'");
            custAttr.HtmlAttributes.Add("maxlength='1'");
            custAttr.HtmlAttributes.Add("size='1'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, prop.PropertyValue, "form-control"));
            s.Append("</div>");
        }

        private void AddCheckInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='checkbox'");
            custAttr.HtmlAttributes.Add("value='true'");
            if((bool?)prop.PropertyValue == true)
                custAttr.HtmlAttributes.Add("checked");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-check my-3"));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, null, "form-check-input"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName, "form-check-label"));
            s.Append("</div>");            
        }

        private void AddDateInput(ref StringBuilder s, PropertyData prop)
        {
            custAttr.HtmlAttributes.Add("type='date'");
            if ((DateTime?)prop.PropertyValue != DateTime.MinValue)
                custAttr.HtmlAttributes.Add($"value='{((DateTime)prop.PropertyValue).ToString("yyyy-MM-dd")}'");

            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildInput(custAttr, prop.PropertyName, null, "form-control"));
            s.Append("</div>");
        }

        private void AddSelect(ref StringBuilder s, PropertyData prop)
        {
            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3"));
            s.Append(HtmlUtils.BuildLabel(custAttr, prop.PropertyName));
            s.Append(HtmlUtils.BuildSelect(custAttr, prop, "form-select"));
            s.Append("</div>");        
        }

        private void OpenEnumerableContainerDiv(ref StringBuilder s)
        {
            s.Append(HtmlUtils.BuildDiv(custAttr, "form-group my-3", $"{enumerableGroupName}-container"));
            s.Append(HtmlUtils.BuildDiv(custAttr, defaultClasses: $"{enumerableGroupName}-group"));
        }

        private void CloseEnumerableContainerDiv(ref StringBuilder s)
        {
            s.Append(HtmlUtils.BuildDiv(custAttr, "col-12 text-end"));
            s.Append(HtmlUtils.BuildButton($"Remove {enumerableGroupName}", "button", $"btn btn-danger remove-{enumerableGroupName}"));
            s.Append("</div>");
            s.Append("</div>");
            s.Append("</div>");
            s.Append(HtmlUtils.BuildButton($"Add {enumerableGroupName}", "button", "btn btn-secondary", $"add-{enumerableGroupName}"));
            s.Append(HtmlUtils.BuildEnumerableGroupScript(enumerableGroupName));
            enumerableFlag = false;
            enumerableGroupName = string.Empty;
            enumerableIndex = 0;
        }

        private void AddHeading(ref StringBuilder s, PropertyData prop)
        {
            s.Append(HtmlUtils.BuildHeading(custAttr, prop.PropertyName, "my-3"));
        }
    }
}
