using IDGeneration.Common.Interfaces;
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
            new FlakeIDGenerationStrategy(2048);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Verify_FlakeId_Generation_Accepts_Integer_Value_Below_2047_And_Above_0()
        {
            new FlakeIDGenerationStrategy(2047);
            new FlakeIDGenerationStrategy(1);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Verify_IDGenerationStrategy_Is_Not_Null()
        {
            Assert.IsNotNull(new IDGenerator(100).IDGenerationStrategy);
        }

        [TestMethod]
        public void Verify_IDGenerationStrategy_Default_IsFlakeIDGeneration()
        {
            Assert.IsInstanceOfType(new IDGenerator(100).IDGenerationStrategy, typeof(FlakeIDGenerationStrategy));
        }

        [TestMethod]
        public void Verify_IDGeneration_Is_Not_Null_When_Using_CustomIDGenerator()
        {
            var customIdGeneration = new CustomIDGenerationStrategy();
            Assert.IsInstanceOfType(new IDGenerator(100, customIdGeneration).IDGenerationStrategy, typeof(CustomIDGenerationStrategy));
            Assert.IsTrue(customIdGeneration.GenerateId() == 100L);
        }

    }

    internal class CustomIDGenerationStrategy : IIDGenerationStrategy
    {
        public long GenerateId()
        {
            return 100L;
        }
    }
}
