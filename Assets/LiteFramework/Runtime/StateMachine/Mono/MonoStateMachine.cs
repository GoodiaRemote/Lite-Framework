using LiteFramework.Runtime.Base;
using LiteFramework.Runtime.StateMachine.Mono.SO;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LiteFramework.Runtime.StateMachine.Mono
{
    public class MonoStateMachine : BaseMonoBehaviour
    {
        [SerializeField, InlineEditor] private StateControllerSO _stateController;
        private StateMachine _stateMachine;

        protected virtual void Awake()
        {
            _stateMachine = _stateController.CreateStateMachine(this);
            _stateMachine.Init();
        }

        protected virtual void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
        
        protected virtual void Update()
        {
            _stateMachine.Update();
        }
        
        protected virtual void LateUpdate()
        {
            _stateMachine.LateUpdate();
        }

#if UNITY_EDITOR
        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            if(!Application.isPlaying) return; 
            if(_stateMachine == null) return;
            GUILayout.Space(10);
            var headStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal =
                {
                    textColor = Color.white
                }
            };
            EditorGUILayout.LabelField("State Machine Debug", headStyle);
            var runtime = _stateMachine.StateRunTime.ToString("F1");
            EditorGUILayout.LabelField($"State runtime: {runtime}s");
            GUILayout.Space(5);
            var startStateName = _stateMachine.StartBlockState.Name;
            var startState = string.IsNullOrEmpty(startStateName) ? "<none>" : startStateName;
            var currentState = _stateMachine.CurrentBlockState;
            DrawDebugState(startState, currentState.Hash == _stateMachine.StartBlockState.Hash, startState + "(Start State)");
            foreach (var state in _stateMachine.AllStatesName)
            {
                if (state != startState)
                {
                    DrawDebugState(state,currentState.Name == state);
                }
            }
        }

        private void DrawDebugState(string stateName, bool isActive, string title = null)
        {
            GUI.backgroundColor = isActive ? Color.green : Color.grey;
            var text = title ?? stateName;
            if (GUILayout.Button(text,GUILayout.Height(25)))
            {
                _stateMachine.ChangeState(stateName);
            }
        }
        
#endif
    }
}