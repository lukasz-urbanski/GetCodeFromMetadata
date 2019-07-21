using System;
using GetCodeFromMetadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeCorp.Training.Test
{
    [TestClass]
    public class UnitTest1
    {
        // Tak na przyszłość:
        // Instancja klasy Program
        // a potem metoda GetCode() z niej
        [TestMethod]
        public void TestMethod1()
        {
            string output1 = FakeExfactor.Get("11977~Iron Pipe_F3M~EL_MQ9~7d115c0a-b1cc-4fba-ac86-8b8d86a4e17e.acm");
            Assert.AreEqual("MQ9", output1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string output2 = FakeExfactor.Get("14180~Iron Pipe_L0J~BG_LBY_LHPK6~bde03469-bbe9-456e-83bb-ef11931d66e4.acm");
            Assert.AreEqual("LHPK6", output2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string output3 = FakeExfactor.Get("972~Wine_B9A~BG_7OY_66FM~9d21165d-1613-49c7-82e4-b2004937bb4e_WUBKU~jIEDi7f.xlf");
            Assert.AreEqual("66FM", output3);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string output4 = FakeExfactor.Get("22752~Wine_I7Q~EN_Y0I_v6~f5d93da7-88cb-45ec-abf4-42d512a89a8f_1MJ8C~c3jdn37.acm");
            Assert.AreEqual("1MJ8C", output4);
        }
        [TestMethod]
        public void TestMethod5()
        {
            string output5 = FakeExfactor.Get("<Object> < ItemType > CoffeGrinder < Metadata > 9RK~ZH_JKB_v5~XX6C~b72bfe30 - 7632 - 4a2c - 8676 - 0222c1e61ba5_LH8D2~9T6mrGZ </ Metadata ></ ItemType ></ Object > ");
            Assert.AreEqual("XX6C", output5);
        }
        [TestMethod]
        public void TestMethod6()
        {
            string output6 = FakeExfactor.Get("{ \"object\": { \"ItemType\": \"JamJar\", \"WarehouseId\": \"CkdGbgd\",    \"Metadata\": \"W5J~SV_1QG_v7~Y34~6006fa3d-9f5a-496a-b7b4-479de451811\a\"  } }");            
            Assert.AreEqual("Y34", output6);
        }
        [TestMethod]
        public void TestMethod7()
        { 
            string output7 = FakeExfactor.Get("<Object><ItemType>Pick</ItemType><Code>XA87</Code><Market>SV</Market><Version>6</Version><Metadata>3OZ~_SE2~37f95122-c8e2-40cc-bad7-76fcffc0d981_ICGNA~3xH5VEB</Metadata></Object>");
            Assert.AreEqual("XA87", output7);
        }
        [TestMethod]
        public void AllTests()
        {
            TestMethod1();
            TestMethod2();
            TestMethod3();
            TestMethod4();
            TestMethod5();
            TestMethod6();
            TestMethod7();
        }

        public static class FakeExfactor
        {
            public static string Get(string metadata)
            {
                return Program.ReturnCodeOutside(metadata);
            }
        }
    }
}