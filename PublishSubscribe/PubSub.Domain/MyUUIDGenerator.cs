using System.Threading.Tasks;

namespace PubSub.Domain
{
    public class MyUUIDGenerator
    {
        public object JsonConvert { get; private set; }

        public async Task<int> GenerateUniqueId(int nodeId)
        {
            //TODO logic to generate Unique Id based on Node
            return await Task.FromResult(1);

        }
    }
}
