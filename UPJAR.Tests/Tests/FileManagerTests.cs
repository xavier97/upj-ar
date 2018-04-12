
using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace UPJAR.Tests
{
    [TestFixture]
    public class FileManagerTests
    {
        [Test]
        public void Pass()
        {
            Assert.True(true);
        }

        [Test]
        public void Fail()
        {
            Assert.False(true);
        }

        [Test]
        [Ignore("another time")]
        public void Ignore()
        {
            Assert.True(false);
        }
    }
}
