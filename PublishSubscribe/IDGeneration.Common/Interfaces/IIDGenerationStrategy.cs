namespace IDGeneration.Common.Interfaces
{
    public interface IIDGenerationStrategy<out TResult>
    {
        TResult GenerateId();
    }
}
