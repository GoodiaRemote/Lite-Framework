using System;
using System.Linq;
using System.Reflection;
using LiteFramework.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace LiteFramework.Editor.GUI
{
    public static partial class LiteFrameworkEditorGUILayout
    {
        private static BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        private static readonly Color _backgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f);
        private static readonly Color _borderColor = new Color(0.1254902f, 0.1254902f, 0.1254902f);

        private static Lazy<Type> splitterStateType = new Lazy<Type>(() =>
        {
            var type = typeof(EditorWindow).Assembly.GetTypes().First(x => x.FullName == "UnityEditor.SplitterState");
            return type;
        });

        private static Lazy<ConstructorInfo> splitterStateCtor = new Lazy<ConstructorInfo>(() =>
        {
            var type = splitterStateType.Value;
            return type.GetConstructor(flags, null, new Type[] { typeof(float[]), typeof(int[]), typeof(int[]) }, null);
        });

        private static Lazy<Type> splitterGUILayoutType = new Lazy<Type>(() =>
        {
            var type = typeof(EditorWindow).Assembly.GetTypes().First(x => x.FullName == "UnityEditor.SplitterGUILayout");
            return type;
        });
        
        public static object CreateSplitterState(float[] relativeSizes, int[] minSizes, int[] maxSizes)
        {
            return splitterStateCtor.Value.Invoke(new object[] { relativeSizes, minSizes, maxSizes });
        }

        private static GUIStyle GroupStyle
        {
            get
            {
                var style = new GUIStyle(EditorStyles.helpBox);
                style.normal.background = TextureHelper.ColorToTexture2D(10, 10, _backgroundColor, _borderColor, 1);
                style.padding = new RectOffset(0, 0, 0, 0);
                style.margin = new RectOffset(0, 0, 0, 0);
                return style;
            }
        }

        #region Vertical split
        
        private static Lazy<MethodInfo> beginVerticalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = splitterGUILayoutType.Value;
            return type.GetMethod("BeginVerticalSplit", flags, null, new Type[] { splitterStateType.Value, typeof(GUILayoutOption[]) }, null);
        });

        private static Lazy<MethodInfo> endVerticalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = splitterGUILayoutType.Value;
            return type.GetMethod("EndVerticalSplit", flags, null, Type.EmptyTypes, null);
        });
        
        public static void BeginVerticalSplit(object splitterState, params GUILayoutOption[] options)
        {
            if (splitterState == null)
            {
                return;
            }
            beginVerticalSplit.Value.Invoke(null, new object[] { splitterState, options });
            EditorGUILayout.BeginVertical(GroupStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }

        public static void EndVerticalSplit()
        {
            endVerticalSplit.Value.Invoke(null, Type.EmptyTypes);
            EditorGUILayout.EndVertical();
        }

        public static void VerticalSplitter()
        {
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(GroupStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }
        
        #endregion
        
        #region Horizontal split

        private static Lazy<MethodInfo> beginHorizontalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = splitterGUILayoutType.Value;
            return type.GetMethod("BeginHorizontalSplit", flags, null, new Type[] { splitterStateType.Value, typeof(GUILayoutOption[]) }, null);
        });

        private static Lazy<MethodInfo> endHorizontalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = splitterGUILayoutType.Value;
            return type.GetMethod("EndHorizontalSplit", flags, null, Type.EmptyTypes, null);
        });
        
        public static void BeginHorizontalSplit(object splitterState, params GUILayoutOption[] options)
        {
            if (splitterState == null)
            {
                return ;
            }
            beginHorizontalSplit.Value.Invoke(null, new object[] { splitterState, options });
            EditorGUILayout.BeginVertical(GroupStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }
        
        public static void EndHorizontalSplit()
        {
            EditorGUILayout.EndHorizontal();
            endHorizontalSplit.Value.Invoke(null, Type.EmptyTypes);
        }

        public static void HorizontalSplitter()
        {
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal(GroupStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }

        #endregion
    }
}