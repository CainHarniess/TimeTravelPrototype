using Osiris.TimeTravelPuzzler.Timeline;
using UnityEditor;

namespace Osiris.TimeTravelPuzzler.EditorCustomisation
{
    [CustomEditor(typeof(TimelineEvent))]
    public class TimelineEventEditor : Editor
    {
        private SerializedProperty _description;

        private void OnEnable()
        {
            _description = serializedObject.FindProperty(nameof(_description));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PrefixLabel(_description.stringValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
