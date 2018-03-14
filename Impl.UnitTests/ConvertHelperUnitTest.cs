using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class ConvertHelperUnitTest
    {
        [TestMethod]
        public void ChangeType_FromString_ToInt32()
        {
            var actual = ConvertHelper.ChangeType<int>("5");

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void ChangeType_FromString_ToDecimal()
        {
            var actual = ConvertHelper.ChangeType<decimal>("15.6");

            Assert.AreEqual(15.6m, actual);
        }
    }
}
