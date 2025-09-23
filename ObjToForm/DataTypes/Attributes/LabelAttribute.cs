namespace ObjToForm.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LabelAttribute : Attribute
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        
        public LabelAttribute(bool _enabled) 
        {
            Enabled = _enabled;
        }

        public LabelAttribute(string _name, bool _enabled = true)
        {
            Name = _name;
            Enabled = _enabled;
        }
    }
}
