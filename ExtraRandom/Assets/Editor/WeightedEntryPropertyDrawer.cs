using ExtraRandom.Weighted;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(WeightedEntry<>))]
    public class WeightedEntryPropertyDrawer : PropertyDrawer
    {
        private const int LabelWidth = 75;
        private const int Offset = 5;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 5;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var prevIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var weight = property.FindPropertyRelative("weight");
            var entry = property.FindPropertyRelative("entry");

            EditorGUI.BeginProperty(position, label, property);
            
            var backgroundRect = new Rect(position.x - Offset, position.y - 1, position.width, position.height);
            backgroundRect.xMax += Offset;
            EditorGUI.LabelField(backgroundRect, GUIContent.none, EditorStyles.helpBox);

            var weightLabelRect = new Rect(position.x, position.y, LabelWidth, EditorGUIUtility.singleLineHeight);
            position.xMin = weightLabelRect.xMax + Offset;
            var weightRect = new Rect(position.x, position.y + 2, position.width / 5, EditorGUIUtility.singleLineHeight);
            position.xMin = weightRect.xMax + Offset;
            
            var entryLabelRect = new Rect(position.x, position.y, LabelWidth, EditorGUIUtility.singleLineHeight);
            position.xMin = entryLabelRect.xMax + Offset;
            var entryRect = new Rect(position.x, position.y + 2, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(weightLabelRect, "Weight", EditorStyles.miniLabel);
            EditorGUI.PropertyField(weightRect, weight, GUIContent.none);
            
            EditorGUI.LabelField(entryLabelRect, "Entry", EditorStyles.miniLabel);
            EditorGUI.PropertyField(entryRect, entry, GUIContent.none);

            EditorGUI.EndProperty();
            EditorGUI.indentLevel = prevIndent;
        }
    }
}