using IDGeneration.Common.Exceptions;
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
            Assert.IsNotNull(new IDGenerator<long>(100, new FlakeIDGenerationStrategy(1)).IDGenerationStrategy);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSystemClockException))]
        public void Verify_GenerateId_Throws_Exception_When_Clock_Moves_Back()
        {
            var timestamp = DateTime.UtcNow.Ticks;
            var idGenerationStrategy = new FlakeIDGenerationStrategy(100, () => timestamp);
            var idGenerator = new IDGenerator<long>(100, idGenerationStrategy);
            idGenerator.GenerateId();
            timestamp = new DateTime(timestamp).AddMilliseconds(-1).Ticks;
            idGenerator.GenerateId();
            Assert.IsTrue(true);

        }

        [TestMethod]
        public void Verify_GenerateIds_Increase_In_Sequence()
        {
            var timestamp = DateTime.UtcNow.Ticks;
            var idGenerationStrategy = new FlakeIDGenerationStrategy(100, () => timestamp);
            var idGenerator = new IDGenerator<long>(100, idGenerationStrategy);
            var id1 = idGenerator.GenerateId();
            timestamp = new DateTime(timestamp).AddMilliseconds(1).Ticks;
            var id2 = idGenerator.GenerateId();
            Assert.IsTrue(id2 > id1);
        }

        [TestMethod]
        public void Verify_GenerateIds_Increase_In_Sequence_When_TimeStamp_Same()
        {
            var timestamp = DateTime.UtcNow.Ticks;
            var idGenerationStrategy = new FlakeIDGenerationStrategy(100, () => timestamp);
            var idGenerator = new IDGenerator<long>(100, idGenerationStrategy);
            var id1 = idGenerator.GenerateId();
            var id2 = idGenerator.GenerateId();
            Assert.IsTrue(id2 > id1);
        }

        [TestMethod]
        public void Verify_IDGeneration_Is_Not_Null_When_Using_CustomIDGenerator()
        {
            var customIdGeneration = new CustomIDGenerationStrategy();
            Assert.IsInstanceOfType(new IDGenerator<int>(100, customIdGeneration).IDGenerationStrategy, typeof(CustomIDGenerationStrategy));
            Assert.IsTrue(customIdGeneration.GenerateId() == 100);
        }

        [TestMethod]
        public void Verify_NodeId_From_Generated_Matches()
        {
            var timestamp = DateTime.UtcNow.Ticks;
            var idGenerationStrategy = new FlakeIDGenerationStrategy(100, () => timestamp);
            var idGenerator = new IDGenerator<long>(100, idGenerationStrategy);
            var id = idGenerator.GenerateId();
            Assert.IsTrue(idGenerationStrategy.GetNodeFromId(id).Equals(100));
        }

        [TestMethod]
        public void Verify_Sequence_From_Generated_Matches()
        {
            var timestamp = DateTime.UtcNow.Ticks;
            var idGenerationStrategy = new FlakeIDGenerationStrategy(100, () => timestamp);
            var idGenerator = new IDGenerator<long>(100, idGenerationStrategy);
            var id = idGenerator.GenerateId();
            Assert.IsTrue(idGenerationStrategy.GetSequenceFromId(id).Equals(1));

            for (int i = 0; i < 9; i++)
            {
                id = idGenerator.GenerateId();
            }
            Assert.IsTrue(idGenerationStrategy.GetSequenceFromId(id).Equals(10));

        }

    }

    internal class CustomIDGenerationStrategy : IIDGenerationStrategy<int>
    {
        public int GenerateId() => 100;
    }
}
