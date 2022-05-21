using UnityEditor;

namespace Osiris.Utilities.Editor.Validation
{
    public class SuccessInputValidationResult : InputValidationResult
    {
        public SuccessInputValidationResult(string message, MessageType messageType = MessageType.None)
            : base(true, message, messageType)
        {

        }

        public SuccessInputValidationResult() : base(true, string.Empty, MessageType.None)
        {

        }
    }
}





