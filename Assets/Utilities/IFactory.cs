namespace Osiris.Utilities
{
    public interface IFactory<T>
    {
        T Create();
    }

    public interface IFactory<T, U>
    {
        T Create(U parameter);
    }
}