using ObjToForm.DataTypes.Attributes;

namespace ObjToForm.DataTypes.Objects
{
    public class CustomAttributes
    {
        public string? Label { get; set; }
        public bool LabelEnabled { get; set; }
        public List<string> DivStyles { get; set; }
        public List<string> LabelStyles { get; set; }
        public List<string> InputStyles { get; set; }
        public List<string> DivClasses { get; set; }
        public List<string> LabelClasses { get; set; }
        public List<string> InputClasses { get; set; }
        public bool OverrideDivClasses { get; set; }
        public bool OverrideLabelClasses { get; set; }
        public bool OverrideInputClasses { get; set; }
        public List<string> HtmlAttributes { get; set; }


        public CustomAttributes (object[] attributes)
        {
            Label = attributes.OfType<LabelAttribute>()?.FirstOrDefault()?.Name;
            LabelEnabled = attributes.OfType<LabelAttribute>()?.FirstOrDefault()?.Enabled ?? true;

            DivStyles = attributes.OfType<DivStyleAttribute>()?.Select(x => x.Style).ToList();
            LabelStyles = attributes.OfType<LabelStyleAttribute>()?.Select(x => x.Style).ToList();
            InputStyles = attributes.OfType<InputStyleAttribute>()?.Select(x => x.Style).ToList();

            DivClasses = attributes.OfType<DivClassAttribute>()?.Select(x => x.Class).ToList();
            LabelClasses = attributes.OfType<LabelClassAttribute>()?.Select(x => x.Class).ToList();
            InputClasses = attributes.OfType<InputClassAttribute>()?.Select(x => x.Class).ToList();

            OverrideDivClasses = attributes.OfType<DivClassAttribute>()?.Where(x => x.Override == true).Any() ?? false;
            OverrideLabelClasses = attributes.OfType<LabelClassAttribute>()?.Where(x => x.Override == true).Any() ?? false;
            OverrideInputClasses = attributes.OfType<InputClassAttribute>()?.Where(x => x.Override == true).Any() ?? false;

            HtmlAttributes = attributes.OfType<HtmlAttributeAttribute>()?.Select(x => x.Attribute).ToList();
        }
    }
}
