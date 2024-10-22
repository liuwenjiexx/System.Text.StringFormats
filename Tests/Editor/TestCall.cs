using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Text.StringFormats.Tests
{
    public class TestCall
    {
        [Test]
        public void Param_Empty()
        {
            Assert.AreEqual("abc", "{0:@ToLower()}".FormatString("ABC"));
        }
        [Test]
        public void Param_Empty_NoBracket()
        {
            Assert.AreEqual("abc", "{0:@ToLower}".FormatString("ABC"));
        }

        [Test]
        public void Param_String()
        {
            Assert.AreEqual("AAA", "{0:@Replace(\"BC\",\"AA\")}".FormatString("ABC"));
        }

        [Test]
        public void Param_int()
        {
            Assert.AreEqual("BC", "{0:@Substring(1,2)}".FormatString("ABC"));
        }

        [Test]
        public void Static_Method()
        {
            Assert.AreEqual("False", "{0:@String.IsNullOrEmpty($)}".FormatString("ABC"));
        }
        [Test]
        public void ParamArray()
        {
            Assert.AreEqual("Hello World", "{0:@String.Join(\" \",$,\"World\")}".FormatString("Hello"));
        }
        [Test]
        public void Property()
        {
            Assert.AreEqual("3", "{0:@Length}".FormatString("ABC"));
        }

        [Test]
        public void Field()
        {
            Assert.AreEqual("1", "{0:@IntField}".FormatString(new TestCallData() { IntField = 1 }));
        }

        [Test]
        public void MultiCall()
        {
            Assert.AreEqual("bc", "{0:@Substring(1,2)@ToLower}".FormatString("ABC"));
        }

        public class TestCallData
        {
            public int IntField;
            public int IntProperty { get; set; }

            public static int StaticIntField;
            public static int StaticIntProperty { get; set; }

        }

        [Test]
        public void Type()
        {
            TestCallData.StaticIntField = 1;
            //Assert.AreEqual("1", "{0:@@StaticIntField}".FormatString(typeof(TestCallData)));
            Assert.AreEqual("1", "{0:@StaticIntField}".FormatString(typeof(TestCallData)));

        }

        [Test]
        public void Type_String_Empty()
        {
            Assert.AreEqual(string.Empty, "{0:@Empty}".FormatString(typeof(string)));
        }
        [Test]
        public void Type_Int_MaxValue()
        {
            Assert.AreEqual(int.MaxValue.ToString(), "{0:@MaxValue}".FormatString(typeof(int)));
        }
        //[Test]
        //public void TestCallFormatString_Null()
        //{
        //    CallStringFormatProvider formatProvider = new CallStringFormatProvider();
        //    Assert.AreEqual("", string.Format(formatProvider, "{0:@string.ToLower()}", null));
        //}
        //[Test]
        //public void TestCallFormatString_Null_ToString()
        //{
        //    CallStringFormatProvider formatProvider = new CallStringFormatProvider();
        //    Assert.AreEqual("", string.Format(formatProvider, "{0:@string.ToString()}", null));
        //}

        [Test]
        public void TestDefaut()
        {
            Assert.AreEqual(DateTime.Now.Year.ToString(), "{0:yyyy}".FormatString(DateTime.Now));

            Assert.AreEqual(DateTime.Now.Year.ToString(), "{0:@Now,yyyy}".FormatString(typeof(DateTime)));
            
        }

        [Test]
        public void GetProperty()
        {
            Assert.AreEqual("3", "{0:@Length}".FormatString("abc"));
            
        }

        

            [Test]
        public void Regex_Method2()
        {
            Regex regex = new Regex("\\((.*?)(?<!\\\\)\\)");
            Match m;
            m=regex.Match("()");
            Assert.AreEqual("", m.Groups[1].Value);
            m = regex.Match("(\\()");
            Assert.AreEqual("\\(", m.Groups[1].Value);
            Assert.AreEqual("(", m.Groups[1].Value.Replace("\\(","("));
            m = regex.Match("(\\))");
            Assert.AreEqual("\\)", m.Groups[1].Value);
            m = regex.Match("(abc\\))");
            Assert.AreEqual("abc\\)", m.Groups[1].Value);
            Assert.AreEqual("abc)", m.Groups[1].Value.Replace("\\)",")"));
        }

            static Regex methodRegex = new Regex("\\s*(?<action>@{1,2})\\s*(?<name>[^\\s\\(,]+)\\s*(\\((?<param>(.*?)(?<!\\\\))\\))?\\s*(,(?<format>.*))?\\s*");

        [Test]
        public void Regex_Method()
        {
            Match m;
            m = methodRegex.Match("@call('a','b')");
            Assert.IsTrue(m.Success);
            Assert.AreEqual("call", m.Groups["name"].Value);
            Assert.AreEqual("'a','b'", m.Groups["param"].Value);

            m = methodRegex.Match("@call('abc\\)')");
            Assert.IsTrue(m.Success);
            Assert.AreEqual("call", m.Groups["name"].Value);
            Assert.AreEqual("'abc\\)'", m.Groups["param"].Value);
        }

    }
}
