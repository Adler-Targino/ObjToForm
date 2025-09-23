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
            s.Append(
                $"<div " +
                $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? "form-group my-3" : "")}'" +
                $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                $">" +
                $"{(custAttr.LabelEnabled ?
                $"<label " +
                $"class='{(custAttr.LabelClasses.Any() ? string.Join(" ", custAttr.LabelClasses) : "")}'" +
                $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                $"for='{prop.PropertyName}'>{custAttr.Label ?? prop.PropertyName}</label>" : "")}" +
                $"<input " +
                $"type='number' id='{prop.PropertyName}' step='any' name='{prop.PropertyName}' " +
                $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? "form-control" : "")}'" +
                $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>" +
                $"</div>"
            );            
        }

        private void AddTextInput(ref StringBuilder s, PropertyData prop)
        {
            s.Append(
                $"<div " +
                $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? "form-group my-3" : "")}'" +
                $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                $">" +
                $"{(custAttr.LabelEnabled ?
                $"<label " +
                $"class='{(custAttr.LabelClasses.Any() ? string.Join(" ", custAttr.LabelClasses) : "")}'" +
                $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                $"for='{prop.PropertyName}'>{custAttr.Label ?? prop.PropertyName}</label>" : "")}" +
                $"<input type='text' id='{prop.PropertyName}' name='{prop.PropertyName}' " +
                $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? "form-control" : "")}'" +
                $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>" +
                $"</div>"
            );
        }

        private void AddCharInput(ref StringBuilder s, PropertyData prop)
        {
            s.Append(
                $"<div " +
                $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? "form-group my-3" : "")}'" +
                $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                $">" +
                $"{(custAttr.LabelEnabled ?
                $"<label " +
                $"class='{(custAttr.LabelClasses.Any() ? string.Join(" ", custAttr.LabelClasses) : "")}'" +
                $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                $"for='{prop.PropertyName}'>{custAttr.Label ?? prop.PropertyName}</label>" : "")}" +
                $"<input type='text' id='{prop.PropertyName}' name='{prop.PropertyName}' maxlength='1' size='1'" +
                $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? "form-control" : "")}'" +
                $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>" +
                $"</div>"
            );
        }

        private void AddCheckInput(ref StringBuilder s, PropertyData prop)
        {
            s.Append(
                $"<div " +
                $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? "form-check my-3" : "")}'" +
                $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                $">" +
                $"{(custAttr.LabelEnabled ?
                $"<label " +
                $"class='{string.Join(" ", custAttr.LabelClasses)} {(!custAttr.OverrideLabelClasses ? "form-check-label" : "")}'" +
                $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                $"for='{prop.PropertyName}'>{custAttr.Label ?? prop.PropertyName}</label>" : "")}" +
                $"<input type='checkbox' id='{prop.PropertyName}' value='true' name='{prop.PropertyName}' " +
                $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? "form-check-input" : "")}'" +
                $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>" +
                $"</div>"
            );
        }

        private void AddDateInput(ref StringBuilder s, PropertyData prop)
        {
            s.Append(
                $"<div " +
                $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? "form-group my-3" : "")}'" +
                $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                $">" +
                $"{(custAttr.LabelEnabled ?
                $"<label " +
                $"class='{(custAttr.LabelClasses.Any() ? string.Join(" ", custAttr.LabelClasses) : "")}'" +
                $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                $"for='{prop.PropertyName}'>{custAttr.Label ?? prop.PropertyName}</label>" : "")}" +
                $"<input type='date' id='{prop.PropertyName}' name='{prop.PropertyName}' " +
                $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? "form-control" : "")}'" +
                $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>" +
                $"</div>"
            );
        }

        private void AddSelect(ref StringBuilder s, PropertyData prop)
        {
            s.Append(
                $"<div " +
                $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? "form-group my-3" : "")}'" +
                $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                $">" +
                $"{(custAttr.LabelEnabled ? 
                $"<label " +
                $"class='{(custAttr.LabelClasses.Any() ? string.Join(" ", custAttr.LabelClasses) : "")}'" +
                $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                $"for='{prop.PropertyName}'>{custAttr.Label ?? prop.PropertyName}</label>" :"")}" +
                string.Join("",
                        Enum.GetNames(prop.PropertyType)
                            .Select(v => $"<option value='{v}'>{v}</option>"))
                            .Insert(0, $"<select id='{prop.PropertyName}' name='{prop.PropertyName}' " +
                                       $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? "form-select" : "")}'" +
                                       $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}") +
                            "</select>" +
                $"</div>"
            );            
        }
    }
}
