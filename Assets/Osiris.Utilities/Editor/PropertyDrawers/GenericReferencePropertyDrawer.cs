using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Editor
{
    public abstract class GenericReferencePropertyDrawer<T> : PropertyDrawer
    {
        private const string _useConstantPropName = "_UseConstant";
        private const string _variableValuePropName = "_VariableValue";
        private const string _constantPropName = "_Constant";

        protected string VariableValuePropertyName => _variableValuePropName;

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            bool useConstant = property.FindPropertyRelative(_useConstantPropName).boolValue;

            Rect propertyRect = EditorGUI.PrefixLabel(position,
                                                      GUIUtility.GetControlID(FocusType.Passive),
                                                      label);
            Rect dropDownRect = DrawDropDown(property, useConstant, propertyRect);
            DrawValueField(property, useConstant, propertyRect, dropDownRect);

            EditorGUI.EndProperty();
        }

        private Rect DrawDropDown(SerializedProperty property, bool useConstant, Rect propertyRect)
        {
            Rect dropDownRect = new Rect(propertyRect.position, 15 * Vector2.one);
            var dropDownStyle = new GUIStyle()
            {
                fixedWidth = 100f,
                border = new RectOffset(1, 1, 1, 1),
            };

            if (EditorGUI.DropdownButton(dropDownRect,
                                         new GUIContent(GetTexture()),
                                         FocusType.Keyboard,
                                         dropDownStyle))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Constant"),
                             useConstant,
                             () => SetProperty(property, true));
                menu.AddItem(new GUIContent("Variable"),
                             !useConstant,
                             () => SetProperty(property, false));
                menu.ShowAsContext();
            }

            return dropDownRect;
        }

        private void DrawValueField(SerializedProperty property, bool useConstant, Rect propertyRect, Rect dropDownRect)
        {
            Rect valueRect = GetValueRect(propertyRect, dropDownRect);
            T variableValue = FindRelativeProperty(property);
            if (useConstant)
            {
                EditorGUI.ObjectField(valueRect,
                                      property.FindPropertyRelative(_constantPropName),
                                      GUIContent.none);
            }
            else
            {
                SetRelativeProperty(property, valueRect, variableValue);
            }
        }

        protected abstract void SetRelativeProperty(SerializedProperty property, Rect valueRect, T variableValue);

        protected abstract T FindRelativeProperty(SerializedProperty property);

        private Rect GetValueRect(Rect propertyRect, Rect dropDownRect)
        {
            Vector2 valueRectPosition = dropDownRect.position + dropDownRect.width * Vector2.right;
            Vector2 valueRectSize = propertyRect.size - dropDownRect.width * Vector2.right;
            Rect valueRect = new Rect(valueRectPosition, valueRectSize);
            return valueRect;
        }

        private Texture GetTexture()
        {
            return Resources.FindObjectsOfTypeAll(typeof(Texture))
                            .Where(t => t.name.ToLower()
                                              .Contains("arrow"))
                            .Cast<Texture>()
                            .FirstOrDefault();
        }

        private void SetProperty(SerializedProperty property, bool value)
        {
            var propRelative = property.FindPropertyRelative(_useConstantPropName);
            propRelative.boolValue = value;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
