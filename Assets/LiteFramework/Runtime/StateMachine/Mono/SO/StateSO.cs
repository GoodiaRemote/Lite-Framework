using System.Collections.Generic;
using LiteFramework.Runtime.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.Runtime.StateMachine.Mono.SO
{
    [CreateAssetMenu(fileName = "New State", menuName = "State Machine/State", order = 0)]
    public class StateSO : DescriptionSO
    {
        [SerializeField, Required, ValidateInput(nameof(ValidateName), "Name is invalid")] private string _stateName;
        [SerializeField] private float _exitTime;
        [SerializeField, InlineEditor] private ActionSO[] _actions;
        
        public string StateName => _stateName;
        public float ExitTime => _exitTime;

        internal IState CreateState(MonoStateMachine stateMachine)
        {
            var actionState = new ActionState();
            for (int i = 0; i < _actions.Length; i++)
            {
               var action = _actions[i].CreateAction();
               action.ActionInit(stateMachine);
               actionState.AddAction(action);
            }

            return actionState;
        }

        private bool ValidateName(string stateName)
        {
            return !string.IsNullOrEmpty(stateName) && stateName.ToLower() != "none";
        }
    }

    public class ActionState: IState
    {
        private List<IAction> _actions = new();

        public void AddAction(IAction action)
        {
            _actions.Add(action);
        }
        
        public void StateEnter()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].ActionEnter();
            }
        }

        public void StateFixedUpdate()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].ActionFixedUpdate();
            }
        }

        public void StateUpdate()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].ActionUpdate();
            }
        }

        public void StateLateUpdate()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].ActionLateUpdate();
            }
        }

        public void StateExit()
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].ActionExit();
            }
        }
    }
}