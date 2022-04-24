using Osiris.Utilities.Validation;
using UnityEngine;

namespace Osiris.Utilities
{
    public class RandomCoroutineTimerValidator : IValidator<float, float>
    {
        public bool IsValid(float minDuration, float maxDuration)
        {
            if (minDuration == 0)
            {
                Debug.LogWarning("Minimum duration value is zero. Check that this is intended.");
                return false;
            }

            if (maxDuration == 0)
            {
                Debug.LogError("Maximum duration value is zero.");
                return false;
            }

            if (maxDuration < minDuration)
            {
                Debug.LogError("Maximum duration is less than the minimum duration.");
                return false;
            }

            return true;
        }
    }
}
