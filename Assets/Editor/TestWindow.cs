using System;
using DG.DemiEditor;
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
            new Test(){ Name = "Test1",Value = 1},
            new Test() { Name = "Test2", Value = 4},
            new Test(){ Name = "Test3", Value = 7},
            new Test(){ Name = "Test4", Value = 8},
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
                EditorGUI.LabelField(rect, item.Name);
            }).SetSorting((item => item.Name));
            _tableView.AddColumn("Value", 10, (rect, item) =>
            {
                EditorGUI.LabelField(rect, item.Value.ToString());
            }).SetSorting((item)=> item.Value);
            _tableView.AddColumn("GameObject", 10, (rect, item) =>
            {
                EditorGUI.LabelField(rect, item.Color.ToString());
            });
            _splitState = LiteFrameworkEditorGUILayout.CreateSplitterState(new float[] { 75f, 25f }, new int[] { 32, 32 }, null);
        }

        private void OnGUI()
        {
            LiteFrameworkEditorGUILayout.BeginHorizontalSplit(_splitState);
            {
                var rect = EditorGUILayout.GetControlRect(GUILayout.ExpandHeight(true));
                _tableView?.DrawTableGUI(rect, _tests);
                LiteFrameworkEditorGUILayout.HorizontalSplitter();
            }
            LiteFrameworkEditorGUILayout.EndHorizontalSplit();
        }
    }
}