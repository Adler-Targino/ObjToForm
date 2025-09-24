namespace ObjToForm.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DivClassAttribute : Attribute
    {
        public string Class { get; set; }
        public bool Override { get; set; }
        public DivClassAttribute(string _class, bool _override = false)
        {
            Class = _class;
            Override = _override;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LabelClassAttribute : Attribute
    {
        public string Class { get; set; }
        public bool Override { get; set; }
        public LabelClassAttribute(string _class, bool _override = false)
        {
            Class = _class;
            Override = _override;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InputClassAttribute : Attribute
    {
        public string Class { get; set; }
        public bool Override { get; set; }
        public InputClassAttribute(string _class, bool _override = false)
        {
            Class = _class;
            Override = _override;
        }
    }
}
