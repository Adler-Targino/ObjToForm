namespace ObjToForm.DataTypes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HtmlAttributeAttribute : Attribute
    {
        public string Attribute { get; set; }        

        public HtmlAttributeAttribute(string _attribute)
        {
            Attribute = _attribute;
        }
    }
}
