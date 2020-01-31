using IDGeneration.Common.Exceptions;
using IDGeneration.Common.Interfaces;
using System;

namespace IDGeneration.Application
{
    public class FlakeIDGenerationStrategy : IIDGenerationStrategy
    {
        private readonly long _sequenceMask;
        private readonly long _groupMask;
        private readonly long _timeMask;
        private readonly object _syncObject = new object();
        public readonly DateTime DefaultEpoch = new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private int _lastSequence = 0;
        private long _lastTime = -1;


        public byte TimeStampBits { get => 42; }

        public byte GroupIdBits { get => 11; }

        public byte SequenceBits { get => 10; }

        public int NodeId { get; }

        public Func<long> GetTicks { get; }

        public int TotalBits { get { return TimeStampBits + GroupIdBits + SequenceBits; } }

        public FlakeIDGenerationStrategy(int nodeId, Func<long> TimeSourceGeneration = null)
        {
            _timeMask = GetMask(TimeStampBits);
            _groupMask = GetMask(GroupIdBits);
            _sequenceMask = GetMask(SequenceBits);

            if (nodeId > _groupMask || nodeId < 1)
                throw new InvalidOperationException($"Invalid nodeId. Cannot exceed {_groupMask}");

            NodeId = nodeId;

            GetTicks = TimeSourceGeneration ?? new Func<long>(() => DateTime.UtcNow.Subtract(DefaultEpoch).Ticks); 

        }

        private static long GetMask(byte bits) => (1L << bits) - 1;

        public long GenerateId()
        {
            lock (_syncObject)
            {
                var ticks = GetTicks();
                var timestamp = ticks & _timeMask;

                if (timestamp < _lastTime)
                    throw new InvalidSystemClockException($"Invalid Clock tick.");

                if (timestamp == _lastTime)
                {
                    if (_lastSequence < _sequenceMask)
                        _lastSequence++;
                    else
                        throw new SequenceOverflowException("Sequence overflow.");
                }
                else
                {
                    _lastSequence = 0;
                    _lastTime = timestamp;
                }

                return (timestamp << (this.GroupIdBits + this.SequenceBits)) + (this.NodeId << this.SequenceBits) + _lastSequence;

            }
        }
    }
}
