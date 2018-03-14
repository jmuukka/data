using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class ObjectFactoryUnitTest
    {
        [TestMethod]
        public void CreateInstance_WithParameterlessPublicConstructor_InstanceCreated()
        {
            var actual = ObjectFactory.CreateInstance<ClassWithParameterlessPublicConstructor>();

            Assert.AreSame(typeof(ClassWithParameterlessPublicConstructor), actual.GetType());
        }

        [TestMethod]
        public void CreateInstance_WithParameterlessPrivateConstructor_InstanceCreated()
        {
            var actual = ObjectFactory.CreateInstance<ClassWithParameterlessPrivateConstructor>();

            Assert.AreEqual(typeof(ClassWithParameterlessPrivateConstructor), actual.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMethodException))]
        public void CreateInstance_WithPublicConstructorWithParameter_ThrowsMissingMethodException()
        {
            ObjectFactory.CreateInstance<ClassWithPublicConstructorWithParameter>();
        }

        [TestMethod]
        public void CreateInstance_WithTypeParameter_WithParameterlessPublicConstructor_InstanceCreated()
        {
            var actual = ObjectFactory.CreateInstance(typeof(ClassWithParameterlessPublicConstructor));

            Assert.AreSame(typeof(ClassWithParameterlessPublicConstructor), actual.GetType());
        }

        [TestMethod]
        public void CreateInstance_WithTypeParameter_WithParameterlessPrivateConstructor_InstanceCreated()
        {
            var actual = ObjectFactory.CreateInstance(typeof(ClassWithParameterlessPrivateConstructor));

            Assert.AreEqual(typeof(ClassWithParameterlessPrivateConstructor), actual.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMethodException))]
        public void CreateInstance_WithTypeParameter_WithPublicConstructorWithParameter_ThrowsMissingMethodException()
        {
            ObjectFactory.CreateInstance(typeof(ClassWithPublicConstructorWithParameter));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateInstance_WithNullTypeParameter_ThrowsArgumentNullException()
        {
            ObjectFactory.CreateInstance(null);
        }
    }
}
