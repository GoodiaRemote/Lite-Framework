using UnityEditor;
using UnityEngine;

namespace LiteFramework.Editor.Utils
{
    public static class SerializedHelper
    {
        public static void AppendArrayElement(this SerializedProperty arrayProperty, Object element)
        {
            arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
            var property  = arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1);
            property.objectReferenceValue = element; ;
        }

        public static void RemoveArrayElement(this SerializedProperty arrayProperty, Object element)
        {
            for (var i = 0; i < arrayProperty.arraySize; ++i)
            {
                var current = arrayProperty.GetArrayElementAtIndex(i);
                if (current.objectReferenceValue == element)
                {
                    arrayProperty.DeleteArrayElementAtIndex(i);
                    return;
                }
            }
        }
    }
}