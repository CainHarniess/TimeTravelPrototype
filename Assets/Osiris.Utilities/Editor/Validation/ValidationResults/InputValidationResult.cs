using UnityEditor;

namespace Osiris.Utilities.Editor.Validation
{
    public class InputValidationResult
    {
        public InputValidationResult(bool isValid, string message, MessageType messageType = MessageType.None)
        {
            IsValid = isValid;
            Message = message;
            MessageType = messageType;
        }

        public bool IsValid { get; private set; }
        public string Message { get; private set; }
        public MessageType MessageType { get; private set; }
    }
}





