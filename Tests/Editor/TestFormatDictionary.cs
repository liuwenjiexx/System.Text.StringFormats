using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace System.Text.StringFormats.Tests
{
    public class TestFormatDictionary
    {
        string path = "dir/sub/file.txt";
        [Test]
        public void TestDictionary()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["Path"] = "parent/child/file.txt";
            Assert.AreEqual("file.txt", "{$Path:#FileName}".FormatStringWithKey(dic));
        }

        [Test]
        public void TestCall()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["Text"] = "ABC";
            Assert.AreEqual("abc", "{$Text:@ToLower}".FormatStringWithKey(dic));
        }

        [Test]
        public void Offset_FormatStringWithKey()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values["Path"] = path;

            Assert.AreEqual("sub", "{$Path:#FileName,-1}".FormatStringWithKey(values));
            Assert.AreEqual("dir", "{$Path:#FileName,-2}".FormatStringWithKey(values));
            Assert.AreEqual("sub/file.txt", "{$Path:#FilePath,1}".FormatStringWithKey(values));
            Assert.AreEqual("sub", "{$Path:#FileName,-1,1}".FormatStringWithKey(values));
            Assert.AreEqual("dir", "{$Path:#DirectoryName,-1}".FormatStringWithKey(values));
            Assert.AreEqual("sub", "{$Path:#DirectoryName,1}".FormatStringWithKey(values));

        }
    }
}