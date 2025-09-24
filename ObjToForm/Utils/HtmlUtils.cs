﻿using ObjToForm.DataTypes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjToForm.Utils
{
    internal static class HtmlUtils
    {
        public static string BuildDiv(CustomAttributes custAttr, string defaultClasses = "") 
        {
            return $"<div " +
                   $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? defaultClasses : "")}'" +
                   $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                   $">";
        }

        public static string BuildLabel(CustomAttributes custAttr, string name, string defaultClasses = "")
        {
            return $"{(custAttr.LabelEnabled ?
                   $"<label " +
                   $"class='{string.Join(" ", custAttr.LabelClasses)} {(!custAttr.OverrideLabelClasses ? defaultClasses : "")}'" +
                   $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                   $"for='{name}'>{custAttr.Label ?? name}</label>" : "")}";
        }

        public static string BuildInput(CustomAttributes custAttr, string name, string defaultClasses = "")
        {
            return $"<input " +
                   $"id='{name}' name='{name}' " +
                   $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? defaultClasses : "")}'" +
                   $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                   $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>";
        }

        public static string BuildSelect(CustomAttributes custAttr, PropertyData prop, string defaultClasses = "")
        {
            return string.Join("",
                     Enum.GetNames(prop.PropertyType)
                         .Select(v => $"<option value='{v}'>{v}</option>"))
                         .Insert(0, $"<select id='{prop.PropertyName}' name='{prop.PropertyName}'" +
                                    $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? defaultClasses : "")}'" +
                                    $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>") +
                         "</select>";                
        }
    }
}
