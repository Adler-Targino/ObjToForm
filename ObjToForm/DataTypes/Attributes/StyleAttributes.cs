namespace ObjToForm.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DivStyleAttribute : Attribute
    {
        public string Style { get; set; }
        public DivStyleAttribute(string _style)
        {
            Style = _style;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LabelStyleAttribute : Attribute
    {
        public string Style { get; set; }
        public LabelStyleAttribute(string _style)
        {
            Style = _style;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InputStyleAttribute : Attribute
    {
        public string Style { get; set; }
        public InputStyleAttribute(string _style)
        {
            Style = _style;
        }
    }
}
