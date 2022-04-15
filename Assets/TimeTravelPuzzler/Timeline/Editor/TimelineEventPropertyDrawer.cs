using Osiris.TimeTravelPuzzler.Timeline;
using UnityEditor;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Editor
{
    [CustomPropertyDrawer(typeof(TimelineEvent))]
    public class TimelineEventPropertyDrawer : PropertyDrawer
    {
        private SerializedProperty _Description;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, "Action", property.FindPropertyRelative(nameof(_Description)).stringValue);
            EditorGUI.EndProperty();
        }
    }
}
