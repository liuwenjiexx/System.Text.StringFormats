using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace System.Text.StringFormats.Tests
{
    public class TestStringFormat
    {

        //[Test]
        //public void This()
        //{
        //    Assert.AreEqual("abc", string.Format("{0:$}", "abc"));
        //}
        //[Test]
        //public void This0()
        //{
        //    Assert.AreEqual("abc", string.Format("{0:$}", "abc"));
        //}
        //[Test]
        //public void This0_1()
        //{
        //    Assert.AreEqual("abc 123", string.Format("{1:$0} {0:$1}", "abc","123"));
        //}

        [Test]
        public void This()
        {
            Assert.AreEqual("False", "{0:@String.IsNullOrEmpty($)}".FormatString("abc"));
        }
        [Test]
        public void Ref_1()
        {
            Assert.AreEqual("abc", "{0:$}".FormatString("abc", "123"));
        }
        [Test]
        public void Ref_2()
        {
            Assert.AreEqual("123", "{1:$}".FormatString("abc", "123"));
        }
        [Test]
        public void Ref0()
        {
            Assert.AreEqual("abc", "{0:$0}".FormatString("abc", "123"));
        }

        [Test]
        public void Ref1()
        {
            Assert.AreEqual("123", "{0:$1}".FormatString("abc", "123"));
        }
        [Test]
        public void Ref0_ToUpper()
        {
            Assert.AreEqual("ABC", "{0:$0.ToUpper}".FormatString("abc", "def"));
        }
        [Test]
        public void Ref1_ToUpper()
        {
            Assert.AreEqual("DEF", "{0:$1.ToUpper}".FormatString("abc", "def"));
        }

        [Test]
        public void Ref_Join_0_1()
        {
            Assert.AreEqual("abc_123", "{0:@String.Join(_,$0,$1)}".FormatString("abc", "123"));

        }
    }
}