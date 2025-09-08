using ObjToForm.Utils;

namespace ObjToForm.Tests
{
    public class ObjectUtilsTest
    {
        [Fact]
        public void Test1()
        {
            var mockup = new
            {
                Attr1 = "attribute1",
                Attr2 = 2,
                Attr3 = true,
                Attr4 = 'a',
                Attr5 = DateTime.Now,
                Attr6 = 3.14
            };

            Dictionary<string, Type> expected = new Dictionary<string, Type>();
            expected.Add("Attr1", typeof(string));
            expected.Add("Attr2", typeof(int));
            expected.Add("Attr3", typeof(bool));
            expected.Add("Attr4", typeof(char));
            expected.Add("Attr5", typeof(DateTime));
            expected.Add("Attr6", typeof(double));

            var result = ObjectUtils.GetAttributesDictionary(mockup.GetType());

            Assert.Equal(expected, result);
        }


        public class Foo
        {
            public int Foo1 { get; set; }
            public string Foo2 { get; set; }
        }

        public class Bar
        {
            public int Bar1 { get; set; }
            private int Bar2 { get; set; }
            public string Bar3 { get; set; }
            private string Bar4 { get; set; }

            public Foo Foo1 { get; set; }
            private Foo Foo2 { get; set; }
        }


        [Fact]
        public void Test2()
        {
            Dictionary<string, Type> expected = new Dictionary<string, Type>();
            expected.Add("Foo1", typeof(int));
            expected.Add("Foo2", typeof(string));


            var result = ObjectUtils.GetAttributesDictionary(typeof(Foo));

            Assert.Equal(expected, result);
        }


        [Fact]
        public void Test3()
        {
            Dictionary<string, Type> expected = new Dictionary<string, Type>();
            expected.Add("Bar1", typeof(int));
            expected.Add("Bar3", typeof(string));
            expected.Add("Foo1.Foo1", typeof(int));
            expected.Add("Foo1.Foo2", typeof(string));

            
            var result = ObjectUtils.GetAttributesDictionary(typeof(Bar));

            Assert.Equal(expected, result);
        }
    }
}