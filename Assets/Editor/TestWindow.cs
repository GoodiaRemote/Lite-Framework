using System;
using LiteFramework.Editor.GUI;
using UnityEditor;
using UnityEngine;

namespace Editor
{

    public class Test
    {
        public string Name;
        public int Value;
        public Color Color;
    }
    
    public class TestWindow : EditorWindow
    {
        private EditorTableView<Test> _tableView;
        private object _splitState;
        Vector2 tableScroll;
        
        private Test[] _tests = new Test[]
        {
            new Test(){ Name = "Test1"},
            new Test() { Name = "Test2"},
            new Test(){ Name = "Test3"},
            new Test(){ Name = "Test4"},
        };
        
        [MenuItem("Test/Table")]
        private static void ShowWindow()
        {
            var window = GetWindow<TestWindow>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }

        private void OnEnable()
        {
            _tableView = new EditorTableView<Test>();
            _tableView.AddColumn("Name", 10, (rect, item) =>
            {
                item.Name = EditorGUI.TextField(rect, item.Name);
            });
            _tableView.AddColumn("Value", 10, (rect, item) =>
            {
                item.Value = EditorGUI.IntField(rect, item.Value);
            });
            _tableView.AddColumn("GameObject", 10, (rect, item) =>
            {
                item.Color = EditorGUI.ColorField(rect, item.Color);
            });
            _splitState = LiteFrameworkEditorGUILayout.CreateSplitterState(new float[] { 75f, 25f }, new int[] { 32, 32 }, null);
        }

        private void OnGUI()
        {
            LiteFrameworkEditorGUILayout.BeginHorizontalSplit(_splitState);
            {
                _tableView.UpdateViewData(_tests);
                _tableView?.OnGUI(EditorGUILayout.GetControlRect(GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)));
                LiteFrameworkEditorGUILayout.HorizontalSplitter();
            }
            LiteFrameworkEditorGUILayout.EndHorizontalSplit();
        }
    }
}