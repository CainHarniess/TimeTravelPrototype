namespace Osiris.Utilities
{
    public interface IFilter<T>
    {
        bool Condition(T input);
    }
}
