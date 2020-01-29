using IDGeneration.Common.Entities;
using System.Collections.Concurrent;

namespace IDGeneration.Application
{
    public class IDGenerator
    {
        private static readonly ConcurrentDictionary<string, IDGenerator> _namedgenerators = new ConcurrentDictionary<string, IDGenerator>();

        public IDGenerator(IDGenerationStrategy idGenerationStrategy)
        {
            if (idGenerationStrategy is null)
            {
                throw new System.ArgumentNullException(nameof(idGenerationStrategy));
            }
        }
    }
}
