using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace System.Text.StringFormats.Tests
{
    public class TestRegex
    {
        [Test]
        public void TestRegexFormatString()
        {
            RegexStringFormatProvider formatProvider = new RegexStringFormatProvider();
            Assert.AreEqual("hello world", "{0:/(?<result>h.*d)/}".FormatString("say hello world ."));
        }

        [Test]
        public void Path()
        {
            RegexStringFormatProvider formatProvider = new RegexStringFormatProvider();
            Assert.AreEqual("sub", "{0:/dir/(?<result>[^/]+)//}".FormatString("dir/sub/file.txt"));
        }

    }
}