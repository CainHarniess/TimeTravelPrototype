namespace Osiris.Utilities.Extensions
{
    public static class BoolExtensions
    {
        public static void ChangeStatus(this ref bool status, bool value)
        {
            if (status == value)
            {
                return;
            }
            status = value;
        }
    }
}
