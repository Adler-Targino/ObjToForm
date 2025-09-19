namespace ObjToForm.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomNameAttribute : Attribute
    {
        public string Name { get; set; }
        public CustomNameAttribute(string name)
        {
            Name = name;
        }
    }
}
