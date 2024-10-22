using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace System.Text.StringFormats.Tests
{
    public class TestPath
    {
        string path = "dir/sub/file.txt";
        NameStringFormatProvider formatProvider = new NameStringFormatProvider();

        [Test]
        public void FileName()
        {            
            Assert.AreEqual("file.txt", string.Format(formatProvider, "{0:#FileName}", path));
        }
        [Test]
        public void FileExtension()
        {
            Assert.AreEqual(".txt", string.Format(formatProvider, "{0:#FileExtension}", path));
        }
        [Test]
        public void FileNameWithoutExtension()
        {
            Assert.AreEqual("file", string.Format(formatProvider, "{0:#FileNameWithoutExtension}", path));
        }
        [Test]
        public void DirectoryName()
        {
            Assert.AreEqual("sub", string.Format(formatProvider, "{0:#DirectoryName}", path));
        }
        [Test]
        public void DirectoryPath()
        {
            Assert.AreEqual("dir/sub", string.Format(formatProvider, "{0:#DirectoryPath}", path));
        }
        [Test]
        public void FullPath()
        {
            Assert.AreEqual(Path.GetFullPath(path).Replace('\\', '/'), string.Format(formatProvider, "{0:#FullPath,/}", path));
        }
        [Test]
        public void FullDirectoryPath()
        {
            Assert.AreEqual(Path.GetDirectoryName(Path.GetFullPath(path)).Replace('\\', '/'), string.Format(formatProvider, "{0:#FullDirectoryPath,/}", path));
        }
        [Test]
        public void SeparatorChar()
        {
            Assert.AreEqual("dir\\sub", string.Format(formatProvider, "{0:#DirectoryPath,\\}", path));
            Assert.AreEqual("dir/sub", string.Format(formatProvider, "{0:#DirectoryPath,/}", "dir\\sub\\file.txt"));
        }

        [Test]
        public void Offset()
        {
            Assert.AreEqual("sub", string.Format(formatProvider, "{0:#FileName,-1}", path));
            Assert.AreEqual("dir", string.Format(formatProvider, "{0:#FileName,-2}", path));
            Assert.AreEqual("sub/file.txt", string.Format(formatProvider, "{0:#FilePath,1}", path));
            Assert.AreEqual("sub", string.Format(formatProvider, "{0:#FileName,-1,1}", path));

            Assert.AreEqual("dir", string.Format(formatProvider, "{0:#DirectoryName,-1}", path));
            Assert.AreEqual("sub", string.Format(formatProvider, "{0:#DirectoryName,1}", path));

        }

        [Test]
        public void FirstDirectoryName()
        {
            Assert.AreEqual("dir", string.Format(formatProvider, "{0:#FirstDirectoryName}", path));
            Assert.AreEqual("sub", string.Format(formatProvider, "{0:#FirstDirectoryName,1}", path));
        }
    }
}
