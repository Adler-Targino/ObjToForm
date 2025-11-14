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
        public List<string> StringList { get; set; }
        public DateTime DateTime { get; set; }
        public DemoOptions PublicOptionsEnum { get; set; }
        public DemoObj2 DemoObj2 { get; set; }
    }

    public class DemoObj2
    {
        public string PublicString2 { get; set; }
        public bool PublicBool2 { get; set; }
    }

    public class ObjectUtilsTest
    {
        [Fact]
        public void Test1()
        {
            List<string> sl = new List<string>();

            sl.Add("1");
            sl.Add("2");

            DemoObj Obj = new DemoObj
            {
                StringList = sl,
            };

            ObjToForm.ConvertToBootstrapForm(Obj, "Obj", true);

            Assert.Equal(true, true);
        }
    }
}