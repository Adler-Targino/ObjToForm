using ObjToForm.DataTypes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjToForm.Utils
{
    internal static class HtmlUtils
    {
        #region Tags
        public static string BuildDiv(CustomAttributes custAttr, string defaultClasses = "", string id = "") 
        {
            return $"<div " +
                   $"class='{string.Join(" ", custAttr.DivClasses)} {(!custAttr.OverrideDivClasses ? defaultClasses : "")}'" +
                   $"style='{(custAttr.DivStyles.Any() ? string.Join(";", custAttr.DivStyles) : "")}'" +
                   $"{(!string.IsNullOrEmpty(id) ? ($"id='{id}'") : "")} >";
        }

        public static string BuildHeading(CustomAttributes custAttr, string name, string defaultClasses = "") 
        {
            return $"{(custAttr.HeadingEnabled ? 
                   $"<{(!string.IsNullOrEmpty(custAttr.HeadingSize.ToString()) ? custAttr.HeadingSize.ToString() : "h4")} " +
                   $"class='{string.Join(" ", custAttr.HeadingClasses)} {(!custAttr.OverrideHeadingClasses ? defaultClasses : "")}' " +
                   $"style='{(custAttr.HeadingStyles.Any() ? string.Join(";", custAttr.HeadingStyles) : "")}' " +
                   $">{custAttr.Heading ?? name}" +
                   $"</{(!string.IsNullOrEmpty(custAttr.HeadingSize.ToString()) ? custAttr.HeadingSize.ToString() : "h4")}>" : "")}";
        }

        public static string BuildLabel(CustomAttributes custAttr, string name, string defaultClasses = "")
        {
            return $"{(custAttr.LabelEnabled ?
                   $"<label " +
                   $"class='{string.Join(" ", custAttr.LabelClasses)} {(!custAttr.OverrideLabelClasses ? defaultClasses : "")}'" +
                   $"style='{(custAttr.LabelStyles.Any() ? string.Join(";", custAttr.LabelStyles) : "")}'" +
                   $"for='{name}'>{custAttr.Label ?? name}</label>" : "")}";
        }

        public static string BuildInput(CustomAttributes custAttr, string name, object? value, string defaultClasses = "")
        {
            return $"<input " +
                   $"id='{name}' name='{name}' {(value != null ? $"value='{value}'" : "")}" +
                   $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? defaultClasses : "")}'" +
                   $"style='{(custAttr.InputStyles.Any() ? string.Join(";", custAttr.InputStyles) : "")}'" +
                   $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>";
        }

        public static string BuildSelect(CustomAttributes custAttr, PropertyData prop, string defaultClasses = "")
        {
            return string.Join("",
                     Enum.GetNames(prop.PropertyType)
                         .Select(v => $"<option value='{v}' {((v == prop.PropertyValue?.ToString()) ? "selected" : "")} >{v}</option>"))
                         .Insert(0, $"<select id='{prop.PropertyName}' name='{prop.PropertyName}'" +
                                    $"class='{string.Join(" ", custAttr.InputClasses)} {(!custAttr.OverrideInputClasses ? defaultClasses : "")}'" +
                                    $"{(custAttr.HtmlAttributes.Any() ? string.Join(" ", custAttr.HtmlAttributes) : "")}>") +
                         "</select>";
        }

        public static string BuildButton(string name, string type = "submit", string defaultClasses = "", string id = "")
        {
            return $"<button class='{defaultClasses}' type='{type}' {(!string.IsNullOrEmpty(id) ? $"id='{id}'" : "")}>{name}</button>";
        }
        #endregion

        #region Scripts
        public static string BuildEnumerableGroupScript(string id = "")
        {
            return @$"
                    <script>
                    document.addEventListener(""DOMContentLoaded"", function () {{
                        const container = document.getElementById(""{id}-container"");
                        const addBtn = document.getElementById(""add-{id}"");

                        function reindexGroups() {{
                            container.querySelectorAll("".{id}-group"").forEach((group, index) => {{
                                group.querySelectorAll(""input, select"").forEach(el => {{
                                    if (el.name) {{
                                        el.name = el.name.replace(/\[\d+\]/g, `[${{index}}]`);
                                    }}
                                    if (el.id) {{
                                        el.id = el.id.replace(/_\d+__/g, `_${{index}}__`);
                                    }}
                                }});
                            }});
                        }}

                        addBtn.addEventListener(""click"", function () {{
                            const count = container.querySelectorAll("".{id}-group"").length;
                            const clone = container.querySelector("".{id}-group"").cloneNode(true);

                            clone.querySelectorAll(""input, select"").forEach(el => {{
                                if (el.name) {{
                                    el.name = el.name.replace(/\[\d+\]/g, `[${{count}}]`);
                                }}
                                if (el.id) {{
                                    el.id = el.id.replace(/_\d+__/g, `_${{count}}__`);
                                }}
                                if (el.tagName === ""INPUT"") el.value = """";
                                if (el.tagName === ""SELECT"") el.selectedIndex = 0;
                            }});

                            container.appendChild(clone);
                        }});

                        container.addEventListener(""click"", function (e) {{
                            if (e.target.classList.contains(""remove-{id}"")) {{
                                e.target.closest("".{id}-group"").remove();
                                reindexGroups();
                            }}
                        }});
                    }});
                    </script>";
        }

        #endregion
    }
}
