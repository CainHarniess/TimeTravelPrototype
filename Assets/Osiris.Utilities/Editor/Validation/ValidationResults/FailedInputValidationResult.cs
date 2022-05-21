using UnityEditor;

namespace Osiris.Utilities.Editor.Validation
{
    public class FailedInputValidationResult : InputValidationResult
    {
        public FailedInputValidationResult(string message, MessageType messageType = MessageType.Info)
            : base(false, message, messageType)
        {

        }
    }
}





