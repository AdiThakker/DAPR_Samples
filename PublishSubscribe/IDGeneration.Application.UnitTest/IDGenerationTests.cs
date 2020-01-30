using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IDGeneration.Application.UnitTest
{
    [TestClass]
    public class IDGenerationTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Verify_FlakeId_Generation_Throws_Invalid_Exception_For_Max_Integer_Value()
        {
            new FlakeIDGenerationStrategy(int.MaxValue);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Verify_FlakeId_Generation_Throws_Invalid_Exception_For_Min_Integer_Value()
        {
            new FlakeIDGenerationStrategy(int.MinValue);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Verify_FlakeId_Generation_Throws_Invalid_Exception_For_Integer_Value_2048()
        {
            FlakeIDGenerationStrategy idGeneration = new FlakeIDGenerationStrategy(2048);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Verify_FlakeId_Generation_Accepts_Integer_Value_Below_2047_And_Above_0()
        {
            new FlakeIDGenerationStrategy(2047);
            new FlakeIDGenerationStrategy(1);
            Assert.IsTrue(true);
        }
    }
}
