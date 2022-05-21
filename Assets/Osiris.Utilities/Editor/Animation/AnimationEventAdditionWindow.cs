using Osiris.Utilities.Editor.Validation;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Editor.Animation
{
    public class AnimationEventAdditionWindow : EditorWindow
    {
        private AddAnimationEventValidator _validator;
        private SerializedObject _window;
        private SerializedProperty _functionNameProp;
        private SerializedProperty _clipArrayProp;
        private SerializedProperty _StrategyProp;

        [Tooltip("The name of the event that will be added to the clip.")]
        [SerializeField] private string _FunctionName;

        [Tooltip("The animation clips to be updated.")]
        [SerializeField] private  AnimationClip[] _ClipsToUpdate = new AnimationClip[0];

        [Tooltip("The animation event addition strategy")]
        [SerializeField] private AnimationEventAdditionUtility _AdditionMethod;

        public string FunctionName { get => _FunctionName; }
        public AnimationClip[] ClipsToUpdate { get => _ClipsToUpdate; }
        public AnimationEventAdditionUtility AdditionMethod { get => _AdditionMethod; }

        [MenuItem("Utilities/Animation/Animation Event Addition")]
        public static void ShowWindow()
        {
            GetWindow<AnimationEventAdditionWindow>("Animation Event Addition");
        }
        void OnEnable()
        {
            _window = new SerializedObject(this);
            _functionNameProp = _window.FindProperty(nameof(_FunctionName));
            _clipArrayProp = _window.FindProperty(nameof(_ClipsToUpdate));
            _StrategyProp = _window.FindProperty(nameof(_AdditionMethod));

            _validator = new AddAnimationEventValidator(this);
        }

        private void OnGUI()
        {
            EditorGUILayout.PropertyField(_clipArrayProp, true);
            EditorGUILayout.PropertyField(_functionNameProp);
            EditorGUILayout.PropertyField(_StrategyProp);

            InputValidationResult result = _validator.Validate();
            if (!result.IsValid)
            {
                GUI.enabled = false;
            }

            if (GUILayout.Button("Add event to clips"))
            {
                Execute();
            }
            GUI.enabled = true;

            if (!result.IsValid)
            {
                EditorGUILayout.HelpBox(result.Message, result.MessageType, false);
            }

            _window.ApplyModifiedProperties();
        }

        private void Execute()
        {
            for (int i = 0; i < _clipArrayProp.arraySize; i++)
            {
                SerializedProperty clipProperty = _clipArrayProp.GetArrayElementAtIndex(i);

                if (!(clipProperty.objectReferenceValue is AnimationClip clip))
                {
                    Debug.LogError($"Array element at index {i} is not of type {nameof(AnimationClip)}");
                    continue;
                }

                _AdditionMethod.AddAnimationEvent(clip, _FunctionName);
            }
        }
    }
}