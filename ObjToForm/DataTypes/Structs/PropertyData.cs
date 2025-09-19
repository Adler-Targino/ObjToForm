using ObjToForm.DataTypes.Attributes;

namespace ObjToForm.DataTypes.Structs
{
    public struct PropertyData
    {
        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public object[] CustomAttributes { get; set; }
    }           
}
