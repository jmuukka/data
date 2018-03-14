using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class DbTypeResolversUnitTest
    {
        [TestMethod]
        public void Instance_ContainsDbTypeResolver()
        {
            var sut = DbTypeResolvers.Instance;

            Assert.AreEqual(1, sut.Count);
            Assert.AreSame(typeof(DbTypeResolver), sut[0].GetType());
        }
    }
}
