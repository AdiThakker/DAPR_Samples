using IDGeneration.Common.Interfaces;

namespace IDGeneration.Application
{
    public class IDGenerator
    {

        public IIDGenerationStrategy IDGenerationStrategy { get; }

        public IDGenerator(int nodeId) => this.IDGenerationStrategy = new FlakeIDGenerationStrategy(nodeId);

        public IDGenerator(int nodeId, IIDGenerationStrategy idGenerationStrategy) => this.IDGenerationStrategy = idGenerationStrategy ?? throw new System.ArgumentNullException(nameof(idGenerationStrategy));

        public long GenerateId() => this.IDGenerationStrategy.GenerateId();
    }
}
