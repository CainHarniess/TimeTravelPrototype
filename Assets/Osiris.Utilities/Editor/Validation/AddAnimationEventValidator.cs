using Osiris.Utilities.Editor.Animation;

namespace Osiris.Utilities.Editor.Validation
{
    public class AddAnimationEventValidator
    {
        private AnimationEventAdditionWindow _editorWindow;

        public AddAnimationEventValidator(AnimationEventAdditionWindow editorWindow)
        {
            _editorWindow = editorWindow;
        }

        public InputValidationResult Validate()
        {
            if (_editorWindow.FunctionName.Length == 0)
            {
                return new FailedInputValidationResult("A function name must be provided.");
            }

            if (_editorWindow.ClipsToUpdate.Length == 0)
            {
                return new FailedInputValidationResult("The animation clips to be updated must be specified.");
            }

            if (_editorWindow.AdditionMethod == null)
            {
                return new FailedInputValidationResult("The event addition method must be specified.");
            }

            return new SuccessInputValidationResult("The event addition method must be specified.");
        }
    }
}