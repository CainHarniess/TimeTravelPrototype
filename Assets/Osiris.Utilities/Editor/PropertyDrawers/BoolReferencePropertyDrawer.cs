using Osiris.Utilities.References;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class BoolReferencePropertyDrawer : GenericReferencePropertyDrawer<bool>
    {
        protected override bool FindRelativeProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(VariableValuePropertyName).boolValue;
        }

        protected override void SetRelativeProperty(SerializedProperty property, Rect valueRect, bool variableValue)
        {
            EditorGUI.Toggle(valueRect, variableValue);
            property.FindPropertyRelative(VariableValuePropertyName).boolValue = variableValue;
        }
    }
}
