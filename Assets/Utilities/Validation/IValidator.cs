namespace Osiris.Utilities.Validation
{
    public interface IValidator
    {
        bool IsValid();
    }

    public interface IValidator<T>
    {
        bool IsValid(T weightChange);
    }
}