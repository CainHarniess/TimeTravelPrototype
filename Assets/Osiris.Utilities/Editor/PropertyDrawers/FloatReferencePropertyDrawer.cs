using Osiris.Utilities.References;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferencePropertyDrawer : GenericReferencePropertyDrawer<float>
    {
        protected override float FindRelativeProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(VariableValuePropertyName).floatValue;
        }

        protected override void SetRelativeProperty(SerializedProperty property, Rect valueRect, float variableValue)
        {
            string newValue = EditorGUI.TextField(valueRect, variableValue.ToString());
            float.TryParse(newValue, out variableValue);
            property.FindPropertyRelative(VariableValuePropertyName).floatValue = variableValue;
        }
    }
}
