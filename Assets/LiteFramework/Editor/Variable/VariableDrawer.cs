using LiteFramework.Runtime.Variable;
using UnityEditor;
using UnityEngine;

namespace LiteFramework.Editor.Variable
{
    [CustomPropertyDrawer(typeof(GenericVariable))]
    public class VariableDrawer : PropertyDrawer
    {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects || property.objectReferenceValue == null)
            {
                EditorGUI.PropertyField(position, property, false);
                return;
            }
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position,GUIUtility.GetControlID(FocusType.Passive), label);
            var inner = new SerializedObject(property.objectReferenceValue);
            var valueProp = inner.FindProperty("_value");
            var previewRect = new Rect(position);
            previewRect.width = GetPreviewSpace(valueProp?.type);
            position.xMin = previewRect.xMax;
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            EditorGUI.BeginDisabledGroup(true);
            if (valueProp != null)
            {
                EditorGUI.PropertyField(previewRect, valueProp,GUIContent.none, false);
            }
            else
            {
                EditorGUI.LabelField(previewRect, "[None serialized value]");
            }
            EditorGUI.EndDisabledGroup();
        
            position.x += 6f;
            position.width -= 6f;
            EditorGUI.PropertyField(position, property,GUIContent.none, false);
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
        
        protected virtual float GetPreviewSpace(string type)
        {
            switch (type)
            {
                case "int":
                case "float":
                case "double":
                case "Color":
                    return 58;
                default:
                    return 128;
            }
        }
    }
}