namespace ObjToForm.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HeadingAttribute : Attribute
    {
        public string? Name { get; set; }
        public HeadingSizes? Size { get; set; }
        public bool Enabled { get; set; }
        
        public HeadingAttribute(bool _enabled) 
        {
            Enabled = _enabled;
        }

        public HeadingAttribute(string _name, bool _enabled = true)
        {
            Name = _name;
            Enabled = _enabled;
        }

        public HeadingAttribute(string _name, HeadingSizes _size, bool _enabled = true)
        {
            Name = _name;
            Size = _size;
            Enabled = _enabled;
        }
    }

    public enum HeadingSizes
    {
        h1,
        h2,
        h3,
        h4,
        h5,
        h6
    }
}
