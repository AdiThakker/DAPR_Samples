using IDGeneration.Common.Interfaces;
using System;

namespace IDGeneration.Application
{
    public class IDGenerator<TResult>
    {

        public IIDGenerationStrategy<TResult> IDGenerationStrategy { get; }

        public IDGenerator(int nodeId, IIDGenerationStrategy<TResult> idGenerationStrategy) => this.IDGenerationStrategy = idGenerationStrategy ?? throw new System.ArgumentNullException(nameof(idGenerationStrategy));

        public TResult GenerateId() => this.IDGenerationStrategy.GenerateId();
    }
}
