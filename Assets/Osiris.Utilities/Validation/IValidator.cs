namespace Osiris.Utilities.Validation
{
    public interface IValidator
    {
        bool IsValid();
    }

    public interface IValidator<T>
    {
        bool IsValid(T parameter);
    }

    public interface IValidator<T, U>
    {
        bool IsValid(T parameter1, U parameter2);
    }
}