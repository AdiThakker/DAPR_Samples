using IDGeneration.Common.Interfaces;

namespace IDGeneration.Application
{
    public class IDGenerator
    {
        // private static readonly ConcurrentDictionary<string, IDGenerator> _namedgenerators = new ConcurrentDictionary<string, IDGenerator>();

        public IIDGenerationStrategy IDGenerationStrategy { get; }

        public IDGenerator(int nodeId)
        {
            this.IDGenerationStrategy = new FlakeIDGenerationStrategy(nodeId);
        }

        public IDGenerator(IIDGenerationStrategy idGenerationStrategy)
        {
            if (idGenerationStrategy is null)
            {
                throw new System.ArgumentNullException(nameof(idGenerationStrategy));
            }
        }

        public long GenerateId() => this.IDGenerationStrategy.GenerateId();
    }
}   
