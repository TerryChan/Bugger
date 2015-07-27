﻿using Bugger.Base.PlugIns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bugger.Domain.Test.PlugIns
{
    [TestClass]
    public class PlugInBaseTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var plugIn = new MockPlugIn(new Guid("1dc425b3-c27b-46ba-9623-a046d1acc754"), PlugInType.TrackingSystem);

            Assert.AreEqual("1dc425b3-c27b-46ba-9623-a046d1acc754", plugIn.Guid.ToString());
            Assert.AreEqual(PlugInType.TrackingSystem, plugIn.PlugInType);
            Assert.IsFalse(plugIn.IsInitialized);
        }

        [TestMethod]
        public void InitializeTest()
        {
            var plugIn = new MockPlugIn(new Guid("1dc425b3-c27b-46ba-9623-a046d1acc754"), PlugInType.TrackingSystem);
            var isInitializeCoreCalled = false;
            plugIn.InitializeCoreAction = () => isInitializeCoreCalled = true;
            plugIn.Initialize();

            Assert.IsTrue(isInitializeCoreCalled);
            Assert.IsTrue(plugIn.IsInitialized);

            isInitializeCoreCalled = false;
            plugIn.Initialize();

            Assert.IsFalse(isInitializeCoreCalled);
            Assert.IsTrue(plugIn.IsInitialized);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InitializeTest_Exception()
        {
            var plugIn = new MockPlugIn(new Guid("1dc425b3-c27b-46ba-9623-a046d1acc754"), PlugInType.TrackingSystem);
            plugIn.InitializeCoreAction = () => { throw new NotSupportedException(); };
            plugIn.Initialize();
        }
    }
}