using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace System.Text.StringFormats.Tests
{
    public class TestComposite
    {

        [Test]
        public void TesDic()
        {
            RegexStringFormatProvider formatProvider = new RegexStringFormatProvider(new NameStringFormatProvider());
            Dictionary<string, object> values = new Dictionary<string, object>();
            values["Path"] = "parent/child/file.txt";
            Assert.AreEqual("file", "{$Path:#FileName:/(?<result>.*)\\./}".FormatStringWithKey(values));
        }

        [Test]
        public void TestRegex()
        {
            Assert.AreEqual("file", "{0:#FileName:/(?<result>.*)\\./}".FormatString("parent/child/file.txt"));
        }
    }
}