using Osiris.Utilities.References;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.EditorCustomisation
{
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferencePropertyDrawer : GenericReferencePropertyDrawer<int>
    {
        protected override int FindRelativeProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative(VariableValuePropertyName).intValue;
        }

        protected override void SetRelativeProperty(SerializedProperty property, Rect valueRect, int variableValue)
        {
            string newValue = EditorGUI.TextField(valueRect, variableValue.ToString());
            int.TryParse(newValue, out variableValue);
            property.FindPropertyRelative(VariableValuePropertyName).intValue = variableValue;
        }
    }
}
