namespace IDGeneration.Common.Entities
{
    public class IDGenerationStrategy
    {
        public byte TimeStampBits { get; }

        public byte NodeIdBits { get; }

        public byte SequenceBits { get; }

        public int TotalBits { get { return TimeStampBits + NodeIdBits + SequenceBits; } }

        public IDGenerationStrategy(byte timestampBits, byte nodeIdBits, byte sequenceBits)
        {
            TimeStampBits = timestampBits;
            NodeIdBits = nodeIdBits;
            SequenceBits = sequenceBits;
        }
    }
}
