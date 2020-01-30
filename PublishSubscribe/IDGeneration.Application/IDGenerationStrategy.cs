using IDGeneration.Common.Exceptions;
using System;

namespace IDGeneration.Application
{
    public class IDGenerationStrategy
    {
        private readonly long _sequenceMask;
        private readonly long _nodeMask;
        private readonly long _idGeneratorMask;
        private readonly object _syncObject = new object();

        private int _lastSequence = 0;
        private long _lastTime = -1;


        public byte TimeStampBits { get; }

        public byte NodeIdBits { get; }

        public byte SequenceBits { get; }

        public int NodeId { get; }

        public int TotalBits { get { return TimeStampBits + NodeIdBits + SequenceBits; } }

        public IDGenerationStrategy(byte timestampBits, byte nodeIdBits, byte sequenceBits, int nodeId)
        {
            // TODO Validation
            TimeStampBits = timestampBits;
            NodeIdBits = nodeIdBits;
            SequenceBits = sequenceBits;
            NodeId = nodeId;

            _idGeneratorMask = GetMask(TimeStampBits);
            _nodeMask = GetMask(NodeIdBits);
            _sequenceMask = GetMask(SequenceBits);
        }

        private static long GetMask(byte bits) => (1L << bits) - 1;

        public long GenerateId()
        {
            lock (_syncObject)
            {
                //TODO Configure timesource
                var ticks = DateTime.UtcNow.Ticks;
                var timestamp = ticks & _idGeneratorMask;

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

                return (timestamp << (this.NodeIdBits + this.SequenceBits)) + (this.NodeId << this.SequenceBits) + _lastSequence;

            }
        }
    }
}
