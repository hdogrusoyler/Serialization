using ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        private readonly Program _service;

        public UnitTest()
        {
            _service = new Program();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Program p = new Program();
            //p.TestCase1();
            //p.TestCase2();
            //p.TestCase3();
            //Assert.AreEqual();

            try
            {
                _service.TestCase1();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }

        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                _service.TestCase2();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            try
            {
                _service.TestCase3();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }
        }
    }
}
