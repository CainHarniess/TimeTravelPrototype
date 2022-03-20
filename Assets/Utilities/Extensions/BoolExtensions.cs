using System;

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

        public static void ChangeStatusWithException(this ref bool status, bool value)
        {
            if (status == value)
            {
                throw new ArgumentException("Current value is equal to the new value. Check"
                                            + "the use of ChangeStatusWithException.");
            }
            status = value;
        }
    }
}
