using ObjToForm.DataTypes.Attributes;

namespace ObjToForm.DataTypes.Structs
{
    public struct CustomAttributes
    {
        public string? CustomName { get; set; }


        public CustomAttributes (object[] attributes)
        {
            CustomName = attributes.OfType<CustomNameAttribute>()?.FirstOrDefault()?.Name;
        }
    }
}
