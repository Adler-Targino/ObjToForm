using ObjToForm.DataTypes.Attributes;
using ObjToForm.Utils;

namespace ObjToForm.Tests
{
    public enum DemoOptions
    {
        Option1,
        Option2,
        Option3,
        Option4,
        Option5
    }

    public class DemoObj
    {
        [Label("My Custom String")]
        public string PublicString { get; set; }
        public int PublicInt { get; set; }
        public DateTime PublicDateTime { get; set; }
        public bool PublicBool { get; set; }
        public DemoOptions PublicOptionsEnum { get; set; }
    }

    public class ObjectUtilsTest
    {
        [Fact]
        public void Test1()
        {
            ObjToForm.ConvertToBootstrapForm(typeof(DemoObj), "Obj", true);

            Assert.Equal(true, true);
        }
    }
}